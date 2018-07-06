using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Unterrichtsbewertungstool;
using WatsonTcp;

namespace Unterrichtsbewertungstool
{
    /// <summary>
    /// Diese Klasse bildet die Grundfunktionalitäten eines Clients ab.
    /// Sie beinaltet alle relevanten Funktionen und Informationen um 7
    /// von einer UI oder einem Batch Job Daten an einen Server zu 
    /// schicken und von diesem zu erhalten.
    /// </summary>
    public class Client : NetworkComponent
    {
        #region ### Felder ###
        /// <summary>
        /// Die Nutzerbewertungen.
        /// Werden z.B. in der UI zur Anzeige der Diagrammlinien verwendet.
        /// </summary>
        public Dictionary<int, List<Bewertung>> bewertungen = new Dictionary<int, List<Bewertung>>();
        /// <summary>
        /// Dieses Lock wird benachrichtigt wenn der Namens Serverrequest abgeschlossen ist.
        /// </summary>
        private readonly object nameLock = new object();
        /// <summary>
        /// Dieses Lock wird benachrichtigt wenn der Daten Serverrequest abgeschlossen ist.
        /// </summary>
        private readonly object dataLock = new object();
        /// <summary>
        /// True solange die Applikation läuft, false wenn sie heruntergefahren wird / nicht läuft
        /// </summary>
        public bool isRunning = false;

        /// <summary>
        /// Der tcpClient, über den die Kommunikation stattfindet.
        /// </summary>
        private WatsonTcpClient _client;
        /// <summary>
        /// Die Adresse des angepeilten Servers.
        /// </summary>
        private IPAddress serverIp;
        /// <summary>
        /// Der Port des angepeilten Servers.
        /// </summary>
        private int serverPort;
        /// <summary>
        /// Der name des Servers.
        /// </summary>
        public string serverName = "###";

        #endregion

        #region ### Öffentliche Methoden ###

        /// <summary>
        /// Initialisiert den Client und baut im hintergrund eine Verbindung zu gegebenem Server auf.
        /// </summary>
        /// <param name="serverIp">Server IP-Adresse</param>
        /// <param name="serverPort">Server Port</param>
        public Client(IPAddress serverIp, int serverPort)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
        }

        /// <summary>
        /// Verbindet sich zu dem im Konstruktor definierten Server
        /// </summary>
        public void Connect()
        {
            isRunning = true;
            //Hier wird der TCP Client initialisiert, da die Library direkt einen Verbindungsversuch startet.
            //Die Mitgabe parameter sind Delegates, die bei den verschiedenen Events aufgerufen werden.
            _client = new WatsonTcpClient(serverIp, serverPort, ServerConnected, ServerDisconnected, MessageReceived, false);
        }

        /// <summary>
        /// Senden einen Request an den Server, der die Daten anfragt.
        /// Blockiert bis die Daten empfangen wurden, maximal 3 Sekunden
        /// Gibt <see cref="bewertungen"/> zurück.
        /// </summary>
        /// <returns><see cref="bewertungen"/></returns>
        public Dictionary<int, List<Bewertung>> RequestServerData()
        {
            TransferObject sendObj = new TransferObject(TransferCode.REQUEST_DATA);
            MemoryStream ms = new MemoryStream();
            //Serialisierung der Daten
            formatter.Serialize(ms, sendObj);
            //Übertragung der Daten
            bool sendSuccessfull = _client.Send(ms.ToArray());
            if (sendSuccessfull)
            {
                lock (dataLock)
                {
                    Monitor.Wait(dataLock, 3000);
                }
            }
            return bewertungen;
        }

