namespace FileDock {
	partial class DriveButton {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
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
			this.driveIconPanel = new System.Windows.Forms.Panel();
			this.driveLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// driveIconPanel
			// 
			this.driveIconPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.driveIconPanel.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_12;
			this.driveIconPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.driveIconPanel.Location = new System.Drawing.Point(18, 1);
			this.driveIconPanel.Margin = new System.Windows.Forms.Padding(0);
			this.driveIconPanel.Name = "driveIconPanel";
			this.driveIconPanel.Size = new System.Drawing.Size(18, 18);
			this.driveIconPanel.TabIndex = 0;
			// 
			// driveLabel
			// 
			this.driveLabel.AutoSize = true;
			this.driveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.driveLabel.Location = new System.Drawing.Point(0, 2);
			this.driveLabel.Margin = new System.Windows.Forms.Padding(0);
			this.driveLabel.Name = "driveLabel";
			this.driveLabel.Size = new System.Drawing.Size(16, 13);
			this.driveLabel.TabIndex = 0;
			this.driveLabel.Text = "D";
			// 
			// DriveButton
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.driveIconPanel);
			this.Controls.Add(this.driveLabel);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(1);
			this.Name = "DriveButton";
			this.Size = new System.Drawing.Size(36, 20);
			this.Load += new System.EventHandler(this.DriveButton_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel driveIconPanel;
		private System.Windows.Forms.Label driveLabel;

	}
}
