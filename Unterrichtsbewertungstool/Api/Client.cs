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
        private List<Bewertung> bewertungen = new List<Bewertung>();
        private string serverIp;
        private int serverPort;
        private TcpClient tcpServer;

        public Client(string serverIp, int serverPort)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
            tcpServer = new TcpClient();
            tcpServer.Connect(this.serverIp, this.serverPort);
        }

        public void addData(Bewertung bewertung)
        {
            bewertungen.Add(bewertung);
        }

        public ServerData getServerData()
        {
            NetworkStream stream = tcpServer.GetStream();
            TransferObject sendObj;
            TransferObject receivedObj;

            sendObj = new TransferObject(ExecutableActions.REQUEST);
            send(stream, sendObj);
            receivedObj = receive(stream);

            //check receivedObj . status
            if (receivedObj.data is ServerData)
            {
                return (ServerData) receivedObj.data;
            }
            else
            {
                throw new Exception("Client -> getServerData() -> server didn't return ServerData obj, but: " + receivedObj.data.GetType());
            }
        }

        public void sendData()
        {
            TransferObject sendObj = new TransferObject(ExecutableActions.SEND, bewertungen);
            send(tcpServer.GetStream(), sendObj);
            //maybe check status afterwards
            //and return if successfull
            //String serializedData = serializeData(data);
            //send(tcpServer, "");
        }
    }
}