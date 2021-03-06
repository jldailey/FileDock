using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FileDock {
	public class TogglePanel: UserControl {
		public FileDockForm Owner;
		public TogglePanel(FileDockForm Owner) {
			this.Owner = Owner;
			TogglePanel.Instances.Add(this);
		}
		private static List<TogglePanel> Instances = new List<TogglePanel>();

		~TogglePanel() {
			TogglePanel.Instances.Remove(this);
		}

		public static void CollapseAll() {
			foreach(TogglePanel p in Instances) {
				if( p.Visible) {
					p.Toggle();
				}
			}
		}

		public delegate void ToggleDelegate();
		public ToggleDelegate BeforeToggle;
		public ToggleDelegate AfterToggle;
		public void Toggle() {
			if ( BeforeToggle != null ) {
				BeforeToggle();
			}
			if ( this.Visible ) {
				Owner.listFiles.Top -= this.Height;
				Owner.listFiles.Height += this.Height;
				this.Hide();
			} else {
				// hide all other toggle-able panels
				TogglePanel.CollapseAll();
				Owner.listFiles.Top += this.Height;
				Owner.listFiles.Height -= this.Height;
				this.Show();
			}
			if ( AfterToggle != null ) {
				AfterToggle();
			}
		}
	}
}
