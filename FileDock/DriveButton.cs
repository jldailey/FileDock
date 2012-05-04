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
					this.BorderStyle = BorderStyle.FixedSingle;
					this.BackColor = Color.FromArgb(255, 255, 180);
				} else {
					this.BorderStyle = BorderStyle.None;
					this.BackColor = Color.Transparent;
				}
			}
		}

		public SelectDriveDelegate OnSelectDrive = null;
		public SelectDriveDelegate BeforeSelectDrive = null;
		public DriveButton(DriveInfo drive) {
			InitializeComponent();
			Selected = false;
			string label = drive.Name.ToUpper().Substring(0,1);
			this.button1.Text = label + ":";

			switch (drive.DriveType) {
				case DriveType.CDRom:
					this.button1.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_12;
					break;
				case DriveType.Removable:
					this.button1.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_8;
					break;
				case DriveType.Network:
					this.button1.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_10;
					break;
				default:
					this.button1.BackgroundImage = global::FileDock.Properties.Resources.SHELL32_9;
					break;
			}
			
			mTip = new ToolTip();
			EventHandler showTip = delegate(object sender, EventArgs e)
			{
				if ( drive.DriveType == DriveType.Fixed ) {
					mTip.Show("Drive: " + label + " Free Space: " + drive.AvailableFreeSpace.ToString(), this, 1500);
				} else {
					mTip.Show("Drive: " + label, this, 1500);
				}
			};
			this.MouseHover += showTip;
			this.button1.MouseHover += showTip;
			EventHandler hideTip = delegate(object sender, EventArgs e)
			{
				mTip.Hide(this);
			};
			this.MouseLeave += hideTip;
			this.button1.MouseLeave += hideTip;

			this.Click += new EventHandler(DriveButton_Click);
			this.button1.Click += new EventHandler(DriveButton_Click);
		}

		void DriveButton_Click(object sender, EventArgs e) {
			if (BeforeSelectDrive != null) BeforeSelectDrive(this.button1.Text);
			this.Selected = true;
			if (OnSelectDrive != null) OnSelectDrive(this.button1.Text);
			
		}

		private void DriveButton_Load(object sender, EventArgs e) {

		}
	}
}
