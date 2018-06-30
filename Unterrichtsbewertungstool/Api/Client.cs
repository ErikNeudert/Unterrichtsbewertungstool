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
        private string _serverTitel = "Server";
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
                tcpServer = new TcpClient();

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
                    tcpServer.Client.Close(100);
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
            send(tcpServer, sendObj);
            receivedObj = receive(tcpServer);
            Disconnect();

            if (receivedObj.data is Dictionary<int, List<Bewertung>>)
            {
                return (Dictionary<int, List<Bewertung>>)receivedObj.data;
            }
            else
            {
                throw new Exception("Client -> getServerData() -> server didn't return ServerData obj, but: " + receivedObj.data.GetType());
            }
        }

        public string requestServerName()
        {
            TransferObject sendObj = new TransferObject(ExecutableActions.REQUEST_NAME);
            TransferObject receivedObj;

            Connect();
            send(tcpServer, sendObj);
            receivedObj = receive(tcpServer);
            Disconnect();

            if (receivedObj.data is string)
            {
                return (string)receivedObj.data;
            }
            else
            {
                throw new Exception("Server didn't return its Name, but: " + receivedObj.data.GetType());
            }

        }

        public void sendData(int punkte)
        {
            TransferObject sendObj = new TransferObject(ExecutableActions.SEND, punkte);

            Connect();
            send(tcpServer, sendObj);
            Disconnect();
        }
    }
}