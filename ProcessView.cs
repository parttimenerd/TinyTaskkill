// Copyright © 2011 Parttimenerd, licenced under the GPL Version 3

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

using Microsoft.VisualBasic.Devices;
using TinyTaskkill;

namespace TinyTaskkill
{
	/// <summary>
	/// Description of ProcessView.
	/// </summary>
	public partial class ProcessView : Form
	{
		DataGridViewCell cpu_cell;
		DataGridViewCell ram_cell;
		DataGridViewCell time_cell;
		DataGridViewCell totaltime_cell;
		DataGridViewCell gesram_cell;
		DataGridViewCell exited_cell;
		DataGridViewCell threads_cell;
		DataGridViewCell windowtitle_cell;
		DataGridViewCell responsing_cell;
		DataGridViewCell procs_cell;
		
		public ProcListItem item_aktuell;
		ProcessListener listener;
		Thread listener_thread;
		Thread cpu_thread;
		Taskmethods taskmethods = new Taskmethods();
		Computer compInfo = new Computer();
		ArrayList proc_list = new ArrayList();
		ThreadView threadview;
		Dictionary<string,string> keydata = new Dictionary<string, string>();
		
		string copy_text = "";
		
		public ProcessView()
		{
			
			InitializeComponent();
			
			UpdateProcList();
			
			ShowPCTable();
			
			
			comboBox.SelectionChangeCommitted += new EventHandler(UpdateTable);
			comboBox.LostFocus += new EventHandler(UpdateProcListEventHandler);
			comboBox.KeyUp += new KeyEventHandler(comboBox_KeyUp);
			dataGridView.TextChanged += new EventHandler(dataGridView_TextChanged);
			dataGridView.AllowUserToResizeColumns = true;
			
			this.MinimizeBox = true;
			this.MaximizeBox = false;
			
			keydata.Add("F1", GUITexts.get("Help"));
            keydata.Add("F2", GUITexts.get("Lock out"));
            keydata.Add("F3", GUITexts.get("Abort the selected process"));
            keydata.Add("F4", GUITexts.get("Abort ProcessView"));
			keydata.Add("F5", GUITexts.get("Update data"));
			keydata.Add("F6", GUITexts.get("Copy data"));
			keydata.Add("F8", GUITexts.get("Fontdialog"));
			
			this.ShowDialog();
		}

		void comboBox_KeyUp(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.F5)
				if (!comboBox.Focused)
					UpdateProcList();
			if (e.KeyCode == Keys.F6)
				Copy_buttonClick();
			if (e.KeyCode == Keys.F1)
				new KeyCombinationHelp(keydata, this.Font);
			if (e.KeyCode == Keys.F2)
				taskmethods.shutdown();
			if (e.KeyCode == Keys.F3){
				try {
					if (item_aktuell.id != -1){
						Close_buttonClick((object)10, new EventArgs());
					}
				} catch (Exception) {}
			}
			if (e.KeyCode == Keys.F4)
				this.Close();
			if (e.KeyCode == Keys.F8){
				fontDialog.Font = this.Font;
				fontDialog.ShowDialog();
				this.Font = fontDialog.Font;
			}
		}
		
		void dataGridView_TextChanged(object sender, EventArgs e)
		{
			DataGridViewCell cell = (DataGridViewCell) sender;
			cell.Value = cell.Value.GetType()+"";
		}
		
