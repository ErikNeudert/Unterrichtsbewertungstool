using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Unterrichtsbewertungstool.NetworkComponent;

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


            //IPAddress address = IPAddress.Parse("127.0.0.1");
            //int port = 1111;

            //Server server = new Server(address, port, "Hello World!");
            //server.Start();
            //Client client = new Client(address, port);

            //try
            //{

            //    //client.Connect();

            //    client.RequestServerName();
            //    Console.WriteLine(client.name);
            //}
            //finally
            //{
            //    server.Stop();
            //}
        }
    }
}
