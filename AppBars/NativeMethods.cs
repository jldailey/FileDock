using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FileDock
{
	/// <summary>
	/// NativeMethods needed for AppBar registration and interop.
	/// </summary>
	internal class NativeMethods
	{
		/// <summary>
		/// Sizes and Moves the AppBar to a screen edge.
		/// </summary>
		internal static void DockAppBar(IntPtr hWnd, int edge, Size idealSize) 
		{
			APP_BAR_DATA abd = new APP_BAR_DATA();
			abd.cbSize = Marshal.SizeOf(abd);
			abd.hWnd = hWnd;
			abd.uEdge = edge;

			if (edge == ABE_LEFT || edge == ABE_RIGHT) 
			{
				abd.rc.top = 0;
				abd.rc.bottom = SystemInformation.PrimaryMonitorSize.Height;
				if (edge == ABE_LEFT) {
					abd.rc.right = idealSize.Width;
				}	else {
					abd.rc.right = SystemInformation.PrimaryMonitorSize.Width;
					abd.rc.left = abd.rc.right - idealSize.Width;
				}
			} else {
				abd.rc.left = 0;
				abd.rc.right = SystemInformation.PrimaryMonitorSize.Width;
				if (edge == ABE_TOP) {
					abd.rc.bottom = idealSize.Height;
				}	else {
					abd.rc.bottom = SystemInformation.PrimaryMonitorSize.Height;
					abd.rc.top = abd.rc.bottom - idealSize.Height;
				}
			}

			// Query the system for an approved size and position. 
			SHAppBarMessage(ABM_QUERYPOS, ref abd); 

			// Adjust the rectangle, depending on the edge to which the 
			// appbar is anchored. 
			switch (edge) { 
				case ABE_LEFT: 
					abd.rc.right = abd.rc.left + idealSize.Width;
					break; 
				case ABE_RIGHT: 
					abd.rc.left= abd.rc.right - idealSize.Width;
					break; 
				case ABE_TOP: 
					abd.rc.bottom = abd.rc.top + idealSize.Height;
					break; 
				case ABE_BOTTOM: 
					abd.rc.top = abd.rc.bottom - idealSize.Height;
					break; 
			}

			// Pass the final bounding rectangle to the system. 
			SHAppBarMessage(ABM_SETPOS, ref abd); 

			// Move and size the appbar so that it conforms to the 
			// bounding rectangle passed to the system. 
			MoveWindow(abd.hWnd, abd.rc.left, abd.rc.top, abd.rc.right - abd.rc.left, abd.rc.bottom - abd.rc.top, true); 
		}

		/// <summary>
		/// Registers the AppBar and assigns the appropriate callback message value.
		/// </summary>
		internal static bool RegisterAppBar(IntPtr hWnd, int uCallbackMessage) 
		{
			APP_BAR_DATA abd = new APP_BAR_DATA();
			abd.cbSize = Marshal.SizeOf(abd);
			abd.hWnd = hWnd;
			abd.uCallbackMessage = uCallbackMessage;
			
			int retVal = SHAppBarMessage(ABM_NEW, ref abd);
			if (retVal == 0) 
			{
				//registration failed
				return false;
			}
			return true;
		}

		/// <summary>
		/// Unregisters the AppBar.
		/// </summary>
		internal static void UnregisterAppBar(IntPtr hWnd) 
		{
			APP_BAR_DATA abd = new APP_BAR_DATA();
			abd.cbSize = Marshal.SizeOf(abd);
			abd.hWnd = hWnd;
			SHAppBarMessage(ABM_REMOVE, ref abd);
		}

		internal static void ActivateAppBar(IntPtr hWnd)
		{
			APP_BAR_DATA abd = new APP_BAR_DATA();
			abd.cbSize = Marshal.SizeOf(abd);
			abd.hWnd = hWnd;
			SHAppBarMessage(ABM_ACTIVATE, ref abd);
		}

		internal static void WindowPosChanged(IntPtr hWnd) {
			APP_BAR_DATA abd = new APP_BAR_DATA();
			abd.cbSize = Marshal.SizeOf(abd);
			abd.hWnd = hWnd;
			SHAppBarMessage(ABM_WINDOWPOSCHANGED, ref abd);
		}
		
		private const int ABM_NEW = 0x00;
		private const int ABM_REMOVE = 0x01;
		private const int ABM_QUERYPOS = 0x02;
		private const int ABM_SETPOS = 0x03;
		private const int ABM_ACTIVATE = 0x03;
		private const int ABM_SETAUTOHIDEBAR = 0x08;
		private const int ABM_WINDOWPOSCHANGED = 0x09;
		private const int ABM_SETSTATE = 0x0000000a;
		private const int ABE_LEFT = 0;
		private const int ABE_TOP = 1;
		private const int ABE_RIGHT = 2;
		private const int ABE_BOTTOM = 3;
		private const int ABS_AUTOHIDE = 0x01;
		private const int ABS_ALWAYSONTOP = 0x02;

		// these are put in the wparam of callback messages
		internal const int ABN_STATECHANGE   = 0x00;
		internal const int ABN_POSCHANGED    = 0x01;
		internal const int ABN_FULLSCREENAPP = 0x02;
		internal const int ABN_WINDOWARRANGE = 0x03; // lParam == TRUE means hide;

		internal const int WM_ACTIVATE = 0x6;
		public enum WindowsMessages {
			WM_ACTIVATE = 0x6,
			WM_ACTIVATEAPP = 0x1C,
			WM_AFXFIRST = 0x360,
			WM_AFXLAST = 0x37F,
			WM_APP = 0x8000,
			WM_ASKCBFORMATNAME = 0x30C,
			WM_CANCELJOURNAL = 0x4B,
			WM_CANCELMODE = 0x1F,
			WM_CAPTURECHANGED = 0x215,
			WM_CHANGECBCHAIN = 0x30D,
			WM_CHAR = 0x102,
			WM_CHARTOITEM = 0x2F,
			WM_CHILDACTIVATE = 0x22,
			WM_CLEAR = 0x303,
			WM_CLOSE = 0x10,
			WM_COMMAND = 0x111,
			WM_COMPACTING = 0x41,
			WM_COMPAREITEM = 0x39,
			WM_CONTEXTMENU = 0x7B,
			WM_COPY = 0x301,
			WM_COPYDATA = 0x4A,
			WM_CREATE = 0x1,
			WM_CTLCOLORBTN = 0x135,
			WM_CTLCOLORDLG = 0x136,
			WM_CTLCOLOREDIT = 0x133,
			WM_CTLCOLORLISTBOX = 0x134,
			WM_CTLCOLORMSGBOX = 0x132,
			WM_CTLCOLORSCROLLBAR = 0x137,
			WM_CTLCOLORSTATIC = 0x138,
			WM_CUT = 0x300,
			WM_DEADCHAR = 0x103,
			WM_DELETEITEM = 0x2D,
			WM_DESTROY = 0x2,
			WM_DESTROYCLIPBOARD = 0x307,
			WM_DEVICECHANGE = 0x219,
			WM_DEVMODECHANGE = 0x1B,
			WM_DISPLAYCHANGE = 0x7E,
			WM_DRAWCLIPBOARD = 0x308,
			WM_DRAWITEM = 0x2B,
			WM_DROPFILES = 0x233,
			WM_ENABLE = 0xA,
			WM_ENDSESSION = 0x16,
			WM_ENTERIDLE = 0x121,
			WM_ENTERMENULOOP = 0x211,
			WM_ENTERSIZEMOVE = 0x231,
			WM_ERASEBKGND = 0x14,
			WM_EXITMENULOOP = 0x212,
			WM_EXITSIZEMOVE = 0x232,
			WM_FONTCHANGE = 0x1D,
			WM_GETDLGCODE = 0x87,
			WM_GETFONT = 0x31,
			WM_GETHOTKEY = 0x33,
			WM_GETICON = 0x7F,
			WM_GETMINMAXINFO = 0x24,
			WM_GETOBJECT = 0x3D,
			WM_GETSYSMENU = 0x313,
			WM_GETTEXT = 0xD,
			WM_GETTEXTLENGTH = 0xE,
			WM_HANDHELDFIRST = 0x358,
			WM_HANDHELDLAST = 0x35F,
			WM_HELP = 0x53,
			WM_HOTKEY = 0x312,
			WM_HSCROLL = 0x114,
			WM_HSCROLLCLIPBOARD = 0x30E,
			WM_ICONERASEBKGND = 0x27,
			WM_IME_CHAR = 0x286,
			WM_IME_COMPOSITION = 0x10F,
			WM_IME_COMPOSITIONFULL = 0x284,
			WM_IME_CONTROL = 0x283,
			WM_IME_ENDCOMPOSITION = 0x10E,
			WM_IME_KEYDOWN = 0x290,
			WM_IME_KEYLAST = 0x10F,
			WM_IME_KEYUP = 0x291,
			WM_IME_NOTIFY = 0x282,
			WM_IME_REQUEST = 0x288,
			WM_IME_SELECT = 0x285,
			WM_IME_SETCONTEXT = 0x281,
			WM_IME_STARTCOMPOSITION = 0x10D,
			WM_INITDIALOG = 0x110,
			WM_INITMENU = 0x116,
			WM_INITMENUPOPUP = 0x117,
			WM_INPUTLANGCHANGE = 0x51,
			WM_INPUTLANGCHANGEREQUEST = 0x50,
			WM_KEYDOWN = 0x100,
			WM_KEYFIRST = 0x100,
			WM_KEYLAST = 0x108,
			WM_KEYUP = 0x101,
			WM_KILLFOCUS = 0x8,
			WM_LBUTTONDBLCLK = 0x203,
			WM_LBUTTONDOWN = 0x201,
			WM_LBUTTONUP = 0x202,
			WM_MBUTTONDBLCLK = 0x209,
			WM_MBUTTONDOWN = 0x207,
			WM_MBUTTONUP = 0x208,
			WM_MDIACTIVATE = 0x222,
			WM_MDICASCADE = 0x227,
			WM_MDICREATE = 0x220,
			WM_MDIDESTROY = 0x221,
			WM_MDIGETACTIVE = 0x229,
			WM_MDIICONARRANGE = 0x228,
			WM_MDIMAXIMIZE = 0x225,
			WM_MDINEXT = 0x224,
			WM_MDIREFRESHMENU = 0x234,
			WM_MDIRESTORE = 0x223,
			WM_MDISETMENU = 0x230,
			WM_MDITILE = 0x226,
			WM_MEASUREITEM = 0x2C,
			WM_MENUCHAR = 0x120,
			WM_MENUCOMMAND = 0x126,
			WM_MENUDRAG = 0x123,
			WM_MENUGETOBJECT = 0x124,
			WM_MENURBUTTONUP = 0x122,
			WM_MENUSELECT = 0x11F,
			WM_MOUSEACTIVATE = 0x21,
			WM_MOUSEFIRST = 0x200,
			WM_MOUSEHOVER = 0x2A1,
			WM_MOUSELAST = 0x20A,
			WM_MOUSELEAVE = 0x2A3,
			WM_MOUSEMOVE = 0x200,
			WM_MOUSEWHEEL = 0x20A,
			WM_MOVE = 0x3,
			WM_MOVING = 0x216,
			WM_NCACTIVATE = 0x86,
			WM_NCCALCSIZE = 0x83,
			WM_NCCREATE = 0x81,
			WM_NCDESTROY = 0x82,
			WM_NCHITTEST = 0x84,
			WM_NCLBUTTONDBLCLK = 0xA3,
			WM_NCLBUTTONDOWN = 0xA1,
			WM_NCLBUTTONUP = 0xA2,
			WM_NCMBUTTONDBLCLK = 0xA9,
			WM_NCMBUTTONDOWN = 0xA7,
			WM_NCMBUTTONUP = 0xA8,
			WM_NCMOUSEHOVER = 0x2A0,
			WM_NCMOUSELEAVE = 0x2A2,
			WM_NCMOUSEMOVE = 0xA0,
			WM_NCPAINT = 0x85,
			WM_NCRBUTTONDBLCLK = 0xA6,
			WM_NCRBUTTONDOWN = 0xA4,
			WM_NCRBUTTONUP = 0xA5,
			WM_NEXTDLGCTL = 0x28,
			WM_NEXTMENU = 0x213,
			WM_NOTIFY = 0x4E,
			WM_NOTIFYFORMAT = 0x55,
			WM_NULL = 0x0,
			WM_PAINT = 0xF,
			WM_PAINTCLIPBOARD = 0x309,
			WM_PAINTICON = 0x26,
			WM_PALETTECHANGED = 0x311,
			WM_PALETTEISCHANGING = 0x310,
			WM_PARENTNOTIFY = 0x210,
			WM_PASTE = 0x302,
			WM_PENWINFIRST = 0x380,
			WM_PENWINLAST = 0x38F,
			WM_POWER = 0x48,
			WM_PRINT = 0x317,
			WM_PRINTCLIENT = 0x318,
			WM_QUERYDRAGICON = 0x37,
			WM_QUERYENDSESSION = 0x11,
			WM_QUERYNEWPALETTE = 0x30F,
			WM_QUERYOPEN = 0x13,
			WM_QUERYUISTATE = 0x129,
			WM_QUEUESYNC = 0x23,
			WM_QUIT = 0x12,
			WM_RBUTTONDBLCLK = 0x206,
			WM_RBUTTONDOWN = 0x204,
			WM_RBUTTONUP = 0x205,
			WM_RENDERALLFORMATS = 0x306,
			WM_RENDERFORMAT = 0x305,
			WM_SETCURSOR = 0x20,
			WM_SETFOCUS = 0x7,
			WM_SETFONT = 0x30,
			WM_SETHOTKEY = 0x32,
			WM_SETICON = 0x80,
			WM_SETREDRAW = 0xB,
			WM_SETTEXT = 0xC,
			WM_SETTINGCHANGE = 0x1A,
			WM_SHOWWINDOW = 0x18,
			WM_SIZE = 0x5,
			WM_SIZECLIPBOARD = 0x30B,
			WM_SIZING = 0x214,
			WM_SPOOLERSTATUS = 0x2A,
			WM_STYLECHANGED = 0x7D,
			WM_STYLECHANGING = 0x7C,
			WM_SYNCPAINT = 0x88,
			WM_SYSCHAR = 0x106,
			WM_SYSCOLORCHANGE = 0x15,
			WM_SYSCOMMAND = 0x112,
			WM_SYSDEADCHAR = 0x107,
			WM_SYSKEYDOWN = 0x104,
			WM_SYSKEYUP = 0x105,
			WM_SYSTIMER = 0x118,  // undocumented, see http://support.microsoft.com/?id=108938
			WM_TCARD = 0x52,
			WM_TIMECHANGE = 0x1E,
			WM_TIMER = 0x113,
			WM_UNDO = 0x304,
			WM_UNINITMENUPOPUP = 0x125,
			WM_USER = 0x400,
			WM_USERCHANGED = 0x54,
			WM_VKEYTOITEM = 0x2E,
			WM_VSCROLL = 0x115,
			WM_VSCROLLCLIPBOARD = 0x30A,
			WM_WINDOWPOSCHANGED = 0x47,
			WM_WINDOWPOSCHANGING = 0x46,
			WM_WININICHANGE = 0x1A,
			WM_XBUTTONDBLCLK = 0x20D,
			WM_XBUTTONDOWN = 0x20B,
			WM_XBUTTONUP = 0x20C
		}
		[DllImport("User32.dll", ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int cx, int cy, bool repaint);
	
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		internal static extern int RegisterWindowMessage(string msg);
		
		[DllImport("Shell32.dll", CharSet=CharSet.Auto)]
		private static extern int SHAppBarMessage(int dwMessage, ref APP_BAR_DATA abd);
		
		[StructLayout(LayoutKind.Sequential)]
		private struct APP_BAR_DATA 
		{
			public int cbSize;
			public IntPtr hWnd;
			public int uCallbackMessage;
			public int uEdge;
			public RECT rc;
			public IntPtr lParam;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct WINDOWPOS {
			public IntPtr hWnd;
			public IntPtr hWndInsertAfter;
			public int x;
			public int y;
			public int cx;
			public int cy;
			public UInt32 flags;
			public override string ToString() {
				string ret = "[WINDOWPOS hWnd:" + ((int)hWnd) + " hWndInsertAfter:" + ((int)hWndInsertAfter) + " x:" + x + " y:" + y + " cx:" + cx + " cy:" + cy + " flags: ";
				ret += (((flags & 0x0001) != 0) ? "SWP_NOSIZE " : "");
				ret += (((flags & 0x0002) != 0) ? "SWP_NOMOVE " : "");
				ret += (((flags & 0x0004) != 0) ? "SWP_NOZORDER " : "");
				ret += (((flags & 0x0008) != 0) ? "SWP_NOREDRAW " : "");
				ret += (((flags & 0x0010) != 0) ? "SWP_NOACTIVATE " : "");
				ret += (((flags & 0x0020) != 0) ? "SWP_FRAMECHANGED " : "");  /* The frame changed: send WM_NCCALCSIZE */
				ret += (((flags & 0x0040) != 0) ? "SWP_SHOWWINDOW " : "");
				ret += (((flags & 0x0080) != 0) ? "SWP_HIDEWINDOW " : "");
				ret += (((flags & 0x0100) != 0) ? "SWP_NOCOPYBITS " : "");
				ret += (((flags & 0x0200) != 0) ? "SWP_NOOWNERZORDER " : ""); /* Don't do owner Z ordering */
				ret += (((flags & 0x0400) != 0) ? "SWP_NOSENDCHANGING " : ""); /* Don't send WM_WINDOWPOSCHANGING */
				ret += (((flags & 0x0020) != 0) ? "SWP_DRAWFRAME " : "");
				ret += (((flags & 0x0200) != 0) ? "SWP_NOREPOSITION " : "");
				ret += (((flags & 0x2000) != 0) ? "SWP_DEFERERASE " : "");
				ret += (((flags & 0x4000) != 0) ? "SWP_ASYNCWINDOWPOS " : "");
				return ret+"]";
			}
			private string zeroPad(string str, int len) {
				while ( str.Length < len ) {
					str = "0" + str;
				}
				return str;
			}
		}

		internal const int  SWP_NOSIZE        =  0x0001;
		internal const int  SWP_NOMOVE        =  0x0002;
		internal const int  SWP_NOZORDER       = 0x0004;
		internal const int  SWP_NOREDRAW       = 0x0008;
		internal const int  SWP_NOACTIVATE     = 0x0010;
		internal const int  SWP_FRAMECHANGED   = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
		internal const int  SWP_SHOWWINDOW    =  0x0040;
		internal const int  SWP_HIDEWINDOW    =  0x0080;
		internal const int  SWP_NOCOPYBITS    =  0x0100;
		internal const int  SWP_NOOWNERZORDER =  0x0200; /* Don't do owner Z ordering */
		internal const int  SWP_NOSENDCHANGING = 0x0400; /* Don't send WM_WINDOWPOSCHANGING */

		internal const int  SWP_DRAWFRAME      = 0x0020;
		internal const int  SWP_NOREPOSITION   = 0x0200;

		internal const int SWP_DEFERERASE      = 0x2000;
		internal const int  SWP_ASYNCWINDOWPOS = 0x4000;


		

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT 
		{
			public int left;
			public int top;
			public int right;
			public int bottom;

			public RECT(int left, int top, int right, int bottom) 
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}

			public static RECT FromXYWH(int x, int y, int width, int height) 
			{
				return new RECT(x, y, x + width, y + height);
			}
		}
	}
}
