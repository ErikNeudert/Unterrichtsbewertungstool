using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace Unterrichtsbewertungstool
{
    public abstract class NetworkComponent
    {
        protected IFormatter formatter = new BinaryFormatter();

        protected void Send(TcpClient tcp, TransferObject obj)
        {

            Console.WriteLine(5);
            //Debug.WriteLine("Send...");
            try
            {
                NetworkStream stream = tcp.GetStream();

                using (MemoryStream ms = new MemoryStream())
                {
                    formatter.Serialize(ms, obj);
                    ms.Position = 0;

                    byte[] sendBuffer = new byte[1024];
                    int numBytesRead;

                    do
                    {
                        numBytesRead = ms.Read(sendBuffer, 0, sendBuffer.Length);
                        stream.Write(sendBuffer, 0, numBytesRead);
                    }
                    while (numBytesRead == sendBuffer.Length);
                }
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("Error while sending" + e);
                return;
            }
        }

        protected virtual TransferObject Receive(NetworkStream stream)
        {
            //Debug.WriteLine("Receive...");
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] data = new byte[1024];
                    int numBytesRead;

                    do
                    {
                        stream.ReadTimeout = 10000;
                        numBytesRead = stream.Read(data, 0, data.Length);
                        ms.Write(data, 0, numBytesRead);
                    }
                    while (numBytesRead == data.Length);
                    ms.Position = 0;

                    return (TransferObject)formatter.Deserialize(ms);
                }
            }
            catch (IOException e)
            {
                return new TransferObject(TransferCodes.NOT_READY);
            }
        }

        protected virtual TransferObject Receive(TcpClient client)
        {
            return Receive(client.GetStream());
        }

        //protected bool AreYouThere()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        protected enum TransferCodes
        {
            SEND_DATA,
            RECEIVED,

            REQUEST_DATA,
            DATA,

            REQUEST_NAME,
            NAME,

            READY,
            NOT_READY
        }

        [Serializable]
        protected class TransferObject
        {
            public TransferCodes Action { get; set; }
            public Object Data { get; set; }

            public TransferObject(TransferCodes action, Object data)
            {
                this.Action = action;
                this.Data = data;
            }

            public TransferObject(TransferCodes action) : this(action, null) { }
        }
    }
}