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
        private object nameLock = new object();
        private object dataLock = new object();
        private Dictionary<int, List<Bewertung>> diagramData;
        private IPAddress serverIp;
        private int serverPort;
        private WatsonTcpClient _client;
        public Dictionary<int, List<Bewertung>> bewertungen = new Dictionary<int, List<Bewertung>>();
        public string name = "###";

        public Client(IPAddress serverIp, int serverPort)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
            diagramData = new Dictionary<int, List<Bewertung>>();
            _client = new WatsonTcpClient(serverIp, serverPort, serverConnected, serverDisconnected, messageReceived, true);
        }

        private bool messageReceived(byte[] arg)
        {
            TransferObject obj = (TransferObject)formatter.Deserialize(new MemoryStream(arg));

            switch (obj.Action)
            {
                case TransferCodes.DATA:
                    bewertungen = (Dictionary<int, List<Bewertung>>)obj.Data;
                    lock (dataLock)
                    {
                        Monitor.Pulse(dataLock);
                    }
                    break;
                case TransferCodes.NAME:
                    name = (string)obj.Data;
                    lock (nameLock)
                    {
                        Monitor.Pulse(nameLock);
                    }
                    break;
                default:
                    Console.WriteLine("CLient received unhandle Message: " + obj.Action + " - " + obj.Data);
                    break;
            }
            return true;
        }

        private bool serverDisconnected()
        {
            Console.WriteLine("Server Disconnected.");
            return true;
        }

        private bool serverConnected()
        {
            Console.WriteLine("Server connected.");
            return true;
        }

        public Dictionary<int, List<Bewertung>> RequestServerData()
        {
            //NetworkStream stream = tcpServer.GetStream();
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