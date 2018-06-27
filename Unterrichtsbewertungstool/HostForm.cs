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
        int _port = 0;
        IPAddress _ip = null;

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
            tbxPort.Text = "Port";
            lblport.Text = "IP & Port";
            btnhost.Text = "Hosten";
            tbxPort.ForeColor = System.Drawing.Color.Gray;

        }

        private void tbxPort_Enter(object sender, EventArgs e)
        {
            //Watermark Text ausblenden(Farbe auf Hellgrau?)
            // Operations.TextBoxWaterMarkTextEnter(ref tbxPort, "Port");
        }

        private void tbxPort_Leave(object sender, EventArgs e)
        {
            //Watermark Text einblenden(Farbe auf Hellgrau?)
            OperationUtils.TextBoxWaterMarkTextLeave(ref tbxPort, "Port");

        }

        private void tbxPort_TextChanged(object sender, EventArgs e)
        {
            //Prüfen ob der Port korrekt ist um den Host Button freizuschalten
            checkinput();

        }

        private void checkinput()
        {
            if (OperationUtils.CheckPort(tbxPort.Text, ref _port))
            {
                if (OperationUtils.CheckIP(cbip.Text, ref _ip))
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

        private void btnhost_Click(object sender, EventArgs e)
        {
            //HostClass host = new HostClass(port); //unvollständig brainstormung ansatz
            //Host oberfläche aufrufen
            Server server = new Server(_ip, _port, "Barometer");
            ClientForm clientform = new ClientForm(_ip, _port);
            this.Visible = false;
            clientform.ShowDialog();
            this.Visible = true;
        }

        private void tbxPort_KeyDown(object sender, KeyEventArgs e)
        {
            OperationUtils.TextBoxWaterMarkTextEnter(ref tbxPort, "Port");

        }

        private void cbip_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkinput();
        }

        private void cbip_TextChanged(object sender, EventArgs e)
        {
            checkinput();
        }
    }
}
