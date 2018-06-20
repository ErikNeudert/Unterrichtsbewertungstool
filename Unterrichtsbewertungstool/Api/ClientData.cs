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
        public ConcurrentDictionary<IPAddress, ClientData> serverData { get; private set; }

        public ServerData()
        {
            serverData = new ConcurrentDictionary<IPAddress, ClientData>();
        }

        public void addData(IPAddress address, ClientData data)
        {
            serverData.TryAdd(address, data);
        }
    }

    public class ClientData
    {
        //public static readonly char BEWERTUNGS_SPLIT_CHAR = ',';
        //public static readonly char FELDER_SPLIT_CHAR = ',';

        public IPAddress ip { get; private set; }
        public List<Bewertung> bewertungen { get; private set; }

        public ClientData(IPAddress ip)
        {
            this.ip = ip;
            this.bewertungen = new List<Bewertung>();
        }

        //public string serialize()
        //{
        //    StringBuilder sb = new StringBuilder();

        //    Boolean firstBewertung = true;
        //    foreach (Bewertung bewertung in bewertungen)
        //    {
        //        if (firstBewertung)
        //        {
        //            firstBewertung = false;
        //        } else
        //        {
        //            sb.Append(BEWERTUNGS_SPLIT_CHAR);
        //        }
        //        sb.Append(bewertung.timeStampMillis);
        //        sb.Append(FELDER_SPLIT_CHAR);
        //        sb.Append(bewertung.punkte);
        //    }

        //    return sb.ToString();
        //}

        //public void deserialize(string serialized)
        //{
        //    string[] bewertungenSplit = serialized.Split();

        //    foreach (string bewertungString in bewertungenSplit)
        //    {
        //        string[] bewertungsFelder = bewertungString.Split(FELDER_SPLIT_CHAR);
        //        int punkte = int.Parse(bewertungsFelder[0]);
        //        long millis = long.Parse(bewertungsFelder[1]);
        //        Bewertung bewertung = new Bewertung(punkte, millis);
        //        bewertungen.Add(bewertung);
        //    }

        //    return bewertungen;
        //}
    }
}
