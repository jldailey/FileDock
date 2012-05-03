using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace FileDock {
	public partial class SearchPanel: TogglePanel {
		public SearchPanel(FileDockForm owner):base(owner) {
			InitializeComponent();
		}
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this.txtContains.KeyUp += new KeyEventHandler(txtFilename_KeyUp);
		}
		
		private void btnSearch_Click(object sender, EventArgs e) {
			btnSearch.Enabled = false;
			Cursor = Cursors.WaitCursor;
			Owner.Cursor = Cursors.WaitCursor;
			Owner.listFiles.Items.Clear();
			ListViewItem item = Owner.listFiles.Items.Add("Clear Search Results");
			item.Tag = "..refresh..";
			item.Group = Owner.listFiles.Groups[0];
			searchHelper(new DirectoryInfo(Owner.currentPath), txtFilename.Text, txtContains.Text);
			btnSearch.Enabled = true;
			Cursor = Cursors.Default;
			Owner.Cursor = Cursors.Default;
		}
		// this helper does the actual search
		private void searchHelper(DirectoryInfo node, string matchName, string matchContent) {
			try {
				foreach ( FileInfo f in node.GetFiles() ) {
					bool nameMatched = false;
					bool textMatched = false;
					List<int> matchLines = new List<int>();
					// check if the filename matches
					if ( matchName != null && matchName.Length > 0 ) {
						if ( chkRegex.Checked ) {
							if ( Regex.Match(f.FullName, matchName, RegexOptions.IgnoreCase).Success ) {
								nameMatched = true;
							}
						} else if ( f.Name.ToLower().Contains(matchName.ToLower()) ) {
							nameMatched = true;
						}
					}

					// check if the file content matches
					if ( matchContent != null && matchContent.Length > 0 ) {
						string[] lines = File.ReadAllLines(f.FullName);
						int lineNum = 0;
						foreach ( string line in lines ) {
							lineNum++;
							if ( chkRegex.Checked ) {
								if ( Regex.Match(line, matchContent, RegexOptions.IgnoreCase).Success ) {
									textMatched = true;
									matchLines.Add(lineNum);
								}
							} else if ( line.ToLower().Contains(matchContent.ToLower()) ) {
								textMatched = true;
								matchLines.Add(lineNum);
							}
						}
					}
					// make input fields optional, by considering an empty field to match everything
					nameMatched = (matchName != null && matchName.Length > 0) ? nameMatched : true;
					textMatched = (matchContent != null && matchContent.Length > 0) ? textMatched : true;
					// if the patterns overall matched for this file, add it to the tree
					if ( nameMatched && textMatched) {
						if ( matchLines.Count > 0 ) {
							foreach ( int line in matchLines ) {
								String displayName = f.Name + (line > 0 ? ": " + line.ToString() : "");
								ListViewItem n = Owner.listFiles.Items.Add(displayName);
								n.Tag = f.FullName;
								n.ImageIndex = 1;
								n.Group = Owner.listFiles.Groups[1];
							}
						} else {
							ListViewItem n = Owner.listFiles.Items.Add(f.Name);
							n.Tag = f.FullName;
							n.ImageIndex = 1;
							n.Group = Owner.listFiles.Groups[1];
						}
					}
				}
			} catch ( System.UnauthorizedAccessException ) {
			}
			try {
				if ( chkRecursive.Checked ) {
					DirectoryInfo[] dirs = node.GetDirectories();
					foreach ( DirectoryInfo dir in dirs ) {
						searchHelper(dir, matchName, matchContent);
					}
				}
			} catch ( System.UnauthorizedAccessException ex ) {
				Debug.Print("Threw away: " + ex.ToString());
			}
		}

		private void txtFilename_KeyUp(object sender, KeyEventArgs e) {
			if ( e.KeyCode == Keys.Enter ) {
				e.Handled = true;
				e.SuppressKeyPress = true;
				btnSearch_Click(null, null);
			} else if ( e.KeyCode == Keys.Escape ) {
				this.Toggle();
				Owner.refreshFiles();
			}
		}

	}
}
