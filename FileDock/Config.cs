using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;


namespace FileDock {
	public class Config {
		private Dictionary<string, Control> mapControls;
		private Dictionary<string, string> mapStrings;
		private Form srcForm;

		public string appName;

		/// <summary>
		/// Make a new config, link it to the Form F.
		/// </summary>
		/// <param name="F">The form to attach to.</param>
		/// <param name="appName">The application name is used to store this config in the registry under Software\\appName</param>
		public Config(Form F, string appName) {
			this.appName = appName;
			mapStrings = new Dictionary<string, string>();
			mapControls = new Dictionary<string, Control>();
			F.FormClosing += new FormClosingEventHandler(F_FormClosing);
			UpdateFormBinding(F);
		}

		void F_FormClosing(object sender, FormClosingEventArgs e)
		{
			Debug.Print("Config: closing form...");
			// if the form is about to close, save all the values in the mapped controls into the string value cache
			foreach (string key in mapControls.Keys)
			{
				mapStrings[key] = GetControlValue(mapControls[key]);
				Debug.Print("Config: setting {0} to {1}", key, mapStrings[key]);
			}
			mapControls.Clear();
		}

		/// <summary>
		///  Clean up
		/// </summary>
		~Config() {
			mapStrings = null;
			mapControls = null;
			srcForm = null;
		}

		public void SaveToRegistry() {
			Debug.Print("Config: saving to registry...");
			RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\"+this.appName, true);
			if (regKey == null) {
				regKey = Registry.CurrentUser.CreateSubKey("Software\\"+this.appName);
			}
			foreach (string key in this.Keys) {
				regKey.SetValue(key, this[key], RegistryValueKind.String);
			}
		}

		public void LoadFromRegistry() {
			Debug.Print("Config: loading from registry...");
			RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\" + this.appName, true);
			if( regKey != null )
			foreach (string key in regKey.GetValueNames()) {
				if (regKey.GetValue(key) != null) { 
					this[key] = (string)regKey.GetValue(key);
					Debug.Print("Debug: loading {0} = {1}", key, this[key]);
				}
			}
		}

		public List<string> Keys {
			get {
				List<string> ret = new List<string>(mapControls.Keys);
				foreach (string key in mapStrings.Keys) {
					if ( ! ret.Contains(key) ) ret.Add(key);
				}
				return ret;
			}
		}

		/// <summary>
		/// Starting at the Control parent,
		/// recursively add element's to our hashtable,
		/// such that (mapControls[c.Tag] == c) for all c underneath parent in the form's layout tree.
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		private string RefreshControlHash(Control parent) {
			string s = "";
			foreach (Control c in parent.Controls) {
				string key = c.Tag as string;
				if (key != null) {
					s += key + ", ";
					if (mapControls.ContainsKey(key)) {
						// update the control on the page with the value set in the config
						if( c != mapControls[key] ) {
							Debug.Print("Setting control value for: " + key);
							SetControlValue(c, GetControlValue(mapControls[key]));
						}
						// then update our mapping to point to the new control
						mapControls[key] = c;
						Debug.Print("mapControls[" + key + "] updated.");
					} else {
						// if we have a string saved and a control to use as a backing store then forget about saving the string
						if (mapStrings.ContainsKey(key)) {
							Debug.Print("Setting control value for: " + key);
							SetControlValue(c, mapStrings[key]);
							mapStrings.Remove(key);
							Debug.Print("mapStrings[" + key + "] removed.");
						}
						mapControls.Add(key, c);
						Debug.Print("mapControls[" + key + "] added.");
					}
				}
				s += RefreshControlHash(c);
			}
			return s;
		}
		/// <summary>
		/// UpdateFormBinding can be used to point an already loaded config instance at a new form.
		/// 
		/// This would be useful is such cases where the form you passed to the constructor must be destroyed for some reason.
		/// In this case, calling this function will clear out assocations to the old form, and bind to the new one
		/// </summary>
		/// <param name="newForm"></param>
		/// <returns></returns>
		public string UpdateFormBinding(Form newForm) {
			mapControls.Clear();
			srcForm = newForm;
			return RefreshControlHash(newForm);
		}


