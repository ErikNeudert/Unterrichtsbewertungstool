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
        private ConcurrentDictionary<IPAddress, List<Bewertung>> Data { get; set; }

        public ServerData()
        {
            Data = new ConcurrentDictionary<IPAddress, List<Bewertung>>();
        }

        public void AddBewertung(IPAddress address, Bewertung bewertung)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (Data.ContainsKey(address))
            {
                Data.TryGetValue(address, out List<Bewertung> bewertungen);
                bewertungen.Add(bewertung);
            }
            else
            {
                List<Bewertung> bewertungen = new List<Bewertung>();
                bewertungen.Add(bewertung);
                Data.TryAdd(address, bewertungen);
            }
        }

        public Dictionary<int, List<Bewertung>> GetBewertungen()
        {
            //Should return obfuscated stuff
            Dictionary<int, List<Bewertung>> obfuscatedDict = new Dictionary<int, List<Bewertung>>();

            int counter = 0;
            foreach (List<Bewertung> bewertungen in Data.Values)
            {
                obfuscatedDict.Add(counter++, bewertungen);
            }

            return obfuscatedDict;
        }
    }
}
