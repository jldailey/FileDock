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
		ConfigForm configForm;
		public Config config;
		FileDockForm rightChild;
		FileDockForm leftChild;
		public bool dockOnLoad = true;
		public Dictionary<string, string> savedPaths; // maps drives to the last path we visited on that drive
		private DrivePanel drives;

		public string currentDrive {
			get { return this.config["CurrentDrive"]; }
			set { this.config["CurrentDrive"] = Regex.Replace(value, @"^(\w+):.*$", @"$1"); }
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

		public FileDockForm( FileDockForm left = null ) {
			this.DoubleBuffered = true;
			savedPaths = new Dictionary<string, string>();
			InitializeComponent();
			leftChild = left;
		}

		// delegate type used when refreshing the file list asynchronously
		private delegate void RefreshDelegate();

		// re-read the current directory and fill up the ListView
		public void refreshFiles( bool allFileDocks ) {
			refreshFiles();
			if( allFileDocks
				&& this.rightChild != null
				&& !this.rightChild.IsDisposed ) {
				this.rightChild.refreshFiles(true);
			}
		}
		private Semaphore listSem = new Semaphore(1, 1);
		private long lastRefresh = 0;
		public void refreshFiles() {
			long delta = DateTime.Now.Ticks - lastRefresh;
			Debug.Print("Refreshing files...{0}, delta: {1}", currentPath, delta);
			if( delta < 500000000 ) {
				Debug.Print("Aborting double-refresh");
				return;
			}
			lastRefresh = DateTime.Now.Ticks;
			try {
				// try using the current path
				try {
					fileSystemWatcher1.Path = this.currentPath;
				} catch( ArgumentException ) {
					// if that fails
					// and we are looking at some subdirectory on the drive
					if( this.currentDirectory != "" ) {
						// then try looking at the root of the drive
						this.currentPath = this.currentDrive + @":\"; // triggers a refresh (and thus recursion)
					} else { // else we are already looking for the drive root
						// and its still invalid, so the whole drive is invalid, so fall all the way back to c:\
						this.currentPath = @"C:\"; // triggers refresh and recursion
						this.drives.SelectedDrive = "C";
					}
					return;
				}
				savedPaths[this.currentDrive] = this.currentDirectory;

				// repaint the form
				// this.Refresh();

				// build a new tree asynchronously, so that form can still draw itself while this is updating
				RefreshDelegate refresh = new RefreshDelegate(delegate() {
					try {
						listSem.WaitOne();
						fileSystemWatcher1.EnableRaisingEvents = false;
						this.SuspendLayout();
						listFiles.Items.Clear();
						int maxLen = 30;
						listFiles.Columns[0].Text = AbbreviatePath(this.currentPath, maxLen);
						listFiles.ItemChecked += new ItemCheckedEventHandler(listFiles_ItemChecked);

						ListViewItem tmp = listFiles.Items.Add("..");
						tmp.Tag = "..";
						tmp.Group = listFiles.Groups[0];
						string[] dirs = Directory.GetDirectories(currentPath);
						Array.Sort<string>(dirs);
						List<string> ignore = new List<string>(this.config["IgnoreFiles"].Split(','));
						foreach( string dir in dirs ) {
							string fname = Path.GetFileName(dir);
							ListViewItem node = listFiles.Items.Add(fname);
							node.ToolTipText = fname;
							node.Tag = Path.GetFullPath(dir);
							node.ImageIndex = 0;
							node.Group = listFiles.Groups[0];
						}
						string[] files = Directory.GetFiles(currentPath, "*.*");
						Array.Sort<string>(files);
						foreach( string file in files ) {
							// check the ignore list
							string ext = Path.GetExtension(file);
							if( ignore.Contains(ext) )
								continue;
							// check for hidden files
							if( this.config["ShowHidden"] == "False" ) {
								FileInfo inf = new FileInfo(file);
								if( (inf.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ) {
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
					} catch( Exception e ) {
						MessageBox.Show("Exception: " + e.ToString());
					} finally {
						this.ResumeLayout();
						listSem.Release();
					}
				}); // end RefreshDelegate
				// begin the async build above
				IAsyncResult res = listFiles.BeginInvoke(refresh);

			} catch( UnauthorizedAccessException ) {
				MessageBox.Show("Access denied");
				this.currentDirectory = "";
			} catch( DirectoryNotFoundException ) {
				MessageBox.Show("Directory not found: " + currentPath);
				this.currentDirectory = "";
				refreshFiles();
			} catch( IOException e ) {
				MessageBox.Show("IOError :" + e.ToString());
			}
		}

		void listFiles_ItemChecked( object sender, ItemCheckedEventArgs e ) {
			e.Item.Selected = e.Item.Checked;
			Debug.Print("Checked items: " + listFiles.CheckedItems.Count.ToString());
			if( listFiles.CheckedItems.Count == 0
				&& (FileDockForm.ModifierKeys & Keys.Control) == 0 ) {
				listFiles.CheckBoxes = false;
			}
		}


		protected override void OnLoad( EventArgs e ) {
			Debug.Print("FileDockForm.OnLoad()");
			if( dockOnLoad ) {
				// make sure to clean up after any thing that failed to unregister before
				RegisterAppBar();
				UnregisterAppBar();
				RegisterAppBar();
				this.idealSize = new Size(this.Width, SystemInformation.PrimaryMonitorSize.Height);
				this.idealLocation = new Point(0, 0);
				this.RefreshPosition();
			} else if( this.leftChild != null ) {
				this.Size = new Size(this.leftChild.Width, this.leftChild.Height);
				this.Left = this.leftChild.Right;
				this.TopMost = true;
			}
			base.OnLoad(e);
			this.DragDrop += new DragEventHandler(FileDockForm_DragDrop);
			this.DragOver += new DragEventHandler(FileDockForm_DragOver);
			this.Enter += new EventHandler(NoFocusAllowed);
			listFiles.ItemDrag += new ItemDragEventHandler(listFiles_ItemDrag);
			listFiles.MouseMove += new MouseEventHandler(listFiles_MouseMove);
			listFiles.MouseLeave += new EventHandler(listFiles_MouseLeave);
			listFiles.MouseClick += new MouseEventHandler(listFiles_MouseClick);
			listFiles.ItemActivate += new EventHandler(listFiles_ItemActivate);
			listFiles.DragOver += new DragEventHandler(FileDockForm_DragOver);
			listFiles.DragDrop += new DragEventHandler(FileDockForm_DragDrop);
			listFiles.KeyUp += new KeyEventHandler(listFiles_KeyUp);
			listFiles.KeyDown += new KeyEventHandler(listFiles_KeyDown);
			listFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

			// set up the file system events that will trigger refreshes
			fileSystemWatcher1.EnableRaisingEvents = false; // dont start responding yet, until all config is done
			fileSystemWatcher1.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName; // notify about directories as well as files
			fileSystemWatcher1.Filter = ""; // all files
			fileSystemWatcher1.Created += new FileSystemEventHandler(delegate( object source, FileSystemEventArgs ev ) {
				Debug.Print("FileSystemEvent: Created {0}", ev.FullPath);
				refreshFiles();
			});
			fileSystemWatcher1.Deleted += new FileSystemEventHandler(delegate( object source, FileSystemEventArgs ev ) {
				Debug.Print("FileSystemEvent: Deleted {0}", ev.FullPath);
				refreshFiles();
			});
			fileSystemWatcher1.Renamed += new RenamedEventHandler(delegate( object source, RenamedEventArgs ev ) {
				Debug.Print("FileSystemEvent: Renamed {0}", ev.FullPath);
				refreshFiles();
			});

			// set double-buffering options
			this.SetExStyles();

			this.InstanceIndex = this.getInstanceIndex();

			this.configForm = new ConfigForm(this);
			this.config = new Config(this.configForm, "FileDock\\Instance" + this.InstanceIndex);

			// set some default config values
			this.config["SingleClick"] = "True";
			this.config["CurrentDirectory"] = "";
			this.config["CurrentDrive"] = "C";
			this.config["SavedPathsMap"] = "";
			this.config["IgnoreFiles"] = "";
			this.config["VimLocation"] = @"C:\Program Files\Vim\vim73\gvim.exe";

			// load any saved values from the registry, overwriting the defaults
			this.config.LoadFromRegistry();

			// now try to de-serialize the savedPaths mapping
			try {
				MemoryStream mem = new MemoryStream();
				UintDecodeBytes(mem, this.config["SavedPathsMap"]);
				mem.WriteByte(0x11);
				mem.Position = 0;
				savedPaths = (Dictionary<string, string>)(new BinaryFormatter()).Deserialize(mem);
				mem.Close();
				mem.Dispose();
			} catch( SerializationException ) {
				// pass
			} catch( Exception ex ) {
				MessageBox.Show(ex.ToString());
			}

			// then last, and highest priority, would be a path passed on the command line
			bool skipone = true;
			foreach( string arg in Environment.GetCommandLineArgs() ) {
				if( skipone ) {
					skipone = false;
					continue;
				}
				Regex R = new Regex(@"(\w{1}):[/\\](.*)$");
				if( R.IsMatch(arg) ) {
					Match m = R.Match(arg);
					this.config["CurrentDrive"] = m.Groups[1].Value;
					if( File.Exists(m.Groups[0].Value) ) {
						this.config["CurrentDirectory"] = Regex.Replace(m.Groups[2].Value, @"[/\\][^/\\]+$", "");
					} else if( Directory.Exists(m.Groups[0].Value) ) {
						this.config["CurrentDirectory"] = m.Groups[2].Value;
					} else {
						MessageBox.Show("File/Directory does not exist: " + arg);
					}
				} else {
					MessageBox.Show("Commandline argument: " + arg + " was not recognized as a valid path/file");
				}
			}

			this.hoverItem = null;

			// wrap the drive panel
			drives = new DrivePanel(flowLayoutPanel1, "C");
			drives.refresh();
			drives.Changed += delegate( string drive ) {
				currentDrive = drive.Replace(":","");
				if( savedPaths.ContainsKey(currentDrive) )
					currentDirectory = savedPaths[currentDrive];
				refreshFiles();
			};

			refreshFiles();
			this.listFiles.Focus();

			makeActionButton(debugButton, delegate( string[] files ) {
				MessageBox.Show(String.Format("files: {0}", files.Length));
			});
			makeActionButton(vimButton, delegate( string[] files ) {
				foreach( string file in files ) {
					EditFile(file);
				}
			});
			makeActionButton(trashButton, delegate( string[] files ) {
				if( files.Length == 0 )
					return;
				if( MessageBox.Show("Are you sure you want to delete:\n " + String.Join("\n", files), "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes ) {
					foreach( string file in files ) {
						deleteFileOrDirectory(file);
					}
				}
			});
			makeActionButton(cloneButton, delegate( string[] files ) {
			});
			makeActionButton(powerButton, delegate( string[] files ) {
				fileSystemWatcher1.Dispose();
				fileSystemWatcher1 = null;
				this.Close();
			});
			makeActionButton(refreshButton, delegate( string[] files ) {
				drives.refresh();
				refreshFiles();
			});
			makeActionButton(cloneButton, delegate( string[] files ) {
				if( files.Length == 0 )
					this.Clone(currentPath);
				else
					foreach( string file in files )
						if( Directory.Exists(file) )
							this.Clone(file);
			});
		}

		protected void Clone( string file ) {
			// if we already have a right child
			if( this.rightChild != null && !this.rightChild.IsDisposed ) {
				this.rightChild.Clone(file);
			} else {
				this.rightChild = new FileDockForm(this);
				this.rightChild.dockOnLoad = true;
				this.rightChild.Show();
				this.rightChild.currentPath = file;
			}
		}


		protected override void OnClosing( CancelEventArgs e ) {
			if( this.rightChild != null && !this.rightChild.IsDisposed ) {
				this.rightChild.Close();
			}
			MemoryStream mem = new MemoryStream();
			(new BinaryFormatter()).Serialize(mem, savedPaths);
			mem.Position = 0;
			string s = UintEncodeBytes(mem);
			mem.Dispose();
			this.config["SavedPathsMap"] = s;
			this.config.SaveToRegistry();
			if( this.isAppBarRegistered ) {
				this.UnregisterAppBar();
			}
			base.OnClosing(e);
		}

		protected override void OnResize( EventArgs e ) {
			base.OnResize(e);
			moveHandle1.Width = this.Width - 6;
			listFiles.Height = this.Height - (listFiles.Top + 32);
			Debug.Print("On Resize: {0}x{1}", this.Width, this.Height);
			Debug.Print("List Files: {0}x{1}", listFiles.Width, listFiles.Height);
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
		private string UintEncodeBytes( MemoryStream input ) {
			StringBuilder s = new StringBuilder();
			int b;
			while( (b = input.ReadByte()) != -1 ) {
				s.AppendFormat("{0:X2}", (byte)b);
			}
			return s.ToString();
		}
		private void UintDecodeBytes( MemoryStream output, string src ) {
			StringReader r = new StringReader(src);
			char[] buffer = new char[2];
			while( r.Read(buffer, 0, 2) == 2 ) {
				byte d = Byte.Parse("" + buffer[0] + buffer[1], System.Globalization.NumberStyles.AllowHexSpecifier);
				output.WriteByte(d);
			}
		}
		#endregion

		void listFiles_KeyUp( object sender, KeyEventArgs e ) {
			Debug.Print("keyUp: " + e.KeyCode.ToString());
			switch( e.KeyCode ) {
				case Keys.F5:
					Refresh();
					break;
				case Keys.ControlKey:
					if( listFiles.CheckedIndices.Count == 0 ) {
						listFiles.CheckBoxes = false;
					}
					break;
			}

		}

		void listFiles_KeyDown( object sender, KeyEventArgs e ) {
			switch( e.KeyCode ) {
				case Keys.ControlKey:
					listFiles.CheckBoxes = true;
					break;
			}
		}

		private void listFiles_MouseClick( object sender, MouseEventArgs e ) {
			if( (FileDockForm.ModifierKeys & Keys.Control) == 0
					&& (FileDockForm.ModifierKeys & Keys.Shift) == 0
					) {
				if( hoverItem != null ) {
					if( listFiles.CheckBoxes && listFiles.CheckedItems.Contains(hoverItem) )
						return;
					else
						activateFileFolder(hoverItem);
				}
			}
		}

		public delegate void HoverItemChanged();
		private ListViewItem prevHoverItem;
		private ListViewItem hoverItem;
		private ToolTip hoverTip;
		private void listFiles_MouseMove( object sender, MouseEventArgs e ) {
			ListViewItem newHoverItem = listFiles.GetItemAt(e.X, e.Y);
			if( newHoverItem != null ) {
				if( prevHoverItem == newHoverItem ) {
					return;
				}
				prevHoverItem = hoverItem;
				hoverItem = newHoverItem;
				Debug.Print("Forcing focus on listFiles");
				SendMessage(this.Handle, 0x086, 1, 0); // force focus
				if( hoverTip != null ) {
					hoverTip.Hide(listFiles);
					hoverTip.Dispose();
					hoverTip = null;
				}
				hoverTip = new ToolTip(this.components);
				Point hoverPos = this.PointToClient(e.Location);
				hoverPos = new Point(hoverPos.X + this.Left + 35, hoverPos.Y);
				string hoverText = hoverItem.Text;
				string fname = (string)hoverItem.Tag;
				if( File.Exists(fname) ) {
					FileInfo f = new FileInfo(fname);
					double kb = f.Length / 1024.0;
					hoverText += String.Format("\n- {0:0,0.00} kb", kb);
					hoverTip.Show(hoverText, listFiles, hoverPos);
				}
			}
			if( prevHoverItem != null ) {
				prevHoverItem.BackColor = Color.White;
				prevHoverItem.ForeColor = Color.Black;
				prevHoverItem.Font = new Font(prevHoverItem.Font, 0);
			}
			if( hoverItem != null ) {
				hoverItem.BackColor = Color.FromArgb(255, 255, 180);
				hoverItem.ForeColor = Color.DarkBlue;
				hoverItem.Font = new Font(hoverItem.Font, FontStyle.Bold | FontStyle.Underline);
			}
		}

		void listFiles_MouseLeave( object sender, EventArgs e ) {
			Debug.Print("closing hoverTip on mouse leave");
			if( hoverTip != null ) {
				hoverTip.Hide(listFiles);
				hoverTip.Dispose();
				hoverTip = null;
			}
		}

		// when an item is first picked up:
		private void listFiles_ItemDrag( object sender, ItemDragEventArgs e ) {
			ListViewItem node = (ListViewItem)e.Item;
			if( node != null ) {
				string[] s = new string[] { (String)node.Tag };
				// if multiple files are selected
				if( listFiles.SelectedItems.Count > 1 ) {
					List<string> files = new List<string>();
					foreach( ListViewItem item in listFiles.SelectedItems ) {
						files.Add((string)(item.Tag));
					}
					// pick up all of them for the drag
					s = files.ToArray();
				}
				DataObject data = new DataObject(DataFormats.FileDrop, s);
				// begin the drag
				listFiles.DoDragDrop(data, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link | DragDropEffects.All | DragDropEffects.None);
			}
		}

		// when an item is being floated over any part of the form
		private void FileDockForm_DragOver( object sender, DragEventArgs e ) {
			bool CtrlPressed = (FileDockForm.ModifierKeys & Keys.Control) == Keys.Control;
			bool AltPressed = (FileDockForm.ModifierKeys & Keys.Alt) == Keys.Alt;
			if( CtrlPressed
				&& (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move ) {
				e.Effect = DragDropEffects.Move;
			} else if( AltPressed
				&& (e.AllowedEffect & DragDropEffects.All) == DragDropEffects.All ) {
				e.Effect = DragDropEffects.All;
			} else if( (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy ) {
				e.Effect = DragDropEffects.Copy;
			} else {
				e.Effect = DragDropEffects.None;
			}
		}

		// when an item is actually dropped on some part of the form
		private void FileDockForm_DragDrop( object sender, DragEventArgs e ) {
			if( e.Effect == DragDropEffects.All ) {
				this.Focus();
				// show a context menu with Copy Move options
				Control target = (Control)sender;
				this.contextMenu1.Show(this, 0, 0);
				// when they make a choice, call this function again with the proper effect
				this.contextMenu1.ItemClicked += new ToolStripItemClickedEventHandler(delegate( object _sender, ToolStripItemClickedEventArgs _e ) {
					if( _e.ClickedItem.Text == "Copy" ) {
						e.Effect = DragDropEffects.Copy;
					} else if( _e.ClickedItem.Text == "Move" ) {
						e.Effect = DragDropEffects.Move;
					}
					FileDockForm_DragDrop(sender, e);
				});
				// do nothing right now, wait for a menu choice
				return;
			} else {
				bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
				bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
				if( isFileDrop ) {

					// determine what that the intended target of the drop is
					Point clt = listFiles.PointToClient(new Point(e.X, e.Y));
					ListViewItem target = listFiles.GetItemAt(clt.X, clt.Y);
					string targetDir = this.currentPath;
					// if it was dropped on a folder node directly
					if( target != null && target.ImageIndex == 0 ) {
						// then target that node directly
						targetDir = target.Name;
					}

					foreach( string srcFile in (string[])e.Data.GetData(DataFormats.FileDrop) ) {
						Debug.Print("Got dropped file: " + srcFile + " on target: " + targetDir);
						// if the user did a tiny mis-drag, of an item onto itself, count it as an activate instead
						if( targetDir == srcFile ) {
							activateFileFolder(target);
							return;
						}
						// if we they are dragging a directory to us
						if( Directory.Exists(srcFile) ) {
							// build the new directory name
							string[] tmp = srcFile.Split(Path.DirectorySeparatorChar);
							string dirName = tmp[tmp.Length - 1];
							string newTargetDir = Path.Combine(targetDir, dirName);
							if( e.Effect == DragDropEffects.Copy ) {
								// copy *.* from the source to the new directory
								using( new ActionHelper(this, sender, "Copy") ) {
									try {
										copyDirectory(srcFile, newTargetDir);
									} catch( UnauthorizedAccessException ) {
										MessageBox.Show("Not Allowed.");
									}
								}
							} else if( e.Effect == DragDropEffects.Move ) {
								// move *.* from the source dir to the target dir
								using( new ActionHelper(this, sender, "Move") ) {
									try {
										Directory.Move(srcFile, newTargetDir);
									} catch( UnauthorizedAccessException ) {
										MessageBox.Show("Not Allowed.");
									}
								}
							} else {
								MessageBox.Show("Unknown directory effect: " + e.ToString());
							}
						} else if( File.Exists(srcFile) ) {
							string newFile = Path.Combine(targetDir, Path.GetFileName(srcFile));
							if( newFile != srcFile ) {
								if( e.Effect == DragDropEffects.Copy ) {
									using( new ActionHelper(this, sender, "Copy") ) {
										try {
											File.Copy(Path.GetFullPath(srcFile), newFile);
										} catch( UnauthorizedAccessException ) {
											MessageBox.Show("Not Allowed.");
										} catch( IOException ex ) {
											MessageBox.Show("IOException: " + ex.ToString());
										}
									}
								} else if( e.Effect == DragDropEffects.Move ) {
									using( new ActionHelper(this, sender, "Move") ) {
										Debug.Print("Moving " + srcFile + " " + newFile);
										try {
											File.Move(Path.GetFullPath(srcFile), newFile);
										} catch( UnauthorizedAccessException ) {
											MessageBox.Show("Not Allowed.");
										} catch( IOException ex ) {
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
				} else if( isStringDrop ) {
					Debug.Print("Got dropped text: " + e.Data.GetData(DataFormats.Text));
				}
			}
		}

		// a helper, since for some reason this is missing from the windows apis
		private void copyDirectory( string Src, string Dst ) {
			if( Src.Equals(Dst) )
				return;
			String[] Files;
			if( Dst[Dst.Length - 1] != Path.DirectorySeparatorChar )
				Dst += Path.DirectorySeparatorChar;
			if( !Directory.Exists(Dst) )
				Directory.CreateDirectory(Dst);
			Files = Directory.GetFileSystemEntries(Src);
			foreach( string Element in Files ) {
				if( Directory.Exists(Element) )
					copyDirectory(Element, Dst + Path.GetFileName(Element));
				else
					File.Copy(Element, Dst + Path.GetFileName(Element), true);
			}
		}

		public void closeAllRightChildren() {
			if( this.rightChild != null ) {
				this.rightChild.closeAllRightChildren();
				this.rightChild.Close();
			}
		}

		// when the user has indicated they want to launch this file or open this directory
		private void activateFileFolder( ListViewItem node ) {
			string new_dir = (String)node.Tag;
			if( new_dir == "." )
				return;
			if( new_dir == ".." ) {
				DirectoryInfo parent = Directory.GetParent(this.currentPath);
				if( parent != null )
					new_dir = parent.ToString();
				else
					return;
			}
			if( Directory.Exists(new_dir) ) {
				this.currentPath = new_dir;
			} else if( File.Exists(new_dir) ) {
				startFileWithDefaultHandler(new_dir);
			}
		}

		// launch a file using the 'start' command-line tool to do the registry/mime lookup
		private void startFileWithDefaultHandler( string filename ) {
			execCmd(@"C:\WINDOWS\System32\cmd.exe", "/c start \"start\" \"" + (filename) + "\"", null, true);
		}

		// execute something in windows
		private void execCmd( string cmd, string arguments, string workingDirectory, bool hidden ) {
			ProcessStartInfo si = new ProcessStartInfo(cmd);
			si.Arguments = arguments;
			if( hidden ) {
				si.CreateNoWindow = true;
				si.WindowStyle = ProcessWindowStyle.Hidden;
			}
			if( workingDirectory != null ) {
				si.WorkingDirectory = workingDirectory;
			} else {
				si.WorkingDirectory = this.currentPath;
			}
			Debug.Print("Exec: " + cmd + " " + arguments);
			System.Diagnostics.Process.Start(si);
			Debug.Print("Exec returned");
		}

		
		// a helper for deleting things safely
		private void deleteFileOrDirectory( string path ) {
			MessageBox.Show("Disabled.");
			return;
			/*
			Debug.Print("delete: " + path);
			if( Directory.Exists(path) ) {
				Directory.Delete(path, true);
				removeListViewItemUsingAnimation(path);
			} else if( File.Exists(path) ) {
				File.Delete(path);
				removeListViewItemUsingAnimation(path);
			} else {
				MessageBox.Show("Could not find file/directory: " + path);
			}
			*/
		}

		private void removeListViewItemUsingAnimation( string path ) {
			foreach( ListViewItem item in listFiles.Items ) {
				if( (string)(item.Tag) == path ) {
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
		private void config_Click( object sender, EventArgs e ) {
			if( configForm.Visible ) {
				configForm.Focus();
			} else if( !configForm.IsDisposed ) {
				configForm.Show(this);
				configForm.Focus();
			} else {
				configForm = new ConfigForm(this);
				configForm.Show(this);
				configForm.Focus();
			}
		}

		public override void Refresh() {
			base.Refresh();
			drives.refresh();
			refreshFiles();
		}

		public void refreshAllInstances() {
			Refresh();
			FileDockForm cursor = this.leftChild;
			// walk left refreshing
			while( cursor != null && !cursor.IsDisposed ) {
				cursor.Refresh();
				cursor = cursor.leftChild;
			}
			// then walk right
			cursor = this.rightChild;
			while( cursor != null && !cursor.IsDisposed ) {
				cursor.Refresh();
				cursor = cursor.rightChild;
			}
		}

		private delegate void FileHandler( string[] files );

		private void makeActionButton( Control button, FileHandler handler ) {
			button.Click += delegate( object sender, EventArgs e ) {
				List<string> files = new List<string>();
				foreach( ListViewItem item in listFiles.SelectedItems ) {
					files.Add((string)item.Tag);
				}
				handler(files.ToArray());
			};
			button.GotFocus += NoFocusAllowed;
			button.DragDrop += delegate (object sender, DragEventArgs e) {
				if( e.Effect == DragDropEffects.All ) {
					bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
					bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
					if( isFileDrop ) {
						handler( (string[])e.Data.GetData(DataFormats.FileDrop) );
					} else if( isStringDrop ) {
						string file = (string)e.Data.GetData(DataFormats.Text);
						string[] args = new string[1] { file };
						handler(args);
					}
				}
			};
			button.DragEnter += delegate( object sender, DragEventArgs e ) {
				bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
				bool isStringDrop = e.Data.GetDataPresent(DataFormats.Text);
				if( isFileDrop || isStringDrop ) {
					e.Effect = DragDropEffects.All;
				} else {
					e.Effect = DragDropEffects.None;
				}
			};
		}
		private void tmp() {
			makeActionButton(
				vimButton,
				delegate( string[] files ) {
				}
			);
		}

		private void listFiles_ItemActivate( object sender, EventArgs e ) {
			foreach( ListViewItem item in listFiles.SelectedItems ) {
				activateFileFolder(item);
			}
		}

		private void EditFile( string file ) {
			try {
				execCmd(this.config["VimLocation"], "--remote-tab-silent \"" + file + "\"", this.currentPath, false);
			} catch( Exception ex ) {
				MessageBox.Show("Failed to launch VIM: " + ex.Message);
			}
		}

		public int InstanceIndex;
		// returns what position we are at in the leftChild/rightChild linked list
		private int getInstanceIndex() {
			if( leftChild == null ) {
				return 0;
			} else {
				return this.leftChild.getInstanceIndex() + 1;
			}
		}

		private void NoFocusAllowed( object sender, EventArgs e ) {
			Debug.Print("Forcing focus on listFiles...");
			this.listFiles.Focus();
		}

		private string AbbreviatePath( string path, Font font, float maxWidth ) {
			string text = path;
			int maxLen = text.Length;
			while( TextRenderer.MeasureText(text, font).Width > maxWidth ) {
				maxLen -= 1;
				text = AbbreviatePath(path, maxLen);
			}
			return text;
		}

		private string AbbreviatePath( string path, int maxLen ) {
			string ret = "";
			if( path.Length > maxLen ) {
				string[] elems = path.Split('\\');

				maxLen -= 10; // reserve a minimum 10 chars for the lead-in we will add later
				int loadIndex = 1;
				string last = elems[elems.Length - loadIndex];
				while( last.Length < maxLen && (loadIndex < (elems.Length - 2)) ) {
					last = elems[elems.Length - (++loadIndex)] + "\\" + last;
				}
				if( last.Length > maxLen ) {
					int halfMax = maxLen >> 1; // divide by 2 (we will display the first and last equal chunk of the last path element)
					ret += "\\" + last.Substring(0, halfMax) + "...";
					ret += last.Substring(last.Length - halfMax, halfMax);
				} else {
					ret += "\\" + last;
				}
				maxLen += 8; // put the space back for the lead-in
				if( elems[1].Length <= Math.Abs(maxLen - ret.Length) ) {
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
		public static extern int SendMessage( IntPtr handle, int messg, int wparam, int lparam );

		// Sets Double_Buffering and BorderSelect style
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
		public ActionHelper( FileDockForm parent, object sender, string type ) {
			parent.Cursor = Cursors.WaitCursor;
			if( type == "Copy" ) {
				//f.StatusText = "Copying...";
				parent.Update();
			} else if( type == "Move" ) {
				//f.StatusText = "Moving...";
				parent.Update();
			}
			this.form = parent;
			this.type = type;
			this.sender = sender;
		}
		public void Dispose() { this.Dispose(true); }
		public void Dispose( bool disposing ) {
			if( this.type == "Copy" ) {
				//form.StatusText = "Copied";
			} else if( this.type == "Move" ) {
				//form.StatusText = "Moved";
			}
			this.form.Cursor = Cursors.Default;
			form.refreshAllInstances();
			this.form = null;
			this.type = null;
			this.sender = null;
		}
	}

}

