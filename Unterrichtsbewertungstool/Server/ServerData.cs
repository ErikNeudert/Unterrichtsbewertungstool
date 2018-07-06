using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Concurrent;

namespace Unterrichtsbewertungstool
{
    /// <summary>
    /// Wrapper Classe die die gesammelten Daten eines Servers zusammenfasst.
    /// </summary>
    public class ServerData
    {
        /// <summary>
        /// Der Speicher der die Daten der Clients speichert.
        /// </summary>
        private ConcurrentDictionary<string, ClientData> Data { get; set; }

        public ServerData()
        {
            Data = new ConcurrentDictionary<string, ClientData>();
        }

        /// <summary>
        /// Fügt dem Datenbestand die gegebene Bewertung hinzu.
        /// </summary>
        /// <param name="clientKey">Der key der den Client bestimmt</param>
        /// <param name="bewertung">Die Bewertung des Clients</param>
        public void AddBewertung(string clientKey, Bewertung bewertung)
        {
            if (clientKey == null)
            {
                throw new ArgumentNullException(nameof(clientKey));
            }

            if (Data.ContainsKey(clientKey))
            {
                Data.TryGetValue(clientKey, out ClientData cdata);
                cdata.bewertungen.Add(bewertung);
            }
            else
            {
                ClientData cdata = new ClientData();
                cdata.bewertungen.Add(bewertung);
                Data.TryAdd(clientKey, cdata);
            }
        }

        /// <summary>
        /// Löscht alle Daten des Clients aus dem Datenbestand
        /// </summary>
        /// <param name="clientKey">Der Key der den Client identifiziert</param>
        public void RemoveClient(string clientKey)
        {
            if (Data.ContainsKey(clientKey))
            {
                Data.TryRemove(clientKey, out ClientData unused);
            }

        }

        /// <summary>
        /// Sammelt die Bewertungen, die der Client noch nicht hat und ob anonymisiert sie.
        /// 
        /// </summary>
        /// <param name="ipPort"></param>
        /// <returns></returns>
        public Dictionary<int, List<Bewertung>> GetBewertungen(string ipPort)
        {
            Dictionary<int, List<Bewertung>> obfuscatedDict = new Dictionary<int, List<Bewertung>>();
            
            //Bestimmung der letzen Abfragezeit
            Data.TryGetValue(ipPort, out ClientData cdata);
            long requestTimeTicks = DateTime.Now.Ticks;
            long ticks = cdata.LastRequestedTimestampTicks;

            //Zusammenfügung der relevanten Daten
            int counter = 0;
            foreach (var dataPoint in Data)
            {
                List<Bewertung> bewertungen = dataPoint.Value.getBewertungen(ticks);

                obfuscatedDict.Add(counter++, bewertungen);
            }

            //Setzen der Zeit zu der der Client die Daten angefordert hat
            cdata.LastRequestedTimestampTicks = requestTimeTicks;

            return obfuscatedDict;
        }
    }
}
