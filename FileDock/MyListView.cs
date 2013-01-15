using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileDock {
	public class MyListView : ListView {
		public MyListView() {
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}
	}
}
