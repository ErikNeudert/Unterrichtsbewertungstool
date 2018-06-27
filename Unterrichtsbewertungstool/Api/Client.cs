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
                tcpServer.Connect(this.serverIp, this.serverPort);
                _serverTitel = requestServerName();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to connect to '" + serverIp + "' - " + e);
                return false;
            }
        }

        public Dictionary<int, List<Bewertung>> RequestServerData()
        {
            NetworkStream stream = tcpServer.GetStream();
            TransferObject sendObj;
            TransferObject receivedObj;

            sendObj = new TransferObject(ExecutableActions.REQUEST);
            send(stream, sendObj);
            receivedObj = receive(stream);

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
            NetworkStream stream = tcpServer.GetStream();
            TransferObject sendObj;
            TransferObject receivedObj;

            sendObj = new TransferObject(ExecutableActions.REQUEST_NAME);
            send(stream, sendObj);
            receivedObj = receive(stream);

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
            send(tcpServer.GetStream(), sendObj);
        }
    }
}