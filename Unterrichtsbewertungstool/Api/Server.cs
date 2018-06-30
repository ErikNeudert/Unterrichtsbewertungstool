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

namespace Unterrichtsbewertungstool
{
    public class Server : NetworkComponent
    {
        //private BlockingCollection<Action> work = new BlockingCollection<Action>();
        private Boolean isRunning;
        private Thread listenerThread;
        private TcpListener tcpListener;
        private ServerData serverData = new ServerData();
        private string _name;

        public Server(IPAddress serverAddress, int port, string name)
        {
            //initialize
            //set variables
            _name = name;
            isRunning = true;
            tcpListener = new TcpListener(serverAddress, port);
        }

        public void Start()
        {
            Debug.WriteLine("Start listeners");
            //start listeners
            listenerThread = GetListener(tcpListener);
            listenerThread.Start();
            //start Workers
        }

        public void Stop()
        {
            isRunning = false;
            listenerThread.Join(100);
            tcpListener.Stop();
        }

        private Thread GetListener(TcpListener listener)
        {
            return new Thread(() =>
                {
                    listener.Start();
                    Debug.WriteLine("Started Listener...");
                    while (isRunning)
                    {
                        Debug.WriteLine("Accepting tcp Client...");
                        try
                        {
                            TcpClient client = listener.AcceptTcpClient();
                            Debug.WriteLine("Accepted client: " + client.ToString());
                            ThreadPool.QueueUserWorkItem(Listen, client);
                        }
                        catch (SocketException e)
                        {
                            Debug.WriteLine("Error while accepting Tcp clients: " + e);
                        }
                    }
                    listener.Stop();
                }
            );
        }

        private void Listen(object clientObject)
        {
            Debug.WriteLine("Listening for Data...");
            TcpClient client = (TcpClient)clientObject;
            NetworkStream stream = client.GetStream();

            TransferObject receivedObj = receive(client);
            WaitCallback actionCallback = GetActionMethod(receivedObj.action);
            Action action = new Action(client, receivedObj.data);

            Debug.WriteLine("Adding action to work queue: '" + action.ToString() + "'.");
            ThreadPool.QueueUserWorkItem(actionCallback, action);
        }

        private WaitCallback GetActionMethod(ExecutableActions actionToTake)
        {
            switch (actionToTake)
            {
                case ExecutableActions.SEND:
                    return ReceiveData;
                case ExecutableActions.REQUEST:
                    return SendData;
                case ExecutableActions.REQUEST_NAME:
                    return SendName;
                default:
                    Debug.WriteLine("Unknown Action: " + actionToTake);
                    return null;
            }
        }

        private void SendName(object state)
        {
            Action action = (Action)state;
            TcpClient client = action.Client;
            //some clients might get more information than others (switch on client ip...)
            TransferObject sendObj = new TransferObject(ExecutableActions.SEND, _name);

            send(client, sendObj);
        }

        /// <summary>
        /// Sends the data of all clients to the client
        /// </summary>
        /// <param name="state"></param>
        private void SendData(object state)
        {
            Action action = (Action)state;
            TcpClient client = action.Client;
            //some clients might get more information than others (switch on client ip...)
            TransferObject sendObj = new TransferObject(ExecutableActions.SEND, serverData.getBewertungen());

            send(client, sendObj);
        }

        private void ReceiveData(object state)
        {
            Action action = (Action)state;
            TcpClient client = action.Client;
            long timeStamp = DateTime.UtcNow.Ticks;

            IPEndPoint remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            IPAddress clientIp = remoteIpEndPoint.Address;

            object dataObject = action.Data;

            if (dataObject is int)
            {
                Bewertung bewertung = new Bewertung((int)dataObject, timeStamp);
                serverData.addBewertung(clientIp, bewertung);
            }
            else
            {
                Debug.WriteLine("ERROR, Invalid DataObject received. Expected int but was: " + dataObject.GetType());
            }
        }

        public class Action
        {
            TcpClient client;
            object dataObject;

            public Action(TcpClient client, object dataObject)
            {
                this.client = client;
                this.dataObject = dataObject;
            }

            public TcpClient Client => client;

            public object Data => dataObject;

            public void SetValue(String value) => this.dataObject = value;
        }
    }
}
