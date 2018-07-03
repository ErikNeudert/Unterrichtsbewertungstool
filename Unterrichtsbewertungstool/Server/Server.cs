using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;
using Unterrichtsbewertungstool;
using System.IO;
using WatsonTcp;

namespace Unterrichtsbewertungstool
{

    public class Server : NetworkComponent
    {
        private delegate void ServerMethod(string ipPort, TransferObject obj);

        private WatsonTcpServer _server;
        private ServerData _serverData = new ServerData();
        private Boolean _isRunning;
        private string _name;

        public Server(IPAddress serverAddress, int port, string name)
        {
            //initialize
            //set variables
            _name = name;
            _isRunning = true;
            _server = new WatsonTcpServer(serverAddress, port, clientConnected, clientDisconnected, messageReceived, true);
        }

        private bool messageReceived(string ipPort, byte[] msg)
        {
            TransferObject obj = (TransferObject)formatter.Deserialize(new MemoryStream(msg));
            ServerMethod method = GetActionMethod(obj.Action);

            method.Invoke(ipPort, obj);

            return true;
        }

        private bool clientDisconnected(string ipPort)
        {
            //throw new NotImplementedException();
            return true;
        }

        private bool clientConnected(string ipPort)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void Stop()
        {
            _isRunning = false;
            _server.Dispose();
        }

        private ServerMethod GetActionMethod(TransferCodes actionToTake)
        {
            switch (actionToTake)
            {
                case TransferCodes.SEND_DATA:
                    return ReceiveData;
                case TransferCodes.REQUEST_DATA:
                    return SendData;
                case TransferCodes.REQUEST_NAME:
                    return SendName;
                default:
                    Debug.WriteLine("Unknown Action: " + actionToTake);
                    return null;
            }
        }

        private void SendName(string ipPort, TransferObject obj)
        {
            //Debug.WriteLine("SendName...");

            TransferObject sendObj = new TransferObject(TransferCodes.NAME, _name);
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, sendObj);

            _server.Send(ipPort, ms.ToArray());
        }

        /// <summary>
        /// Sends the data of all clients to the client
        /// </summary>
        /// <param name="state"></param>
        private void SendData(string ipPort, TransferObject obj)
        {
            //Debug.WriteLine("SendData...");

            TransferObject sendObj = new TransferObject(TransferCodes.DATA, _serverData.GetBewertungen());

            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, sendObj);

            _server.Send(ipPort, ms.ToArray());
        }

        private void ReceiveData(string ipPort, TransferObject obj)
        {
            //Debug.WriteLine("ReceiveData...");

            long timeStamp = DateTime.Now.Ticks;
            object dataObject = obj.Data;

            if (dataObject is int)
            {
                Bewertung bewertung = new Bewertung((int)dataObject, timeStamp);
                _serverData.AddBewertung(ipPort, bewertung);
            }
            else
            {
                Debug.WriteLine("ERROR, Invalid DataObject received. Expected int but was: " + dataObject.GetType());
            }
        }
    }
}
