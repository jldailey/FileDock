using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FileDock {
	public delegate void FavoriteSelectedDelegate(string path);

	public partial class FavoritesPanel: TogglePanel {
		public FavoritesPanel(FileDockForm owner):base(owner) {
			InitializeComponent();
		}
		public FavoriteSelectedDelegate FavoriteSelected;
		public int maxCharsToDisplay = -1;

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			listFavs.SelectedIndexChanged += new EventHandler(listFavs_SelectedIndexChanged);
		}

		void listFavs_SelectedIndexChanged(object sender, EventArgs e) {
			string path = (string)(listFavs.SelectedItem);
			if ( FavoriteSelected != null ) {
				FavoriteSelected(path);
			} else {
				MessageBox.Show(path);
			}
			this.Toggle();
		}

	}
}
