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
        private long _lowestdate = long.MaxValue;
        private long _highestdate = long.MinValue;
        private Diagram _diagram = null;
        private int _colorindex = 0;
        private int _scrollbarvalue = 0;
        private Client _client;
        private Thread _abfrageThread;
        private int _shownMinutesSpan = 30;

        public ClientForm(IPAddress ip, int port)
        {
            InitializeComponent();
            _client = new Client(ip, port);
            StartPosition = FormStartPosition.CenterScreen;
            _diagram = new Diagram(tbscore.Maximum, pbdiagram.CreateGraphics());
            _abfrageThread = new Thread(() =>
            {
                do
                {
                    long now = DateTime.Now.Millisecond;
                    long beginn = now - _shownMinutesSpan * 60 * 1000;
                    _client.sendData(_scrollbarvalue);
                    _diagram.GenerateDiagram(_client.getServerData(), beginn, now);
                    Thread.Sleep(5000);
                } while (true);
            });
            _abfrageThread.Start();

            lbldiatitle.Text = "Bewertungen";
            lblscore.Text = tbscore.Value.ToString();

        }
       


        private void DiagramForm_Paint(object sender, PaintEventArgs e)
        {
            _diagram.Draw();
        }

        private void tbscore_Scroll(object sender, EventArgs e)
        {
            _scrollbarvalue = tbscore.Value;
            lblscore.Text = _scrollbarvalue.ToString();
        }

        private void pbdiagram_Paint(object sender, PaintEventArgs e)
        {
            _diagram.Draw();
        }
    }
}
