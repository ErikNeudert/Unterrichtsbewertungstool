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
        int port = 0;
        IPAddress ip;

        public ConnectForm()
        {
            InitializeComponent();
            btnconnect.Enabled = false;                         //Verbindungsbutton deaktivieren
            StartPosition = FormStartPosition.CenterScreen;     //Startposition Zentrieren
            connectThread = new Thread(connectTest);                //thread initialisieren //Unvollständig 

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
            Operations.TextBoxWaterMarkTextLeave(ref tbxPort, "Port");
        }

        private void tbxIP_Enter(object sender, EventArgs e)
        {
            //Watermark Text ausblenden(Farbe auf Hellgrau?)
          // Operations.TextBoxWaterMarkTextEnter(ref tbxIP, "IP");
        }

        private void tbxIP_Leave(object sender, EventArgs e)
        {
            //Watermark Text ausblenden(Farbe auf Hellgrau?)
            Operations.TextBoxWaterMarkTextLeave(ref tbxIP, "IP");
        }

        private void btnconnect_Click(object sender, EventArgs e)
        {
            //Methode unvollständig!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //try to connect in neuem thread und öffnen der nächsten form 
            //connectThread.Start(); //noch wird es aufgrund von unvollständigkeit nicht benutzt ... zudem auch nur brainsorm ansatz
            DiagramForm diagramform = new DiagramForm();
            this.Visible = false;
            diagramform.ShowDialog();
            this.Visible = true;

        }
        private void connectTest()
        {
            //Methode unvollständig!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            TcpClient client = new TcpClient();
            client.Connect(ip, port);
            ASCIIEncoding encoder = new ASCIIEncoding();
            NetworkStream stream = client.GetStream();

            IPAddress localip = ip;
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
        private void checkforButton()
        {
            //Prüfen ob IP und Port korrekt sind um den Verbindungsbutton freizuschalten
            if (Operations.CheckIP(tbxIP.Text, ref ip) && Operations.CheckPort(tbxPort.Text, ref port))
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
            checkforButton();
        }

        private void tbxPort_TextChanged(object sender, EventArgs e)
        {
            //Verknüpfung der Buttons für die Prüfmethode
            checkforButton();
        }

        private void tbxIP_KeyDown(object sender, KeyEventArgs e)
        {
            Operations.TextBoxWaterMarkTextEnter(ref tbxIP, "IP");

        }

        private void tbxPort_KeyDown(object sender, KeyEventArgs e)
        {
            Operations.TextBoxWaterMarkTextEnter(ref tbxPort, "Port");

        }
    }
}
