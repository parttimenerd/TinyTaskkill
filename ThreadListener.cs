/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Johannes
 * Datum: 02.03.2011
 * Zeit: 19:01
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */

using System;
using System.Diagnostics;
using System.Threading;

namespace TinyTaskkill
{
	/// <summary>
	/// ThreadListener sind dafür da, um von einem bestimmten ProzessThread
	/// die CPU-Auslastung herrauszufinden
	/// </summary>
	public class ThreadListener
	{
		protected double _cpu_auslastung = 0;
		public int cpu_auslastung {
			get {
				return (int) (_cpu_auslastung * 100);
			}
		}
		protected int _aktualisierungsintervall; // in ms
		protected ProcessThread _thread;
		public ProcessThread thread {
			get { return _thread; }
		}
		protected int _id; 
		public int id {
			get { return _id; }
		}
		
		/// <param name="thread"></param>Thread, dessen CPU-Auslastung das Programm herrausfinden soll </param>
		/// <param name="aktualisierungsintervall">gibt die Pause zwischen zwei Messungen in ms an</param>
		public ThreadListener (ProcessThread thread, int aktualisierungsintervall){
			_aktualisierungsintervall = aktualisierungsintervall;
			_thread = thread;
			_id = thread.Id;
		}
		
		/// <param name="thread"></param>Thread, dessen CPU-Auslastung das Programm herrausfinden soll </param>
		public ThreadListener (ProcessThread thread){
			_aktualisierungsintervall = 4000;
			_thread = thread;
			_id = thread.Id;
		}
		
		public ThreadListener (){
			_aktualisierungsintervall = 4000;
			_thread = Process.GetCurrentProcess().Threads[0];
			_id = _thread.Id;
		}
		
		/// <summary>
		/// Messschleife
		/// </summary>
		public void start(){
			double _cpu_lastValue;
			try {
				while (true){
					_cpu_lastValue =_thread.UserProcessorTime.TotalMilliseconds;
					Thread.Sleep(_aktualisierungsintervall);
					_cpu_auslastung = (_thread.UserProcessorTime.TotalMilliseconds - _cpu_lastValue) / _aktualisierungsintervall;
				}
			}
			catch (Exception){
				_cpu_auslastung = -1;
			}
		}
		
		public bool werte_erwartbar(){
			try {
				double zahl = _thread.UserProcessorTime.TotalMilliseconds;
				return true;
			}
			catch (Exception) {
				return false;
			}
		}
	}
}
