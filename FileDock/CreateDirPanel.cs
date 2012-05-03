using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileDock {

	public partial class CreateDirPanel: TogglePanel {
		public CreateDirPanel(FileDockForm Owner):base(Owner) {
			InitializeComponent();
			this.AfterToggle += delegate {
				if ( this.Visible ) {
					this.textBox1.Text = "";
					this.textBox1.Focus();
				}
			};
		}
		private void create_Click(object sender, EventArgs e) {
			string newDir = this.textBox1.Text;
			newDir = Owner.currentPath + "\\" + newDir;
			if ( File.Exists(newDir) || Directory.Exists(newDir) ) {
				MessageBox.Show("Already exists.");
			} else {
				try {
					Directory.CreateDirectory(newDir);
					this.Toggle();
				} catch ( UnauthorizedAccessException ) {
					MessageBox.Show("Not allowed.");
				} catch ( DirectoryNotFoundException ) {
					MessageBox.Show("Directory not found: " + newDir);
				} catch ( PathTooLongException ) {
					MessageBox.Show("Path is too long: " + newDir);
				} catch ( ArgumentException ) {
					MessageBox.Show("New path is not a valid path: " + newDir);
				} catch ( IOException ex ) {
					MessageBox.Show("IOException: " + ex.ToString());
				}
			}
		}
	}
}