		void UpdateTable(object sender, EventArgs e){
			item_aktuell = (ProcListItem)comboBox.SelectedItem;
			if (item_aktuell == null)
				return;
			if (item_aktuell.id == -1) ShowPCTable();
			else {
				if (threadview != null)
					UpdateThreadView();
				threadview_button.Text = "ThreadView...";
				dataGridView.Rows.Clear();
				DataGridViewRow row;
				procs_cell = null;
				try {
					Process proc = Process.GetProcessById(item_aktuell.id);
                    close_button.Text = GUITexts.get("Abort this process");
					copy_text = proc.ProcessName + "\\n";
					addRow(GUITexts.get("Process id"), proc.Id, "", true);
					try {
						addRowS(GUITexts.get("Description"), proc.MainModule.FileVersionInfo.FileDescription,
                                GUITexts.get("Description of the main program"), true);
					} catch (Exception) { }
					try {
						addRowS(GUITexts.get("Version"), proc.MainModule.FileVersionInfo.FileVersion,
                                GUITexts.get("Version of the main program"), true);
					} catch (Exception) { }
					try {
						addRowS(GUITexts.get("Producer"), proc.MainModule.FileVersionInfo.CompanyName,
                                GUITexts.get("Producer of this program"), true);
					} catch (Exception) { }
					try {
						addRowS(GUITexts.get("Comment"), proc.MainModule.FileVersionInfo.Comments,
                                GUITexts.get("Comment of the producer"), true);
					} catch (Exception) { }
					try {
						if (proc.MainWindowTitle.Length > 2){
							dataGridView.Rows.Add(new string[]{ proc.MainWindowTitle });
							row = dataGridView.Rows[dataGridView.Rows.Count-1];
                            row.HeaderCell.Value = GUITexts.get("Title of the window");
                            windowtitle_cell.ToolTipText = GUITexts.get("Title of the main window");
							windowtitle_cell = row.Cells[0];
							windowtitle_cell.ReadOnly = true;
						}
					}
					catch {
						windowtitle_cell = null;
					}
					try {
                        addRow(GUITexts.get("Processpriority"), proc.PriorityClass.ToString());
					}
					catch (Exception) { }
					try {
						dataGridView.Rows.Add(new string[]{ proc.Responding+"" });
						row = dataGridView.Rows[dataGridView.Rows.Count-1];
                        row.HeaderCell.Value = GUITexts.get("Responding");
                        responsing_cell.ToolTipText = GUITexts.get("Says whether GUI of the process responds");
						responsing_cell = row.Cells[0];
						responsing_cell.ReadOnly = true;
					}
					catch {
						responsing_cell = null;
						dataGridView.Rows.RemoveAt(dataGridView.Rows.Count-1);
					}
					try {
						dataGridView.Rows.Add(new string[]{ proc.MainWindowTitle });
						row = dataGridView.Rows[dataGridView.Rows.Count-1];
                        row.HeaderCell.Value = "Abort";
                        exited_cell.ToolTipText = GUITexts.get("Says whether the process has been aborted");
						exited_cell = row.Cells[0];
						exited_cell.ReadOnly = true;
					}
					catch {
						exited_cell = null;
						dataGridView.Rows.RemoveAt(dataGridView.Rows.Count-1);
					}
					try {
						dataGridView.Rows.Add(new string[]{ proc.Threads.Count+"" });
						row = dataGridView.Rows[dataGridView.Rows.Count-1];
                        row.HeaderCell.Value = GUITexts.get("Number of threads");
						threads_cell = row.Cells[0];
						threads_cell.ReadOnly = true;
					}
					catch {
						threads_cell = null;
						if ((string)dataGridView.Rows[dataGridView.Rows.Count-1].HeaderCell.Value == GUITexts.get("Number of threads"))
							dataGridView.Rows.RemoveAt(dataGridView.Rows.Count-1);
					}
					if (listener_thread != null){
						listener_thread.Abort();
						cpu_thread.Abort();
					}
					try {
						listener = new ProcessListener(proc, taskmethods.max_ram, 1000);
						string[] arr = { };
						if (listener.werte_erwartbar()){
							dataGridView.Rows.Add(arr);
							row = dataGridView.Rows[dataGridView.Rows.Count-1];
							row.HeaderCell.Value = GUITexts.get("CPU usage");
							cpu_cell = row.Cells[0];
							cpu_cell.ReadOnly = true;
							dataGridView.Rows.Add(arr);
							row = dataGridView.Rows[dataGridView.Rows.Count-1];
							row.HeaderCell.Value = GUITexts.get("RAM usage");
							ram_cell = row.Cells[0];
							ram_cell.ReadOnly = true;
							listener_thread = new Thread(listener.start);
							listener_thread.Start();
							cpu_thread = new Thread(cpuThread);
							cpu_thread.Start();
							dataGridView.Rows.Add(arr);
							row = dataGridView.Rows[dataGridView.Rows.Count-1];
                            row.HeaderCell.Value = GUITexts.get("Used memory space");
							gesram_cell = row.Cells[0];
							gesram_cell.ReadOnly = true;
							dataGridView.Rows.Add(arr);
							row = dataGridView.Rows[dataGridView.Rows.Count-1];
                            row.HeaderCell.Value = GUITexts.get("UProcessortime");
                            row.HeaderCell.ToolTipText = GUITexts.get("UserProcessortime (hh:mm:ss)");
							time_cell = row.Cells[0];
							time_cell.ReadOnly = true;
							dataGridView.Rows.Add(arr);
							row = dataGridView.Rows[dataGridView.Rows.Count-1];
                            row.HeaderCell.Value = GUITexts.get("Processortime");
                            row.HeaderCell.ToolTipText = GUITexts.get("Total processortime");
							totaltime_cell = row.Cells[0];
							totaltime_cell.ReadOnly = true;
						}
						else {
							cpu_cell = null;
							ram_cell = null;
						}
					}
					catch (Exception) { }
				}
				catch (Exception) { }
			}
		}
		
