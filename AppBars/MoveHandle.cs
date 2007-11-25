using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AppBars {
	public partial class MoveHandle: UserControl {
		public MoveHandle() {
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this.Height = 5;
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			e.Graphics.DrawLine(Pens.White, new Point(1, 1), new Point(Width - 1, 1));
			e.Graphics.DrawLine(Pens.White, new Point(1, 2), new Point(1, 2));
			e.Graphics.DrawLine(Pens.DarkGray, new Point(1, 3), new Point(Width - 1, 3));
			e.Graphics.DrawLine(Pens.DarkGray, new Point(Width - 1, 3), new Point(Width - 1, 2));
		}
	}
}
