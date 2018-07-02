using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Unterrichtsbewertungstool;

namespace Unterrichtsbewertungstool
{
    public class Client : NetworkComponent
    {
        private Dictionary<int, List<Bewertung>> diagramData;
        private IPAddress serverIp;
        private TcpClient tcpServer;
        private int serverPort;

        public Client(IPAddress serverIp, int serverPort)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
            tcpServer = new TcpClient();
            diagramData = new Dictionary<int, List<Bewertung>>();
        }

        public Boolean Connect()
        {
            try
            {
                tcpServer = new TcpClient()
                {
                    SendTimeout = 1000,
                    ReceiveTimeout = 1000
                };

                if (!tcpServer.Connected)
                {
                    tcpServer.Connect(this.serverIp, this.serverPort);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to connect to '" + serverIp + "' - " + e);
                return false;
            }
        }

        public Boolean Disconnect()
        {
            try
            {
                if (tcpServer.Connected)
                {
                    tcpServer.Client.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to disconect to '" + serverIp + "' - " + e);
                return false;
            }
        }

        public Dictionary<int, List<Bewertung>> RequestServerData()
        {
            //NetworkStream stream = tcpServer.GetStream();
            TransferObject sendObj = new TransferObject(ExecutableActions.REQUEST);
            TransferObject receivedObj;

            Connect();
            Send(tcpServer, sendObj);
            receivedObj = Receive(tcpServer);
            Disconnect();

            if (receivedObj.Data is Dictionary<int, List<Bewertung>>)
            {
                return (Dictionary<int, List<Bewertung>>)receivedObj.Data;
            }
            else
            {
                throw new Exception("Client -> getServerData() -> server didn't return ServerData obj, but: " + receivedObj.Data.GetType());
            }
        }

        public string RequestServerName()
        {
            TransferObject sendObj = new TransferObject(ExecutableActions.REQUEST_NAME);
            TransferObject receivedObj;

            Connect();
            Send(tcpServer, sendObj);
            receivedObj = Receive(tcpServer);
            Disconnect();

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
            TransferObject sendObj = new TransferObject(ExecutableActions.SEND, punkte);

            Connect();
            Send(tcpServer, sendObj);
            Disconnect();
        }
    }
}