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
    public partial class ConnectForm : Form
    {
        //Lokale variablen
        Thread connectThread;
        int _port = 0;
        IPAddress _ip;
        Client client;

        public ConnectForm()
        {
            InitializeComponent();
            btnconnect.Enabled = false;                         //Verbindungsbutton deaktivieren
            StartPosition = FormStartPosition.CenterScreen;     //Startposition Zentrieren
            connectThread = new Thread(ConnectTest);                //thread initialisieren //Unvollständig 

            //Beschriften der Elemente
            lblipandport.Text = "IP und Port";
            btnconnect.Text = "Verbinden";
            tbxIP.Text = "IP";
            tbxPort.Text = "Port";
            tbxIP.ForeColor = System.Drawing.Color.Gray;
            tbxPort.ForeColor = System.Drawing.Color.Gray;

        }

        private void tbxPort_Enter(object sender, EventArgs e)
        {
            //Watermark Text ausblenden(Farbe auf Hellgrau?)
           //Operations.TextBoxWaterMarkTextEnter(ref tbxPort, "Port");
        }

        private void tbxPort_Leave(object sender, EventArgs e)
        {
            //Watermark Text einblenden(Farbe auf Hellgrau?)
            OperationUtils.TextBoxWaterMarkTextLeave(ref tbxPort, "Port");
        }

        private void tbxIP_Enter(object sender, EventArgs e)
        {
            //Watermark Text ausblenden(Farbe auf Hellgrau?)
          // Operations.TextBoxWaterMarkTextEnter(ref tbxIP, "IP");
        }

        private void tbxIP_Leave(object sender, EventArgs e)
        {
            //Watermark Text ausblenden(Farbe auf Hellgrau?)
            OperationUtils.TextBoxWaterMarkTextLeave(ref tbxIP, "IP");
        }

        private void btnconnect_Click(object sender, EventArgs e)
        {
            ClientForm diagramform = new ClientForm(ip, port);
            this.Visible = false;
            diagramform.ShowDialog();
            this.Visible = true;

        }
        private void ConnectTest()
        {
            TcpClient client = new TcpClient();
            client.Connect(_ip, _port);
            ASCIIEncoding encoder = new ASCIIEncoding();
            NetworkStream stream = client.GetStream();

            IPAddress localip = _ip;
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    localip = addr;
                }
            }

            byte[] buffer = encoder.GetBytes(String.Format("PING,{0}", localip.ToString()));
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();


        }
        private void CheckforButton()
        {
            //Prüfen ob IP und Port korrekt sind um den Verbindungsbutton freizuschalten
            if (OperationUtils.CheckIP(tbxIP.Text, ref _ip) && OperationUtils.CheckPort(tbxPort.Text, ref _port))
            {
                btnconnect.Enabled = true;
            }
            else
            {
                btnconnect.Enabled = false;
            }
        }
        private void tbxIP_TextChanged(object sender, EventArgs e)
        {
            //Verknüpfung der Buttons für die Prüfmethode
            CheckforButton();
        }

        private void tbxPort_TextChanged(object sender, EventArgs e)
        {
            //Verknüpfung der Buttons für die Prüfmethode
            CheckforButton();
        }

        private void tbxIP_KeyDown(object sender, KeyEventArgs e)
        {
            OperationUtils.TextBoxWaterMarkTextEnter(ref tbxIP, "IP");

        }

        private void tbxPort_KeyDown(object sender, KeyEventArgs e)
        {
            OperationUtils.TextBoxWaterMarkTextEnter(ref tbxPort, "Port");

        }
    }
}
