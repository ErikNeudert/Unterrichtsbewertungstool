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

namespace Unterrichtsbewertungstool
{

    public partial class ClientForm : Form
    {
        private Diagram _diagram = null;
        private int _scrollbarvalue = 0;
        private Client _client;
        private Thread _abfrageThread;
        private int _shownMinutesSpan = 30;

        public ClientForm(Client client)
        {
            InitializeComponent();
            _client = client;                                                       //Übergabe des Verbundenen Clients
            StartPosition = FormStartPosition.CenterScreen;                         //Zentrieren der Fensterposition
            _diagram = new Diagram(tbscore.Maximum, pbdiagram.CreateGraphics());    //Diagram Initialisieren

            //Thread Initialiseren und starten
            _abfrageThread = new Thread(() =>
            {
                //Auswählen der Anzuzeigenden Zeitspanne
                do
                {
                    long now = DateTime.UtcNow.Ticks;
                    long beginn = now - _shownMinutesSpan * 60 * 1000 * 10000;
                    _client.sendData(_scrollbarvalue);
                    _diagram.GenerateDiagram(_client.RequestServerData(), beginn, now);
                    _diagram.Draw();
                    Thread.Sleep(100);
                } while (true);
            });
            _abfrageThread.Start(); 

            //Beschriften der Elemente
            lbldiatitle.Text = "Bewertungen";
            lblscore.Text = tbscore.Value.ToString();
        }
       
        private void DiagramForm_Paint(object sender, PaintEventArgs e)
        {
            //Diagram zeichnen
            _diagram.Draw();
        }

        private void Tbscore_Scroll(object sender, EventArgs e)
        {
            //Verändern der anzeige und anpassen der Aktuellen bewertungszahl
            _scrollbarvalue = tbscore.Value;
            lblscore.Text = _scrollbarvalue.ToString();
        }

        private void Pbdiagram_Paint(object sender, PaintEventArgs e)
        {
            //Diagram zeichnen
            _diagram.Draw();
        }
    }
}
