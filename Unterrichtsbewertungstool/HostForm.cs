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

namespace Unterrichtsbewertungstool
{
    public partial class HostForm : Form
    {
        //Lokale Variablen
        private int _port = 0;
        private IPAddress _ip = null;
        private string _title = "";

        public HostForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;         //Startposition Zentrieren  
            btnhost.Enabled = false;                                //Hostbutton deaktivieren

            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            cbip.Text = string.Empty;
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    cbip.Items.Add(addr);
                }
            }

            //Beschriften der Elemente
            tbxTitel.Text = "Titel";
            tbxPort.Text = "Port";
            btnhost.Text = "Hosten";
            lblHostTitle.Text = "Hosten";
            lblIP.Text = "IP";
            lblPort.Text = "Port";
            lblTitle.Text = "Titel";
            tbxTitel.TextAlign = HorizontalAlignment.Left;
            tbxPort.TextAlign = HorizontalAlignment.Left;
            tbxTitel.ForeColor = Color.Gray;
            tbxPort.ForeColor = Color.Gray;

        }

        private void TbxPort_Enter(object sender, EventArgs e)
        {
            //Watermark Text ausblenden(Farbe auf Hellgrau?)
            // Operations.TextBoxWaterMarkTextEnter(ref tbxPort, "Port");
        }

        private void TbxPort_Leave(object sender, EventArgs e)
        {
            //Watermark Text einblenden(Farbe auf Hellgrau?)
            OperationUtils.TextBoxWaterMarkTextLeave(ref tbxPort, "Port");

        }

        private void TbxPort_TextChanged(object sender, EventArgs e)
        {
            //Prüfen ob der Port korrekt ist um den Host Button freizuschalten
            Checkinput();

        }

        /// <summary>
        /// Überprüft ob alle Kriterien gegeben sind um den Butten zu Aktivieren. Gültige IP | Gültiger Port | Titel != "" oder "Titel"
        /// </summary>
        private void Checkinput()
        {
            if (OperationUtils.CheckPort(tbxPort.Text, ref _port))
            {
                if (OperationUtils.CheckIP(cbip.Text, ref _ip) && tbxTitel.Text != "" && tbxTitel.Text != "Titel")
                {
                    btnhost.Enabled = true;
                }
                else
                {
                    btnhost.Enabled = false;
                }
            }
            else
            {
                btnhost.Enabled = false;
            }
        }

        private void Btnhost_Click(object sender, EventArgs e)
        {
            _title = tbxTitel.Text;
            Server server = new Server(_ip, _port, _title);
            server.start();
            Client client = new Client(_ip, _port);
            client.Connect();
            ClientForm clientform = new ClientForm(client);
            this.Visible = false;
            clientform.ShowDialog();
            server.stop();
            this.Visible = true;
        }

        private void TbxPort_KeyDown(object sender, KeyEventArgs e)
        {
            OperationUtils.TextBoxWaterMarkTextKeyDown(ref tbxPort, "Port");

        }

        private void Cbip_SelectedIndexChanged(object sender, EventArgs e)
        {
            Checkinput();
        }

        private void Cbip_TextChanged(object sender, EventArgs e)
        {
            //Verknüpfung der TextBox für die Prüfmethode
            Checkinput();
        }

        private void TbxTitel_Leave(object sender, EventArgs e)
        {
            OperationUtils.TextBoxWaterMarkTextLeave(ref tbxTitel, "Titel");
        }

        private void TbxTitel_KeyDown(object sender, KeyEventArgs e)
        {
            OperationUtils.TextBoxWaterMarkTextKeyDown(ref tbxTitel, "Titel");
        }

        private void tbxTitel_TextChanged(object sender, EventArgs e)
        {
            //Verknüpfung der TextBox für die Prüfmethode
            Checkinput();
        }
    }
}
