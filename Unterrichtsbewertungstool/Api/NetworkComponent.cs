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
            formatter.Serialize(stream, obj);
        }

        public TransferObject receive(Stream stream)
        {
            return (TransferObject) formatter.Deserialize(stream);
        }

        public enum ExecutableActions
        {
            SEND,
            REQUEST,
        }

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

        //public void send(TcpClient recipient, String data)
        //{
        //    Debug.WriteLine("Entered send()");
        //    Debug.WriteLine("Will send data: '" + data + "'");

        //    NetworkStream stream = recipient.GetStream();
        //    //do stuff with buffer and send it

        //    if (stream.DataAvailable)
        //    {
        //        Debug.WriteLine("Stream has data available, should not if send is requested.");
        //        //this should not happen,
        //        //client should not request data and send at the same time,
        //        //something went wrong, try to send error code
        //    }

        //    if (stream.CanWrite)
        //    {
        //        Debug.WriteLine("Writing buffer to NetworkStream..");

        //        writeAllData(data, stream);
        //    }
        //}

        //public String receive(TcpClient client)
        //{
        //    NetworkStream stream = client.GetStream();
        //    Debug.WriteLine("Receiving");

        //    String readData = readAllData(stream);
        //    // format receive data
        //    // write receiving stuff to database

        //    Debug.WriteLine("Read content: '" + readData + "'");

        //    //maybe close afterwards
        //    return readData;
        //}

        //public string readAllData(NetworkStream stream)
        //{
        //    string readData;

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        byte[] data = new byte[1024];

        //        int numBytesRead;
        //        while (stream.CanRead && (numBytesRead = stream.Read(data, 0, data.Length)) > 0)
        //        {
        //            Debug.WriteLine("read:" + ASCIIEncoding.ASCII.GetString(data));
        //            ms.Write(data, 0, numBytesRead);
        //        }
        //        readData = Encoding.ASCII.GetString(ms.ToArray(), 0, (int) ms.Length);
        //    }

        //    return readData;
        //}

        //public void writeAllData(String data, NetworkStream stream)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        ms.Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
        //        byte[] sendBuffer = new byte[1024];
        //        int numBytesRead;

        //        while ((numBytesRead = ms.Read(sendBuffer, 0, sendBuffer.Length)) > 0)
        //        {
        //            stream.Write(sendBuffer, 0, numBytesRead);
        //        }
        //    }
        //}
    }
}