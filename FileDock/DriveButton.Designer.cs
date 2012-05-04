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
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_12;
			this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button1.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Margin = new System.Windows.Forms.Padding(0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 24);
			this.button1.TabIndex = 2;
			this.button1.Text = "D";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// DriveButton
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.button1);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "DriveButton";
			this.Size = new System.Drawing.Size(24, 24);
			this.Load += new System.EventHandler(this.DriveButton_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;



	}
}
