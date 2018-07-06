using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Unterrichtsbewertungstool
{

    public partial class ClientForm : Form
    {
        private Diagram _diagram = null;
        private int _scrollbarvalue = 0;
        private Client _client;
        private Thread _abfrageThread;
        private int _shownMinutesSpan = 1;

        /// <summary>
        /// Initialisiert die Komponenten.
        /// </summary>
        /// <param name="client">Der zugrundeliegende Client</param>
        public ClientForm(Client client)
        {
            InitializeComponent();
            _client = client;                                                       //Übergabe des Verbundenen Clients
            StartPosition = FormStartPosition.CenterScreen;                         //Zentrieren der Fensterposition
            _diagram = new Diagram(tbscore.Maximum, pbdiagram.CreateGraphics());    //Diagram Initialisieren

            //Thread Initialiseren, wird durch Start gestartet
            _abfrageThread = new Thread(() =>
            {
                do
                {
                    //Zeitspannen Begrenzer
                    long now = DateTime.Now.Ticks;
                    long beginn = now - _shownMinutesSpan * TimeSpan.TicksPerMinute;
                    
                    //Sendet die den Datenpunkt an dem sich die Scrollbar befindet
                    _client.SendData(_scrollbarvalue);
                    
                    //Daten anfordern
                    _client.RequestServerData();
                    //generiert das Diagram
                    _diagram.GenerateDiagram(_client.bewertungen, beginn, now);
                    _diagram.Draw();

                    Thread.Sleep(500);
                } while (client.isRunning);
            });

            //Icon festlegen
            OperationUtils.IconFestlegen(this);
        }

        /// <summary>
        /// Startet den Server
        /// </summary>
        public void Start()
        {
            _abfrageThread.Start();
        }

        /// <summary>
        /// Stoppt den Server
        /// </summary>
        public void Stop()
        {
            _client.isRunning = false;
            _abfrageThread.Abort();
        }

        /// <summary>
        /// Setzt den den Titeltext des Dialogs
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            lbldiatitle.Text = name;
        }

        /// <summary>
        /// Wird aufgerufen wenn die Scrollbar verschoben wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbscore_Scroll(object sender, EventArgs e)
        {
            //Verändern der anzeige und anpassen der Aktuellen bewertungszahl
            _scrollbarvalue = tbscore.Value;
            lblscore.Text = (_scrollbarvalue).ToString();
        }

        /// <summary>
        /// Wird aufgerufen wenn das Feld zu einstellung des Zeitrahmens verändert wurde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _shownMinutesSpan = decimal.ToInt32(numericUpDown1.Value);
        }
    }
}
