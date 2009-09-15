namespace FileDock {
	partial class ConfigForm {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnRemoveIgnore = new System.Windows.Forms.Button();
			this.btnAddIgnore = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtAddIgnore = new System.Windows.Forms.TextBox();
			this.listIgnore = new System.Windows.Forms.ListBox();
			this.button11 = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtEditVimLocation = new System.Windows.Forms.TextBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(12, 12);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(160, 17);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Tag = "SingleClick";
			this.checkBox1.Text = "Single-click to activate items";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(12, 36);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(114, 17);
			this.checkBox2.TabIndex = 2;
			this.checkBox2.Tag = "ShowHidden";
			this.checkBox2.Text = "Show Hidden Files";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnRemoveIgnore);
			this.groupBox1.Controls.Add(this.btnAddIgnore);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtAddIgnore);
			this.groupBox1.Controls.Add(this.listIgnore);
			this.groupBox1.Location = new System.Drawing.Point(12, 59);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 120);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Ignore File Types";
			// 
			// btnRemoveIgnore
			// 
			this.btnRemoveIgnore.Location = new System.Drawing.Point(7, 91);
			this.btnRemoveIgnore.Name = "btnRemoveIgnore";
			this.btnRemoveIgnore.Size = new System.Drawing.Size(73, 23);
			this.btnRemoveIgnore.TabIndex = 4;
			this.btnRemoveIgnore.Text = "Remove";
			this.btnRemoveIgnore.UseVisualStyleBackColor = true;
			this.btnRemoveIgnore.Click += new System.EventHandler(this.btnRemoveIgnore_Click);
			// 
			// btnAddIgnore
			// 
			this.btnAddIgnore.Location = new System.Drawing.Point(121, 63);
			this.btnAddIgnore.Name = "btnAddIgnore";
			this.btnAddIgnore.Size = new System.Drawing.Size(73, 23);
			this.btnAddIgnore.TabIndex = 6;
			this.btnAddIgnore.Text = "Add";
			this.btnAddIgnore.UseVisualStyleBackColor = true;
			this.btnAddIgnore.Click += new System.EventHandler(this.btnAddIgnore_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(118, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Extension:";
			// 
			// txtAddIgnore
			// 
			this.txtAddIgnore.Location = new System.Drawing.Point(121, 37);
			this.txtAddIgnore.Name = "txtAddIgnore";
			this.txtAddIgnore.Size = new System.Drawing.Size(73, 20);
			this.txtAddIgnore.TabIndex = 5;
			// 
			// listIgnore
			// 
			this.listIgnore.FormattingEnabled = true;
			this.listIgnore.Location = new System.Drawing.Point(7, 20);
			this.listIgnore.Name = "listIgnore";
			this.listIgnore.Size = new System.Drawing.Size(107, 69);
			this.listIgnore.TabIndex = 3;
			this.listIgnore.Tag = "IgnoreFiles";
			// 
			// button11
			// 
			this.button11.AllowDrop = true;
			this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button11.BackgroundImage = global::FileDock.Properties.Resources.gvim_IDR_VIM;
			this.button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button11.FlatAppearance.BorderSize = 0;
			this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button11.Location = new System.Drawing.Point(7, 16);
			this.button11.Name = "button11";
			this.button11.Padding = new System.Windows.Forms.Padding(4);
			this.button11.Size = new System.Drawing.Size(24, 24);
			this.button11.TabIndex = 7;
			this.button11.UseVisualStyleBackColor = true;
			this.button11.Click += new System.EventHandler(this.btnEditVimLocation_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtEditVimLocation);
			this.groupBox2.Controls.Add(this.button11);
			this.groupBox2.Location = new System.Drawing.Point(12, 185);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 49);
			this.groupBox2.TabIndex = 23;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Vim Location";
			// 
			// txtEditVimLocation
			// 
			this.txtEditVimLocation.Location = new System.Drawing.Point(37, 19);
			this.txtEditVimLocation.Name = "txtEditVimLocation";
			this.txtEditVimLocation.Size = new System.Drawing.Size(157, 20);
			this.txtEditVimLocation.TabIndex = 22;
			this.txtEditVimLocation.TabStop = false;
			this.txtEditVimLocation.Tag = "VimLocation";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(222, 238);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Name = "ConfigForm";
			this.Text = "Configuration";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnAddIgnore;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtAddIgnore;
		private System.Windows.Forms.ListBox listIgnore;
		private System.Windows.Forms.Button btnRemoveIgnore;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TextBox txtEditVimLocation;

	}
}