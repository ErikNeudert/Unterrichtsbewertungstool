using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Unterrichtsbewertungstool;

namespace Unterrichtsbewertungstool
{
    public class Client : NetworkComponent
    {
        private Dictionary<int, List<Bewertung>> diagramData;
        private TcpClient tcpServer;
        private IPAddress serverIp;
        private int serverPort;

        public Client(IPAddress serverIp, int serverPort)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
            tcpServer = new TcpClient();
            diagramData = new Dictionary<int, List<Bewertung>>();
        }

        public void Connect()
        {
            if (!tcpServer.Connected)
            {
                tcpServer.Connect(this.serverIp, this.serverPort);
            }
        }

        public bool isConnected()
        {
            return tcpServer.Connected;
        }

        public void Disconnect()
        {
            tcpServer.Client.Close();
        }

        public Dictionary<int, List<Bewertung>> RequestServerData()
        {
            //NetworkStream stream = tcpServer.GetStream();
            TransferObject sendObj = new TransferObject(TransferCodes.REQUEST_DATA);
            TransferObject receivedObj = sendAndReceive(tcpServer, sendObj);

            if (receivedObj.Data is Dictionary<int, List<Bewertung>>)
            {
                return (Dictionary<int, List<Bewertung>>)receivedObj.Data;
            }
            else
            {
                Debug.WriteLine("Client -> getServerData() -> server didn't return ServerData obj, but: " + receivedObj.Data.GetType());
                return null;
            }
        }

        public string RequestServerName()
        {
            TransferObject sendObj = new TransferObject(TransferCodes.REQUEST_NAME);
            TransferObject receivedObj = sendAndReceive(tcpServer, sendObj);

            if (receivedObj.Data is string)
            {
                return (string)receivedObj.Data;
            }
            else
            {
                throw new Exception("Server didn't return its Name, but: " + receivedObj.Data.GetType());
            }

        }

        public void SendData(int punkte)
        {
            TransferObject sendObj = new TransferObject(TransferCodes.SEND_DATA, punkte);
            TransferObject receivedObj = sendAndReceive(tcpServer, sendObj);

            //handle if ! returnes RECEIVED
        }

        private TransferObject sendAndReceive(TcpClient client, TransferObject obj)
        {
            TransferObject receivedObj;
            TransferObject serverReady;

            lock (tcpServer)
            {

                Console.WriteLine(2);
                serverReady = Receive(client);

                if (serverReady.Action != TransferCodes.READY)
                {
                    throw new ArgumentOutOfRangeException("Expected 'TransferCodes.READY', actual: '{serverReady.Action}' ");
                }


                Console.WriteLine(3);
                Send(tcpServer, obj);

                Console.WriteLine(6);
                receivedObj = Receive(tcpServer);
            }

            return receivedObj;
        }
    }
}