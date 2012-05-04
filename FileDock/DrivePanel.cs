using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.ComponentModel;

namespace FileDock {
	public delegate void DriveChanged(string drive);
	class DrivePanel {
		Control parent;
		private string _drive;
		public string SelectedDrive {
			get { return _drive; }
			set {
				Debug.Print("Setting SelectedDrive: {0}", value);
				Changed(_drive = value);
			}
		}
		public DriveChanged Changed;
		public DrivePanel( Control parent, string initialDrive ) {
			this.parent = parent;
			this.Changed += new DriveChanged(delegate( string drive ) {
				Debug.Print("Drive Changed: {0}",drive);
			});
			this.SelectedDrive = initialDrive;
		}
		// re-read all the system drives and re-draw the DriveButtons
		public void refresh() {
			DriveInfo[] drives = DriveInfo.GetDrives();
			Debug.Print("DrivePanel.refresh: got list of drives");
			parent.Controls.Clear();
			foreach (DriveInfo drive in drives) {
				String name = drive.Name.Substring(0, 1);
				DriveButton b = new DriveButton(drive);
				b.Selected = (name == SelectedDrive);
				b.BeforeSelectDrive += delegate(string newDrive)
				{
					foreach (Control c in parent.Controls) {
						DriveButton db = ((DriveButton)c);
						db.Selected = false;
					}
				};
				b.OnSelectDrive += delegate(string newDrive)
				{
					if (newDrive != SelectedDrive) {
						this.SelectedDrive = newDrive;
					}
				};
				parent.Controls.Add(b);
			}
		}

	}
}
