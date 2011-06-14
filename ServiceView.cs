// Copyright © 2011 Parttimenerd, licenced under the GPL Version 3

using System;
using System.Collections.Generic;
using System.Drawing;
using System.ServiceProcess;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;

namespace TinyTaskkill
{
	/// <summary>
	/// Description of ServiceView.
	/// </summary>
	public partial class ServiceView : Form
	{
		private int lauf_variable = 0;
		Dictionary<string,string> keydata = new Dictionary<string, string>();
		
		public ServiceView()
		{
			InitializeComponent();
			UpdateTable();
			keydata.Add("F1", GUITexts.get("Help"));
			keydata.Add("F2", GUITexts.get("Log out"));
            keydata.Add("F4", GUITexts.get("Close ServiceView"));
			keydata.Add("F5", GUITexts.get("Update data"));
			keydata.Add("F6", GUITexts.get("Copy data"));
			keydata.Add("F7", GUITexts.get("Show/Hide advanced functions"));
			keydata.Add("F8", GUITexts.get("Fontdialog"));
			this.MinimizeBox = true;
			this.ShowDialog();
		}
		
		public ServiceView(Font font)
		{
			InitializeComponent();
			UpdateTable();
			this.Font = font;
            keydata.Add("F1", GUITexts.get("Help"));
            keydata.Add("F2", GUITexts.get("Log out"));
            keydata.Add("F4", GUITexts.get("Close ServiceView"));
            keydata.Add("F5", GUITexts.get("Update data"));
            keydata.Add("F6", GUITexts.get("Copy data"));
            keydata.Add("F7", GUITexts.get("Show/Hide advanced functions"));
            keydata.Add("F8", GUITexts.get("Fontdialog"));
			this.MinimizeBox = true;
			this.Size = dataGridView.PreferredSize;
			this.ShowDialog();
		}
		
		
		void UpdateTable(){
			dataGridView.Rows.Clear();
			DataGridViewRow row;
			DataGridViewButtonCell button;
			string text;
			foreach(ServiceController controller in ServiceController.GetServices()){
				row = null;
				//try {
				foreach (DataGridViewRow _row in dataGridView.Rows){
					if ((string)_row.Tag == controller.ServiceName)
						_row.HeaderCell.Value = lauf_variable+"";
				}
				//} catch (Exception) {}
				if (row != null){
					row.HeaderCell.Value = lauf_variable+"";
					row.HeaderCell.Tag = lauf_variable;
					try {
						addCell(row, 2, controller.Status.ToString(), "", true);
					} catch (Exception) {}
				}
				else {
					row = dataGridView.Rows[dataGridView.Rows.Add()];
					row.HeaderCell.Tag = lauf_variable;
					row.Tag = controller.ServiceName;
					try {
						addCell(row, 0, controller.ServiceName+"", "", true);
					} catch (Exception) {}
					try {
						addCell(row, 1, controller.DisplayName, "", true);
					} catch (Exception) {}
					try {
						addCell(row, 2, controller.Status.ToString(), "", true);
					} catch (Exception) {}
					if (Pausieren.Visible) {
						try {
							if (controller.CanPauseAndContinue){
								button = new DataGridViewButtonCell();
								if (controller.Status.ToString() == "Running")
									button.Value = GUITexts.get("Pause");
								else
									button.Value = GUITexts.get("Fortsetzen");
								button.Tag = controller.ServiceName;
								button.ToolTipText = GUITexts.get("Pause the service")+" " + controller.ServiceName;
								row.Cells[3] = button;
							}else
                                addCell(row, 3, "     ", GUITexts.get("Unable to pause the service"), true);
							if (controller.CanStop){
								button = new DataGridViewButtonCell();
								button.Value = GUITexts.get("Stop");
								button.Tag = controller.ServiceName;
								button.ToolTipText = GUITexts.get("Stop the service")+" " + controller.ServiceName;
								row.Cells[4] = button;
							}
							else
                                addCell(row, 4, "     ", GUITexts.get("Unable to stop the service"), true);
							if (controller.CanShutdown){
								button = new DataGridViewButtonCell();
                                button.Value = GUITexts.get("Abort");
								button.Tag = controller.ServiceName;
								button.ToolTipText = GUITexts.get("Abort the service")+" " + controller.ServiceName;
								row.Cells[5] = button;
							}
							else
                                addCell(row, 5, "     ", GUITexts.get("Unable to abort the service"), true);
						} catch (Exception) {}
					}
					text = "";
					try {
						foreach(ServiceController contr in controller.DependentServices)
							text += " " + contr.ServiceName + ";";
						addCell(row, dataGridView.Rows.Count - 1, text, "", true);
					}
					catch (Exception) {}
					text = "";
					try {
						foreach(ServiceController contr in controller.ServicesDependedOn)
							text += " " + contr.ServiceName + ";";
						addCell(row, dataGridView.Rows.Count - 1, text, "", true);
					}
					catch (Exception) {}
				}
			}
			foreach (DataGridViewRow _row in dataGridView.Rows){
				if((int)_row.HeaderCell.Tag == lauf_variable-1)
					_row.HeaderCell.Value = lauf_variable+"";
				//dataGridView.Rows.Remove(_row);
			}
			lauf_variable++;
		}
		
		
		
		void addCell(DataGridViewRow row, int stelle, string text, string tag, bool read_only){
			DataGridViewCell cell = row.Cells[stelle];
			cell.Value = text;
			cell.Tag = tag;
			cell.ReadOnly = read_only;
		}
		
		public static void Main(string[] args){
			new ServiceView();
		}
		
		void DataGridView1CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		}
		
		ServiceController SearchController(string name){
			foreach(ServiceController controller in ServiceController.GetServices()){
				if (controller.ServiceName == name)
					return controller;
			}
			return new ServiceController();
		}
		
		void ServiceViewResize(object sender, EventArgs e)
		{
		}
		
		void DataGridViewKeyUp(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F5)
				UpdateTable();
			if(e.KeyCode == Keys.F1)
				new KeyCombinationHelp(keydata, this.Font);
			if(e.KeyCode == Keys.F6){
				string str = "";
				foreach (DataGridViewColumn col in dataGridView.Columns)
					str += string.Format("\"{0}\",", col.HeaderText);
				foreach (DataGridViewRow row in dataGridView.Rows){
					str += "\n";
					foreach (DataGridViewCell cell in row.Cells){
						try {
							str += string.Format("\"{0}\",", cell.Value.ToString());
						} catch (Exception) {}
					}
				}
				(new Computer()).Clipboard.SetText(str);
			}
			if(e.KeyCode == Keys.F6){
				Pausieren.Visible = !Pausieren.Visible;
				Stoppen.Visible = !Stoppen.Visible;
				Schließen.Visible = !Schließen.Visible;
				UpdateTable();
			}
			if(e.KeyCode == Keys.F2)
				Taskmethods.logOut();
			if(e.KeyCode == Keys.F4)
				this.Close();
			if (e.KeyCode == Keys.F8){
				fontDialog.Font = this.Font;
				fontDialog.ShowDialog();
				this.Font = fontDialog.Font;
			}
		}
	}
}
