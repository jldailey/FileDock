using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Win32;
using System.Runtime.Remoting.Messaging;

namespace FileDock {
	public partial class FileDockForm : AppBar {

		#region LVS_EX
		public enum LVS_EX {
			LVS_EX_GRIDLINES = 0x00000001,
			LVS_EX_SUBITEMIMAGES = 0x00000002,
			LVS_EX_CHECKBOXES = 0x00000004,
			LVS_EX_TRACKSELECT = 0x00000008,
			LVS_EX_HEADERDRAGDROP = 0x00000010,
			LVS_EX_FULLROWSELECT = 0x00000020,
			LVS_EX_ONECLICKACTIVATE = 0x00000040,
			LVS_EX_TWOCLICKACTIVATE = 0x00000080,
			LVS_EX_FLATSB = 0x00000100,
			LVS_EX_REGIONAL = 0x00000200,
			LVS_EX_INFOTIP = 0x00000400,
			LVS_EX_UNDERLINEHOT = 0x00000800,
			LVS_EX_UNDERLINECOLD = 0x00001000,
			LVS_EX_MULTIWORKAREAS = 0x00002000,
			LVS_EX_LABELTIP = 0x00004000,
			LVS_EX_BORDERSELECT = 0x00008000,
			LVS_EX_DOUBLEBUFFER = 0x00010000,
			LVS_EX_HIDELABELS = 0x00020000,
			LVS_EX_SINGLEROW = 0x00040000,
			LVS_EX_SNAPTOGRID = 0x00080000,
			LVS_EX_SIMPLESELECT = 0x00100000
		}