		void ShowPCTable(){
			item_aktuell = new ProcListItem("", -1);
			threadview_button.Text = GUITexts.get("ServiceView...");
			comboBox.SelectedIndex = proc_list.IndexOf(GUITexts.get("Common"));
			copy_text = "\""+GUITexts.get("Common")+"\"\n";
			close_button.Text = GUITexts.get("Log out");
			dataGridView.Rows.Clear();
			exited_cell = null;
			threads_cell = null;
			windowtitle_cell = null;
			responsing_cell = null;
			DataGridViewRow row;
			try {
				try {
					addRow(GUITexts.get("Computer name"), Environment.MachineName);
					addRow(GUITexts.get("User name"), Environment.UserName);
                    addRow(GUITexts.get("User domain name"), SystemInformation.UserDomainName);
					addRow(GUITexts.get("OS name"), compInfo.Info.OSFullName);
                    addRow(GUITexts.get("OS platform"), compInfo.Info.OSPlatform);
					addRow(GUITexts.get("OS version"), compInfo.Info.OSVersion);
                    addRow(GUITexts.get("Bootmode"), SystemInformation.BootMode.ToString());
                    addRow(GUITexts.get("Ping"), "", "Type network address in", false);
                    addRow(GUITexts.get("Number of monitors"), SystemInformation.MonitorCount);
                    addRow(GUITexts.get("Size of the monitor"), string.Format(GUITexts.get("Height")+": {0}, "+GUITexts.get("Width")+": {1}",
					                                        SystemInformation.PrimaryMonitorSize.Height,
					                                        SystemInformation.PrimaryMonitorSize.Width),
                           GUITexts.get("Size of the primary monitor"), true);
					try {
						string volume = "";
						string label;
						foreach(string str in Environment.GetLogicalDrives()){
							label = compInfo.FileSystem.GetDriveInfo(str).VolumeLabel;
							volume += str;
							if (label != "")
								volume += string.Format(" ({0})", label);
							volume += "; ";
						}
						addRow(GUITexts.get("Volumes"), volume);
					}
					catch { }
				}
				catch (Exception) { }
				dataGridView.Rows.Add(new string[]{});
				row = dataGridView.Rows[dataGridView.Rows.Count-1];
				row.HeaderCell.Value = GUITexts.get("Number of processes");
				procs_cell = row.Cells[0];
				procs_cell.ReadOnly = true;
				dataGridView.Rows.Add(new string[]{});
				row = dataGridView.Rows[dataGridView.Rows.Count-1];
				row.HeaderCell.Value = GUITexts.get("CPU usage");
				cpu_cell = row.Cells[0];
				cpu_cell.ReadOnly = true;
				dataGridView.Rows.Add(new string[]{});
				row = dataGridView.Rows[dataGridView.Rows.Count-1];
				row.HeaderCell.Value = GUITexts.get("RAM usage");
				ram_cell = row.Cells[0];
				ram_cell.ReadOnly = true;
				cpu_thread = new Thread(cpuThread);
				cpu_thread.Start();
			}
			catch (Exception) { }
		}		
		
