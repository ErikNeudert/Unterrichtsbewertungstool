using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Concurrent;

namespace Unterrichtsbewertungstool
{
    public class ServerData
    {
        private ConcurrentDictionary<string, ClientData> Data { get; set; }

        public ServerData()
        {
            Data = new ConcurrentDictionary<string, ClientData>();
        }

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

        public void RemoveClient(string clientKey)
        {
            if (Data.ContainsKey(clientKey))
            {
                Data.TryRemove(clientKey, out ClientData unused);
            }

        }

        public Dictionary<int, List<Bewertung>> GetBewertungen(string ipPort, long ticks)
        {
            Dictionary<int, List<Bewertung>> obfuscatedDict = new Dictionary<int, List<Bewertung>>();

            int counter = 0;
            foreach (var dataPoint in Data)
            {
                List<Bewertung> bewertungen = dataPoint.Value.getBewertungen(ticks);

                obfuscatedDict.Add(counter++, bewertungen);
            }

            return obfuscatedDict;
        }
    }
}
