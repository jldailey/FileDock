using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileDock {
	public partial class CreateFilePanel: TogglePanel {
		public CreateFilePanel(FileDockForm Owner):base(Owner) {
			InitializeComponent();
			this.AfterToggle += delegate {
				if ( this.Visible ) {
					this.textBox1.Text = "";
					this.textBox1.Focus();
				}
			};
			this.button1.Click += new EventHandler(create_Click);
		}

		private void create_Click(object sender, EventArgs e) {

			string newFile = this.textBox1.Text;
			newFile = Owner.currentPath + "\\" + newFile;

			if ( File.Exists(newFile) || Directory.Exists(newFile) ) {
				MessageBox.Show("Already exists.");
			} else {
				try {
					File.Create(newFile).Close();
					this.Toggle();
				} catch ( UnauthorizedAccessException ) {
					MessageBox.Show("Not allowed.");
				} catch ( DirectoryNotFoundException ) {
					MessageBox.Show("Directory not found: " + newFile);
				} catch ( PathTooLongException ) {
					MessageBox.Show("Path is too long: " + newFile);
				} catch ( ArgumentException ) {
					MessageBox.Show("New path is not a valid path: " + newFile);
				} catch ( IOException ex ) {
					MessageBox.Show("IOException: " + ex.ToString());
				}
			}
		}
	}
}
