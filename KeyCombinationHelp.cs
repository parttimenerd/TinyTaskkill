// Copyright © 2011 Parttimenerd, licenced under the GPL Version 3

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TinyTaskkill
{
	/// <summary>
	/// Description of KeyCombinationHelp.
	/// </summary>
	public partial class KeyCombinationHelp : Form
	{
		public KeyCombinationHelp(Dictionary<string,string> data, Font font)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
            this.Text = GUITexts.get("Help");
			InitializeComponent();
			this.Font = font;
			DataGridViewRow row;
			DataGridViewCell cell;
			foreach (string str in data.Keys){
				dataGridView.Rows.Add();
				row = dataGridView.Rows[dataGridView.Rows.Count-1];
				cell = row.Cells[0];
				cell.Value = str;
				cell.ReadOnly = true;
				cell = row.Cells[1];
				cell.Value = data[str];
				cell.ReadOnly = true;
				if (Ereignis.Width < cell.PreferredSize.Width)
					Ereignis.Width = cell.PreferredSize.Width / 2;
			}
			
			this.Size = dataGridView.PreferredSize;
			
			this.ShowDialog();
		}
	}
}
