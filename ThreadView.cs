// Copyright © 2011 Parttimenerd, licenced under the GPL Version 3

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;

namespace TinyTaskkill
{
	/// <summary>
	/// Description of ThreadView.
	/// </summary>
	public partial class ThreadView : Form
	{
		private int lauf_variable = 0;
		Dictionary<string,string> keydata = new Dictionary<string, string>();
		private Dictionary<int, ThreadListener> threadlistener_list = new Dictionary<int, ThreadListener>();
		private Dictionary<int, Thread> thread_list = new Dictionary<int, Thread>();
		private Process _proc;
		public Process proc {
			get { return _proc; }
			set {
				try {
					if (value != null){
						_proc = value;
						foreach (Thread threadl in thread_list.Values)
							threadl.Abort();
						try {
							foreach (ProcessThread threadl in _proc.Threads)
								getProcessThread(threadl.Id);
						} catch (Exception) {}
						try {
							this.Text = "ThreadView (" + _proc.ProcessName + ")";
						}
						catch (Exception){
							try {
								this.Text = "ThreadView (ID: " + _proc.Id + ")";
							}
							catch (Exception) {
								this.Text = "ThreadView";
							}
						}
						UpdateTable();
					}
				} catch (Exception) {}
			}
		}
		
		public ThreadView(Process proc, Font font)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.Font = font;
			this.proc = proc;
			Thread thread = new Thread(cpuThread);
			thread.Start();
			this.MinimizeBox = true;
			UpdateTable();
			this.Size = dataGridView.PreferredSize;
			keydata.Add("F1", GUITexts.get("Help"));
			keydata.Add("F2", GUITexts.get("Log out"));
			keydata.Add("F4", GUITexts.get("Close ThreadView"));
			keydata.Add("F5", GUITexts.get("Update data"));
			keydata.Add("F6", GUITexts.get("Copy data"));
			keydata.Add("F8", GUITexts.get("Fontdialog"));
			this.Visible = true;
			Application.Run();
		}
		
		public static void Main(string[] args){
			//new ThreadView(Process.GetProcessesByName("firefox")[0]);
		}
		
		private void UpdateTable(){
			//dataGridView.Rows.Clear();
			try {
				DataGridViewRow row = null;
				//bool boo;
				foreach (ProcessThread thread in _proc.Threads){
					row = null;
					try {
						foreach (DataGridViewRow _row in dataGridView.Rows){
							if((string)_row.Cells[0].Value == thread.Id + "")
								row = _row;
						}
					} catch (Exception) {}
					if (row != null){
						//row = getRow(thread.Id);
						row.HeaderCell.Tag = lauf_variable;
						try {
							addCell(row, 1, thread.ThreadState.ToString(), "", true);
						} catch (Exception) {}
						try {
							addCell(row, 2, thread.PriorityLevel.ToString(), "", true);
						} catch (Exception) {
							this.Column1.Visible = false;
						}
						try {
							int zahl = getThreadListenerById(thread.Id).cpu_auslastung;
							if (zahl == -100){
								CPU_Auslastung.Visible = false;
								addCell(row, 3, "", "", true);
							}
							else
								addCell(row, 3, zahl + "%", "", true);
						} catch (Exception) {}
						try {
							addCell(row, 4, thread.UserProcessorTime.ToString(), "", true);
						} catch (Exception) {
							this.UProzessorzeit.Visible = false;
						}
						try {
							addCell(row, 5, thread.TotalProcessorTime.ToString(), "", true);
						} catch (Exception) {
							this.TProzessorzeit.Visible = false;
						}
						try {
							addCell(row, 6, thread.WaitReason.ToString(),"", true);
						} catch (Exception) {}
					}
					else {
						row = dataGridView.Rows[dataGridView.Rows.Add()];
						try {
							row.Tag = thread.Id;
							row.HeaderCell.Tag = lauf_variable;
							addCell(row, 0, thread.Id + "", "", true);
							try {
								addCell(row, 1, thread.ThreadState.ToString(), "", true);
							} catch (Exception) {}
							try {
								addCell(row, 2, thread.PriorityLevel.ToString(), "", true);
							} catch (Exception) {}
							try {
								addCell(row, 3, getThreadListenerById(thread.Id).cpu_auslastung + "%", "", true);
							} catch (Exception) {}
							try {
								addCell(row, 4, thread.UserProcessorTime.ToString(), "", true);
							} catch (Exception) {}
							try {
								addCell(row, 5, thread.TotalProcessorTime.ToString(), "", true);
							} catch (Exception) {}
							try {
								addCell(row, 6, thread.WaitReason.ToString(),"", true);
							} catch (Exception) {}
						} catch (Exception) {}
					}
				}
				foreach (DataGridViewRow _row in dataGridView.Rows){
					if((int)_row.HeaderCell.Tag == lauf_variable-1)
						dataGridView.Rows.Remove(_row);
				}
				lauf_variable++;
			} catch (Exception) {}
		}
		
		private	void addCell(DataGridViewRow row, int stelle, string text, string tag, bool read_only){
			DataGridViewCell cell = row.Cells[stelle];
			cell.Value = text;
			cell.Tag = tag;
			cell.ReadOnly = read_only;
		}
		
		private ThreadListener getThreadListenerById(int id){
			try {
				if (threadlistener_list.ContainsKey(id))
					return threadlistener_list[id];
				else {
					ThreadListener listener = new ThreadListener(getProcessThread(id), 4000);
					threadlistener_list.Add(id, listener);
					try {
						if (_proc.ProcessName == "TinyTaskkill")
							return threadlistener_list[id];
					} catch (Exception) {}
					Thread thread = new Thread(listener.start);
					thread_list.Add(id, thread);
					thread.Start();
					return threadlistener_list[id];
				}
			} catch (Exception) {}
			return new ThreadListener();
		}
		
		private void cpuThread(){
			try {
				while(true){
					Invoke(new MethodInvoker(UpdateTable));
					Thread.Sleep(4000);
				}
			}
			catch (Exception) {}
		}
		
		private ProcessThread getProcessThread(int id){
			try {
				foreach (ProcessThread thread in _proc.Threads){
					if (thread.Id == id)
						return thread;
				}
			} catch (Exception) {}
			return _proc.Threads[0];
		}
		
		void DataGridViewKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5)
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

