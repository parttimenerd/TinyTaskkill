// Copyright © 2011 Parttimenerd, licenced under the GPL Version 3

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Microsoft.VisualBasic.Devices;
using System.Resources;
using System.Reflection;

namespace TinyTaskkill
{
	/// <summary>
	/// Description of Preferences.
	/// </summary>
	public class GUITexts
	{
		private static Dictionary<string,string> dictionary = new Dictionary<string,string>();
        private static string resource = @"
en=de
Exit=Schließen
Licence information=Lizenzinfos
Preferences=Einstellungen
Turn expertmode out=Expertenmodus ausschalten
Turn expertmode on=Expertenmodus anschalten
Abort a process=Prozess schließen
PC functions=PC Funktionen
Log out=Abmelden
CPU usage=CPU-Auslastung
RAM usage=RAM-Auslastung
Not working processes=Funktionsunfähige Prozesse
Abort this process=Diesen Prozess schließen
Successfully aborted=Schließen erfolgreich
Aborting the process=Schließen des Prozesses
was successful=erfolgreich
Unsuccessfully aborted=Schließen fehlgeschlagen
was unsuccesful, maybe you aren't allowed to close this process=fehlgeschlagen, womöglich besitzen sie keine ausreichenden Rechte
Unknown error=Unbekannter Fehler
Aborting the following processes was succesful=Das Schließen der folgenden Prozesse war erfolgreich
Aborting the process/es was unsuccesful=Das Schließen der/des Prozesse/s ist fehlgeschlagen
Help=Hilfe
Key=Taste
Combination=Kombination
Event=Ereignis
Abort the selected process=Prozess schließen
Abort ProcessView=ProcessView schließen
Update data=Aktualisierung der Daten
Copy data=Kopieren der Daten
Fontdialog=Schriftdialog
Process id=Prozess-Id
Description=Beschreibung
Description of the main program=Beschreibung des Hauptprogrammes
Version=Version
Version of the main program=Version des Hauptprogrammes
Producer=Hersteller
Producer of this program=Hersteller der Programmes
Comment=Kommentar
Comment of the producer=Kommentar des Herstellers zum Hauptprogramm
Title of the window=Fenstertitel
Title of the main window=Titel des Hauptfensters
Processpriority=Prozesspriorität
Responding=Antwortend?
Says whether GUI of the process responds=Gibt an ob die GUI des Prozesses noch antwortet
Aborted?=Geschlossen?
Says whether the process has been aborted=Gibt an ob der Prozess geschlossen wurde
Number of threads=Threadanzahl
Used memory space=Speicherbedarf
UProcessortime=UProzessorzeit
UserProcessortime (hh:mm:ss)=UserProzessorzeit (hh:mm:ss)
Processortime=Prozessorzeit
Total processortime=Gesammte Prozessorzeit (hh:mm:ss)
Common=Allgemein
Computer name=Computername
User name=Benutzername
User domain name=Benutzer-Domain
OS name=OS-Name
OS platform=OS-Plattform
OS version=OS-Version
Bootmode=Bootmode
Ping=Ping
Type network address in=Netzerkaddresse eingeben
Number of monitors=Bildschirmanzahl
Size of the monitor=Bildschirmgröße
Size of the primary monitor=Größe des primären Bildschirms
Height=Höhe
Width=Breite
Volumes=Laufwerke
Number of processes=Prozessanzahl
Height of the monitor=Bildschirmhöhe
Width of the monitor=Bildschirmbreite
responds=anwortet
doesn't respond=antwortet nicht
Yes=Ja
No=Nein
Close ServiceView=ServiceView schließen
ID=ID
Name=Name
ID of the thread=ID des Threads
State=Status
State of the thread=Status des Threads
Priority=Priorität
Priority of the thread=Priorität des Threads
CPU usage of the thread=Prozessorauslastung des Threads
Total UserProcessortime=UserProzessorzeit, die der Threads bis jetzt im gesammten verbraucht hat
Total Processortime=Prozessorzeit, die der Threads bis jetzt im gesammten verbraucht hat
Reason for waiting=Wartegrund
Reason, why the thread is waiting=Grund, warum der Thread wartet
Close ThreadView=ThreadView schließen
Close ServiceView=ServiceView schließen
Show/Hide advanced functions=Erweiterte Funktionen an/aus
Pause=Pausieren
Pause the service=Pausieren des Dienstes
Unable to pause the service=Pausieren des Dienstes nicht möglich
Stop=Stoppen
Stop the service=Stoppen des Dienstes
Unable to stop the service=Stoppen des Dienstes nicht möglich
Abort=Schließen
Abort the service=Schließen des Dienstes
Unable to abort the service=Schließen des Dienstes nicht möglich
Name of the service=Name des Dienstes
Description=Beschreibung
Description of the service=Beschreibung des Dienstes
State of the service=Status des Dienstes
Depending services=Abhängige Dienste
Services which are dependent on this one=Dienste, die von diesem Dienst abhängig sind
Dependents on services=Von folgenden Diensten Abhängig
Services, this one dependents on=Dienste, von diesen dieser Dienst abhängig ist
      ";
		
		public GUITexts()
		{
			load(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag);
		}
		
		public static void load(string language){
                string[] lines = resource.Split('\r');
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Length > 3 && lines[i].Contains("="))
                    {
                        string[] val = lines[i].Split('\n')[1].Split('=');
                        if (language == "de-DE")
                        {
                            try
                            {
                                dictionary.Add(val[0], val[1]);
                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            dictionary.Add(val[0], val[0]);
                        }
                    }
                }
		}
		
		public static string get(string englishtext){
			if (dictionary.ContainsKey(englishtext)){
				return dictionary[englishtext];
			}
			return englishtext;
		}
	}
}
