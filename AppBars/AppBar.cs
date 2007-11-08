using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileDock
{
	/// <summary>
	/// A Windows Forms Application Desktop Toolbar
	/// </summary>
	public class AppBar : Form
	{
		private System.ComponentModel.Container components = null;

		private bool autoRegisterOnLoad;
		private bool isAppBarRegistered;
		private AppBarDockStyle appBarDock;
		private Size idealSize;
		private int appBarCallback;//our callback message value for the wndproc

		private const int WS_CAPTION = 0x00C00000;//used for hiding the border
		private const int WS_BORDER = 0x00800000;//used for hiding the border

		/// <summary>
		/// Standard Constructor.  Initializes local variables.
		/// </summary>		
		public AppBar()
		{

			appBarCallback = NativeMethods.RegisterWindowMessage("Windows Forms AppBar");
			isAppBarRegistered = false;
			appBarDock = AppBarDockStyle.None;
			idealSize = new Size(100,100);
			this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
		}

		/// <summary>
		/// Gets/Sets the AppBar's screen-docking property.
		/// </summary>
		[
		Browsable(false),
		Category("AppBar Behavior"),
		DefaultValue(AppBarDockStyle.ScreenTop),
		Description("Designates which side of the screen the AppBar will dock to.")
		]
		public AppBarDockStyle AppBarDock 
		{
			get 
			{
				return appBarDock;
			}
			set 
			{
				appBarDock = value;
				UpdateDockedAppBarPosition(appBarDock);
			}
		}

		/// <summary>
		/// If true, the AppBar will register itself as it loads.
		/// </summary>>
		[
		Category("AppBar Behavior"),
		DefaultValue(false),
		Description("Registers the AppBar as it loads.")
		]
		public bool AutoRegisterOnLoad
		{
			get 
			{
				return autoRegisterOnLoad;
			}
			set 
			{
				autoRegisterOnLoad = value;
			}
		}

		
		/// <summary>
		/// Hide the border of our AppBar Form by turning off
		/// these style bits
		/// </summary>>
		protected override CreateParams CreateParams 
		{
			get 
			{
				CreateParams cp = base.CreateParams;
				cp.Style &= (~WS_CAPTION);
				cp.Style &= (~WS_BORDER);
				return cp;
			}
		}
		

		/// <summary>
		/// The "ideal size" of the AppBar.  If docked to the top or 
		/// bottom of the screen the height is honored.  If docked to
		/// the left or right of the screen the width is honored.
		/// </summary>
		[
		Category("AppBar Behavior"),
		Description("The ideal size of the AppBar.  If docked top or bottom, width is ignored.  If docked left or right, height is ignored.")
		]
		public Size IdealSize 
		{
			get 
			{
				return idealSize;
			}
			set 
			{
				idealSize = value;
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Returns true if the AppBar has been properly registered.
		/// </summary>
		public bool IsAppBarRegistered() 
		{
			return isAppBarRegistered;
		}

		/// <summary>
		/// Updates the AppBar's position with respect to the AppBarDock property.
		/// </summary>
		public void RefreshPosition() 
		{
			UpdateDockedAppBarPosition(AppBarDock);
		}

		/// <summary>
		/// Registers the AppBar with the OS.
		/// </summary>
		public bool RegisterAppBar() 
		{
			bool retVal = NativeMethods.RegisterAppBar(this.Handle, this.appBarCallback);
			this.isAppBarRegistered = retVal;
			return retVal;
		}

		/// <summary>
		/// Unregisters the AppBar with the OS.
		/// </summary>
		public void UnregisterAppBar() 
		{
			NativeMethods.UnregisterAppBar(this.Handle);
			this.isAppBarRegistered = false;
		}

		/// <summary>
		/// Updates the AppBar's position
		/// </summary>
		private void UpdateDockedAppBarPosition(AppBarDockStyle dockStyle) 
		{
			int edge = 0;
			switch (dockStyle) 
			{
				case AppBarDockStyle.None: return;
				case AppBarDockStyle.ScreenLeft: edge = 0;break;
				case AppBarDockStyle.ScreenTop: edge = 1;break;
				case AppBarDockStyle.ScreenRight: edge = 2;break;
				default : edge = 3;break;

			}
			NativeMethods.DockAppBar(this.Handle, edge , IdealSize);
		}

		/// <summary>
		/// The WndProc is overriden to respond to the messages
		/// the OS sends the registered AppBar.
		/// </summary>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == this.appBarCallback) 
			{
				switch ((int)m.WParam) 
				{
					case NativeMethods.ABN_FULLSCREENAPP:
						//Debug.Print("FullScreen App: "+m.ToString());
						break;
					case NativeMethods.ABN_POSCHANGED:
						//Debug.Print("POS changed: " + m.ToString());
						break;
					case NativeMethods.ABN_STATECHANGE: /*TODO: respond to StateChanged message */;
						//Debug.Print("State changed: " + m.ToString());
						break;
					case NativeMethods.ABN_WINDOWARRANGE:
						//Debug.Print("Window arrange: " + m.ToString());
						break;
				}
			}
			base.WndProc(ref m);
		}

		/// <summary>
		/// When closing, make sure the AppBar is unregistered.
		/// </summary>
		protected override void OnClosing(CancelEventArgs e)
		{
			UnregisterAppBar();
			base.OnClosing(e);
		}

		/// <summary>
		/// When loading, register the AppBar if the property is set.
		/// </summary>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (AutoRegisterOnLoad) 
			{
				RegisterAppBar();
			}
		}

		private void InitializeComponent() {
			this.SuspendLayout();
			// 
			// AppBar
			// 
			this.ClientSize = new System.Drawing.Size(286, 259);
			this.Name = "AppBar";
			this.ResumeLayout(false);

		}

		
	}
}
