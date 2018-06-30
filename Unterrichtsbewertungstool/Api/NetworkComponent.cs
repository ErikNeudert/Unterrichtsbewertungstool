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

        protected void send(Stream stream, TransferObject obj)
        {
            if (stream.CanWrite)
            {
                var ms = new MemoryStream();
                formatter.Serialize(ms, obj);
                stream.Write(ms.ToArray(), 0, (int)ms.Length);
                stream.Flush();
            }
            else
            {
                Debug.WriteLine("Could not write to stream: " + stream.ToString());
            }
        }

        protected TransferObject receive(Stream stream)
        {
            return (TransferObject)formatter.Deserialize(stream);
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