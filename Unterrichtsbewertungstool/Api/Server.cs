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

        public void start()
        {
            Debug.WriteLine("Start listeners");
            //start listeners
            listenerThread = getListener(tcpListener);
            listenerThread.Start();
            //start Workers
        }

        public void stop()
        {
            isRunning = false;
            listenerThread.Join(100);
            tcpListener.Stop();
        }

        private Thread getListener(TcpListener listener)
        {
            return new Thread(() =>
                {
                    listener.Start();
                    Debug.WriteLine("Started Listener...");
                    while (isRunning)
                    {
                        Debug.WriteLine("Accepting tcp Client...");
                        TcpClient client = listener.AcceptTcpClient();
                        Debug.WriteLine("Accepted client: " + client.ToString());
                        ThreadPool.QueueUserWorkItem(listen, client);
                    }
                    listener.Stop();
                }
            );
        }

        private void listen(object clientObject)
        {
            TcpClient client = (TcpClient) clientObject;
            NetworkStream stream = client.GetStream();

            if(stream.CanRead && stream.DataAvailable)
            {
                TransferObject receivedObj = receive(stream);
                WaitCallback actionCallback = getActionMethod(receivedObj.action);
                Action action = new Action(client, receivedObj.data);

                Debug.WriteLine("Adding action to work queue: '" + action.ToString() + "'.");
                ThreadPool.QueueUserWorkItem(actionCallback, action);
            }
            else
            {
                client.Close();
                return;
            }
        }

        private WaitCallback getActionMethod(ExecutableActions actionToTake)
        {
            switch (actionToTake)
            {
                case ExecutableActions.SEND:
                    return sendData;
                case ExecutableActions.REQUEST:
                    return receiveData;
                case ExecutableActions.REQUEST_NAME:
                    return sendName;
                default:
                    Debug.WriteLine("Unknown Action: " + actionToTake);
                    return null;
            }
        }

        private void sendName(object state)
        {
            Action action = (Action)state;
            TcpClient client = action.getClient();
            NetworkStream stream = client.GetStream();
            //some clients might get more information than others (switch on client ip...)
            TransferObject sendObj = new TransferObject(ExecutableActions.SEND, _name);

            send(stream, sendObj);
        }

        /// <summary>
        /// Sends the data of all clients to the client
        /// </summary>
        /// <param name="state"></param>
        private void sendData(object state)
        {
            Action action = (Action) state;
            TcpClient client = action.getClient();
            NetworkStream stream = client.GetStream();
            //some clients might get more information than others (switch on client ip...)
            TransferObject sendObj = new TransferObject(ExecutableActions.SEND, serverData.getBewertungen());

            send(stream, sendObj);
        }

        private void receiveData(object state)
        {
            Action action = (Action) state;
            TcpClient client = action.getClient();
            long timeStamp = DateTime.Now.Millisecond;

            IPEndPoint remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            IPAddress clientIp = remoteIpEndPoint.Address;

            object dataObject = action.getData();

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

            public TcpClient getClient()
            {
                return client;
            }

            public object getData()
            {
                return dataObject;
            }

            public void setValue(String value)
            {
                this.dataObject = value;
            }
        }
    }
}
