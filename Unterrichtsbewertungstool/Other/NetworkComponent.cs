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
    /// <summary>
    /// Repräsentiert Netzwerkbestandteil.
    /// </summary>
    public abstract class NetworkComponent
    {
        /// <summary>
        /// Der serialisierer mit dem die versendeten Daten serialisiert werden
        /// </summary>
        protected IFormatter formatter = new BinaryFormatter();

        /// <summary>
        /// Sendet dem TcpClient das <see cref="TransferObject"/>.
        /// </summary>
        /// <param name="tcp">Der Empfänger</param>
        /// <param name="obj">Die Daten</param>
        protected void Send(TcpClient tcp, TransferObject obj)
        {
            try
            {
                NetworkStream stream = tcp.GetStream();

                using (MemoryStream ms = new MemoryStream())
                {
                    //Daten serilisierung
                    formatter.Serialize(ms, obj);
                    ms.Position = 0;

                    byte[] sendBuffer = new byte[1024];
                    int numBytesRead;

                    //Daten päckchenweiße verschicken
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

        /// <summary>
        /// Empfängt von gegebenem Stream das <see cref="TransferObject"/>.
        /// Wartet bis zu 10 Sekunden auf Daten
        /// </summary>
        /// <param name="stream">Der Stream von dem die Daten gelesen werden können</param>
        /// <returns></returns>
        protected virtual TransferObject Receive(NetworkStream stream)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] data = new byte[1024];
                    int numBytesRead;

                    //Daten päckchenweiße lesen
                    do
                    {
                        stream.ReadTimeout = 10000;
                        numBytesRead = stream.Read(data, 0, data.Length);
                        ms.Write(data, 0, numBytesRead);
                    }
                    while (numBytesRead == data.Length);
                    ms.Position = 0;

                    //Daten deserialisieren
                    return (TransferObject)formatter.Deserialize(ms);
                }
            }
            catch (IOException)
            {
                return new TransferObject(TransferCode.NOT_READY);
            }
        }

        /// <summary>
        /// Wrapper Methode für <see cref="Receive(NetworkStream)"/>
        /// </summary>
        /// <param name="client">DerClient von dessen Stream die Daten gelesen werden.</param>
        /// <returns></returns>
        protected virtual TransferObject Receive(TcpClient client)
        {
            return Receive(client.GetStream());
        }

        /// <summary>
        /// Die möglichen Transfercodes.
        /// Davon hängt ab wie das <see cref="TransferObject"/> verarbeitet wird.
        /// </summary>
        public enum TransferCode
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

        /// <summary>
        /// Objekt das die Daten hält.
        /// </summary>
        [Serializable]
        protected class TransferObject
        {
            /// <summary>
            /// Die Aktion die mit den enthaltenen Daten ausgeführt werden soll
            /// </summary>
            public TransferCode Action { get; set; }
            /// <summary>
            /// Die Daten.
            /// Können Null sein.
            /// </summary>
            public Object Data { get; set; }

            public TransferObject(TransferCode action, Object data)
            {
                this.Action = action;
                this.Data = data;
            }

            public TransferObject(TransferCode action) : this(action, null) { }
        }
    }
}