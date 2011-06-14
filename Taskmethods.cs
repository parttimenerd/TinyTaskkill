// Copyright © 2011 Johannes Bechberger, lizensiert unter der GPL Version 3

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.Devices;

namespace TinyTaskkill
{
	class Taskmethods
	{
		
		private PerformanceCounter cpuCounter;
		private PerformanceCounter ramCounter;
		private ulong _max_ram;
		public ulong max_ram {
			get { return _max_ram; }
		}
		private float lastValue = 0;
		private ComputerInfo _compInfo;
		
		public Taskmethods(){
			cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
			ramCounter = new PerformanceCounter("Memory", "Available Bytes");
			_compInfo = new ComputerInfo();
			_max_ram = _compInfo.TotalPhysicalMemory;
		}
		
		/// <summary>
		/// Prozessorauslastung, funktioniert nicht
		/// </summary>
		/// <returns>Prozessorauslastung in Prozent</returns>
		public float getCPU()
		{
			float val = cpuCounter.NextValue();
			if(val != 100 && val != 0){
				lastValue = val;
				return val;
			}
			else {
				System.Threading.Thread.Sleep(50);
				return cpuCounter.NextValue();
			}
		}

		/// <summary>
		/// Arbeitsspeicherauslastung, funktioniert nicht
		/// </summary>
		/// <returns>Arbeitsspeicherauslastung in Prozent</returns>
		public double getRAM(){
			float ram = ramCounter.NextValue();
			return ((float)_max_ram-ram)/_max_ram*100.0;
		}
		
		/// <summary>
		/// Gibt die Prozessliste als ArrayList zurück,
		/// </summary>
		/// <returns>Schema: {{Name, ID, Responding (0=true, 1=false), Description}}, sortiert</returns>
		public ArrayList getTasklist()
		{
			ArrayList arr = new ArrayList();
			//ArrayList hilfarr = new ArrayList();
			ArrayList retarr = new ArrayList();
			Process[] process = Process.GetProcesses();
			string str = "";
			foreach (Process proc in process)
			{
				str = proc.ProcessName;
				str += "|" + proc.Id + "|";
				try
				{
					if (proc.Responding) str += "0";
					else str += "1";
				}
				catch (Exception)
				{
					str += "0";
				}
				try
				{
					str += "|" + proc.MainModule.FileVersionInfo.FileDescription;
				}
				catch (Exception)
				{
					str += "|";
				}
				arr.Add(str);
			}
			arr.Sort();
			char[] splitarr = {'|'};
			//string[] textarr;
			foreach (string text in arr)
			{
				retarr.Add(text.Split(splitarr));
			}
			return retarr;
		}

		/// <summary>
		/// Versucht alle Prozesse zu schließen, deren Benutzeroberfläche nicht
		/// mehr antwortet.
		/// </summary>
		/// <returns>List der erfolgreich geschlossenen Prozesse</returns>
		public ArrayList killBadProcesses()
		{
			ArrayList retarr = new ArrayList();
			try
			{
				foreach (Process proc in Process.GetProcesses())
				{
					try
					{
						if (!proc.Responding && proc.MainModule.FileVersionInfo.FileDescription.Length > 5)
						{
							proc.Kill();
							System.Threading.Thread.Sleep(100);
							if (!proc.HasExited) proc.Kill();
							System.Threading.Thread.Sleep(100);
							if (proc.HasExited) retarr.Add(proc);
						}
					}
					catch (Exception) { }
				}
			}
			catch (Exception) { }
			return retarr;
		}

		/// <summary>
		/// Gibt an, ob Prozesse verhanden sind, deren Benutzeroberfläche nicht antwortet.
		/// </summary>
		public bool BadProcessesAvaliable()
		{
			bool avaliable = false;
			try
			{
				foreach (Process proc in Process.GetProcesses())
				{
					try
					{
						if (!proc.Responding && proc.MainModule.FileVersionInfo.FileDescription.Length > 5)
						{
							avaliable = true;
						}
					}
					catch (Exception) { }
				}
			}
			catch (Exception) { }
			return avaliable;
		}
		
		/// <summary>
		/// Schließen des Prozesses mit der ID pid
		/// </summary>
		/// <param name="pid">Prozessor-ID</param>
		/// <returns>"1" wenn erfolgreich, "0" wenn nicht</returns>
		public string taskkill(string pid)
		{
			try
			{
				Process proc = Process.GetProcessById(Convert.ToInt32(pid, 10));
				proc.Kill();
				if (proc.HasExited) return "1";
				else return "0";
			}
			catch (Exception)
			{
				return "0";
			}
		}

		// Einbinden der benötigten dll zum Abmelden
		[DllImport("user32.dll", ExactSpelling=true, SetLastError=true)]
		internal static extern bool ExitWindowsEx(int flg, int rea);

		/// <summary>
		/// Meldet den Benutzer ab
		/// </summary>
		public static void logOut()
		{
			
			ExitWindowsEx(0x0, 0x0);
		}

		/// <summary>
		/// Fährt den PC herrunter
		/// </summary>
		public void shutdown()
		{
			performShutDown(4);
		}

		/// <summary>
		/// Startet den PC neu
		/// </summary>
		public void reboot()
		{
			performShutDown(2);
		}

		/// <summary>
		/// Schaltet den PC aus (wenn möglich)
		/// </summary>
		public void turnoff()
		{
			performShutDown(8);
		}

		/// <summary>
		/// Fährt den PC herrunter
		/// </summary>
		/// <param name="flag">Herrunterfahrenoption:
		///  0 = Den PC vom Netzwerk abklemmen.
		///  1 = Herrunterfahren.
		///  2 = Neustarten.
		///  4 = Hartes herrunterfahren (alle Anwendungen werden abrupt geschlossen).
		///  8 = Herrunterfahren und, wenn möglich, ausschalten.
		/// </param>
		private void performShutDown(int flag)
		{
			ManagementBaseObject outParameters = null;
			ManagementClass sysOS = new ManagementClass("Win32_OperatingSystem");
			sysOS.Get();
			sysOS.Scope.Options.EnablePrivileges = true;
			ManagementBaseObject inParameters = sysOS.GetMethodParameters("Win32Shutdown");
			inParameters["Flags"] = flag+"";
			inParameters["Reserved"] = "0";
			foreach (ManagementObject manObj in sysOS.GetInstances())
			{
				outParameters = manObj.InvokeMethod("Win32Shutdown", inParameters, null);
			}
		}

		//        [DllImport("user32.dll")]
		//        privateextern IntPtr GetForegroundWindow();

		//        public Process getForegroundWindowProcess()
		//        {
		//            IntPtr ptr = GetForegroundWindow();
		//            foreach (Process proc in Process.GetProcesses())
		//            {
		//                try
		//                {
		//                    if (proc.MainWindowHandle == ptr) return proc;
		//                }
		//                catch (Exception) { }
		//            }
		//            return new Process();
		//        }
	}
}