		public enum LVM {
			LVM_FIRST = 0x1000,
			LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54),
			LVM_GETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 55),
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr handle, int messg, int wparam, int lparam);


		/// <summary>
		/// Sets Double_Buffering and BorderSelect style
		/// </summary>
		public void SetExStyles() {
			LVS_EX styles = (LVS_EX)SendMessage(listFiles.Handle, (int)LVM.LVM_GETEXTENDEDLISTVIEWSTYLE, 0, 0);
			styles |= LVS_EX.LVS_EX_DOUBLEBUFFER | LVS_EX.LVS_EX_BORDERSELECT;
			SendMessage(listFiles.Handle, (int)LVM.LVM_SETEXTENDEDLISTVIEWSTYLE, 0, (int)styles);

			styles = (LVS_EX)SendMessage(this.Handle, (int)LVM.LVM_GETEXTENDEDLISTVIEWSTYLE, 0, 0);
			styles |= LVS_EX.LVS_EX_DOUBLEBUFFER | LVS_EX.LVS_EX_BORDERSELECT;
			SendMessage(this.Handle, (int)LVM.LVM_SETEXTENDEDLISTVIEWSTYLE, 0, (int)styles);
		}

		#endregion

		public string currentDrive {
			get {	return this.config["CurrentDrive"];	}
			set {	this.config["CurrentDrive"] = value;}
		}
		public string currentDirectory {
			get { return this.config["CurrentDirectory"]; }
			set { this.config["CurrentDirectory"] = value; }
		}
		public string currentPath {
			get { return currentDrive + @":\" + currentDirectory; }
		}
		public Dictionary<string, string> savedPaths; // maps drives to the last path we visited on that drive

		public FileDockForm() {
			InitializeComponent();
			savedPaths = new Dictionary<string, string>();
		}
		
		// re-read all the system drives and re-draw the DriveButtons
		public void refreshDrives() {
			Debug.Print("refreshDrives");
			int driveCount = 0;
			DriveInfo[] drives = DriveInfo.GetDrives();
			Debug.Print("refreshDrives: got list of drives");
			flowLayoutPanel1.Controls.Clear();
			foreach (DriveInfo drive in drives) {
				String name = drive.Name.Substring(0, 1);
				DriveButton b = new DriveButton(drive);
				if (name == currentDrive) {
					b.Selected = true;
				}
				b.BeforeSelectDrive += delegate(string newDrive)
				{
					foreach (Control c in flowLayoutPanel1.Controls) {
						try {
							DriveButton db = ((DriveButton)c);
							db.Selected = false;
						} catch (InvalidCastException) {
							//pass
						}
					}
				};
				b.OnSelectDrive += delegate(string newDrive)
				{
					if (newDrive != currentDrive) {
						this.currentDrive = newDrive;
						this.currentDirectory = (savedPaths.ContainsKey(newDrive) ? savedPaths[newDrive] : "");
						refreshFiles();
					}
				};

				flowLayoutPanel1.Controls.Add(b);
				driveCount++;
			}
			listFiles.Top = button3.Bottom + 4;
			listFiles.Left = 2;
		}
		public string formatFileNameForList(string filename) {
			string ret = "";
			int limit = (int)(listFiles.Columns[0].Width / listFiles.Font.SizeInPoints);

			return ret;
		}
		// re-read the current directory and fill up the ListView
		public void refreshFiles(bool allFileDocks) {
			refreshFiles();
			if (allFileDocks
				&& this.rightChild != null
				&& !this.rightChild.IsDisposed) {
				this.rightChild.refreshFiles(true);
			}
		}
		public void refreshFiles() {
			savedPaths[this.currentDrive] = this.currentDirectory;
			listFiles.Items.Clear();
			this.Refresh();
			try {
				// build a new tree asynchronously, so that form can still draw itself while this is updating
				RefreshDelegate d = new RefreshDelegate(delegate()
				{
					listFiles.Columns[0].Text = this.currentPath;
					ListViewItem tmp = listFiles.Items.Add("..");
					tmp.Tag = "..";
					tmp.Group = listFiles.Groups[0];
					try {
						string[] dirs = Directory.GetDirectories(currentPath);
						Array.Sort<string>(dirs);
						foreach (string dir in dirs) {
							ListViewItem node = listFiles.Items.Add(Path.GetFileName(dir));
							node.ToolTipText = Path.GetFileName(dir);
							node.Tag = Path.GetFullPath(dir);
							node.ImageIndex = 0;
							node.Group = listFiles.Groups[0];
						}
						string[] files = Directory.GetFiles(currentPath,"*.*");
						Array.Sort<string>(files);
						foreach (string file in files) {
							ListViewItem node = listFiles.Items.Add(Path.GetFileName(file));
							if ( this.config["ShowHidden"] == "False" ) {
								FileInfo inf = new FileInfo(file);
								if ( (inf.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ) {
									continue;
								}
							}
						
							node.ToolTipText = Path.GetFileName(file);
							node.Tag = Path.GetFullPath(file);
							node.ImageIndex = 1;
							node.Group = listFiles.Groups[1];
						}
					} catch (Exception e) {
						MessageBox.Show("Exception: " + e.ToString());
					}
				});

				// begin the async build above
				IAsyncResult res = listFiles.BeginInvoke(d);

			} catch (UnauthorizedAccessException) {
				MessageBox.Show("Access denied");
				this.currentDirectory = "";
			} catch (DirectoryNotFoundException) {
				MessageBox.Show("Directory not found: " + currentPath);
				this.currentDirectory = "";
			} catch (IOException e) {
				MessageBox.Show("IOError :" + e.ToString());
			}
		}

		SearchPanel searchPanel;
		ConfigForm configForm;
		Config config;
		FileDockForm rightChild;

		CreateDirPanel createDirPanel;
		CreateFilePanel createFilePanel;

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this.DragDrop += new DragEventHandler(FileDockForm_DragDrop);
			this.DragOver += new DragEventHandler(FileDockForm_DragOver);
			this.KeyUp += new KeyEventHandler(FileDockForm_KeyUp);
			this.KeyPress += new KeyPressEventHandler(FileDockForm_KeyPress);
			listFiles.ItemDrag += new ItemDragEventHandler(listFiles_ItemDrag);
			listFiles.MouseMove += new MouseEventHandler(listFiles_MouseMove);
			listFiles.MouseClick += new MouseEventHandler(listFiles_MouseClick);
			listFiles.DoubleClick += new EventHandler(listFiles_DoubleClick);
			listFiles.DragOver += new DragEventHandler(FileDockForm_DragOver);
			listFiles.DragDrop += new DragEventHandler(FileDockForm_DragDrop);

			this.RegisterAppBar();
			this.IdealSize = new Size(200, SystemInformation.PrimaryMonitorSize.Height);
			this.AppBarDock = AppBarDockStyle.ScreenLeft;
			
			// set double-buffering options
			this.SetExStyles();

			
			this.searchPanel = new SearchPanel(this);
			this.searchPanel.Location = flowLayoutPanel1.PointToScreen(new Point(flowLayoutPanel1.Left, listFiles.Top));
			this.searchPanel.Hide();
			this.Controls.Add(this.searchPanel);

			this.createDirPanel = new CreateDirPanel(this);
			this.createDirPanel.Location = flowLayoutPanel1.PointToScreen(new Point(flowLayoutPanel1.Left, listFiles.Top));
			this.createDirPanel.Hide();
			this.Controls.Add(this.createDirPanel);

			this.createFilePanel = new CreateFilePanel(this);
			this.createFilePanel.Location = flowLayoutPanel1.PointToScreen(new Point(flowLayoutPanel1.Left, listFiles.Top));
			this.createFilePanel.Hide();
			this.Controls.Add(this.createFilePanel);

			this.configForm = new ConfigForm();
			this.config = new Config(this.configForm, "FileDock");

			// set some default config values
			this.config["SingleClick"] = "True";
			this.config["CurrentDirectory"] = "";
			this.config["CurrentDrive"] = "C";
			// load any saved values from the registry, overwriting the defaults
			this.config.LoadFromRegistry();
			// then last, and highest priority, would be a path passed on the command line
			bool skipone = true;
			foreach ( string arg in Environment.GetCommandLineArgs() ) {
				if ( skipone ) {
					skipone = false;
					continue;
				}
				Regex R = new Regex(@"(\w{1}):[/\\](.*)$");
				if ( R.IsMatch(arg) ) {
					Match m = R.Match(arg);
					this.config["CurrentDrive"] = m.Groups[1].Value;
					if ( File.Exists(m.Groups[0].Value) ) {
						this.config["CurrentDirectory"] = Regex.Replace(m.Groups[2].Value, @"[/\\][^/\\]+$", "");
					} else if ( Directory.Exists(m.Groups[0].Value) ) {
						this.config["CurrentDirectory"] = m.Groups[2].Value;
					} else {
						MessageBox.Show("File/Directory does not exist: " + arg);
					}
				} else {
					MessageBox.Show("Commandline argument: " + arg + " was not recognized as a valid path/file");
				}
			}

			this.hoveredItem = null;
			
			refreshDrives();
			refreshFiles();
		}

		void FileDockForm_KeyPress(object sender, KeyPressEventArgs e) {
			Debug.Print("keyPress: " + e.KeyChar);
		}

		void FileDockForm_KeyUp(object sender, KeyEventArgs e) {
			Debug.Print("keyUp: " + e.KeyCode.ToString());
		}

		protected override void OnClosing(CancelEventArgs e) {
			if (this.rightChild != null && !this.rightChild.IsDisposed) {
				this.rightChild.Close();
			}
			this.config.SaveToRegistry();
			this.UnregisterAppBar();
			base.OnClosing(e);
		}
		
		private void listFiles_DoubleClick(object sender, EventArgs e) {
			if ( (FileDockForm.ModifierKeys & Keys.Control) == 0
					&& (FileDockForm.ModifierKeys & Keys.Shift) == 0
					) {
				Debug.Print("double click fired");
				if ( hoveredItem != null ) {
					if ( config["SingleClick"] == "True" ) {
						Debug.Print("Double click ignored");
						return;
					}
					Debug.Print("Double click allowed");
					activateFileFolder(hoveredItem);
				}
			}
		}

		private void listFiles_MouseClick(object sender, MouseEventArgs e) {
			if ( (FileDockForm.ModifierKeys & Keys.Control) == 0
					&& (FileDockForm.ModifierKeys & Keys.Shift) == 0
					) {
				Debug.Print("single click fired");
				if ( hoveredItem != null && config["SingleClick"] == "True" ) {
					Debug.Print("single click allowed");
					activateFileFolder(hoveredItem);
				}
			}
		}

		private ListViewItem prevHoveredItem;
		private ListViewItem hoveredItem;
		private ToolTip hoveredTip;
		private void listFiles_MouseMove(object sender, MouseEventArgs e) {
			// select a whole row all at once here, so ignore the x 
			//Debug.Print("X: " + e.X + " Y: " + e.Y);
			ListViewItem newHoveredItem = listFiles.GetItemAt(1, e.Y);
			if (newHoveredItem != null) {
				if (prevHoveredItem != null && prevHoveredItem == newHoveredItem) {
					return;
				}
				prevHoveredItem = hoveredItem;
				hoveredItem = newHoveredItem;
				if ( hoveredTip != null ) {
					hoveredTip.Hide(listFiles);
					hoveredTip.Dispose();
					hoveredTip = null;
				}
				hoveredTip = new ToolTip();
				Point hoverPos = this.PointToClient(e.Location);
				hoverPos = new Point(hoverPos.X + this.Left + 15, hoverPos.Y + 10);
				hoveredTip.Show(hoveredItem.Text, listFiles, hoverPos);
			}
			//Debug.Print("Hovered: " + hoveredItem.Text + " prevHovered: " + (prevHoveredItem != null ? prevHoveredItem.Text : "null"));
			if (prevHoveredItem != null) {
				prevHoveredItem.ForeColor = Color.Black;
				prevHoveredItem.Font = new Font(prevHoveredItem.Font, 0);
			}
			if (hoveredItem != null) {
				hoveredItem.ForeColor = Color.DarkBlue;
				hoveredItem.Font = new Font(hoveredItem.Font, FontStyle.Bold | FontStyle.Underline);
			}
		}

		// when an item is first picked up:
		private void listFiles_ItemDrag(object sender, ItemDragEventArgs e) {
			ListViewItem node = (ListViewItem)e.Item;
			if (node != null) {
				string[] s = new string[] { (String)node.Tag };
				// if multiple files are selected
				if( listFiles.SelectedItems.Count > 1 ) {
					List<string> files = new List<string>();
					foreach ( ListViewItem item in listFiles.SelectedItems ) {
						files.Add((string)(item.Tag));
					}
					// pick up all of them for the drag
					s = files.ToArray();
				}
				DataObject data = new DataObject(DataFormats.FileDrop, s);
				// begin the drag
				listFiles.DoDragDrop(data, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.All);
			}
		}
		
		// when an item is being floated over any part of the form
		private void FileDockForm_DragOver(object sender, DragEventArgs e) {
			bool CtrlPressed = (FileDockForm.ModifierKeys & Keys.Control) == Keys.Control;
			bool AltPressed = (FileDockForm.ModifierKeys & Keys.Alt) == Keys.Alt;
			if (CtrlPressed
				&& (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move) {
				e.Effect = DragDropEffects.Move;
			} else if (AltPressed
				&& (e.AllowedEffect & DragDropEffects.All) == DragDropEffects.All) {
				e.Effect = DragDropEffects.All;
			} else if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy) {
				e.Effect = DragDropEffects.Copy;
			} else {
				e.Effect = DragDropEffects.None;
			}
		}
		
		// when an item is actually dropped on some part of the form
		private void FileDockForm_DragDrop(object sender, DragEventArgs e) {
			Debug.Print("DragDrop: " + sender.ToString() + " " + e.ToString());
			if (e.Effect == DragDropEffects.All) {
				this.Focus();
				// show a context menu with Copy Move options
				Control target = (Control)sender;
				this.contextMenu1.Show(this, 0, 0);
				// when they make a choice, call this function again with the proper effect
				this.contextMenu1.ItemClicked += new ToolStripItemClickedEventHandler(delegate(object _sender, ToolStripItemClickedEventArgs _e)
				{
					if (_e.ClickedItem.Text == "Copy") {
						e.Effect = DragDropEffects.Copy;
					} else if (_e.ClickedItem.Text == "Move") {
						e.Effect = DragDropEffects.Move;
					}
					FileDockForm_DragDrop(sender, e);
				});
				// do nothing right now, wait for a menu choice
				return;
			} else {
				bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
				bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
				if (isFileDrop) {

					// determine what that the intended target of the drop is
					Point clt = listFiles.PointToClient(new Point(e.X, e.Y));
					ListViewItem target = listFiles.GetItemAt(clt.X, clt.Y);
					string targetDir = this.currentPath;
					// if it was dropped on a folder node directly
					if (target != null && target.ImageIndex == 0) {
						// then target that node directly
						targetDir = target.Name;
					}

					foreach (string srcFile in (string[])e.Data.GetData(DataFormats.FileDrop)) {
						Debug.Print("Got dropped file: " + srcFile + " on target: " + targetDir);
						// if the user did a tiny mis-drag, of an item onto itself, count it as an activate instead
						if (targetDir == srcFile) {
							activateFileFolder(target);
							return;
						}
						// if we they are dragging a directory to us
						if (Directory.Exists(srcFile)) {
							// build the new directory name
							string[] tmp = srcFile.Split(Path.DirectorySeparatorChar);
							string dirName = tmp[tmp.Length - 1];
							string newTargetDir = Path.Combine(targetDir, dirName);
							if (e.Effect == DragDropEffects.Copy) {
								// copy *.* from the source to the new directory
								using (new ActionHelper(this, sender, "Copy")) {
									try {
										copyDirectory(srcFile, newTargetDir);
									} catch ( UnauthorizedAccessException ) {
										MessageBox.Show("Not Allowed.");
									}
								}
							} else if (e.Effect == DragDropEffects.Move) {
								// move *.* from the source dir to the target dir
								using (new ActionHelper(this, sender, "Move")) {
									try {
										Directory.Move(srcFile, newTargetDir);
									} catch ( UnauthorizedAccessException ) {
										MessageBox.Show("Not Allowed.");
									}
								}
							} else {
								MessageBox.Show("Unknown directory effect: " + e.ToString());
							}
						} else if (File.Exists(srcFile)) {
							string newFile = Path.Combine(targetDir, Path.GetFileName(srcFile));
							if (newFile != srcFile) {
								if (e.Effect == DragDropEffects.Copy) {
									using (new ActionHelper(this, sender, "Copy")) {
										try {
											File.Copy(Path.GetFullPath(srcFile), newFile);
										} catch ( UnauthorizedAccessException ) {
											MessageBox.Show("Not Allowed.");
										} catch ( IOException ex ) {
											MessageBox.Show("IOException: " + ex.ToString());
										}
									}
								} else if (e.Effect == DragDropEffects.Move) {
									using (new ActionHelper(this, sender, "Move")) {
										Debug.Print("Moving " + srcFile + " " + newFile);
										try {
											File.Move(Path.GetFullPath(srcFile), newFile);
										} catch ( UnauthorizedAccessException ) {
											MessageBox.Show("Not Allowed.");
										} catch ( IOException ex ) {
											MessageBox.Show("IOException: " + ex.ToString());
										}
									}
								} else {
									MessageBox.Show("Unknown file effect: " + e.Effect.ToString());
								}
							}
						} else {
							MessageBox.Show("No such file or directory: " + srcFile);
						}
					}
				} else if (isStringDrop) {
					Debug.Print("Got dropped text: " + e.Data.GetData(DataFormats.Text));
				}
			}
		}
		
		// a helper, since for some reason this is missing from the windows apis
		private void copyDirectory(string Src, string Dst) {
			MessageBox.Show("TODO");
			return;
			String[] Files;
			if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
				Dst += Path.DirectorySeparatorChar;
			if (!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
			Files = Directory.GetFileSystemEntries(Src);
			foreach (string Element in Files) {
				if (Directory.Exists(Element))
					copyDirectory(Element, Dst + Path.GetFileName(Element));
				else
					File.Copy(Element, Dst + Path.GetFileName(Element), true);
			}
		}
		
		// when the user has indicated they want to launch this file or open this directory
		private void activateFileFolder(ListViewItem node) {
			string save_dir = currentDirectory;
			try {
				string new_dir = (String)node.Tag;
				if (new_dir == ".") {	// do nothing
					return;
				} else if (new_dir == "refresh") {
					// dont change any dirs, just refresh the current one
					refreshFiles();
					return;
				} else if (new_dir == "..") {
					if (this.currentDirectory.Contains("\\")) {
						new_dir = Regex.Replace(this.currentDirectory, "\\\\[^\\\\]+$", "");
					} else {
						new_dir = "";
					}
				}
				this.currentDirectory = new_dir;
			} catch (Exception err) {
				MessageBox.Show("Error: " + err.ToString() + " this.currentDirectory: " + this.currentDirectory);
				this.currentDirectory = "";
			}
			if (currentDirectory.Contains(":\\")) {
				currentDirectory = currentDirectory.Substring(3);
			}
			if (!Directory.Exists(currentPath)) {
				Debug.Print("Executing");
				startFileWithDefaultHandler(currentPath);
				// cover the loading time with a little animation
				Form floater = new Form();
				floater.FormBorderStyle = FormBorderStyle.None;
				floater.Size = new Size(200, (int)(floater.Font.SizeInPoints) + 4);
				floater.Font = node.Font;
				floater.MinimumSize = new Size(200, (int)(floater.Font.SizeInPoints) + 4);
				//floater.MaximumSize = new Size(0, (int)(floater.Font.SizeInPoints) + 4);
				floater.ShowInTaskbar = false;
				floater.Paint += new PaintEventHandler(delegate(object sender, PaintEventArgs e)
				{
					SizeF size = e.Graphics.MeasureString(node.Text, floater.Font);
					floater.Size = new Size((int)(size.Width + 1), (int)(floater.Font.Size * 1.3) + 7);
					//e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue),floater.Left,floater.Top,floater.Width,floater.Height);
					e.Graphics.DrawString(node.Text, floater.Font, new SolidBrush(Color.Black), new Point(0, 0));
				});
				floater.Show(this);
				floater.Location = node.ListView.PointToScreen(node.Bounds.Location);
				floater.TopMost = true;
				floater.Update();


				while (floater.Opacity > 0.001) {
					floater.Opacity -= .10;
					if (new Random().Next(0, 1000) > 300) floater.Top -= 1;
					if (floater.Opacity < .7) {
						floater.Font = new Font(floater.Font.FontFamily, floater.Font.SizeInPoints - .3f);
					} else {
						floater.Font = new Font(floater.Font.FontFamily, floater.Font.SizeInPoints + .3f);
					}
					floater.Update();
					System.Threading.Thread.Sleep(30);
				}

				floater.Hide();
				floater.Dispose();

				currentDirectory = save_dir;
			} else {
				refreshFiles();
			}
		}
		
		// delegate type used when refreshing the file list asynchronously
		private delegate void RefreshDelegate();
		
		// launch a file using the 'start' command-line tool to do the registry/mime lookup
		private void startFileWithDefaultHandler(string filename) {
			execCmd(@"C:\WINDOWS\System32\cmd.exe", "/c start \"start\" \"" + (filename) + "\"", null, true);
		}
		
		// execute something in windows
		private void execCmd(string cmd, string arguments, string workingDirectory, bool hidden) {
			ProcessStartInfo si = new ProcessStartInfo(cmd);
			si.Arguments = arguments;
			if (hidden) {
				si.CreateNoWindow = true;
				si.WindowStyle = ProcessWindowStyle.Hidden;
			}
			if (workingDirectory != null) {
				si.WorkingDirectory = workingDirectory;
			} else {
				si.WorkingDirectory = this.currentPath;
			}
			Debug.Print("Exec: " + cmd + " " + arguments);
			System.Diagnostics.Process.Start(si);
			Debug.Print("Exec returned");
		}
		
		// the search button
		public void search_Click(object sender, EventArgs e) {
			searchPanel.Toggle();
		}

		// create directory button
		private void createDir_Click(object sender, EventArgs e) {
			createDirPanel.Toggle();
		}

		// the create file button
		private void createFile_Click(object sender, EventArgs e) {
			createFilePanel.Toggle();
		}

		// the refresh button
		private void refresh_Click(object sender, EventArgs e) {
			refreshDrives();
			refreshFiles();
		}
		
		// the close button
		private void close_Click(object sender, EventArgs e) {
			this.Close();
		}
		
		// the clone button
		protected void clone_Click(object sender, EventArgs e) {
			if (this.rightChild != null && !this.rightChild.IsDisposed) {
				this.rightChild.clone_Click(null, null);
			} else {
				this.rightChild = new FileDockForm();
				if (this.IsMasterDock) {
					this.rightChild.masterFileDock = this;
				} else {
					this.rightChild.masterFileDock = this.masterFileDock;
				}
				this.rightChild.Show();
			}
		}
		public FileDockForm masterFileDock; // null if this instance is the head of the linked list of docks
		// otherwise, a reference to the first form in the list
		public bool IsMasterDock {
			get {
				return (masterFileDock == null);
			}
		}

		// the zip button (and its droppable effects)
		private void sevenZip_Click(object sender, EventArgs e) {
			execCmd(@"C:\Program Files\7-Zip\7zFM.exe", "\"" + this.currentPath + "\"", null, false);
		}
		private void sevenZip_DragDrop(object sender, DragEventArgs e) {
			if (e.Effect == DragDropEffects.Copy) {
				bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
				bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
				if (isFileDrop) {
					foreach (string srcFile in (string[])e.Data.GetData(DataFormats.FileDrop)) {
						Debug.Print(Directory.GetCurrentDirectory());
						execCmd(@"7zG.exe", "x \"" + srcFile + "\" -o\"" + this.currentPath + "\" -r", Directory.GetCurrentDirectory(), false);
					}
				} else if (isStringDrop) {
					string srcFile = (string)e.Data.GetData(DataFormats.Text);
					Debug.Print(Directory.GetCurrentDirectory());
					execCmd(@"7zG.exe", "x \"" + srcFile + "\" -o\"" + this.currentPath + "\" -r", Directory.GetCurrentDirectory(), false);
				}
			} else {
				MessageBox.Show("huh?");
			}
		}
		private void sevenZip_DragEnter(object sender, DragEventArgs e) {
			bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
			bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
			if (isFileDrop || isStringDrop) {
				e.Effect = DragDropEffects.Copy;
			} else {
				e.Effect = DragDropEffects.None;
			}
		}
		
		// the delete button (and its droppable effects)
		private void delete_Click(object sender, EventArgs e) {
			MessageBox.Show("Drop files here to delete them.");
		}
		private void delete_DragEnter(object sender, DragEventArgs e) {
			bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
			bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
			if (isFileDrop || isStringDrop) {
				e.Effect = DragDropEffects.Copy;
			} else {
				e.Effect = DragDropEffects.None;
			}
		}
		private void delete_DragDrop(object sender, DragEventArgs e) {
			if (e.Effect == DragDropEffects.Copy) {
				bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
				bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
				if (isFileDrop) {
					string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
					
					if (MessageBox.Show("Are you sure you want to delete:\n " + String.Join("\n",files), "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes) {
						foreach (string srcFile in files) {
							deleteFileOrDirectory(srcFile);
						}
					}
				} else if (isStringDrop) {
					string srcFile = (string)e.Data.GetData(DataFormats.Text);
					if (MessageBox.Show("Are you sure you want to delete " + srcFile + "?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes) {
						deleteFileOrDirectory(srcFile);
					}
				}
			} else {
				MessageBox.Show("huh?");
			}
		}
		// a helper for deleting things safely
		private void deleteFileOrDirectory(string path) {
			Debug.Print("delete: " + path);
			if (Directory.Exists(path)) {
				Directory.Delete(path, true);
				removeListViewItemUsingAnimation(path);
			} else if (File.Exists(path)) {
				File.Delete(path);
				removeListViewItemUsingAnimation(path);
			} else {
				MessageBox.Show("Could not find file/directory: " + path);
			}
		}

		private void removeListViewItemUsingAnimation(string path) {
			foreach ( ListViewItem item in listFiles.Items ) {
				if ( (string)(item.Tag) == path ) {
					listFiles.Focus();
					item.Selected = false;
					item.ForeColor = Color.Red;
					item.Font = new Font(item.Font, FontStyle.Strikeout);
					listFiles.Update();
					System.Threading.Thread.Sleep(300);
					listFiles.Items.Remove(item);
					break;
				}
			}
		}
		
		// the config/options button
		private void config_Click(object sender, EventArgs e) {
			if (configForm.Visible) {
				configForm.Focus();
			} else if (!configForm.IsDisposed) {
				configForm.Show(this);
				configForm.Focus();
			} else {
				configForm = new ConfigForm();
				config.UpdateFormBinding(configForm);
				configForm.Show(this);
				configForm.Focus();
			}
		}
		
		public string StatusText {
			get { return statusLabel1.Text; }
			set { statusLabel1.Text = value; }
		}


	}

	public class ActionHelper : IDisposable {
		private FileDockForm form;
		private string type;
		private object sender;
		public ActionHelper(FileDockForm f, object sender, string type) {
			f.Cursor = Cursors.WaitCursor;
			if (type == "Copy") {
				f.StatusText = "Copying...";
				f.Update();
			} else if (type == "Move") {
				f.StatusText = "Moving...";
				f.Update();
			}
			this.form = f;
			this.type = type;
			this.sender = sender;
		}
		public void Dispose() {
			if (this.type == "Copy") {
				form.StatusText = "Copied";
			} else if (this.type == "Move") {
				form.StatusText = "Moved";
			}
			this.form.Cursor = Cursors.Default;
			// refresh all open panels
			if (this.form.IsMasterDock) {
				this.form.refreshFiles(true);
			} else {
				this.form.masterFileDock.refreshFiles(true);
			}
		}
	}

}

