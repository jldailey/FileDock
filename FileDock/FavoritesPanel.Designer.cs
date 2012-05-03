using System.Drawing;
namespace FileDock {
	partial class FavoritesPanel {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if ( disposing && (components != null) ) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.listFavs = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// listFavs
			// 
			this.listFavs.Anchor = ((System.Windows.Forms.AnchorStyles)
				(System.Windows.Forms.AnchorStyles.Top 
				| System.Windows.Forms.AnchorStyles.Bottom
				| System.Windows.Forms.AnchorStyles.Left
				| System.Windows.Forms.AnchorStyles.Right));
			this.listFavs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listFavs.BackColor = Color.White;
			this.listFavs.FormattingEnabled = true;
			this.listFavs.Location = new System.Drawing.Point(2, 2);
			this.listFavs.Name = "listFavs";
			this.listFavs.Size = new System.Drawing.Size(235, 55);
			this.listFavs.TabIndex = 0;
			// 
			// FavoritesPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listFavs);
			this.Anchor = ((System.Windows.Forms.AnchorStyles)
				(System.Windows.Forms.AnchorStyles.Top
				| System.Windows.Forms.AnchorStyles.Bottom
				| System.Windows.Forms.AnchorStyles.Left
				| System.Windows.Forms.AnchorStyles.Right));
			this.Name = "FavoritesPanel";
			this.Size = new System.Drawing.Size(243, 61);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.ListBox listFavs;
	}
}
