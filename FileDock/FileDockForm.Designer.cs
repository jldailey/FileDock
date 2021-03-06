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
			System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Folders", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Files", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Hidden/System", System.Windows.Forms.HorizontalAlignment.Left);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileDockForm));
			this.fileSystemWatcher = new System.IO.FileSystemWatcher();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.vimButton = new System.Windows.Forms.Button();
			this.cloneButton = new System.Windows.Forms.Button();
			this.configButton = new System.Windows.Forms.Button();
			this.trashButton = new System.Windows.Forms.Button();
			this.powerButton = new System.Windows.Forms.Button();
			this.refreshButton = new System.Windows.Forms.Button();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.sortButton = new System.Windows.Forms.Button();
			this.favButton = new System.Windows.Forms.Button();
			this.debugButton = new System.Windows.Forms.Button();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.listFiles = new FileDock.MyListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			this.moveHandle1 = new FileDock.MoveHandle();
			this.favContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
			this.flowLayoutPanel3.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel4.SuspendLayout();
			this.favContextMenu.SuspendLayout();
			this.contextMenu1.SuspendLayout();
			this.SuspendLayout();
			// 
			// fileSystemWatcher
			// 
			this.fileSystemWatcher.EnableRaisingEvents = true;
			this.fileSystemWatcher.SynchronizingObject = this;
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
			// vimButton
			// 
			this.vimButton.AllowDrop = true;
			this.vimButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.vimButton.BackColor = System.Drawing.Color.Transparent;
			this.vimButton.BackgroundImage = global::FileDock.Properties.Resources.gvim_IDR_VIM;
			this.vimButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.vimButton.FlatAppearance.BorderSize = 0;
			this.vimButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.vimButton.ForeColor = System.Drawing.Color.Transparent;
			this.vimButton.Location = new System.Drawing.Point(0, 104);
			this.vimButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.vimButton.Name = "vimButton";
			this.vimButton.Size = new System.Drawing.Size(20, 20);
			this.vimButton.TabIndex = 20;
			this.vimButton.TabStop = false;
			this.vimButton.UseVisualStyleBackColor = false;
			this.vimButton.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// cloneButton
			// 
			this.cloneButton.AllowDrop = true;
			this.cloneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cloneButton.BackColor = System.Drawing.Color.Transparent;
			this.cloneButton.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_290;
			this.cloneButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.cloneButton.FlatAppearance.BorderSize = 0;
			this.cloneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cloneButton.ForeColor = System.Drawing.Color.Transparent;
			this.cloneButton.Location = new System.Drawing.Point(0, 32);
			this.cloneButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.cloneButton.Name = "cloneButton";
			this.cloneButton.Size = new System.Drawing.Size(20, 20);
			this.cloneButton.TabIndex = 14;
			this.cloneButton.TabStop = false;
			this.cloneButton.UseVisualStyleBackColor = false;
			this.cloneButton.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// configButton
			// 
			this.configButton.AllowDrop = true;
			this.configButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.configButton.BackColor = System.Drawing.Color.Transparent;
			this.configButton.BackgroundImage = global::FileDock.Properties.Resources.xpsp2res_900;
			this.configButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.configButton.FlatAppearance.BorderSize = 0;
			this.configButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.configButton.ForeColor = System.Drawing.Color.Transparent;
			this.configButton.Location = new System.Drawing.Point(0, 130);
			this.configButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.configButton.Name = "configButton";
			this.configButton.Size = new System.Drawing.Size(20, 20);
			this.configButton.TabIndex = 13;
			this.configButton.TabStop = false;
			this.configButton.UseVisualStyleBackColor = false;
			this.configButton.Click += new System.EventHandler(this.config_Click);
			this.configButton.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// trashButton
			// 
			this.trashButton.AllowDrop = true;
			this.trashButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.trashButton.BackColor = System.Drawing.Color.Transparent;
			this.trashButton.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_144;
			this.trashButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.trashButton.FlatAppearance.BorderSize = 0;
			this.trashButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.trashButton.ForeColor = System.Drawing.Color.Transparent;
			this.trashButton.Location = new System.Drawing.Point(0, 156);
			this.trashButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.trashButton.Name = "trashButton";
			this.trashButton.Size = new System.Drawing.Size(20, 20);
			this.trashButton.TabIndex = 10;
			this.trashButton.TabStop = false;
			this.trashButton.UseVisualStyleBackColor = false;
			this.trashButton.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// powerButton
			// 
			this.powerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.powerButton.BackColor = System.Drawing.Color.Transparent;
			this.powerButton.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_28;
			this.powerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.powerButton.FlatAppearance.BorderSize = 0;
			this.powerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.powerButton.ForeColor = System.Drawing.Color.Transparent;
			this.powerButton.Location = new System.Drawing.Point(0, 6);
			this.powerButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.powerButton.Name = "powerButton";
			this.powerButton.Size = new System.Drawing.Size(20, 20);
			this.powerButton.TabIndex = 6;
			this.powerButton.TabStop = false;
			this.powerButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.powerButton.UseVisualStyleBackColor = false;
			this.powerButton.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// refreshButton
			// 
			this.refreshButton.BackColor = System.Drawing.Color.Transparent;
			this.refreshButton.BackgroundImage = global::FileDock.Properties.Resources.xpsp2res_2020;
			this.refreshButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.refreshButton.FlatAppearance.BorderSize = 0;
			this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.refreshButton.ForeColor = System.Drawing.Color.Transparent;
			this.refreshButton.Location = new System.Drawing.Point(0, 58);
			this.refreshButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(20, 20);
			this.refreshButton.TabIndex = 5;
			this.refreshButton.TabStop = false;
			this.refreshButton.UseVisualStyleBackColor = false;
			this.refreshButton.Enter += new System.EventHandler(this.NoFocusAllowed);
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.AutoSize = true;
			this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.flowLayoutPanel3.Controls.Add(this.powerButton);
			this.flowLayoutPanel3.Controls.Add(this.cloneButton);
			this.flowLayoutPanel3.Controls.Add(this.refreshButton);
			this.flowLayoutPanel3.Controls.Add(this.sortButton);
			this.flowLayoutPanel3.Controls.Add(this.vimButton);
			this.flowLayoutPanel3.Controls.Add(this.configButton);
			this.flowLayoutPanel3.Controls.Add(this.trashButton);
			this.flowLayoutPanel3.Controls.Add(this.favButton);
			this.flowLayoutPanel3.Controls.Add(this.debugButton);
			this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(179, 3);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.flowLayoutPanel3.Size = new System.Drawing.Size(20, 231);
			this.flowLayoutPanel3.TabIndex = 22;
			// 
			// sortButton
			// 
			this.sortButton.BackColor = System.Drawing.Color.Transparent;
			this.sortButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.sortButton.FlatAppearance.BorderSize = 0;
			this.sortButton.Font = new System.Drawing.Font("Arial", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.sortButton.Location = new System.Drawing.Point(0, 81);
			this.sortButton.Margin = new System.Windows.Forms.Padding(0);
			this.sortButton.Name = "sortButton";
			this.sortButton.Size = new System.Drawing.Size(20, 20);
			this.sortButton.TabIndex = 22;
			this.sortButton.TabStop = false;
			this.sortButton.Text = "az";
			this.sortButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.sortButton.UseVisualStyleBackColor = false;
			// 
			// favButton
			// 
			this.favButton.BackColor = System.Drawing.Color.Transparent;
			this.favButton.BackgroundImage = global::FileDock.Properties.Resources.shell32_44;
			this.favButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.favButton.FlatAppearance.BorderSize = 0;
			this.favButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.favButton.ForeColor = System.Drawing.Color.Transparent;
			this.favButton.Location = new System.Drawing.Point(0, 182);
			this.favButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.favButton.Name = "favButton";
			this.favButton.Size = new System.Drawing.Size(20, 20);
			this.favButton.TabIndex = 23;
			this.favButton.TabStop = false;
			this.favButton.UseVisualStyleBackColor = false;
			this.favButton.Click += new System.EventHandler(this.favButton_Click);
			// 
			// debugButton
			// 
			this.debugButton.BackColor = System.Drawing.Color.Transparent;
			this.debugButton.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_240;
			this.debugButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.debugButton.FlatAppearance.BorderSize = 0;
			this.debugButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.debugButton.ForeColor = System.Drawing.Color.Transparent;
			this.debugButton.Location = new System.Drawing.Point(0, 208);
			this.debugButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.debugButton.Name = "debugButton";
			this.debugButton.Size = new System.Drawing.Size(20, 20);
			this.debugButton.TabIndex = 21;
			this.debugButton.TabStop = false;
			this.debugButton.UseVisualStyleBackColor = false;
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
			// listFiles
			// 
			this.listFiles.AllowDrop = true;
			this.listFiles.CausesValidation = false;
			this.listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listFiles.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listFiles.FullRowSelect = true;
			listViewGroup4.Header = "Folders";
			listViewGroup4.Name = "listViewGroup1";
			listViewGroup5.Header = "Files";
			listViewGroup5.Name = "listViewGroup2";
			listViewGroup6.Header = "Hidden/System";
			listViewGroup6.Name = "listViewGroup3";
			this.listFiles.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup4,
            listViewGroup5,
            listViewGroup6});
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
			// contextMenuStrip1
			// 
			this.favContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem});
			this.favContextMenu.Name = "contextMenuStrip1";
			this.favContextMenu.Size = new System.Drawing.Size(133, 26);
			// 
			// addNewToolStripMenuItem
			// 
			this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
			this.addNewToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.addNewToolStripMenuItem.Text = "Add New...";
			// 
			// contextMenu1
			// 
			this.contextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.moveToolStripMenuItem});
			this.contextMenu1.Name = "contextMenu1";
			this.contextMenu1.Size = new System.Drawing.Size(153, 70);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			// 
			// moveToolStripMenuItem
			// 
			this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
			this.moveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.moveToolStripMenuItem.Text = "Move";
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
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.flowLayoutPanel4.ResumeLayout(false);
			this.flowLayoutPanel4.PerformLayout();
			this.favContextMenu.ResumeLayout(false);
			this.contextMenu1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

				private System.Windows.Forms.Button refreshButton;
			private System.Windows.Forms.Button powerButton;
			private System.IO.FileSystemWatcher fileSystemWatcher;
			private System.Windows.Forms.Button trashButton;
			private System.Windows.Forms.ColumnHeader columnHeader1;
			private System.Windows.Forms.Button configButton;
			private System.Windows.Forms.Button cloneButton;
			private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
			public MyListView listFiles;
			private FileDock.MoveHandle moveHandle1;
			private System.Windows.Forms.Button vimButton;
			private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
			private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
			private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
			private System.Windows.Forms.Button debugButton;
			private System.Windows.Forms.Button sortButton;
			private System.Windows.Forms.Button favButton;
			private System.Windows.Forms.ContextMenuStrip favContextMenu;
			private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
			private System.Windows.Forms.ContextMenuStrip contextMenu1;
			private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
			private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
    }
}

