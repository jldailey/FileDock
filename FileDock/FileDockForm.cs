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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Threading;

namespace FileDock {
	public partial class FileDockForm : AppBar {

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
			set {
				currentDrive = Regex.Replace(value, @"^(\w+):.*$", @"$1");
				currentDirectory = Regex.Replace(value, @"^\w+:\\(.*)$", @"$1");
				refreshFiles();
			}
		}
		public Dictionary<string, string> savedPaths; // maps drives to the last path we visited on that drive

		public FileDockForm() {
			this.DoubleBuffered = true;
			savedPaths = new Dictionary<string, string>();
			InitializeComponent();
			leftChild = null;
		}

		public FileDockForm(FileDockForm left) {
			this.DoubleBuffered = true;
			savedPaths = new Dictionary<string, string>();
			InitializeComponent();
			leftChild = left;
		}
		
		// re-read all the system drives and re-draw the DriveButtons
		public void refreshDrives() {
			refreshDrives(true);
		}
		public void refreshDrives(bool moveListFiles) {
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
					this.listFiles.Focus();
				};
				

				flowLayoutPanel1.Controls.Add(b);
				driveCount++;
			}
			if ( moveListFiles ) {
				listFiles.Top = button3.Bottom + 4;
				listFiles.Left = 2;
			}
		}
		public string formatFileNameForList(string filename) {
			string ret = "";
			int limit = (int)(listFiles.Columns[0].Width / listFiles.Font.SizeInPoints);

			return ret;
		}


		// delegate type used when refreshing the file list asynchronously
		private delegate void RefreshDelegate();
				
		// re-read the current directory and fill up the ListView
		public void refreshFiles(bool allFileDocks) {
			refreshFiles();
			if (allFileDocks
				&& this.rightChild != null
				&& !this.rightChild.IsDisposed) {
				this.rightChild.refreshFiles(true);
			}
		}
		private Semaphore listSem = new Semaphore(1, 1);
		public void refreshFiles() {
			try {
				fileSystemWatcher1.Path = this.currentPath;
				savedPaths[this.currentDrive] = this.currentDirectory;

				this.Refresh();
			
				// build a new tree asynchronously, so that form can still draw itself while this is updating
				RefreshDelegate d = new RefreshDelegate(delegate() {
					try {
						listSem.WaitOne();
						fileSystemWatcher1.EnableRaisingEvents = false;
						listFiles.Items.Clear();
						int maxLen = 30;
						listFiles.Columns[0].Text = AbbreviatePath(this.currentPath, maxLen);

						ListViewItem tmp = listFiles.Items.Add("..");
						tmp.Tag = "..";
						tmp.Group = listFiles.Groups[0];
						string[] dirs = Directory.GetDirectories(currentPath);
						Array.Sort<string>(dirs);
						foreach ( string dir in dirs ) {
							ListViewItem node = listFiles.Items.Add(Path.GetFileName(dir));
							node.ToolTipText = Path.GetFileName(dir);
							node.Tag = Path.GetFullPath(dir);
							node.ImageIndex = 0;
							node.Group = listFiles.Groups[0];
						}
						string[] files = Directory.GetFiles(currentPath, "*.*");
						Array.Sort<string>(files);
						foreach ( string file in files ) {
							if ( this.config["ShowHidden"] == "False" ) {
								FileInfo inf = new FileInfo(file);
								if ( (inf.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ) {
									continue;
								}
							}
							ListViewItem node = listFiles.Items.Add(Path.GetFileName(file));
							node.ToolTipText = Path.GetFileName(file);
							node.Tag = Path.GetFullPath(file);
							node.ImageIndex = 1;
							node.Group = listFiles.Groups[1];
						}
						fileSystemWatcher1.EnableRaisingEvents = true;
					} catch ( Exception e ) {
						MessageBox.Show("Exception: " + e.ToString());
					} finally {
						listSem.Release();
					}
				});
				// begin the async build above
				IAsyncResult res = listFiles.BeginInvoke(d);

			} catch (UnauthorizedAccessException) {
				MessageBox.Show("Access denied");
				this.currentDirectory = "";
			} catch( ArgumentException ) {
				MessageBox.Show("Previous directory: " + this.currentDirectory + " is no longer valid.");
				this.currentPath = "C:\\";
			} catch (DirectoryNotFoundException) {
				MessageBox.Show("Directory not found: " + currentPath);
				this.currentDirectory = "";
			} catch (IOException e) {
				MessageBox.Show("IOError :" + e.ToString());
			}
		}

		SearchPanel searchPanel;
		FavoritesPanel favPanel;
		ConfigForm configForm;
		public Config config;
		FileDockForm rightChild;
		FileDockForm leftChild;
		
		CreateDirPanel createDirPanel;
		CreateFilePanel createFilePanel;

		public bool dockOnLoad = true;

		protected override void OnLoad(EventArgs e) {
			if ( dockOnLoad ) {
				RegisterAppBar();
				UnregisterAppBar();
				RegisterAppBar();
				this.idealSize = new Size(200, SystemInformation.PrimaryMonitorSize.Height);
				this.idealLocation = new Point(0, 0);
				this.RefreshPosition();
			} else if ( this.leftChild != null ) {
				this.Size = new Size(this.leftChild.Width, this.leftChild.Height);
				this.Left = this.leftChild.Right;
				this.TopMost = true;
			}
			base.OnLoad(e);
			this.DragDrop += new DragEventHandler(FileDockForm_DragDrop);
			this.DragOver += new DragEventHandler(FileDockForm_DragOver);
			this.KeyUp += new KeyEventHandler(FileDockForm_KeyUp);
			this.Enter += new EventHandler(NoFocusAllowed);
			listFiles.ItemDrag += new ItemDragEventHandler(listFiles_ItemDrag);
			listFiles.MouseMove += new MouseEventHandler(listFiles_MouseMove);
			listFiles.MouseLeave += new EventHandler(listFiles_MouseLeave);
			listFiles.MouseClick += new MouseEventHandler(listFiles_MouseClick);
			listFiles.DoubleClick += new EventHandler(listFiles_DoubleClick);
			listFiles.DragOver += new DragEventHandler(FileDockForm_DragOver);
			listFiles.DragDrop += new DragEventHandler(FileDockForm_DragDrop);
			listFiles.KeyUp += new KeyEventHandler(FileDockForm_KeyUp);
			fileSystemWatcher1.EnableRaisingEvents = false;
			fileSystemWatcher1.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName;
			fileSystemWatcher1.Filter = "";
			fileSystemWatcher1.Created += new FileSystemEventHandler(delegate(object source, FileSystemEventArgs ev) {
				Debug.Print("Created "+ev.Name);
				refreshFiles();
			});
			fileSystemWatcher1.Deleted += new FileSystemEventHandler(delegate(object source, FileSystemEventArgs ev) {
				refreshFiles();
				Debug.Print("Deleted "+ev.Name);
			});
			fileSystemWatcher1.Renamed += new RenamedEventHandler(delegate(object source, RenamedEventArgs ev) {
				refreshFiles();
				Debug.Print("Renamed: "+ev.OldName+" "+ev.Name);
			});
			
			// set double-buffering options
			this.SetExStyles();
			
			this.searchPanel = new SearchPanel(this);
			this.searchPanel.Location = new Point(flowLayoutPanel1.Left, listFiles.Top);
			this.searchPanel.Hide();
			this.searchPanel.AfterToggle += delegate {
				if ( this.searchPanel.Visible )
					this.searchPanel.txtFilename.Focus();
			};
			this.Controls.Add(this.searchPanel);

			this.favPanel = new FavoritesPanel(this);
			this.favPanel.Location = new Point(flowLayoutPanel1.Left, listFiles.Top);
			this.favPanel.Width = this.Width - 8;
			this.favPanel.Hide();
			this.favPanel.FavoriteSelected += delegate(string path) {
				this.currentPath = path;
			};
			this.favPanel.AfterToggle += delegate {
				if ( this.favPanel.Visible ) {
					this.favPanel.listFavs.Items.Clear();
					this.favPanel.listFavs.Items.AddRange(favoriteFolders.ToArray());
				} else {
					this.favPanel.listFavs.Items.Clear();
				}
			};
			this.favPanel.listFavs.DrawMode = DrawMode.OwnerDrawVariable;
			this.favPanel.listFavs.DrawItem += new DrawItemEventHandler(delegate (object sender, DrawItemEventArgs ev) {
				string text = AbbreviatePath(this.favPanel.listFavs.Items[ev.Index].ToString(), this.listFiles.Font, ev.Bounds.Width);
				ev.Graphics.DrawString(text,this.listFiles.Font, Brushes.Black, ev.Bounds.X+1, ev.Bounds.Y+3);
			});
			this.favPanel.listFavs.MeasureItem += new MeasureItemEventHandler(delegate(object sender, MeasureItemEventArgs ev) {	});
			
			this.Controls.Add(this.favPanel);

			this.createDirPanel = new CreateDirPanel(this);
			this.createDirPanel.Location = new Point(flowLayoutPanel1.Left, listFiles.Top);
			this.createDirPanel.Hide();
			this.Controls.Add(this.createDirPanel);

			this.createFilePanel = new CreateFilePanel(this);
			this.createFilePanel.Location = new Point(flowLayoutPanel1.Left, listFiles.Top);
			this.createFilePanel.Hide();
			this.Controls.Add(this.createFilePanel);

			this.InstanceIndex = this.getInstanceIndex();

			this.configForm = new ConfigForm();
			this.config = new Config(this.configForm, "FileDock\\Instance"+this.InstanceIndex);

			// set some default config values
			this.config["SingleClick"] = "True";
			this.config["CurrentDirectory"] = "";
			this.config["CurrentDrive"] = "C";
			this.config["SavedPathsMap"] = "";
			this.config["Favorites"] = "";

			// load any saved values from the registry, overwriting the defaults
			this.config.LoadFromRegistry();

			// now try to de-serialize the savedPaths mapping
			try {
				MemoryStream mem = new MemoryStream();
				UintDecodeBytes(mem, this.config["SavedPathsMap"]);
				mem.WriteByte(0x11);
				mem.Position = 0;
				savedPaths = (Dictionary<string, string>)(new BinaryFormatter()).Deserialize(mem);
			} catch ( SerializationException ex ) {
				// pass
			} catch ( Exception ex ) {
				MessageBox.Show(ex.ToString());
			}

			// then de-serialize the list of favorites
			if ( this.leftChild != null ) {
				this.favoriteFolders = this.leftChild.favoriteFolders;
			} else {
				this.favoriteFolders = new List<string>();
				this.favoriteFolders.AddRange(this.config["Favorites"].Split(';'));
				this.favoriteFolders.Remove("");
			}

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
			this.listFiles.Focus();


		}

		protected override void OnClosing(CancelEventArgs e) {
			if (this.rightChild != null && !this.rightChild.IsDisposed) {
				this.rightChild.Close();
			}
			MemoryStream mem = new MemoryStream();
			(new BinaryFormatter()).Serialize(mem,savedPaths);
			mem.Position = 0;
			string s = UintEncodeBytes(mem);
			mem.Close();
			this.config["SavedPathsMap"] = s;
			this.config["Favorites"] = String.Join(";", favoriteFolders.ToArray());
			this.config.SaveToRegistry();
			if ( this.isAppBarRegistered ) {
				this.UnregisterAppBar();
			}
			base.OnClosing(e);
		}

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			moveHandle1.Width = this.Width - 4;
			listFiles.Columns[0].Width = this.Width - 4;
			Debug.Print("On Resize: " + this.Width + " : "+moveHandle1.Width);
		}

		protected override CreateParams CreateParams {
			get {
				CreateParams cp = base.CreateParams;
				//cp.Style &= (~0x00C00000); // no caption
				cp.Style &= (~0x00800000); // no border
				return cp;
			}
		}

		#region UintEncode/DecodeBytes
		private string UintEncodeBytes(MemoryStream input) {
			StringBuilder s = new StringBuilder();
			int b;
			while ( (b = input.ReadByte()) != -1 ) {
				s.AppendFormat("{0:X2}", (byte)b);
			}
			return s.ToString();
		}
		private void UintDecodeBytes(MemoryStream output, string src) {
			StringReader r = new StringReader(src);
			char[] buffer = new char[2];
			while ( r.Read(buffer, 0, 2) == 2 ) {
				byte d = Byte.Parse("" + buffer[0] + buffer[1], System.Globalization.NumberStyles.AllowHexSpecifier);
				output.WriteByte(d);
			}
		}
		#endregion

		#region Path rolling
		/* Path rolling is the mechanism that would enforce mac-like file manager behavior.
		 * To do this, you make activating a folder spawn a new instance.
		 * You also limit the maximum number of instances to something like 3, then once you hit that limit,
		 * you would roll paths left instead of spawning the new instance.
		 */
		// this stack would be used by Instance 0, the far left bar, to track left-side overflow
		private Stack<string> pathStack = null;
		public void pushPathLeft(string path) {
			if ( this.leftChild != null ) {
				// push our current path left
				this.leftChild.pushPathLeft(this.currentPath);
			} else {
				// or push it up on the stack of paths that have overflowed left
				if ( pathStack == null ) {
					pathStack = new Stack<string>();
				}
				pathStack.Push(this.currentPath);
			}
			// then update our current path
			this.currentPath = path;
		}
		#endregion

		void FileDockForm_KeyUp(object sender, KeyEventArgs e) {
			Debug.Print("keyUp: " + e.KeyCode.ToString());
			switch ( e.KeyCode ) {
				case Keys.OemQuestion:
					searchPanel.Toggle();
					break;
				case Keys.F5:
					refresh_Click(null, null);
					break;
			}

		}

		private void listFiles_DoubleClick(object sender, EventArgs e) {
			if ( (FileDockForm.ModifierKeys & Keys.Control) == 0
					&& (FileDockForm.ModifierKeys & Keys.Shift) == 0
					) {
				if ( hoveredItem != null ) {
					if ( config["SingleClick"] == "True" ) {
						return;
					}
					activateFileFolder(hoveredItem);
				}
			}
		}

		private void listFiles_MouseClick(object sender, MouseEventArgs e) {
			if ( (FileDockForm.ModifierKeys & Keys.Control) == 0
					&& (FileDockForm.ModifierKeys & Keys.Shift) == 0
					) {
				if ( hoveredItem != null && config["SingleClick"] == "True" ) {
					activateFileFolder(hoveredItem);
				}
			}
		}

		private ListViewItem prevHoveredItem;
		private ListViewItem hoveredItem;
		private ToolTip hoveredTip;
		private void listFiles_MouseMove(object sender, MouseEventArgs e) {
			ListViewItem newHoveredItem = listFiles.GetItemAt(e.X, e.Y);
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
				string hoverText = hoveredItem.Text;
				try {
					string fname = (string)hoveredItem.Tag;
					FileInfo f = new FileInfo(fname);
					double kb = f.Length / 1024.0;
					hoverText += String.Format("\n- {0:0,0.00} kb", kb);
				} catch ( FileNotFoundException ) {
				}
				hoveredTip.Show(hoverText, listFiles, hoverPos);
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

		void listFiles_MouseLeave(object sender, EventArgs e) {
			if ( hoveredTip != null ) {
				hoveredTip.Hide(listFiles);
				hoveredTip.Dispose();
				hoveredTip = null;
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
				listFiles.DoDragDrop(data, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link | DragDropEffects.All | DragDropEffects.None );
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

		public void closeAllRightChildren() {
			if ( this.rightChild != null ) {
				this.rightChild.closeAllRightChildren();
				this.rightChild.Close();
			}
		}

		// when the user has indicated they want to launch this file or open this directory
		private void activateFileFolder(ListViewItem node) {
			string save_dir = currentDirectory;
			try {
				string new_dir = (String)node.Tag;
				if (new_dir == ".") {	// do nothing
					return;
				} else if( new_dir == "..refresh.." ) {
					refreshFiles();
					return;
				} else if (new_dir == "..") {
					if (this.currentDirectory.Contains("\\")) {
						//remove the last path element from the current directory
						new_dir = Regex.Replace(this.currentDirectory, "\\\\[^\\\\]+$", "");
					} else {
						new_dir = "";
					}
				}

				// do the actual state change
				if ( false ) { // if we want to load folders mac-style, scrolling them out to the right
					// if we already have a right child
					if ( this.rightChild != null && !this.rightChild.IsDisposed ) {
						this.rightChild.currentPath = new_dir;
						this.rightChild.closeAllRightChildren();
					} else {
						this.rightChild = new FileDockForm(this);
						this.rightChild.Show();
						this.rightChild.currentPath = new_dir;
					}
				} else {
					this.currentDirectory = new_dir;
				}
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
				// cover the loading time with a little bounce animation of the clicked item
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
					floater.Left += 1;
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

		// the favorites button
		private void favorites_Click(object sender, EventArgs e) {
			this.favPanel.Toggle();
		}
		private void favorites_DragEnter(object sender, DragEventArgs e) {
			bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
			bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
			if ( isFileDrop || isStringDrop ) {
				e.Effect = DragDropEffects.All;
			} else {
				e.Effect = DragDropEffects.None;
			}
		}
		private void favorites_DragDrop(object sender, DragEventArgs e) {
			if ( e.Effect == DragDropEffects.All ) {
				bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
				bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
				string srcFile = null;
				if ( isFileDrop ) {
					srcFile = ((string[])(e.Data.GetData(DataFormats.FileDrop)))[0];
				} else if ( isStringDrop ) {
					srcFile = (string)e.Data.GetData(DataFormats.Text);
				}
				addFolderToFavorites(srcFile);
			}
		}

		protected List<string> favoriteFolders;
		public void addFolderToFavorites(string srcFile) {
			if ( !Directory.Exists(srcFile) ) {
				Debug.Print("Directory not found, trying to trim: " + srcFile);
				srcFile = Regex.Replace(srcFile, "\\\\[^\\\\]+$", "");
			}
			if ( !Directory.Exists(srcFile) ) {
				MessageBox.Show("No such directory: " + srcFile);
				return;
			}
			if ( !favoriteFolders.Contains(srcFile) ) {
				favoriteFolders.Add(srcFile);
			}
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
			refreshDrives(false);
			refreshFiles();
		}
		
		// the close button
		private void close_Click(object sender, EventArgs e) {
			this.Close();
		}
		
		// the clone button
		protected void clone_Click(object sender, EventArgs e) {
			// if we already have a right child
			if (this.rightChild != null && !this.rightChild.IsDisposed) {
				this.rightChild.clone_Click(sender, e);
			} else {
				this.rightChild = new FileDockForm(this);
				this.dockOnLoad = true;
				this.rightChild.Show();
			}
		}
		private void clone_DragDrop(object sender, DragEventArgs e) {
			if ( e.Effect == DragDropEffects.All ) {
				bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
				bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
				string srcFile = "";
				if ( isFileDrop ) {
					srcFile = ((string[])(e.Data.GetData(DataFormats.FileDrop)))[0];
				} else if ( isStringDrop ) {
					srcFile = (string)e.Data.GetData(DataFormats.Text);
				}
				if ( !Directory.Exists(srcFile) ) {
					srcFile = Regex.Replace(srcFile, "\\\\[^\\\\]+$", "");
				}
				if ( !Directory.Exists(srcFile) ) {
					MessageBox.Show("No such folder: " + srcFile);
					return;
				}
				this.clone_Click(null, null); // pop up a new frame
				this.rightChild.currentPath = srcFile;
			} else {
				MessageBox.Show("huh?");
			}
		}
		private void clone_DragEnter(object sender, DragEventArgs e) {
			bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
			bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
			if ( isFileDrop || isStringDrop ) {
				e.Effect = DragDropEffects.All;
			} else {
				e.Effect = DragDropEffects.None;
			}
		}

		// the zip button (and its droppable effects)
		private void sevenZip_Click(object sender, EventArgs e) {
			execCmd(@"C:\Program Files\7-Zip\7zFM.exe", "\"" + this.currentPath + "\"", null, false);
		}
		private void sevenZip_DragDrop(object sender, DragEventArgs e) {
			if (e.Effect == DragDropEffects.All) {
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
				e.Effect = DragDropEffects.All;
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
		
		public void refreshAllInstances() {
			refresh_Click(null, null);
			FileDockForm cursor = this.leftChild;
			// walk left refreshing
			while ( cursor != null && !cursor.IsDisposed ) {
				cursor.refresh_Click(null, null);
				cursor = cursor.leftChild;
			}
			// then walk right
			cursor = this.rightChild;
			while ( cursor != null && !cursor.IsDisposed ) {
				cursor.refresh_Click(null, null);
				cursor = cursor.rightChild;
			}
		}

		public int InstanceIndex;
		// returns what position we are at in the leftChild/rightChild linked list
		private int getInstanceIndex() {
			if ( leftChild == null ) {
				return 0;
			} else {
				return this.leftChild.getInstanceIndex() + 1;
			}
		}

		private void NoFocusAllowed(object sender, EventArgs e) {
			this.listFiles.Focus();
		}

		private string AbbreviatePath(string path, Font font, float maxWidth) {
			string text = path;
			int maxLen = text.Length;
			while ( TextRenderer.MeasureText(text, font).Width > maxWidth ) {
				maxLen -= 1;
				text = AbbreviatePath(path, maxLen);
			}
			return text;
		}

		private string AbbreviatePath(string path, int maxLen) {
			string ret = "";
			if ( path.Length > maxLen ) {
				string[] elems = path.Split('\\');
				
				maxLen -= 10; // reserve a minimum 10 chars for the lead-in we will add later
				int loadIndex = 1;
				string last = elems[elems.Length - loadIndex];
				while ( last.Length < maxLen && (loadIndex < (elems.Length - 2)) ) {
					last = elems[elems.Length - (++loadIndex)] + "\\" + last;
				}
				if ( last.Length > maxLen ) {
					int halfMax = maxLen >> 1; // divide by 2 (we will display the first and last equal chunk of the last path element)
					ret += "\\" + last.Substring(0, halfMax) + "...";
					ret += last.Substring(last.Length - halfMax, halfMax);
				} else {
					ret += "\\" + last;
				}
				maxLen += 8; // put the space back for the lead-in
				if ( elems[1].Length <= Math.Abs(maxLen - ret.Length) ) {
					ret = elems[0] + "\\" + elems[1] + ret;
				} else {
					ret = elems[0] + "\\" + elems[1].Substring(0, Math.Abs(maxLen - ret.Length)) + "..." + ret;
				}

			} else {
				ret = path;
			}
			return ret;
		}

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

		}

		#endregion

	}

	public class ActionHelper : IDisposable {
		private FileDockForm form;
		private string type;
		private object sender;
		public ActionHelper(FileDockForm f, object sender, string type) {
			f.Cursor = Cursors.WaitCursor;
			if (type == "Copy") {
				//f.StatusText = "Copying...";
				f.Update();
			} else if (type == "Move") {
				//f.StatusText = "Moving...";
				f.Update();
			}
			this.form = f;
			this.type = type;
			this.sender = sender;
		}
		public void Dispose() {
			if (this.type == "Copy") {
				//form.StatusText = "Copied";
			} else if (this.type == "Move") {
				//form.StatusText = "Moved";
			}
			this.form.Cursor = Cursors.Default;
			form.refreshAllInstances();
		}
	}

}

