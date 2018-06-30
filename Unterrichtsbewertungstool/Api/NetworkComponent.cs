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

        protected void send(TcpClient tcp, TransferObject obj)
        {
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

        protected TransferObject receive(TcpClient tcp)
        {
            try
            {
                NetworkStream stream = tcp.GetStream();

                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] data = new byte[1024];
                    int numBytesRead;

                    while (!stream.DataAvailable)
                    {
                        Thread.Sleep(200);
                    }

                    do
                    {
                        stream.ReadTimeout = 1000;
                        numBytesRead = stream.Read(data, 0, data.Length);
                        ms.Write(data, 0, numBytesRead);
                    }
                    while (numBytesRead == data.Length);
                    ms.Position = 0;

                    return (TransferObject)formatter.Deserialize(ms);
                }
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("Error while sending" + e);
                return new TransferObject(ExecutableActions.SEND, null, TransferObject.StatusCode.ERROR);
            }
        }

        protected enum ExecutableActions
        {
            SEND,
            REQUEST,
            REQUEST_NAME
        }

        [Serializable]
        protected class TransferObject
        {
            public StatusCode status { get; set; }
            public ExecutableActions action { get; set; }
            public Object data { get; set; }

            public TransferObject(ExecutableActions action, Object data, StatusCode status)
            {
                this.action = action;
                this.data = data;
                this.status = status;
            }

            public TransferObject(ExecutableActions action, Object data) : this(action, data, StatusCode.OK) { }
            public TransferObject(ExecutableActions action) : this(action, null, StatusCode.OK) { }

            public enum StatusCode
            {
                OK,
                ERROR,
            }
        }
    }
}