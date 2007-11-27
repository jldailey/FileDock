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
					this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
					this.listFiles = new System.Windows.Forms.ListView();
					this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
					this.contextMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
					this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
					this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
					this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
					this.button9 = new System.Windows.Forms.Button();
					this.button8 = new System.Windows.Forms.Button();
					this.button7 = new System.Windows.Forms.Button();
					this.button6 = new System.Windows.Forms.Button();
					this.button5 = new System.Windows.Forms.Button();
					this.button3 = new System.Windows.Forms.Button();
					this.button2 = new System.Windows.Forms.Button();
					this.button4 = new System.Windows.Forms.Button();
					this.button1 = new System.Windows.Forms.Button();
					this.button10 = new System.Windows.Forms.Button();
					this.moveHandle1 = new FileDock.MoveHandle();
					((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
					this.contextMenu1.SuspendLayout();
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
					this.listFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
											| System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
					this.listFiles.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
					this.listFiles.Location = new System.Drawing.Point(2, 111);
					this.listFiles.Name = "listFiles";
					this.listFiles.Size = new System.Drawing.Size(185, 397);
					this.listFiles.TabIndex = 12;
					this.listFiles.UseCompatibleStateImageBehavior = false;
					this.listFiles.View = System.Windows.Forms.View.Details;
					// 
					// columnHeader1
					// 
					this.columnHeader1.Text = "";
					this.columnHeader1.Width = 212;
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
					this.contextMenu1.Size = new System.Drawing.Size(87, 48);
					// 
					// toolStripMenuItem1
					// 
					this.toolStripMenuItem1.Name = "toolStripMenuItem1";
					this.toolStripMenuItem1.Size = new System.Drawing.Size(86, 22);
					this.toolStripMenuItem1.Text = "Copy";
					// 
					// toolStripMenuItem2
					// 
					this.toolStripMenuItem2.Name = "toolStripMenuItem2";
					this.toolStripMenuItem2.Size = new System.Drawing.Size(86, 22);
					this.toolStripMenuItem2.Text = "Move";
					// 
					// flowLayoutPanel1
					// 
					this.flowLayoutPanel1.AutoSize = true;
					this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
					this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 9);
					this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(160, 0);
					this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(160, 70);
					this.flowLayoutPanel1.Name = "flowLayoutPanel1";
					this.flowLayoutPanel1.Size = new System.Drawing.Size(160, 70);
					this.flowLayoutPanel1.TabIndex = 11;
					this.flowLayoutPanel1.Enter += new System.EventHandler(this.NoFocusAllowed);
					// 
					// button9
					// 
					this.button9.BackgroundImage = global::FileDock.Properties.Resources.shell32_319;
					this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
					this.button9.FlatAppearance.BorderSize = 0;
					this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
					this.button9.Location = new System.Drawing.Point(62, 85);
					this.button9.Margin = new System.Windows.Forms.Padding(0);
					this.button9.Name = "button9";
					this.button9.Size = new System.Drawing.Size(20, 20);
					this.button9.TabIndex = 15;
					this.button9.TabStop = false;
					this.button9.UseVisualStyleBackColor = true;
					this.button9.Enter += new System.EventHandler(this.NoFocusAllowed);
					this.button9.Click += new System.EventHandler(this.createDir_Click);
					// 
					// button8
					// 
					this.button8.AllowDrop = true;
					this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
					this.button8.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_290;
					this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
					this.button8.FlatAppearance.BorderSize = 0;
					this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
					this.button8.Location = new System.Drawing.Point(167, 31);
					this.button8.Name = "button8";
					this.button8.Size = new System.Drawing.Size(20, 20);
					this.button8.TabIndex = 14;
					this.button8.TabStop = false;
					this.button8.UseVisualStyleBackColor = true;
					this.button8.Enter += new System.EventHandler(this.NoFocusAllowed);
					this.button8.Click += new System.EventHandler(this.clone_Click);
					this.button8.DragDrop += new System.Windows.Forms.DragEventHandler(this.clone_DragDrop);
					this.button8.DragEnter += new System.Windows.Forms.DragEventHandler(this.clone_DragEnter);
					// 
					// button7
					// 
					this.button7.AllowDrop = true;
					this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
					this.button7.BackgroundImage = global::FileDock.Properties.Resources.xpsp2res_900;
					this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
					this.button7.FlatAppearance.BorderSize = 0;
					this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
					this.button7.Location = new System.Drawing.Point(115, 85);
					this.button7.Name = "button7";
					this.button7.Size = new System.Drawing.Size(20, 20);
					this.button7.TabIndex = 13;
					this.button7.TabStop = false;
					this.button7.UseVisualStyleBackColor = true;
					this.button7.Enter += new System.EventHandler(this.NoFocusAllowed);
					this.button7.Click += new System.EventHandler(this.config_Click);
					// 
					// button6
					// 
					this.button6.AllowDrop = true;
					this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
					this.button6.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_144;
					this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
					this.button6.FlatAppearance.BorderSize = 0;
					this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
					this.button6.Location = new System.Drawing.Point(167, 85);
					this.button6.Name = "button6";
					this.button6.Size = new System.Drawing.Size(20, 20);
					this.button6.TabIndex = 10;
					this.button6.TabStop = false;
					this.button6.UseVisualStyleBackColor = true;
					this.button6.Enter += new System.EventHandler(this.NoFocusAllowed);
					this.button6.Click += new System.EventHandler(this.delete_Click);
					this.button6.DragDrop += new System.Windows.Forms.DragEventHandler(this.delete_DragDrop);
					this.button6.DragEnter += new System.Windows.Forms.DragEventHandler(this.delete_DragEnter);
					// 
					// button5
					// 
					this.button5.AllowDrop = true;
					this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
					this.button5.BackgroundImage = global::FileDock.Properties.Resources.SzFM_1011;
					this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
					this.button5.FlatAppearance.BorderSize = 0;
					this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
					this.button5.Location = new System.Drawing.Point(141, 85);
					this.button5.Name = "button5";
					this.button5.Size = new System.Drawing.Size(20, 20);
					this.button5.TabIndex = 9;
					this.button5.TabStop = false;
					this.button5.UseVisualStyleBackColor = true;
					this.button5.Enter += new System.EventHandler(this.NoFocusAllowed);
					this.button5.Click += new System.EventHandler(this.sevenZip_Click);
					this.button5.DragDrop += new System.Windows.Forms.DragEventHandler(this.sevenZip_DragDrop);
					this.button5.DragEnter += new System.Windows.Forms.DragEventHandler(this.sevenZip_DragEnter);
					// 
					// button3
					// 
					this.button3.BackgroundImage = global::FileDock.Properties.Resources.xpsp2res_2023;
					this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
					this.button3.FlatAppearance.BorderSize = 0;
					this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
					this.button3.Location = new System.Drawing.Point(2, 85);
					this.button3.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
					this.button3.Name = "button3";
					this.button3.Size = new System.Drawing.Size(20, 20);
					this.button3.TabIndex = 4;
					this.button3.TabStop = false;
					this.button3.UseVisualStyleBackColor = true;
					this.button3.Enter += new System.EventHandler(this.NoFocusAllowed);
					this.button3.Click += new System.EventHandler(this.search_Click);
					// 
					// button2
					// 
					this.button2.BackgroundImage = global::FileDock.Properties.Resources.xpsp2res_2020;
					this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
					this.button2.FlatAppearance.BorderSize = 0;
					this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
					this.button2.Location = new System.Drawing.Point(22, 85);
					this.button2.Margin = new System.Windows.Forms.Padding(0);
					this.button2.Name = "button2";
					this.button2.Size = new System.Drawing.Size(20, 20);
					this.button2.TabIndex = 5;
					this.button2.TabStop = false;
					this.button2.UseVisualStyleBackColor = true;
					this.button2.Enter += new System.EventHandler(this.NoFocusAllowed);
					this.button2.Click += new System.EventHandler(this.refresh_Click);
					// 
					// button4
					// 
					this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
					this.button4.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_28;
					this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
					this.button4.FlatAppearance.BorderSize = 0;
					this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
					this.button4.Location = new System.Drawing.Point(167, 9);
					this.button4.Margin = new System.Windows.Forms.Padding(0);
					this.button4.Name = "button4";
					this.button4.Size = new System.Drawing.Size(20, 19);
					this.button4.TabIndex = 6;
					this.button4.TabStop = false;
					this.button4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
					this.button4.UseVisualStyleBackColor = true;
					this.button4.Enter += new System.EventHandler(this.NoFocusAllowed);
					this.button4.Click += new System.EventHandler(this.close_Click);
					// 
					// button1
					// 
					this.button1.BackgroundImage = global::FileDock.Properties.Resources.shell32_186;
					this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
					this.button1.FlatAppearance.BorderSize = 0;
					this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
					this.button1.Location = new System.Drawing.Point(82, 85);
					this.button1.Margin = new System.Windows.Forms.Padding(0);
					this.button1.Name = "button1";
					this.button1.Size = new System.Drawing.Size(20, 20);
					this.button1.TabIndex = 17;
					this.button1.TabStop = false;
					this.button1.UseVisualStyleBackColor = true;
					this.button1.Enter += new System.EventHandler(this.NoFocusAllowed);
					this.button1.Click += new System.EventHandler(this.createFile_Click);
					// 
					// button10
					// 
					this.button10.AllowDrop = true;
					this.button10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
					this.button10.BackgroundImage = global::FileDock.Properties.Resources.shell32_44;
					this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
					this.button10.FlatAppearance.BorderSize = 0;
					this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
					this.button10.Location = new System.Drawing.Point(42, 85);
					this.button10.Margin = new System.Windows.Forms.Padding(0);
					this.button10.Name = "button10";
					this.button10.Size = new System.Drawing.Size(20, 20);
					this.button10.TabIndex = 18;
					this.button10.TabStop = false;
					this.button10.UseVisualStyleBackColor = true;
					this.button10.Click += new System.EventHandler(this.favorites_Click);
					this.button10.DragDrop += new System.Windows.Forms.DragEventHandler(this.favorites_DragDrop);
					this.button10.DragEnter += new System.Windows.Forms.DragEventHandler(this.favorites_DragEnter);
					// 
					// moveHandle1
					// 
					this.moveHandle1.Cursor = System.Windows.Forms.Cursors.SizeAll;
					this.moveHandle1.Location = new System.Drawing.Point(2, 2);
					this.moveHandle1.Name = "moveHandle1";
					this.moveHandle1.Size = new System.Drawing.Size(186, 5);
					this.moveHandle1.TabIndex = 19;
					// 
					// FileDockForm
					// 
					this.AllowDrop = true;
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(192, 511);
					this.Controls.Add(this.moveHandle1);
					this.Controls.Add(this.flowLayoutPanel1);
					this.Controls.Add(this.button10);
					this.Controls.Add(this.button8);
					this.Controls.Add(this.button7);
					this.Controls.Add(this.button1);
					this.Controls.Add(this.button9);
					this.Controls.Add(this.button6);
					this.Controls.Add(this.button4);
					this.Controls.Add(this.button5);
					this.Controls.Add(this.button3);
					this.Controls.Add(this.listFiles);
					this.Controls.Add(this.button2);
					this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
					this.Name = "FileDockForm";
					this.ShowInTaskbar = false;
					this.Text = "FileDock";
					this.TransparencyKey = System.Drawing.Color.Blue;
					this.Enter += new System.EventHandler(this.NoFocusAllowed);
					this.Activated += new System.EventHandler(this.NoFocusAllowed);
					((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
					this.contextMenu1.ResumeLayout(false);
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

			private System.Windows.Forms.Button button3;
			private System.Windows.Forms.Button button2;
			private System.Windows.Forms.Button button4;
			private System.IO.FileSystemWatcher fileSystemWatcher1;
			private System.Windows.Forms.Button button5;
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
			private System.Windows.Forms.Button button10;
			private FileDock.MoveHandle moveHandle1;
    }
}

