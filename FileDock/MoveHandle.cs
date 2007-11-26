using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileDock {
	public partial class MoveHandle: UserControl {
		public MoveHandle() {
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this.previewForm = new Form();
			this.previewForm.Size = this.ParentForm.Size;
			this.previewForm.BackColor = Color.Red;
			this.previewForm.FormBorderStyle = FormBorderStyle.None;
			this.previewForm.Hide();
		}

		protected override void OnPaint(PaintEventArgs e) {
			Debug.Print("OnPaint begins: " + this.Width);
			e.Graphics.DrawLine(Pens.White, new Point(1, 1), new Point(this.Width - 1, 1));
			e.Graphics.DrawLine(Pens.White, new Point(1, 2), new Point(1, 2));
			e.Graphics.DrawLine(Pens.DarkGray, new Point(1, 3), new Point(this.Width - 1, 3));
			e.Graphics.DrawLine(Pens.DarkGray, new Point(this.Width - 1, 3), new Point(this.Width - 1, 2));
			Debug.Print("OnPaint finished: "+ this.Width);
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			Debug.Print("Drag Begin");
			dragBegin = new Point(e.Location.X,e.Location.Y);
			this.MouseMove +=	MoveHandler;
		}
		private Point dragBegin;
		private Form previewForm;
		void MoveHandler(object sender, MouseEventArgs args) {
			Point delta = new Point(args.Location.X - dragBegin.X, args.Location.Y - dragBegin.Y);
			//Debug.Print("Moved: " + delta.ToString());
			Rectangle rc = ((FileDockForm)this.ParentForm).QueryPos(new Point(this.ParentForm.Left + delta.X, this.ParentForm.Top), this.ParentForm.Size);
			this.previewForm.Bounds = rc;
			if( ! this.previewForm.Visible ) 
				this.previewForm.Show();
			//this.ParentForm.Left += delta.X;
			//dragBegin = this.ParentForm.Location;
			this.ParentForm.Invalidate();
		}
		protected override void OnMouseUp(MouseEventArgs e) {
			base.OnMouseUp(e);
			this.previewForm.Hide();
			this.MouseMove -= MoveHandler;
			Debug.Print("Drag End");
		}
	}
}