		/// <summary>
		/// GetControlValue converts a variety of controls to plain string values.
		/// Supports custom unpacking for: CheckBox, RadioButton, NumericUpDown, ListBox.
		/// For all other element types, the .Text member is returned.
		/// </summary>
		/// <param name="c">The Control to read from.</param>
		/// <returns>
		/// For CheckBox, "True" or "False".
		/// For RadioButton, "True" or "False".
		/// For NumericUpDown, the number as a string.
		/// For ListBox, a comma-seperated list of values.
		/// For anything else, the value of it's .Text member.
		/// </returns>
		public static string GetControlValue(Control c) {
			if (c.GetType() == typeof(CheckBox)) {
				return ((CheckBox)c).Checked ? "True" : "False";
			} else if (c.GetType() == typeof(RadioButton)) {
				return ((RadioButton)c).Checked ? "True" : "False";
			} else if (c.GetType() == typeof(NumericUpDown)) {
				return ((NumericUpDown)c).Value.ToString();
			} else if (c.GetType() == typeof(ListBox)) {
				string str = "";
				foreach (object o in ((ListBox)c).Items) {
					str += o + ",";
				}
				if (str.Length > 0) {
					str = str.Substring(0, str.Length - 1);
				}
				return str;
			} else {
				try {
					return c.Text;
				} catch (NullReferenceException) {
					throw (new Exception("(unhandled control type:" + c.GetType().ToString() + ")"));
				}
			}
		}

		/// <summary>
		/// SetControlValue converts a string to the proper setting of a Control.
		/// </summary>
		/// <param name="c">The Control to write to.</param>
		/// <param name="s">The string value to write.</param>
		/// <see cref="GetControlValue"></see>
		public static void SetControlValue(Control c, string s) {
			if (c.GetType() == typeof(TextBox)) {
				c.Text = s;
			} else if (c.GetType() == typeof(CheckBox)) {
				((CheckBox)c).Checked = s == "True";
			} else if (c.GetType() == typeof(RadioButton)) {
				((RadioButton)c).Checked = s == "True";
			} else if (c.GetType() == typeof(NumericUpDown)) {
				((NumericUpDown)c).Value = Int32.Parse(s);
			} else if (c.GetType() == typeof(ListBox)) {
				string[] items = s.Split(',');
				//MessageBox.Show("Loading items into list: "+s);
				((ListBox)c).Items.Clear();
				foreach (string item in items) {
					((ListBox)c).Items.Add(item);
				}
			} else {
				try {
					c.Text = s;
				} catch (Exception) {
					// ignore
				}
			}
		}

		/// <summary>
		/// Map strings to strings.  Using controls on a form as the data source when possible, or fall back on a string store otherwise.
		/// </summary>
		public string this[string name, string def] {
			get {
				if (mapControls.ContainsKey(name)) {
					string val = Config.GetControlValue(mapControls[name]);
					mapStrings[name] = val; // cache the control value in the strings table, in case the form is disposed
					Debug.Print("config[" + name + "] = \""+val+"\" (from control)");
					return val;
				}
				// this branch will only be followed in the form is destroyed
				if( mapStrings.ContainsKey(name) ) {
					Debug.Print("config[" + name + "] = \"" + mapStrings[name] + "\" (from cache)");
					return mapStrings[name];
				} else {
					Debug.Print("no such config[" + name + "]");
				}
				return def;
			}
		}
		public string this[string name] {
			get { return this[name, null]; }
			set {
				// always cache the value in the strings table, in case the form is disposed
				Debug.Print("caching config[" + name + "] = \"" + value + "\" (from setter)");
				mapStrings[name] = value;
	
				// and also store the value in any bound controls
				if (mapControls.ContainsKey(name)) {
					Debug.Print("Setting form control: {0} (from setter)", value.ToString());
					Config.SetControlValue(mapControls[name], value);
				}
			}
		}
	}
}
