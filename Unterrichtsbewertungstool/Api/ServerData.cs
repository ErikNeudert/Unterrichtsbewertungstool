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

        public void addBewertungen(IPAddress address, List<Bewertung> data)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (serverData.ContainsKey(address))
            {
                List<Bewertung> bewertungen;
                serverData.TryGetValue(address, out bewertungen);
                bewertungen.AddRange(data);
            }
            else
            {
                serverData.TryAdd(address, data);
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
