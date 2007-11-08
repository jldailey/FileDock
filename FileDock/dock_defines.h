// Sends an appbar message to the system. 
		[DllImport("shell32.dll")]
		public static extern UInt32 SHAppBarMessage(
				UInt32 dwMessage,         // Appbar message value to send.
				ref APPBARDATA pData);    // Address of an APPBARDATA structure. The 
		// content of the structure depends on the 
		// value set in the dwMessage parameter. 

		[StructLayout(LayoutKind.Sequential)]
		public struct APPBARDATA {
			public UInt32 cbSize;
			public IntPtr hWnd;
			public UInt32 uCallbackMessage;
			public UInt32 uEdge;
			public RECT rc;
			public Int32 lParam;
		}
		public struct RECT {
			public float top;
			public float bottom;
			public float left;
			public float right;
		}