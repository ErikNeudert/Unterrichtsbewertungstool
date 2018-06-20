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
        private BlockingCollection<Action> work;
        private IPAddress address;
        private Boolean isRunning;
        private Thread listenerThread;
        private Thread workerThread;
        private TcpListener tcpListener;
        private ConcurrentDictionary<IPAddress, ClientData> clientDatas;

        public Server(String serverAdress, int port) : this(IPAddress.Parse(serverAdress), port)
        {

        }

        /*
         * TODO:
         * Log Ip
         *   
         */
        public Server(IPAddress serverAddress, int port)
        {
            //initialize
            work = new BlockingCollection<Action>();
            clientDatas = new ConcurrentDictionary<IPAddress, ClientData>();
            //set variables
            address = serverAddress;
            isRunning = true;
            tcpListener = new TcpListener(address, port);
        }

        public void start()
        {
            Debug.WriteLine("Start listeners");
            //start listeners
            listenerThread = startListeners(tcpListener);
            listenerThread.Start();
            //start Workers
            workerThread = startWorkers(work);
            workerThread.Start();
        }

        public void stop()
        {
            listenerThread.Join(1000);
            workerThread.Join(1000);
            tcpListener.Stop();
        }

        private Thread startListeners(TcpListener listener)
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

        private Thread startWorkers(BlockingCollection<Action> queue)
        {
            return new Thread(() =>
            {
                while (isRunning)
                {
                    Debug.WriteLine("Waiting on queue");

                    Action action = queue.Take();

                    Debug.WriteLine("Assigning action to worker: " + action);

                    WaitCallback actionCallback = action.GetActionCallback();
                    ThreadPool.QueueUserWorkItem(actionCallback, action);
                }
            }
            );
        }

        private void listen(object clientObject)
        {
            TcpClient client = (TcpClient) clientObject;
            NetworkStream stream = client.GetStream();

            if(stream.CanRead && stream.DataAvailable)
            {
                WaitCallback actionCallback = getActionToTake(stream);
                Object dataObject = receive(stream);
                Action action = new Action(actionCallback, client, dataObject);

                Debug.WriteLine("Adding action to work queue: '" + action.ToString() + "'.");
                work.Add(action);
            }
            else
            {
                return;
            }
        }

        /* C# Doc goes here
         * 
         * use powers of two as value to make it bit wise
         */

        /**
         * Should probably return the actual action like do stuff to the data, send the data etc
         * 
         */
        private WaitCallback getActionToTake(NetworkStream stream)
        {
            byte actionByte = (byte) stream.ReadByte();
            ExecutableActions actionToTake = (ExecutableActions) actionByte;

            switch (actionToTake)
            {
                case ExecutableActions.SEND_DATA:
                    Debug.WriteLine("Send");
                    return sendData;
                case ExecutableActions.RECEIVE_DATA:
                    Debug.WriteLine("Receive");
                    return receiveData;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Sends the data of all clients to the client
        /// </summary>
        /// <param name="state"></param>
        private void sendData(object state)
        {
            Action action = (Action) state;
            TcpClient client = action.getClient();
            //append data of all clients

            send(client.GetStream(), clientDatas);
        }

        private void receiveData(object state)
        {
            Action action = (Action) state;
            TcpClient client = action.getClient();
            object dataObject = action.getData();

            String clientIp = client.Client.RemoteEndPoint.ToString();
            Debug.WriteLine(clientIp);

            if (dataObject is ClientData)
            {
                clientDatas.TryAdd(IPAddress.Parse("1.1.1.1"), (ClientData) dataObject);
            }
            else
            {
                Debug.WriteLine("ERROR, Invalid DataObject received." + dataObject.GetType());
            }
        }

        public class Action
        {
            TcpClient client;
            WaitCallback actionCallback;
            object dataObject;

            public Action(WaitCallback action, TcpClient client, object dataObject)
            {
                this.actionCallback = action;
                this.client = client;
                this.dataObject = dataObject;
            }

            public TcpClient getClient()
            {
                return client;
            }

            public WaitCallback GetActionCallback()
            {
                return actionCallback;
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