        /// <summary>
        /// Fragt den Namen des Servers an.
        /// Blockiert bis der Name empfangen wurde, maximal 3 Sekunden.
        /// </summary>
        /// <returns><see cref="serverName"/></returns>
        public string RequestServerName()
        {
            TransferObject sendObj = new TransferObject(TransferCode.REQUEST_NAME);
            MemoryStream ms = new MemoryStream();
            formatter.Serialize(ms, sendObj);
            bool sendSuccessfull = _client.Send(ms.ToArray());

            if (sendSuccessfull)
            {
                lock (nameLock)
                {
                    Monitor.Wait(nameLock, 3000);
                }
            }

            return serverName;
        }

        /// <summary>
        /// Sendet die gegebene Punktebewertung an den Server.
        /// Es finden keine Range checks statt, 
        /// der Aufrufende oder der Empfangende hat sich darum zu kümmern!
        /// </summary>
        /// <param name="punkte">Die Bewertung</param>
        public void SendData(int punkte)
        {
            TransferObject sendObj = new TransferObject(TransferCode.SEND_DATA, punkte);
            MemoryStream ms = new MemoryStream();
            //Serialisieren
            formatter.Serialize(ms, sendObj);
            //Senden
            _client.Send(ms.ToArray());
        }

        #endregion

        #region ### Private Methoden ###

        /// <summary>
        /// Wird ausgeführt wenn der TcpClient eine Nachricht empfängt.
        /// Entscheidet je nach Inhalt des transferierten Objekts welche Methode ausgeführt wird.
        /// </summary>
        /// <param name="messageBytes">Die vom Server empfangenen Bytes</param>
        /// <returns>true</returns>
        private bool MessageReceived(byte[] messageBytes)
        {

            TransferObject obj = (TransferObject)formatter.Deserialize(new MemoryStream(messageBytes));

            switch (obj.Action)
            {
                case TransferCode.DATA:
                    //Daten wurden empfangen
                    Dictionary<int, List<Bewertung>> newBewertungen = (Dictionary<int, List<Bewertung>>)obj.Data;
                    
                    //Daten an Bisherige anfügen
                    //Dies brachte eine reduzierung der gesendeten Daten um Faktor 50(!) auf 6KB bei Volllast
                    appendDict(bewertungen, newBewertungen);
                    lock (dataLock)
                    {
                        Monitor.Pulse(dataLock);
                    }

                    break;
                case TransferCode.NAME:
                    //Name wurde empfangen
                    serverName = (string)obj.Data;
                    lock (nameLock) Monitor.Pulse(nameLock);
                    break;
                default:
                    Console.WriteLine("Client received unhandle Message: '" + obj.Action + "' - '" + obj.Data + "'");
                    break;
            }
            return true;
        }

        /// <summary>
        /// Fügt den Inhalt von <paramref name="srcDict"/> an <paramref name="destDict"/> an.
        /// </summary>
        /// <typeparam name="T1">Key Typ</typeparam>
        /// <typeparam name="T2">Value Typ</typeparam>
        /// <param name="destDict">Das Ziel</param>
        /// <param name="srcDict">Die Quelle</param>
        private void appendDict<T1, T2>(Dictionary<T1, List<T2>> destDict, Dictionary<T1, List<T2>> srcDict)
        {
            foreach (var list in srcDict)
            {
                if (destDict.ContainsKey(list.Key))
                {
                    destDict.TryGetValue(list.Key, out var value);
                    if (value == null)
                    {
                        value = new List<T2>();
                    }
                    value.AddRange(list.Value);
                }
                else
                {
                    destDict.Add(list.Key, list.Value);
                }
            }
        }

        /// <summary>
        /// Wird Aufgerufen wenn der Server die Verbindung abbricht.
        /// </summary>
        /// <returns></returns>
        private bool ServerDisconnected()
        {
            Console.WriteLine("Server Disconnected.");
            isRunning = false;
            MessageBox.Show("Die Verbindung zum Server ist abgebrochen.", "Server Disconnected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        /// <summary>
        /// Wird aufgerufen wenn der Server verbunden wurde.
        /// </summary>
        /// <returns></returns>
        private bool ServerConnected()
        {
            Console.WriteLine("Server connected.");
            return true;
        }

        #endregion
    }
}