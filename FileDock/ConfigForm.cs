using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FileDock {
	public partial class ConfigForm : Form {
		public ConfigForm() {
			InitializeComponent();
		}

		private void btnAddIgnore_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < listIgnore.Items.Count; i++)
			{
				string item = (string)listIgnore.Items[i];
				if (item.Length < 1 || item == " ")
				{
					listIgnore.Items.RemoveAt(i);
					i--;
				}
			}
			string text = txtAddIgnore.Text;
			if( text.Length > 0 ) {

				if (text[0] == '*')
				{
					text = text.Substring(1);
				}
				if (text[0] == '.')
				{
					text = text.Substring(1);
				}
				text = "." + text;
				if( listIgnore.Items.Contains(text) )
					return;
				listIgnore.Items.Add(text);
			}
		}

		private void btnRemoveIgnore_Click(object sender, EventArgs e)
		{
			if (listIgnore.SelectedIndex > -1)
			{
				listIgnore.Items.RemoveAt(listIgnore.SelectedIndex);
			}
		}
	}
}