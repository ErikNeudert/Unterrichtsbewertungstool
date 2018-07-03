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

        public HostForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;         //Startposition Zentrieren  
            btnhost.Enabled = false;                                //Hostbutton deaktivieren

            //Beschriften der Elemente
            cbip.Text = string.Empty;
            tbxTitel.Text = "Titel";
            tbxPort.Text = "Port";
            btnhost.Text = "Hosten";
            lblHostTitle.Text = "Hosten";
            lblIP.Text = "IP";
            lblPort.Text = "Port";
            lblTitle.Text = "Titel";

            //Festlegen der verfügbaren IPs für die CheckBox
            OperationUtils.AddIPs(ref cbip);

            //Ausrichtungs Einstellung
            tbxTitel.TextAlign = HorizontalAlignment.Left;
            tbxPort.TextAlign = HorizontalAlignment.Left;

            //Watermark Text Farbeinstellung
            tbxTitel.ForeColor = Color.Gray;
            tbxPort.ForeColor = Color.Gray;

            //Icon festlegen
            OperationUtils.IconFestlegen(this);
        }

        private void TbxPort_Leave(object sender, EventArgs e) =>
            //Watermark Text anzeigen
            OperationUtils.TextBoxWaterMarkTextLeave(ref tbxPort, "Port");

        private void TbxPort_TextChanged(object sender, EventArgs e) =>
            //Prüfen ob der Port korrekt ist um den Host Button freizuschalten
            Checkinput();

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
            //Initialisieren des Servers und des Clients
            Server server = new Server(_ip, _port, tbxTitel.Text);
            Client client = new Client(_ip, _port);

            ClientForm clientForm = new ClientForm(client);   //Clientform Initialisieren
            try
            {
                server.Start();
                client.Connect();

                //Initialisieren des Servers
                clientForm.Start();

                //Servernamen abfragen
                String name = client.RequestServerName();
                clientForm.SetName(client.name);

                //Deaktivieren der Aktuellen Form
                this.Visible = false;

                //Zeigen der ClientForm
                clientForm.ShowDialog();

                //Aktivieren der Aktuellen Form
                this.Visible = true;
            }
            catch (Exception exception)
            {
                //Anzeigen einer Fehlermeldung wenn die Verbindung nicht möglich war
                MessageBox.Show("Aufbau des Servers nicht möglich! Fehler Nachricht: " + exception.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Stoppen des Servers nachdem der Dialog geschlossen wurde
                clientForm.Stop();
                server.Stop();
            }
        }

        private void TbxPort_KeyDown(object sender, KeyEventArgs e) =>
            //Watermark Text anzeigen
            OperationUtils.TextBoxWaterMarkTextKeyDown(ref tbxPort, "Port");

        private void Cbip_SelectedIndexChanged(object sender, EventArgs e) =>
            //Prüfen ob der Port korrekt ist um den Host Button freizuschalten
            Checkinput();

        private void Cbip_TextChanged(object sender, EventArgs e) =>
            //Prüfen ob der Port korrekt ist um den Host Button freizuschalten
            Checkinput();

        private void TbxTitel_Leave(object sender, EventArgs e) =>
            //Watermark Text anzeigen
            OperationUtils.TextBoxWaterMarkTextLeave(ref tbxTitel, "Titel");

        private void TbxTitel_KeyDown(object sender, KeyEventArgs e) =>
            //Watermark Text anzeigen
            OperationUtils.TextBoxWaterMarkTextKeyDown(ref tbxTitel, "Titel");

        private void TbxTitel_TextChanged(object sender, EventArgs e) =>
            //Prüfen ob der Port korrekt ist um den Host Button freizuschalten
            Checkinput();
    }
}
