using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Unterrichtsbewertungstool
{
    public abstract class NetworkComponent
    {

        public enum ExecutableActions
        {
            SEND = 'S',
            RECEIVE = 'R',
            //Maybe should do with powers of 2 (e.g. 0x01, 0x02, 0x04...) ,
            //so that we can combine up to 8 1/0 states in one byte
        }

        public void send(Object state)
        {
            Debug.WriteLine("Entered send()");
            Action action = (Action) state;
            TcpClient client = action.getClient();
            String data = action.getValue();

            Debug.WriteLine("Will send data: '" + data + "'");

            NetworkStream stream = client.GetStream();
            //do stuff with buffer and send it

            if (stream.DataAvailable)
            {
                Debug.WriteLine("Stream has data available, should not if send is requested.");
                //this should not happen,
                //client should not request data and send at the same time,
                //something went wrong, try to send error code
            }

            if (stream.CanWrite)
            {
                Debug.WriteLine("Writing buffer to NetworkStream..");

                writeAllData(data, stream);
            }
        }
             
        public void receive(Object state)
        {
            Action action = (Action) state;
            TcpClient client = action.getClient();
            NetworkStream stream = client.GetStream();
            String data = action.getValue();
            Debug.WriteLine("Receiving");

            //String readData = readAllData(stream);
            // format receive data
            // write receiving stuff to database

            Debug.WriteLine("Read content: '" + data + "'");

            //maybe close afterwards
            action.setValue(data);
        }

        public string readAllData(NetworkStream stream)
        {
            string readData;

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] data = new byte[1024];

                int numBytesRead;
                while ((numBytesRead = stream.Read(data, 0, data.Length)) > 0)
                {
                    Debug.WriteLine("read:" + ASCIIEncoding.ASCII.GetString(data));
                    ms.Write(data, 0, numBytesRead);
                }
                readData = Encoding.ASCII.GetString(ms.ToArray(), 0, (int) ms.Length);
            }
            return readData;
        }

        public void writeAllData(String data, NetworkStream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
                byte[] sendBuffer = new byte[1024];
                int numBytesRead;

                while ((numBytesRead = ms.Read(sendBuffer, 0, sendBuffer.Length)) > 0)
                {
                    stream.Write(sendBuffer, 0, numBytesRead);
                }
            }
        }

        public class Action
        {
            TcpClient client;
            WaitCallback actionCallback;
            String value;

            public Action(WaitCallback action, TcpClient client, String value)
            {
                this.actionCallback = action;
                this.client = client;
                this.value = value;
            }

            public TcpClient getClient()
            {
                return client;
            }

            public WaitCallback GetActionCallback()
            {
                return actionCallback;
            }

            public String getValue()
            {
                return value;
            }

            public void setValue(String value)
            {
                this.value = value;
            }
        }
    }
}