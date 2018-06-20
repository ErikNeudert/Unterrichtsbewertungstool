using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unterrichtsbewertungstool
{
    public partial class MainForm : Form
    {
        //Lokale Variablen
        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;         //Startposition Zentrieren  

            //Beschriften der Elemente
            lbltitle.Text = "Unterrichts bewertungs";
            lbltitle2.Text = "T00l";
            btnconnect.Text = "Verbinden";
            btnhost.Text = "Hosten";
        }

        private void btnconnect_Click(object sender, EventArgs e)
        {
            //Ausblenden der Aktuellen Form und einblenden der ConnectForm
            //Nach dem Schließen wird die MainForm wieder sichtbar
            ConnectForm connectdialog = new ConnectForm();
            this.Visible = false;
            connectdialog.ShowDialog();
            this.Visible = true;
        }

        private void btnhost_Click(object sender, EventArgs e)
        {
            //Ausblenden der Aktuellen Form und einblenden der HostForm
            //Nach dem Schließen wird die MainForm wieder sichtbar
            HostForm hostdialog = new HostForm();
            this.Visible = false;
            hostdialog.ShowDialog();
            this.Visible = true;
        }
    }
}