		void addRow(string header, object val, string tooltip, bool read_only, string tag){
			if (val != null){
				dataGridView.Rows.Add(new string[]{});
				DataGridViewRow row = dataGridView.Rows[dataGridView.Rows.Count-1];
				row.HeaderCell.Value = header;
				row.HeaderCell.ToolTipText = tooltip;
				DataGridViewCell cell = row.Cells[0];
				cell.ReadOnly = read_only;
				cell.Value = val;
				cell.ToolTipText = tooltip;
				cell.Tag = tag;
                if (header != GUITexts.get("Size of the monitor"))
					addCopyText(header, val);
				else {
					string[] str = ((string)val).Replace(" ", "").Split(new char[]{
					                                                    	',',
					                                                    	':'
					                                                    });
                    addCopyText(GUITexts.get("Height of the monitor"), str[1].toInteger());
                    addCopyText(GUITexts.get("Width of the monitor"), str[3].toInteger());
				}
			}
		}
		
		void addCopyText(string header, Object val){
			copy_text += "\"" + header + "\",";
			if (val.GetType() == typeof(string)){
				if ((string)val != "")
					copy_text += "\"" + (string)val + "\"\n";
			}
			if (val.GetType() == typeof(int))
				copy_text += (int)val + "\n";
			if (val.GetType() == typeof(long))
				copy_text += (long)val + "\n";
			if (val.GetType() == typeof(ulong))
				copy_text += (ulong)val + "\n";
			if (val.GetType() == typeof(bool))
				copy_text += (bool)val + "\n";
		}
		
		void addRow(string header, object val, string tooltip, bool read_only){
			addRow(header, val, tooltip, read_only, "");
		}
		
		void addRowS(string header, string val, string tooltip, bool read_only){
			if (val.Length > 2){
				addRow(header, val, tooltip, read_only);
			}
		}
		
		void addRow(string header, object val){
			addRow(header, val, "", true);
		}
		
		void UpdateProcListEventHandler(object sender, EventArgs e){
		}
		
		void UpdateProcList(){
			comboBox.Items.Clear();
			Process[] procs = Process.GetProcesses();
			ProcListItem item = new ProcListItem(GUITexts.get("Common"), -1);
			if (!comboBox.Items.Contains(item))
				comboBox.Items.Add(item);
			for (int i = 0; i < procs.Length; i++) {
				Process proc = procs[i];
				string str;
				try {
					str = string.Format("{0} ({1})", proc.ProcessName, proc.MainModule.FileVersionInfo.FileDescription);
				}
				catch {
					str = proc.ProcessName;
				}
				if (!comboBox.Items.Contains(new ProcListItem(str, proc.Id)))
					comboBox.Items.Add(new ProcListItem(str, proc.Id));
			}
			bool boo = false;
			try {
				int id;
				foreach (object obj in comboBox.Items){
					boo = false;
					id = ((ProcListItem)obj).id;
					foreach (Process proc in Process.GetProcesses()){
						if ( id == proc.Id || id == -1)
							boo = true;
					}
					if (!boo)
						comboBox.Items.Remove(obj);
				}
			} catch (Exception) {}
		}
		
