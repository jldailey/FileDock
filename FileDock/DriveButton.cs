using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileDock {
	public delegate void SelectDriveDelegate(string drive_name);

	public partial class DriveButton : UserControl {
		private ToolTip mTip;

		private bool _Selected;
		public bool Selected {
			get { return _Selected; }
			set {
				_Selected = value;
				if (value) {
					this.BorderStyle = BorderStyle.Fixed3D;
				} else {
					this.BorderStyle = BorderStyle.None;
				}
			}
		}

		public SelectDriveDelegate OnSelectDrive = null;
		public SelectDriveDelegate BeforeSelectDrive = null;
		public DriveButton(DriveInfo drive) {
			InitializeComponent();
			Selected = false;
			string label = drive.Name.ToUpper().Substring(0,1);

			switch (drive.DriveType) {
				case DriveType.CDRom:
					this.driveIconPanel.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_12;
					break;
				case DriveType.Removable:
					this.driveIconPanel.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_8;
					break;
				case DriveType.Network:
					this.driveIconPanel.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_10;
					break;
				default:
					this.driveIconPanel.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_9;
					break;
			}
			this.driveLabel.Text = label;
			
			mTip = new ToolTip();
			EventHandler showTip = delegate(object sender, EventArgs e)
			{
				mTip.Show("Drive: " + label + " Free Space: "+drive.AvailableFreeSpace.ToString(), this, 2000);
			};
			this.MouseHover += showTip;
			this.driveLabel.MouseHover += showTip;
			this.driveIconPanel.MouseHover += showTip;
			EventHandler hideTip = delegate(object sender, EventArgs e)
			{
				mTip.Hide(this);
			};
			this.MouseLeave += hideTip;
			this.driveLabel.MouseLeave += hideTip;
			this.driveIconPanel.MouseLeave += hideTip;

			this.Click += new EventHandler(DriveButton_Click);
			this.driveLabel.Click += new EventHandler(DriveButton_Click);
			this.driveIconPanel.Click += new EventHandler(DriveButton_Click);
		}

		void DriveButton_Click(object sender, EventArgs e) {
			if (BeforeSelectDrive != null) BeforeSelectDrive(this.driveLabel.Text);
			this.Selected = true;
			if (OnSelectDrive != null) OnSelectDrive(this.driveLabel.Text);
			
		}

		private void DriveButton_Load(object sender, EventArgs e) {

		}
	}
}
