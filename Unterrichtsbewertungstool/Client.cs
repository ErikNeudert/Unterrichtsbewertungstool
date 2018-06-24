using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Unterrichtsbewertungstool
{
    class Client : NetworkComponent
    {
        private string _serverIp;
        private int _serverPort;
        private TcpClient _tcpClient;

        public Client(string serverIp, int serverPort)
        {
            _serverIp = serverIp;
            _serverPort = serverPort;
            _tcpClient = new TcpClient();
            _tcpClient.Connect(_serverIp, _serverPort);

        }

        public string testSend(string data)
        {
            Action action = new Action(send, _tcpClient, ExecutableActions.SEND + data);

            send(action);
            receive(action);

            Debug.WriteLine(action.getValue());

            return action.getValue();
            
        }
    }
}