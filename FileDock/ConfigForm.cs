using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FileDock {
	public partial class ConfigForm : Form {
		private FileDockForm parent;
		public ConfigForm(FileDockForm parent) {
			this.parent = parent;
			InitializeComponent();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			this.parent.refreshAllInstances();
			base.OnClosing(e);
		}

		private void btnAddIgnore_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < listIgnore.Items.Count; i++)
			{
				string item = (string)listIgnore.Items[i];
				if (item.Length < 1 || item == " ")
				{
					listIgnore.Items.RemoveAt(i);
					i--;
				}
			}
			string text = txtAddIgnore.Text;
			if( text.Length > 0 ) {

				if (text[0] == '*')
				{
					text = text.Substring(1);
				}
				if (text[0] == '.')
				{
					text = text.Substring(1);
				}
				text = "." + text;
				if( listIgnore.Items.Contains(text) )
					return;
				listIgnore.Items.Add(text);
			}
		}

		private void btnRemoveIgnore_Click(object sender, EventArgs e)
		{
			if (listIgnore.SelectedIndex > -1)
			{
				listIgnore.Items.RemoveAt(listIgnore.SelectedIndex);
			}
		}

		private void btnEditVimLocation_Click(object sender, EventArgs e)
		{
			openFileDialog1.FileName = "gvim.exe";
			openFileDialog1.FileOk += new CancelEventHandler(openFileDialog1_FileOk);
			openFileDialog1.ShowDialog(this);
		}

		void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			txtEditVimLocation.Text = openFileDialog1.FileName;
		}

		private void btnBrowse7Zip_Click( object sender, EventArgs e ) {

		}

	}
}