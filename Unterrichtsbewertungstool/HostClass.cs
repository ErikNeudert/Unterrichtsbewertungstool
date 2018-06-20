using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Unterrichtsbewertungstool
{

    //Gesammte Klasse unvollständig verbuggt und nur als Brainstorming ansatz gedacht...
    //schau es dir mal an vieleicht verwendest ja manche dinge weiter...
    //TODO Clients rauswerfen nach intervall
    //server stoppen
    //threads stoppen

    class HostClass
    {
        private static List<ClientData> userpool;
        private Server server;
        private bool run;

        public HostClass(int port)
        {
            //Gibt eigene IP zurück
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress ip = null;
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip = addr;
                }
            }
            server = new Server(ip, port);
        }

        private void StartHost()
        {
            //Startet den Server 
            server.start();
        }
    }
}
