using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileDock
{
	public partial class MoveHandle : UserControl
	{
		public MoveHandle()
		{
			InitializeComponent();
		}

		private Point dragBegin;
		private FileDockForm previewForm;
		private Form littlePreviewForm;
		private FileDockForm.QueryPosResult dragResult;

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.previewForm = new FileDockForm();
			this.previewForm.dockOnLoad = false;
			this.previewForm.BackColor = Color.DarkGray;
			this.previewForm.Opacity = 0.75;
			this.previewForm.FormBorderStyle = FormBorderStyle.None;
			this.previewForm.Hide();

			this.littlePreviewForm = new Form();
			this.littlePreviewForm.Size = this.ParentForm.Size;
			this.littlePreviewForm.BackColor = Color.DarkGray;
			this.littlePreviewForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			this.littlePreviewForm.Hide();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			// Debug.Print("OnPaint begins: " + this.Width);
			e.Graphics.DrawLine(Pens.White, new Point(1, 1), new Point(this.Width - 1, 1));
			e.Graphics.DrawLine(Pens.White, new Point(1, 2), new Point(1, 2));
			e.Graphics.DrawLine(Pens.DarkGray, new Point(1, 3), new Point(this.Width - 1, 3));
			e.Graphics.DrawLine(Pens.DarkGray, new Point(this.Width - 1, 3), new Point(this.Width - 1, 2));
			// Debug.Print("OnPaint finished: "+ this.Width);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Debug.Print("Drag Begin");
			dragBegin = new Point(e.Location.X, e.Location.Y);
			this.MouseMove += MoveHandler;
			FileDockForm.UnregisterAppBar(this.ParentForm.Handle);
		}
		void MoveHandler(object sender, MouseEventArgs args)
		{
			Point delta = new Point(args.Location.X - dragBegin.X, args.Location.Y - dragBegin.Y);
			//Debug.Print("Moved: " + delta.ToString());
			Point draggedTo = new Point(this.ParentForm.Left + delta.X, this.ParentForm.Top);
			// this.littlePreviewForm.Location = draggedTo;
			this.ParentForm.Location = draggedTo;
			this.ParentForm.Size = this.littlePreviewForm.Size;
			dragResult = FileDockForm.QueryPos(this.ParentForm.Handle, draggedTo, this.ParentForm.Size);
			this.previewForm.Bounds = dragResult.rc;
			if (!this.previewForm.Visible)
				this.previewForm.Show();
			// if ( !this.littlePreviewForm.Visible )
			// this.littlePreviewForm.Show();
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.previewForm.Hide();
			this.littlePreviewForm.Hide();
			FileDockForm.RegisterAppBar(this.ParentForm.Handle);
			FileDockForm.SetPos(this.ParentForm.Handle, dragResult);
			this.MouseMove -= MoveHandler;
			Debug.Print("Drag End");

		}
	}
}
