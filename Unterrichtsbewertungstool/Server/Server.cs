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
using WatsonTcp;

namespace Unterrichtsbewertungstool
{
    /// <summary>
    /// Diese Klasse bildet die Grundfunktionalitäten eines Servers ab.
    /// Diese umfasst im wesentlichen das halten der Verbindungen zu den verbunden Clients
    /// und das antworten auf deren Requests.
    /// </summary>
    public class Server : NetworkComponent
    {
        /// <summary>
        /// Eine Delegate das durch einen Request ausgelöst werden kann.
        /// </summary>
        /// <param name="ipPort">Der String bestehend aus IP und Port. Bibliothek bedingt.</param>
        /// <param name="obj">Das Transferobjekt das die Daten enthält</param>
        private delegate void ServerMethod(string ipPort, TransferObject obj);

        /// <summary>
        /// Der TCP Server der sich um die Kommunikation kümmert
        /// </summary>
        private WatsonTcpServer _server;
        /// <summary>
        /// Die Daten des Servers, die er obfuskiert und an die Clients verteilt
        /// </summary>
        private ServerData _serverData = new ServerData();

        /// <summary>
        /// Der Name des Servers
        /// </summary>
        private string _name;

        public Server(IPAddress serverAddress, int port, string name)
        {
            _name = name;
            _server = new WatsonTcpServer(serverAddress, port, ClientConnected, ClientDisconnected, MessageReceived, true);
        }
        
        /// <summary>
        /// Startet den Server.
        /// </summary>
        public void Start()
        {
            _server.Start();
        }

        /// <summary>
        /// Wird aufgerufen wenn der Server eine Nachricht erhält
        /// </summary>
        /// <param name="ipPort">Der String bestehend aus IP und Port des Senders</param>
        /// <param name="msg">Die Nachricht</param>
        /// <returns>true</returns>
        private bool MessageReceived(string ipPort, byte[] msg)
        {
            //Deserilaisierung
            TransferObject obj = (TransferObject)formatter.Deserialize(new MemoryStream(msg));
            //Methoden bestimmung
            ServerMethod method = GetActionMethod(obj.Action);
            //Methoden Aufruf
            method.Invoke(ipPort, obj);
            return true;
        }

        /// <summary>
        /// Wird aufgerufen wen ein Client die Verbindung trennt.
        /// </summary>
        /// <param name="ipPort">Der String bestehend aus IP und Port des Verbindungtrennenden Clients</param>
        /// <returns></returns>
        private bool ClientDisconnected(string ipPort)
        {
            return true;
        }

        /// <summary>
        /// Wird aufgerufen wenn ein Client die Verbindung herstellt.
        /// </summary>
        /// <param name="ipPort">Der String bestehend aus IP und Port des verbindenden Clients</param>
        /// <returns></returns>
        private bool ClientConnected(string ipPort)
        {
            return true;
        }

        /// <summary>
        /// Stoppt den Server
        /// </summary>
        public void Stop()
        {
            _server.Dispose();
        }

        /// <summary>
        /// Mappt den <see cref="Unterrichtsbewertungstool.NetworkComponent.TransferCode"/> auf ein <see cref="ServerMethod"/> Delegate um.
        /// </summary>
        /// <param name="actionToTake">Der TransferCode der definiert werden soll</param>
        /// <returns>Das ServerMethod Delegate</returns>
        private ServerMethod GetActionMethod(TransferCode actionToTake)
        {
            switch (actionToTake)
            {
                case TransferCode.SEND_DATA:
                    return ReceiveData;
                case TransferCode.REQUEST_DATA:
                    return SendData;
                case TransferCode.REQUEST_NAME:
                    return SendName;
                default:
                    Debug.WriteLine("Unknown Action: " + actionToTake);
                    return null;
            }
        }

        /// <summary>
        /// Sendet den Namen des Servers an gebenene IP
        /// </summary>
        /// <param name="ipPort">Der String bestehend aus IP und Port des Namensempfängers</param>
        /// <param name="obj">Nur um der Delegatesignatur gerecht zu werden</param>
        private void SendName(string ipPort, TransferObject obj)
        {
            TransferObject sendObj = new TransferObject(TransferCode.NAME, _name);
            MemoryStream ms = new MemoryStream();

            formatter.Serialize(ms, sendObj);

            _server.Send(ipPort, ms.ToArray());
        }

        /// <summary>
        /// Schickt die gesammelten Daten der Clients an den Anforderer.
        /// </summary>
        /// <param name="ipPort">Der String bestehend aus IP und Port des Empfängers</param>
        /// <param name="obj">Nur um der Delegatesignatur gerecht zu werden</param>
        private void SendData(string ipPort, TransferObject obj)
        {
            var bewertungen = _serverData.GetBewertungen(ipPort);
            TransferObject sendObj = new TransferObject(TransferCode.DATA, bewertungen);

            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, sendObj);

            _server.Send(ipPort, ms.ToArray());
        }

        /// <summary>
        /// Empfängt die vom Client geschickten Daten und speichert sie in <see cref="_serverData"/>
        /// </summary>
        /// <param name="ipPort">Der String bestehend aus IP und Port des Senders</param>
        /// <param name="obj">Fügt die Daten aus dem TransferObjekt an die Datensammlung an</param>
        private void ReceiveData(string ipPort, TransferObject obj)
        {
            long timeStamp = DateTime.Now.Ticks;
            object dataObject = obj.Data;

            if (dataObject is int)
            {
                Bewertung bewertung = new Bewertung((int)dataObject, timeStamp);
                _serverData.AddBewertung(ipPort, bewertung);
            }
            else
            {
                Debug.WriteLine("ERROR, Invalid DataObject received. Expected int but was: " + dataObject.GetType());
            }
        }
    }
}
