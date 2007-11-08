using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FileDock {
	public abstract class TogglePanel: UserControl {
		public FileDockForm Owner;
		public TogglePanel(FileDockForm Owner) {
			this.Owner = Owner;
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
