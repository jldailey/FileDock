namespace FileDock
{
    partial class FileDockForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
						this.listSem.Close();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Folders", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Files", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Hidden/System", System.Windows.Forms.HorizontalAlignment.Left);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileDockForm));
			this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
			this.listFiles = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.button11 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			this.moveHandle1 = new FileDock.MoveHandle();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
			this.contextMenu1.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// fileSystemWatcher1
			// 
			this.fileSystemWatcher1.EnableRaisingEvents = true;
			this.fileSystemWatcher1.SynchronizingObject = this;
			// 
			// listFiles
			// 
			this.listFiles.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listFiles.AllowDrop = true;
			this.listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listFiles.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listFiles.FullRowSelect = true;
			listViewGroup1.Header = "Folders";
			listViewGroup1.Name = "listViewGroup1";
			listViewGroup2.Header = "Files";
			listViewGroup2.Name = "listViewGroup2";
			listViewGroup3.Header = "Hidden/System";
			listViewGroup3.Name = "listViewGroup3";
			this.listFiles.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
			this.listFiles.Location = new System.Drawing.Point(0, 26);
			this.listFiles.Margin = new System.Windows.Forms.Padding(0);
			this.listFiles.Name = "listFiles";
			this.listFiles.Size = new System.Drawing.Size(175, 416);
			this.listFiles.TabIndex = 12;
			this.listFiles.UseCompatibleStateImageBehavior = false;
			this.listFiles.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "";
			this.columnHeader1.Width = 197;
			// 
			// contextMenu1
			// 
			this.contextMenu1.AutoClose = false;
			this.contextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
			this.contextMenu1.Name = "contextMenu1";
			this.contextMenu1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.contextMenu1.ShowImageMargin = false;
			this.contextMenu1.ShowItemToolTips = false;
			this.contextMenu1.Size = new System.Drawing.Size(80, 48);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(79, 22);
			this.toolStripMenuItem1.Text = "Copy";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(79, 22);
			this.toolStripMenuItem2.Text = "Move";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 6);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(175, 0);
			this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(175, 20);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(175, 20);
			this.flowLayoutPanel1.TabIndex = 11;
			this.flowLayoutPanel1.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// button11
			// 
			this.button11.AllowDrop = true;
			this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button11.BackgroundImage = global::FileDock.Properties.Resources.gvim_IDR_VIM;
			this.button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button11.FlatAppearance.BorderSize = 0;
			this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button11.Location = new System.Drawing.Point(0, 58);
			this.button11.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(20, 20);
			this.button11.TabIndex = 20;
			this.button11.TabStop = false;
			this.button11.UseVisualStyleBackColor = true;
			this.button11.Click += new System.EventHandler(this.vim_Click);
			this.button11.DragDrop += new System.Windows.Forms.DragEventHandler(this.vim_DragDrop);
			this.button11.DragEnter += new System.Windows.Forms.DragEventHandler(this.vim_DragEnter);
			this.button11.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// button8
			// 
			this.button8.AllowDrop = true;
			this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button8.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_290;
			this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button8.FlatAppearance.BorderSize = 0;
			this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button8.Location = new System.Drawing.Point(0, 32);
			this.button8.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(20, 20);
			this.button8.TabIndex = 14;
			this.button8.TabStop = false;
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.clone_Click);
			this.button8.DragDrop += new System.Windows.Forms.DragEventHandler(this.clone_DragDrop);
			this.button8.DragEnter += new System.Windows.Forms.DragEventHandler(this.clone_DragEnter);
			this.button8.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// button7
			// 
			this.button7.AllowDrop = true;
			this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button7.BackgroundImage = global::FileDock.Properties.Resources.xpsp2res_900;
			this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button7.FlatAppearance.BorderSize = 0;
			this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button7.Location = new System.Drawing.Point(0, 136);
			this.button7.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(20, 20);
			this.button7.TabIndex = 13;
			this.button7.TabStop = false;
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.config_Click);
			this.button7.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// button1
			// 
			this.button1.BackgroundImage = global::FileDock.Properties.Resources.shell32_186;
			this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(0, 84);
			this.button1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(20, 20);
			this.button1.TabIndex = 17;
			this.button1.TabStop = false;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// button9
			// 
			this.button9.BackgroundImage = global::FileDock.Properties.Resources.shell32_319;
			this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button9.FlatAppearance.BorderSize = 0;
			this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button9.Location = new System.Drawing.Point(0, 110);
			this.button9.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(20, 20);
			this.button9.TabIndex = 15;
			this.button9.TabStop = false;
			this.button9.UseVisualStyleBackColor = true;
			this.button9.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// button6
			// 
			this.button6.AllowDrop = true;
			this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button6.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_144;
			this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button6.FlatAppearance.BorderSize = 0;
			this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button6.Location = new System.Drawing.Point(0, 188);
			this.button6.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(20, 20);
			this.button6.TabIndex = 10;
			this.button6.TabStop = false;
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.delete_Click);
			this.button6.DragDrop += new System.Windows.Forms.DragEventHandler(this.delete_DragDrop);
			this.button6.DragEnter += new System.Windows.Forms.DragEventHandler(this.delete_DragEnter);
			this.button6.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_28;
			this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button4.FlatAppearance.BorderSize = 0;
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button4.Location = new System.Drawing.Point(0, 6);
			this.button4.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(20, 20);
			this.button4.TabIndex = 6;
			this.button4.TabStop = false;
			this.button4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.close_Click);
			this.button4.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// button2
			// 
			this.button2.BackgroundImage = global::FileDock.Properties.Resources.xpsp2res_2020;
			this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button2.FlatAppearance.BorderSize = 0;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(0, 162);
			this.button2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(20, 20);
			this.button2.TabIndex = 5;
			this.button2.TabStop = false;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.refresh_Click);
			this.button2.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.flowLayoutPanel3.Controls.Add(this.button4);
			this.flowLayoutPanel3.Controls.Add(this.button8);
			this.flowLayoutPanel3.Controls.Add(this.button11);
			this.flowLayoutPanel3.Controls.Add(this.button1);
			this.flowLayoutPanel3.Controls.Add(this.button9);
			this.flowLayoutPanel3.Controls.Add(this.button7);
			this.flowLayoutPanel3.Controls.Add(this.button2);
			this.flowLayoutPanel3.Controls.Add(this.button6);
			this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(179, 3);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.flowLayoutPanel3.Size = new System.Drawing.Size(20, 217);
			this.flowLayoutPanel3.TabIndex = 22;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoSize = true;
			this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel1);
			this.flowLayoutPanel2.Controls.Add(this.listFiles);
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(1, 0);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.flowLayoutPanel2.Size = new System.Drawing.Size(175, 442);
			this.flowLayoutPanel2.TabIndex = 23;
			// 
			// flowLayoutPanel4
			// 
			this.flowLayoutPanel4.Controls.Add(this.flowLayoutPanel3);
			this.flowLayoutPanel4.Controls.Add(this.flowLayoutPanel2);
			this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel4.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel4.Name = "flowLayoutPanel4";
			this.flowLayoutPanel4.Size = new System.Drawing.Size(202, 455);
			this.flowLayoutPanel4.TabIndex = 13;
			// 
			// moveHandle1
			// 
			this.moveHandle1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.moveHandle1.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.moveHandle1.Location = new System.Drawing.Point(-1, 0);
			this.moveHandle1.Name = "moveHandle1";
			this.moveHandle1.Size = new System.Drawing.Size(200, 6);
			this.moveHandle1.TabIndex = 19;
			// 
			// FileDockForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(202, 455);
			this.Controls.Add(this.moveHandle1);
			this.Controls.Add(this.flowLayoutPanel4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FileDockForm";
			this.ShowInTaskbar = false;
			this.Text = "FileDock";
			this.TransparencyKey = System.Drawing.Color.Blue;
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
			this.contextMenu1.ResumeLayout(false);
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.flowLayoutPanel4.ResumeLayout(false);
			this.flowLayoutPanel4.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

				private System.Windows.Forms.Button button2;
			private System.Windows.Forms.Button button4;
			private System.IO.FileSystemWatcher fileSystemWatcher1;
			private System.Windows.Forms.Button button6;
			private System.Windows.Forms.ColumnHeader columnHeader1;
			private System.Windows.Forms.Button button7;
			private System.Windows.Forms.Button button8;
			private System.Windows.Forms.ContextMenuStrip contextMenu1;
			private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
			private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
			private System.Windows.Forms.Button button9;
			private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
			private System.Windows.Forms.Button button1;
			public System.Windows.Forms.ListView listFiles;
			private FileDock.MoveHandle moveHandle1;
			private System.Windows.Forms.Button button11;
			private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
			private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
			private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
    }
}

