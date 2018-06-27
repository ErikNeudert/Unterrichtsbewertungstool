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
        //private ClientData clientData
        private Dictionary<int, List<Bewertung>> diagramData;
        private IPAddress serverIp;
        private int serverPort;
        private TcpClient tcpServer;

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
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to connect to '" + serverIp + "' - " + e);
                return false;
            }
        }

        public Dictionary<int, List<Bewertung>> getServerData()
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

        public void sendData(int punkte)
        {
            TransferObject sendObj = new TransferObject(ExecutableActions.SEND, punkte);
            send(tcpServer.GetStream(), sendObj);
        }
    }
}