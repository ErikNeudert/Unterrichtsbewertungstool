using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;


namespace Unterrichtsbewertungstool
{
    //Klassenname überarbeiten 
    class Operations
    {
        public static bool CheckPort(string s, ref int port)
        {
            //Prüft ob der Port eine Zahl und im richtigen Bereich liegt und weist diesen der Referenz zu
            if (Int32.TryParse(s, out port))
            {
                if (port < 65535 && port > 1000)
                {
                    return true;
                }
            } return false;
        }
        public static bool CheckIP(string s,ref IPAddress ip)
        {
            //Prüft ob die IP gültig ist und weist diese der Referenz zu
            if (System.Net.IPAddress.TryParse(s, out ip))
            {
                return true;
            }
            return false;
        }
        public static void TextBoxWaterMarkTextLeave(ref TextBox tbx, string text)
        {
            if (tbx.Text == "")
            {
                tbx.ForeColor = System.Drawing.Color.Gray;
                tbx.Text = text;
            }
        }
        public static void TextBoxWaterMarkTextEnter(ref TextBox tbx, string text)
        {
            if (tbx.Text == text)
            {
                tbx.ForeColor = System.Drawing.Color.Black;
                tbx.Text = "";
            }
        }
    }
}
