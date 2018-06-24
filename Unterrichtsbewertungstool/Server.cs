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

namespace Unterrichtsbewertungstool
{
    public class Server : NetworkComponent
    {
        private BlockingCollection<Action> _work = new BlockingCollection<Action>();
        private IPAddress _address;
        private Boolean _isRunning;
        private Thread _listenerThread;
        private Thread _workerThread;
        private TcpListener _tcpListener;


        public Server(String serverAdress, int port) 
            : this(IPAddress.Parse(serverAdress), port) { }

        /*
         * TODO:
         * Log Ip
         *   
         */
        public Server(IPAddress serverAddress, int port)
        {
            //set variables
            _address = serverAddress;
            _isRunning = true;

            Debug.WriteLine("Start listeners");
            //start listeners
            _tcpListener = new TcpListener(_address, port);
            _listenerThread = startListeners(_tcpListener);
            _listenerThread.Start();
            //start Workers
            _workerThread = startWorkers(_work);
            _workerThread.Start();
        }

        public void stop()
        {
            _listenerThread.Join(1000);
            _workerThread.Join(1000);
            _tcpListener.Stop();
        }

        private Thread startListeners(TcpListener listener)
        {
            return new Thread(() =>
                {
                    listener.Start();
                    Debug.WriteLine("Started Listener...");
                    while (_isRunning)
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
                while (_isRunning)
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
                String data = readAllData(stream);

                WaitCallback actionCallback = getActionToTake(data[0]);
                Action action = new Action(actionCallback, client, data);

                Debug.WriteLine("Adding action to work queue: '" + action.ToString() + "'.");
                _work.Add(action);
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
        private WaitCallback getActionToTake(char actionByte)
        {
            ExecutableActions actionToTake = (ExecutableActions)actionByte;
            switch (actionToTake)
            {
                case ExecutableActions.SEND:
                    Debug.WriteLine("Send");
                    return send;
                case ExecutableActions.RECEIVE:
                    Debug.WriteLine("Receive");
                    return receive;
                default:
                    return (Object state) => { };
            }
        }
    }
}
