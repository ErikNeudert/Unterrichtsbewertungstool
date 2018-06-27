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
        private ConcurrentDictionary<IPAddress, List<Bewertung>> serverData { get; set; }

        public ServerData()
        {
            serverData = new ConcurrentDictionary<IPAddress, List<Bewertung>>();
        }

        public void addBewertung(IPAddress address, Bewertung bewertung)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (serverData.ContainsKey(address))
            {
                List<Bewertung> bewertungen;
                serverData.TryGetValue(address, out bewertungen);
                bewertungen.Add(bewertung);
            }
            else
            {
                List<Bewertung> bewertungen = new List<Bewertung>();
                bewertungen.Add(bewertung);
                serverData.TryAdd(address, bewertungen);
            }
        }

        public Dictionary<int, List<Bewertung>> getBewertungen()
        {
            //Should return obfuscated stuff
            Dictionary<int, List<Bewertung>> obfuscatedDict = new Dictionary<int, List<Bewertung>>();

            int counter = 0;
            foreach (List<Bewertung> bewertungen in serverData.Values)
            {
                obfuscatedDict.Add(counter++, bewertungen);
            }

            return obfuscatedDict;
        }
    }
}
