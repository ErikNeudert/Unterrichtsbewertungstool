using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unterrichtsbewertungstool
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            //int port = 2105;
            //Server server = new Server(ipAddress, port, "Test");
            //Client client = new Client(ipAddress, port);

            //server.start();


            //client.sendData(1);

            //String serverName = client.requestServerName();
            //var asd = client.RequestServerData();


        }
    }
}
