using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Unterrichtsbewertungstool;
using WatsonTcp;

namespace Unterrichtsbewertungstool
{
    public class Client : NetworkComponent
    {
        //Synchronization Locks
        private object nameLock = new object();
        private object dataLock = new object();

        public Dictionary<int, List<Bewertung>> bewertungen = new Dictionary<int, List<Bewertung>>();
        private WatsonTcpClient _client;
        private IPAddress serverIp;
        private int serverPort;

        public string name = "###";

        /// <summary>
        /// Initialisiert den Client und baut im hintergrund eine Verbindung zu gegebenem Server auf.
        /// </summary>
        /// <param name="serverIp"></param>
        /// <param name="serverPort"></param>
        public Client(IPAddress serverIp, int serverPort)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
        }

        /// <summary>
        /// Verbindet sich zu dem im Konstruktor definierten Server
        /// </summary>
        public void Connect()
        {
            //Hässlich, jedoch bietet die es nicht besser an
            _client = new WatsonTcpClient(serverIp, serverPort, ServerConnected, ServerDisconnected, MessageReceived, false);
        }

        /// <summary>
        /// Wird ausgeführt wenn der TcpClient eine Nachricht empfängt.
        /// Entscheidet je nach Inhalt des transferierten Objekts welche Methode ausgeführt wird.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool MessageReceived(byte[] arg)
        {

            TransferObject obj = (TransferObject)formatter.Deserialize(new MemoryStream(arg));

            switch (obj.Action)
            {
                case TransferCodes.DATA:
                    bewertungen = (Dictionary<int, List<Bewertung>>)obj.Data;
                    //Benachrtichtigt das Schloss das der Name gesetzt wurde
                    lock (dataLock) Monitor.Pulse(dataLock);
                    break;
                case TransferCodes.NAME:
                    name = (string)obj.Data;
                    //Benachrtichtigt das Schloss das der Name gesetzt wurde
                    lock (nameLock) Monitor.Pulse(nameLock);
                    break;
                default:
                    Console.WriteLine("CLient received unhandle Message: " + obj.Action + " - " + obj.Data);
                    break;
            }
            return true;
        }

        private bool ServerDisconnected()
        {
            Console.WriteLine("Server Disconnected.");
            return true;
        }

        private bool ServerConnected()
        {
            Console.WriteLine("Server connected.");
            return true;
        }

        public Dictionary<int, List<Bewertung>> RequestServerData()
        {
            TransferObject sendObj = new TransferObject(TransferCodes.REQUEST_DATA);
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, sendObj);
            _client.Send(ms.ToArray());

            lock (dataLock)
            {
                Monitor.Wait(dataLock);
            }

            return bewertungen;
        }

        public string RequestServerName()
        {
            TransferObject sendObj = new TransferObject(TransferCodes.REQUEST_NAME);
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, sendObj);
            _client.Send(ms.ToArray());

            lock (nameLock)
            {
                Monitor.Wait(nameLock);
            }

            return name;
        }

        public void SendData(int punkte)
        {
            TransferObject sendObj = new TransferObject(TransferCodes.SEND_DATA, punkte);
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, sendObj);
            _client.Send(ms.ToArray());
        }
    }
}