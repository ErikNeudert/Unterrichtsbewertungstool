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
        private ConcurrentDictionary<string, List<Bewertung>> Data { get; set; }

        public ServerData()
        {
            Data = new ConcurrentDictionary<string, List<Bewertung>>();
        }

        public void AddBewertung(string clientKey, Bewertung bewertung)
        {
            if (clientKey == null)
            {
                throw new ArgumentNullException(nameof(clientKey));
            }

            if (Data.ContainsKey(clientKey))
            {
                Data.TryGetValue(clientKey, out List<Bewertung> bewertungen);
                bewertungen.Add(bewertung);
            }
            else
            {
                List<Bewertung> bewertungen = new List<Bewertung>();
                bewertungen.Add(bewertung);
                Data.TryAdd(clientKey, bewertungen);
            }
        }

        public void RemoveClient(string clientKey)
        {
            if (Data.ContainsKey(clientKey))
            {
                List<Bewertung> unusedBewertungen;
                Data.TryRemove(clientKey, out unusedBewertungen);
            }

        }

        public Dictionary<int, List<Bewertung>> GetBewertungen()
        {
            Dictionary<int, List<Bewertung>> obfuscatedDict = new Dictionary<int, List<Bewertung>>();

            int counter = 0;
            foreach (var dataPoint in Data)
            {
                List<Bewertung> bewertungen = dataPoint.Value;

                obfuscatedDict.Add(counter++, bewertungen);
            }

            return obfuscatedDict;
        }
    }
}
