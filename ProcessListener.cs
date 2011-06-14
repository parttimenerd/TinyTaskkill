// Copyright © 2011 Parttimenerd, licenced under the GPL Version 3

using System;
using System.Diagnostics;
using System.Threading;

namespace TinyTaskkill
{
	public class ProcessListener
	{
		protected double _cpu_auslastung = 0;
		protected double _ram_auslastung = 0;
		public int cpu_auslastung {
			get {
				return (int) (_cpu_auslastung * 100);
			}
		}
		public int ram_auslastung {
			get {
				return (int) (_ram_auslastung * 100);
			}
		}
		protected int _aktualisierungsintervall; // in ms
		public int aktualisierungsintervall {
			get { return _aktualisierungsintervall; }
			set {
				if (value >= 0)
					_aktualisierungsintervall = value;
			}
		}
		protected Process _proc;
		public Process proc {
			get { return _proc; }
		}
		protected double _max_ram;
		protected int _id; 
		public int id {
			get { return _id; }
		}
		
		public ProcessListener (Process proc, ulong max_ram, int aktualisierungsintervall){
			_aktualisierungsintervall = aktualisierungsintervall;
			_proc = proc;
			_max_ram = max_ram;
			_id = proc.Id;
		}
		
		public ProcessListener (Process proc, ulong max_ram){
			_aktualisierungsintervall = 4000;
			_proc = proc;
			_max_ram = max_ram;
			_id = proc.Id;
		}
		
		public ProcessListener (){
			_aktualisierungsintervall = 4000;
			_proc = Process.GetCurrentProcess();
			_max_ram = 10000;
			_id = _proc.Id;
		}
		
		public void start(){
			double _cpu_lastValue;
			try {
				while (!_proc.HasExited){
					_cpu_lastValue =_proc.UserProcessorTime.TotalMilliseconds;
					Thread.Sleep(_aktualisierungsintervall);
					_cpu_auslastung = (_proc.UserProcessorTime.TotalMilliseconds - _cpu_lastValue) / _aktualisierungsintervall;
					_ram_auslastung = _proc.WorkingSet64 / _max_ram;
				}
			}
			catch (Exception){
				_cpu_auslastung = -1;
				_ram_auslastung = -1;
			}
		}
		//Getting values expected?
		public bool werte_erwartbar(){
			try {
				double zahl = _proc.UserProcessorTime.TotalMilliseconds;
				zahl = _proc.WorkingSet64 / _max_ram;
				return true;
			}
			catch (Exception) {
				return false;
			}
		}
	}
}
