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
    class OperationUtils
    {
        /// <summary>
        /// Prüft ob der Port eine Zahl und im richtigen Bereich liegt und weist diesen der Referenz zu
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="port">Port</param>
        /// <returns></returns>
        public static bool CheckPort(string s, ref int port)
        {
            if (Int32.TryParse(s, out port))
            {
                if (port < 65535 && port > 1000)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Fügt die verfügbaren IPs der ComboBox hinzu
        /// </summary>
        /// <param name="cbip">ComboBox</param>
        public static void AddIPs(ref ComboBox cbip)
        {
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    cbip.Items.Add(addr);
                }
            }
        }

        /// <summary>
        /// Prüft ob die IP gültig ist und weist diese der Referenz zu
        /// </summary>
        /// <param name="s">IP als String</param>
        /// <param name="ip">IP als IPAddress</param>
        /// <returns>Ob die IP gültig oder ungültig ist</returns>
        public static bool CheckIP(string s, ref IPAddress ip)
        {
            if (IPAddress.TryParse(s, out ip))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Wenn der Test der TextBox leer ist wird der Text in Grau geschrieben 
        /// </summary>
        /// <param name="tbx">TextBox</param>
        /// <param name="text">Text</param>
        public static void TextBoxWaterMarkTextLeave(ref TextBox tbx, string text)
        {
            if (tbx.Text == "")
            {
                tbx.ForeColor = System.Drawing.Color.Gray;
                tbx.Text = text;
            }
        }

        /// <summary>
        /// Wenn der Text der TextBox dem übergebenen Text entspricht wird die TextBox 
        /// geleert und die Farbe auf Schwarz zurückgesetzt
        /// </summary>
        /// <param name="tbx">TextBox</param>
        /// <param name="text">Text</param>
        public static void TextBoxWaterMarkTextKeyDown(ref TextBox tbx, string text)
        {
            if (tbx.Text == text)
            {
                tbx.ForeColor = System.Drawing.Color.Black;
                tbx.Text = "";
            }
        }
    }
}
