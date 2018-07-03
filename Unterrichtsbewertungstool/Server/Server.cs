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

namespace Unterrichtsbewertungstool
{

    public class Server : NetworkComponent
    {
        private delegate void ServerMethod(TcpClient client, TransferObject obj);

        private ServerData serverData = new ServerData();
        private TcpListener tcpListener;
        private Thread listenerThread;
        private int clientThreads = 0;
        private Boolean isRunning;
        private string _name;

        public Server(IPAddress serverAddress, int port, string name)
        {
            //initialize
            //set variables
            _name = name;
            isRunning = true;
            tcpListener = new TcpListener(serverAddress, port);
        }


        protected override TransferObject Receive(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            while (!stream.DataAvailable)
            {

                Console.WriteLine("x");
                Send(client, new TransferObject(TransferCodes.READY));
                Thread.Sleep(200);
            }

            return base.Receive(stream);
        }

        public void Start()
        {
            Debug.WriteLine("Start listeners");
            //start listeners
            listenerThread = GetListener(tcpListener);
            listenerThread.Start();
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
                //Notifying that he server is running
                while (isRunning)
                {
                    Debug.WriteLine("Accepting tcp Client...");
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Debug.WriteLine("Accepted client: " + client.ToString());
                        GetClientConnectionThread(client).Start();
                        //ThreadPool.QueueUserWorkItem(Listen, client);
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

        private Thread GetClientConnectionThread(TcpClient client)
        {
            return new Thread(() =>
            {
                try
                {
                    clientThreads++;
                    while (isRunning)
                    {
                        lock (client)
                        {

                            Console.WriteLine("y");
                            Send(client, new TransferObject(TransferCodes.READY));
                            TransferObject receivedObj = Receive(client);

                            //client will send if READY if he expects a value
                            if (receivedObj.Action == TransferCodes.READY)
                            {
                                continue;
                            }
                            else if (receivedObj.Action == TransferCodes.NOT_READY)
                            {
                                Thread.Sleep(200);
                                continue;
                            }


                            Console.WriteLine(4);
                            ServerMethod callback = GetActionMethod(receivedObj.Action);
                            callback.Invoke(client, receivedObj);
                        }
                    }
                }
                catch (IOException e)
                {
                    Debug.WriteLine("IOException in ClientConnectionThread: " + e.Message);
                }
                finally
                {
                    clientThreads--;
                    client.Close();
                }
            });
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

        private void SendName(TcpClient client, TransferObject obj)
        {
            //Debug.WriteLine("SendName...");

            TransferObject sendObj = new TransferObject(TransferCodes.NAME, _name);
            Send(client, sendObj);
        }

        /// <summary>
        /// Sends the data of all clients to the client
        /// </summary>
        /// <param name="state"></param>
        private void SendData(TcpClient client, TransferObject obj)
        {
            //Debug.WriteLine("SendData...");

            TransferObject sendObj = new TransferObject(TransferCodes.DATA, serverData.GetBewertungen());
            Send(client, sendObj);
        }

        private void ReceiveData(TcpClient client, TransferObject obj)
        {
            //Debug.WriteLine("ReceiveData...");

            IPEndPoint clientKey = client.Client.RemoteEndPoint as IPEndPoint;
            long timeStamp = DateTime.UtcNow.Ticks;
            object dataObject = obj.Data;

            if (dataObject is int)
            {
                Bewertung bewertung = new Bewertung((int)dataObject, timeStamp);
                serverData.AddBewertung(clientKey, bewertung);
            }
            else
            {
                Debug.WriteLine("ERROR, Invalid DataObject received. Expected int but was: " + dataObject.GetType());
            }

            Send(client, new TransferObject(TransferCodes.RECEIVED));
        }
    }
}
