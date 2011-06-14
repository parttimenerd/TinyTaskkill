// Copyright © 2011 Parttimenerd, licenced under the GPL Version 3

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace TinyTaskkill
{
	partial class GUI : Form
	{
		static NotifyIcon notifyIcon = new NotifyIcon();
		static NotifyIcon notifyIcon_expert = new NotifyIcon();
		static ContextMenu contextMenu = new ContextMenu();
		static ContextMenu contextMenu_expert = new ContextMenu();
		static MenuItem taskmenu = new MenuItem();
		static MenuItem extmenu = new MenuItem();
		static MenuItem automatic = new MenuItem();
		static MenuItem optmenu = new MenuItem();
		static Taskmethods taskmethods = new Taskmethods();
		static MenuItem procview_menu = new MenuItem();
		static MenuItem serviceview_menu = new MenuItem();
		static ulong max_ram;
		static Thread thread;
		static bool expertmodeval;
		static bool expertmode {
			get { return expertmodeval; }
			set {
				if (expertmodeval != value){
					expertmodeval = value;
							procview_menu.Visible = expertmodeval;
							serviceview_menu.Visible = expertmodeval;
				}
			}
		}
		
		public GUI()
		{
		}

		[STAThread]
		static void Main(string[] astrArg)
		{
            new GUITexts();

			notifyIcon.Visible = true;
			
			notifyIcon_expert.Visible = expertmode;
			notifyIcon_expert.ContextMenu = contextMenu_expert;
			
			thread = new Thread(new ThreadStart(updateIcon));
			thread.Name = "UpdateIcon";
			thread.Priority = ThreadPriority.BelowNormal;
			thread.Start();

			string username = Environment.UserName;
			
			max_ram = taskmethods.max_ram;
		
			if (username.Contains(".")) username = username.Replace("."," ");
			MenuItem item = new MenuItem();
			item.Text = GUITexts.get("Exit");
			item.Click += new System.EventHandler(Exit);
			contextMenu.MenuItems.Add(item);
			item = new MenuItem();
			item.Text = GUITexts.get("Licence information");
			item.Click += new System.EventHandler(Lizenz);
			contextMenu.MenuItems.Add(item);
			optmenu.Text = GUITexts.get("Preferences");
			contextMenu.MenuItems.Add(optmenu);
			item = new MenuItem();
			if (expertmode){
				item.Text = GUITexts.get("Turn expertmode out");
			}
			else {
                item.Text = GUITexts.get("Turn expertmode on");
			}
			item.Click += new EventHandler(opt_expertmode);
			optmenu.MenuItems.Add(item);
            taskmenu.Text = GUITexts.get("Abort a process");
			extmenu.Text = GUITexts.get("PC functions");
			item = new MenuItem();
			item.Text = GUITexts.get("Log out");
			item.Click += new EventHandler(LogOut);
			extmenu.MenuItems.Add(item);
			contextMenu.MenuItems.Add(extmenu);
			procview_menu.Text = "ProcessView";
			procview_menu.Visible = expertmode;
			procview_menu.Click += new EventHandler(procview_menu_Click);
			contextMenu.MenuItems.Add(procview_menu);
			serviceview_menu.Text = "ServiceView";
			serviceview_menu.Visible = expertmode;
			serviceview_menu.Click += new EventHandler(serviceview_menu_Click);
			contextMenu.MenuItems.Add(serviceview_menu);
			contextMenu.MenuItems.Add(taskmenu);

            notifyIcon.MouseClick += new MouseEventHandler(MenuListener);
			notifyIcon.ContextMenu = contextMenu;
			UpdateTaskmenu();
			Application.Run ();
		}

		static void serviceview_menu_Click(object sender, EventArgs e)
		{
			new ServiceView();
		}

		static void procview_menu_Click(object sender, EventArgs e)
		{
			new ProcessView();
		}

		private static void updateIcon(){
			try {
				int height = 16;
				Bitmap bitmap = new Bitmap(height, height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
				Graphics g = Graphics.FromImage(bitmap);
				IntPtr iconHandle;
				float cpu;
				double ram = taskmethods.getRAM();
				string cputext = (int)taskmethods.getCPU() + "";
				if (cputext.Length == 1) cputext = " "+cputext;
				int y;
				int y2;
				float longcpu;
				System.Drawing.Icon icon;
				string ramtext = (int)ram+"";
				if (ramtext.Length == 1) ramtext = " "+ramtext;
				while (true){
					longcpu = 0;
					for(int i=0; i<2; i++){
						cpu = taskmethods.getCPU();
						Thread.Sleep(500);
						y = (int)(height*cpu/100);
						y2 = height-(int)(ram*height/100);
						g.Clear(Color.DarkGreen);
						g.FillRectangle(Brushes.LightGreen, 0, height-y, height, y);
						g.DrawLine(Pens.Orange, 0, y2, height, y2);
						iconHandle = bitmap.GetHicon();
						icon = Icon.FromHandle(iconHandle);
						notifyIcon.Icon = icon;
						longcpu = longcpu + cpu;
					}
					cputext = (int)(longcpu/2) + "";
					if (cputext.Length == 1) cputext = " "+cputext;
					ram = taskmethods.getRAM();
					ramtext = (int)ram+"";
					if (ramtext.Length == 1) ramtext = " "+ramtext;
                    notifyIcon.Text = GUITexts.get("CPU usage") + ": " + cputext + "% | " + GUITexts.get("RAM usage") + ": " + ramtext + "%";
				}
			}
			catch(Exception){}
		}
		
		private static void Exit(Object sender, EventArgs e)
		{
			notifyIcon.Dispose();
			Process.GetCurrentProcess().Kill();
		}

		private static void Lizenz(Object sender, EventArgs e)
		{
			Informations infos = new Informations();
			infos.Show();
		}


		
		private static void MenuListener(Object sender, MouseEventArgs e)
		{
			try {
				thread.Abort();
			} catch (Exception) {}
			thread = new Thread(new ThreadStart(updateIcon));
			thread.Name = "UpdateIcon";
			thread.Priority = ThreadPriority.BelowNormal;
			thread.Start();
			UpdateTaskmenu();
		}
		
		private static void UpdateTaskmenu(){
			MenuItem item;
			taskmenu.MenuItems.Clear();
			int index = 0;
			int kaputteProzAnz = 0;
			MenuItem kaputteProzesse = new MenuItem();
			kaputteProzesse.Text = GUITexts.get("Not working processes");
			kaputteProzesse.Index = index;
			taskmenu.MenuItems.Add(kaputteProzesse);
			index++;
			ArrayList list = taskmethods.getTasklist();
			Process[] process = Process.GetProcesses();
			foreach (string[] arr in list)
			{
				try
				{
					item = new MenuItem();
					item.Text = arr[0];
					if (arr[3] != "") item.Text += " (" + arr[3] + ")";
					item.Name = arr[1] + "";
					item.Index = index;
					index++;
					if (arr[2] == "1")
					{
						item.Click += new EventHandler(killProcess);
						kaputteProzesse.MenuItems.Add(item);
						kaputteProzAnz++;
					}
					item.Click += new EventHandler(killProcess);
					taskmenu.MenuItems.Add(item);
				}
				catch (Exception) { }
			}
			if (kaputteProzAnz == 0)
			{
				taskmenu.MenuItems.RemoveAt(0);
				automatic.Visible = false;
			}
			else automatic.Visible = true;
			if (index < 3) UpdateTaskmenu();
			taskmenu.Visible = true;
			contextMenu.MenuItems.Add(taskmenu);
		}
		
		private static void killProcess(Object sender, EventArgs e)
		{
			try
			{
				MenuItem menu = (MenuItem)sender;
                if (menu.Text == GUITexts.get("Abort this process"))
                {
					Process proc = Process.GetProcessById(menu.Name.toInteger());
					menu.Text = proc.ProcessName;
					try {
						menu.Text += "("+proc.MainModule.FileVersionInfo.FileDescription+")";
					}
					catch (Exception) {}
				}
				if (true)
				{
					try
					{
						int id = Convert.ToInt32(menu.Name, 10);
						Process proc = Process.GetProcessById(id);
						proc.Kill();
						System.Threading.Thread.Sleep(100);
						if (!proc.HasExited) proc.Kill();
						System.Threading.Thread.Sleep(200);
						if (proc.HasExited)
						{
                            notifyIcon.ShowBalloonTip(4, GUITexts.get("Successfully aborted"), GUITexts.get("Aborting the process") + " " + menu.Text + " " + GUITexts.get("was successful") + ".", ToolTipIcon.Info);
						}
						else
						{
                            notifyIcon.ShowBalloonTip(4, GUITexts.get("Unsuccessfully aborted"), GUITexts.get("Aborting the process") + " " + menu.Text + " " + GUITexts.get("was unsuccesful, maybe you aren't allowed to close this process") + ".", ToolTipIcon.Error);
						}
					}
					catch (Exception)
					{
                        notifyIcon.ShowBalloonTip(4, GUITexts.get("Unsuccessfully aborted"), GUITexts.get("Aborting the process") + " " + menu.Text + " " + GUITexts.get("was unsuccesful, maybe you aren't allowed to close this process") + ".", ToolTipIcon.Error);
					}
				}
			}
			catch (Exception)
			{
                notifyIcon.ShowBalloonTip(4, GUITexts.get("Unsuccessfully aborted"), GUITexts.get("Unknown error"), ToolTipIcon.Error);
			}
		}

		private static void LogOut(Object sender, EventArgs e)
		{
			Taskmethods.logOut();
		}

		private static void ShutDown(Object sender, EventArgs e)
		{
			taskmethods.shutdown();
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// GUI
			// 
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Name = "GUI";
			this.ResumeLayout(false);

		}

		private static void Automatic(Object sender, EventArgs e)
		{
			string str = GUITexts.get("Aborting the following processes was succesful")+": ";
			ArrayList arr = taskmethods.killBadProcesses();
			if (arr.Count > 0)
			{
				foreach (Object obj in arr)
				{
					try
					{
						Process proc = (Process)obj;
						str += proc.ProcessName + "(" + proc.MainModule.FileVersionInfo + "), ";
					}
					catch { }
				}
				str = str.Substring(0, str.Length - 2);
				notifyIcon.ShowBalloonTip(3000, GUITexts.get("Successfully aborted"), str, ToolTipIcon.Info);
			}
			else
			{
				notifyIcon.ShowBalloonTip(4000, GUITexts.get("Unsuccessfully aborted"), GUITexts.get("Closing the process/es was unsuccesful")+".", ToolTipIcon.Error);
			}
		}
		
		private static void opt_expertmode(Object sender, EventArgs e){
			MenuItem item = (MenuItem) sender;
			if (expertmode){
				expertmode = false;
				item.Text = GUITexts.get("Turn expertmode on");
			}
			else {
				expertmode = true;
                item.Text = GUITexts.get("Turn expertmode out");
			}
		}
	}
}