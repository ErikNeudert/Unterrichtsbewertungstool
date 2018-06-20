using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Unterrichtsbewertungstool;

namespace Unterrichtsbewertungstool
{
    class Client : NetworkComponent
    {
        private ClientData clientData;
        private string _serverIp;
        private int _serverPort;
        private TcpClient tcpServer;

        public Client(string serverIp, int serverPort)
        {
            _serverIp = serverIp;
            _serverPort = serverPort;
            tcpServer = new TcpClient();
            tcpServer.Connect(_serverIp, _serverPort);
        }

        public void addData(Bewertung bewertung)
        {
            clientData.bewertungen.Add(bewertung);
        }

        public void sendData()
        {
            send(tcpServer.GetStream(), clientData);
            //String serializedData = serializeData(data);
            //send(tcpServer, "");
        }
    }
}