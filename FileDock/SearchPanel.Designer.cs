namespace FileDock {
	partial class SearchPanel {
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
			this.btnSearch = new System.Windows.Forms.Button();
			this.chkRegex = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtFilename = new System.Windows.Forms.TextBox();
			this.chkRecursive = new System.Windows.Forms.CheckBox();
			this.txtContains = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(7, 72);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(63, 23);
			this.btnSearch.TabIndex = 14;
			this.btnSearch.Text = "Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// chkRegex
			// 
			this.chkRegex.AutoSize = true;
			this.chkRegex.Checked = true;
			this.chkRegex.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRegex.Location = new System.Drawing.Point(92, 59);
			this.chkRegex.Name = "chkRegex";
			this.chkRegex.Size = new System.Drawing.Size(61, 17);
			this.chkRegex.TabIndex = 11;
			this.chkRegex.Text = "Reg-Ex";
			this.chkRegex.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Contains Text:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Match Filename:";
			// 
			// txtFilename
			// 
			this.txtFilename.Location = new System.Drawing.Point(92, 7);
			this.txtFilename.Name = "txtFilename";
			this.txtFilename.Size = new System.Drawing.Size(96, 20);
			this.txtFilename.TabIndex = 7;
			this.txtFilename.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilename_KeyUp);
			// 
			// chkRecursive
			// 
			this.chkRecursive.AutoSize = true;
			this.chkRecursive.Checked = true;
			this.chkRecursive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRecursive.Location = new System.Drawing.Point(92, 78);
			this.chkRecursive.Name = "chkRecursive";
			this.chkRecursive.Size = new System.Drawing.Size(74, 17);
			this.chkRecursive.TabIndex = 13;
			this.chkRecursive.Text = "Recursive";
			this.chkRecursive.UseVisualStyleBackColor = true;
			// 
			// txtContains
			// 
			this.txtContains.Location = new System.Drawing.Point(92, 33);
			this.txtContains.Name = "txtContains";
			this.txtContains.Size = new System.Drawing.Size(96, 20);
			this.txtContains.TabIndex = 10;
			// 
			// SearchPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.chkRegex);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtFilename);
			this.Controls.Add(this.chkRecursive);
			this.Controls.Add(this.txtContains);
			this.Name = "SearchPanel";
			this.Size = new System.Drawing.Size(193, 102);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.CheckBox chkRegex;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFilename;
		private System.Windows.Forms.CheckBox chkRecursive;
		private System.Windows.Forms.TextBox txtContains;
	}
}
