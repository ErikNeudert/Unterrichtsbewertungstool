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
    public partial class HostForm : Form
    {       
        //Lokale Variablen
        int port = 0;
        public HostForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;         //Startposition Zentrieren  
            btnhost.Enabled = false;                                //Hostbutton deaktivieren

            //Beschriften der Elemente
            tbxPort.Text = "Port";
            lblport.Text = "Port";
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
            Operations.TextBoxWaterMarkTextLeave(ref tbxPort, "Port");

        }

        private void tbxPort_TextChanged(object sender, EventArgs e)
        {
            //Prüfen ob der Port korrekt ist um den Host Button freizuschalten
            if (Operations.CheckPort(tbxPort.Text, ref port))
            {
                btnhost.Enabled = true;
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
            
        }

        private void tbxPort_KeyDown(object sender, KeyEventArgs e)
        {
            Operations.TextBoxWaterMarkTextEnter(ref tbxPort, "Port");

        }
    }
}