		void DataGridViewCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[0];
			cell.Value = cell.ValueType.FullName;
		}
		
		void DataGridViewRowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
		{
			DataGridViewRow row = e.Row;
			DataGridViewCell cell = row.Cells[0];
			string str = (string)row.HeaderCell.Value;
			try {
				if (str == "Ping") {
					string text = (string)cell.Value;
					if (text == "" || text.Contains(GUITexts.get("reponds")))
						return;
					try {
						if (compInfo.Network.Ping(text))
							cell.Value = text + " "+GUITexts.get("responds");
						else
							cell.Value = text + " "+GUITexts.get("doesn't respond");
					}
					catch (Exception) {
                        cell.Value = text + " " + GUITexts.get("doesn't respond");
					}
				}
			}
			catch (Exception) { }
		}
		
		void cpuThread(){
			try {
				while(true){
					Invoke(new MethodInvoker(UpdateValues));
					Thread.Sleep(1000);
				}
			}
			catch (Exception) {}
		}
		
		void UpdateValues(){
			if(cpu_cell != null){
				if (listener != null){
					cpu_cell.Value = listener.cpu_auslastung+"%";
					ram_cell.Value = listener.ram_auslastung+"%";
					time_cell.Value = listener.proc.UserProcessorTime.ToString();
					gesram_cell.Value = listener.proc.WorkingSet64/1024+"kB";
					totaltime_cell.Value = listener.proc.TotalProcessorTime.ToString();
				}
				else {
					cpu_cell.Value = (int)taskmethods.getCPU()+"%";
					ram_cell.Value = (int)taskmethods.getRAM()+"%";
				}
			}
			if (procs_cell != null){
				procs_cell.Value = Process.GetProcesses().Length;
			}
			if (exited_cell != null){
				if (listener.proc.HasExited) exited_cell.Value = GUITexts.get("Yes");
				else exited_cell.Value = GUITexts.get("No");
			}
			if (threads_cell != null){
				try {
					threads_cell.Value = listener.proc.Threads.Count;
				}
				catch (Exception) {
					threads_cell.Value = "-";
				}
			}
			if (windowtitle_cell != null){
				try {
					windowtitle_cell.Value = listener.proc.MainWindowTitle;
				} catch (Exception) { }
			}
			if (responsing_cell != null){
				if (listener.proc.Responding) responsing_cell.Value = GUITexts.get("Yes");
				else  responsing_cell.Value = GUITexts.get("No");
			}
		}
		
		void Close_buttonClick(object sender, EventArgs e)
		{
			close_button.Show();
			if (item_aktuell == null){
				taskmethods.shutdown();
				return;
			}
			if (item_aktuell.id	== -1)
				taskmethods.shutdown();
			else {
				try {
					Process proc =  Process.GetProcessById(item_aktuell.id);
					proc.Kill();
					Thread.Sleep(300);
					if (proc.HasExited){
						UpdateProcList();
						ShowPCTable();
						return;
					}
					else {
						proc.Kill();
						Thread.Sleep(300);
						if (proc.HasExited){
							UpdateProcList();
							ShowPCTable();
							return;
						}
					}
				}
				catch (Exception) {}
				UpdateProcList();
			}
		}
		
		void Copy_buttonClick()
		{
			//UpdateThreadView();
			string str = "";
			try {
				str += string.Format("\"{0}\",\"{1}\"\n", dataGridView.Columns[0].HeaderText,
				                     dataGridView.Columns[1].HeaderText);
			} catch (Exception) {}
			foreach (DataGridViewRow row in dataGridView.Rows){
				try {
					str += string.Format("\"{0}\",\"{1}\"\n", row.HeaderCell.Value.ToString(),
					                     row.Cells[0].Value.ToString());
				} catch (Exception) {}
			}
			compInfo.Clipboard.SetText(str);
		}
		
		void ProcessViewResize(object sender, EventArgs e)
		{
			comboBox.Width = this.Width - 15;
			comboBox.DropDownHeight = comboBox.Width;
			comboBox.Height = (int)(comboBox.PreferredSize.Height);
			int height = (int)(comboBox.PreferredSize.Height * 1.1);
			int width = (int)((this.Width - 18) / 2);
			dataGridView.Location = new Point(0, comboBox.Height + 3);
			dataGridView.Width = this.Width - 17;
			dataGridView.Height = this.Height - (comboBox.Height + 3) - (height + 6 + 40);
			int x = dataGridView.Height + dataGridView.Bounds.X + (int)((this.Height - dataGridView.Height - dataGridView.Bounds.X) / 3);
			close_button.Location = new Point(0, x);
			close_button.Width = width;
			close_button.Height = height;
			threadview_button.Location = new Point(width + 6, x);
			threadview_button.Width = width;
			threadview_button.Height = height;
		}
		
		void Threadview_buttonClick(object sender, EventArgs e)
		{
			try {
				if(item_aktuell != null){
					if (item_aktuell.id != -1)
						threadview = new ThreadView(Process.GetProcessById(item_aktuell.id), this.Font);
					else
						new ServiceView(this.Font);
				}
			} catch (Exception) {}
		}
		
		void UpdateThreadView(){
		}
		
		void ProcessViewFormClosing(object sender, FormClosingEventArgs e)
		{
			if (threadview != null)
				threadview.Close();
		}
	}
	
	public class ProcListItem {
		
		private string _text;
		public int id;
		
		public ProcListItem(string text, int id){
			_text = text;
			this.id = id;
		}
		
		public override string ToString()
		{
			return _text;
		}
	}
}
