﻿using System;
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
        private IPAddress address;
        private Boolean isRunning;
        private Thread listenerThread;
        private Thread workerThread;
        private TcpListener tcpListener;
        private ServerData serverData = new ServerData();

        public Server(String serverAdress, int port) : this(IPAddress.Parse(serverAdress), port) { }

        /*
         * TODO:
         * Log Ip
         *   
         */
        public Server(IPAddress serverAddress, int port)
        {
            //initialize
            //set variables
            address = serverAddress;
            isRunning = true;
            tcpListener = new TcpListener(address, port);
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
            listenerThread.Join(1000);
            workerThread.Join(1000);
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

        //private Thread startWorkers(BlockingCollection<Action> queue)
        //{
        //    return new Thread(() =>
        //    {
        //        while (isRunning)
        //        {
        //            Debug.WriteLine("Waiting on queue");

        //            Action action = queue.Take();

        //            Debug.WriteLine("Assigning action to worker: " + action);

        //            WaitCallback actionCallback = action.GetActionCallback();
        //            ThreadPool.QueueUserWorkItem(actionCallback, action);
        //        }
        //    }
        //    );
        //}

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
                    Debug.WriteLine("Send");
                    return sendData;
                case ExecutableActions.REQUEST:
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
            NetworkStream stream = client.GetStream();
            //some clients might get more information than others (switch on client ip...)
            TransferObject sendObj = new TransferObject(ExecutableActions.SEND, serverData);

            send(stream, sendObj);
        }

        private void receiveData(object state)
        {
            Action action = (Action) state;
            TcpClient client = action.getClient();
            object dataObject = action.getData();

            String clientIp = client.Client.RemoteEndPoint.ToString();
            Debug.WriteLine(clientIp);

            if (dataObject is List<Bewertung>)
            {
                serverData.addBewertungen(IPAddress.Parse("1.1.1.1"), (List<Bewertung>) dataObject);
            }
            else
            {
                Debug.WriteLine("ERROR, Invalid DataObject received." + dataObject.GetType());
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
