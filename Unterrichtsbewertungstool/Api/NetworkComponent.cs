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

        public void send(Stream stream, TransferObject obj)
        {
            if (stream.CanWrite)
            {
                formatter.Serialize(stream, obj);
            }
            else
            {
                Debug.WriteLine("Could not write to stream: " + stream.ToString());
            }
        }

        public TransferObject receive(Stream stream)
        {
            return (TransferObject)formatter.Deserialize(stream);
        }

        public enum ExecutableActions
        {
            SEND,
            REQUEST,
        }

        [Serializable]
        public class TransferObject
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