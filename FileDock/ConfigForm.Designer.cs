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
			this.listIgnore = new System.Windows.Forms.ListBox();
			this.txtAddIgnore = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnAddIgnore = new System.Windows.Forms.Button();
			this.btnRemoveIgnore = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
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
			this.groupBox1.Text = "Ignore Files";
			// 
			// listIgnore
			// 
			this.listIgnore.FormattingEnabled = true;
			this.listIgnore.Location = new System.Drawing.Point(7, 20);
			this.listIgnore.Name = "listIgnore";
			this.listIgnore.Size = new System.Drawing.Size(107, 69);
			this.listIgnore.TabIndex = 0;
			this.listIgnore.Tag = "IgnoreFiles";
			// 
			// txtAddIgnore
			// 
			this.txtAddIgnore.Location = new System.Drawing.Point(121, 37);
			this.txtAddIgnore.Name = "txtAddIgnore";
			this.txtAddIgnore.Size = new System.Drawing.Size(73, 20);
			this.txtAddIgnore.TabIndex = 1;
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
			// btnAddIgnore
			// 
			this.btnAddIgnore.Location = new System.Drawing.Point(121, 63);
			this.btnAddIgnore.Name = "btnAddIgnore";
			this.btnAddIgnore.Size = new System.Drawing.Size(73, 23);
			this.btnAddIgnore.TabIndex = 3;
			this.btnAddIgnore.Text = "Add";
			this.btnAddIgnore.UseVisualStyleBackColor = true;
			this.btnAddIgnore.Click += new System.EventHandler(this.btnAddIgnore_Click);
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
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(222, 210);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Name = "ConfigForm";
			this.Text = "Options";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
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

	}
}