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
    public partial class ConnectForm : Form
    {
        //Lokale variablen
        private int _port = 0;
        private IPAddress _ip;
        private Client _client;

        public ConnectForm()
        {
            InitializeComponent();
            btnconnect.Enabled = false;                         //Verbindungsbutton deaktivieren
            StartPosition = FormStartPosition.CenterScreen;     //Startposition Zentrieren

            //Beschriften der Elemente
            lblipandport.Text = "IP und Port";
            btnconnect.Text = "Verbinden";
            tbxIP.Text = "IP";
            tbxPort.Text = "Port";
            tbxIP.ForeColor = Color.Gray;
            tbxPort.ForeColor = Color.Gray;
        }

        private void TbxPort_Leave(object sender, EventArgs e)
        {
            //Watermark Text einblenden
            OperationUtils.TextBoxWaterMarkTextLeave(ref tbxPort, "Port");
        }

        private void TbxIP_Leave(object sender, EventArgs e)
        {
            //Watermark Text ausblenden(Farbe auf Hellgrau?)
            OperationUtils.TextBoxWaterMarkTextLeave(ref tbxIP, "IP");
        }

        private void Btnconnect_Click(object sender, EventArgs e)
        {
            _client = new Client(_ip, _port);
            ClientForm clientForm = new ClientForm(_client);
            try
            {
                //Initialisiert den Client und verbinde
                clientForm.Start();

                //Fordert den Servernamen an und setzt ihn in der Applikation
                String name = _client.RequestServerName();
                clientForm.SetName(_client.name);

                //Zeigt die Clientoberfläche an
                this.Visible = false;
                clientForm.ShowDialog();
            }
            catch (Exception exception)
            {
                //Anzeigen einer Fehlermeldung wenn die Verbindung nicht möglich war
                MessageBox.Show("Verbindung nicht möglich! Fehler Nachricht: " + exception.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                clientForm.Stop();
                //Resourcen freigeben
                this.Visible = true;
            }
        }

        /// <summary>
        /// Überprüft ob alle Kriterien gegeben sind um den Butten zu Aktivieren. Gültige IP und Gültiger Port.
        /// </summary>
        private void CheckforButton()
        {
            if (OperationUtils.CheckIP(tbxIP.Text, ref _ip) && OperationUtils.CheckPort(tbxPort.Text, ref _port))
            {
                btnconnect.Enabled = true;
            }
            else
            {
                btnconnect.Enabled = false;
            }
        }

        private void TbxIP_TextChanged(object sender, EventArgs e)
        {
            //Verknüpfung der TextBox für die Prüfmethode
            CheckforButton();
        }

        private void TbxPort_TextChanged(object sender, EventArgs e)
        {
            //Verknüpfung der TextBox für die Prüfmethode
            CheckforButton();
        }

        private void TbxIP_KeyDown(object sender, KeyEventArgs e)
        {
            //Verknüpfung der TextBox für die Prüfmethode
            OperationUtils.TextBoxWaterMarkTextKeyDown(ref tbxIP, "IP");
        }

        private void TbxPort_KeyDown(object sender, KeyEventArgs e)
        {
            //Verknüpfung der TextBox für die Prüfmethode
            OperationUtils.TextBoxWaterMarkTextKeyDown(ref tbxPort, "Port");
        }
    }
}
