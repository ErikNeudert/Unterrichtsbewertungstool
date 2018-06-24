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
  
    public partial class ClientForm : Form
    {
        Graphics graphic;
        int maxdiagramwidth = 500;
        int maxdiagramheight = 300;
        Point diagramposition = new Point(10, 10);
        int clientcount = 2;
        List<User> dataList;
        long lowestdate = long.MaxValue;
        long highestdate = long.MinValue;
        List<Color> linecolors = new List<Color>();
        int colorindex = 0;
        public ClientForm()
        {
            InitializeComponent();
            graphic = this.CreateGraphics();
            StartPosition = FormStartPosition.CenterScreen;
            tbscore.Maximum = 25;
            tbscore.Minimum = 0;
            lblscore.Text = "0";
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            InitializeColors();
            InitializeDiagram("3-6,5-5,12-7,17-8|2-1,5-4|3-1,4-3,5-7,9-3,10-1,14-23,15-17|1-5,2-10,3-5,4-10,5-5,6-10,7-5,8-10,9-5,10-10,11-5,12-10,13-5,14-10,15-5,16-10,17-5,18-10,19-5,20-10");


        }
        private void InitializeColors()
        {
            linecolors.Add(Color.Red);
            linecolors.Add(Color.Blue);
            linecolors.Add(Color.Green);
            linecolors.Add(Color.Violet);
            linecolors.Add(Color.Tomato);
            linecolors.Add(Color.SeaGreen);
            linecolors.Add(Color.RoyalBlue);
            linecolors.Add(Color.Orange);
            linecolors.Add(Color.Olive);
            linecolors.Add(Color.Navy);
            linecolors.Add(Color.Lime);
            linecolors.Add(Color.Khaki);
            linecolors.Add(Color.Goldenrod);
            linecolors.Add(Color.Fuchsia);
            linecolors.Add(Color.ForestGreen);
            linecolors.Add(Color.Aqua);
            linecolors.Add(Color.Beige);
            linecolors.Add(Color.Chocolate);
            linecolors.Add(Color.DodgerBlue);
            linecolors.Add(Color.IndianRed);
        }
        private Color getnextColor()
        {
            if (colorindex > linecolors.Count)
            {
                colorindex = 0;
            }
            return linecolors[colorindex++];
        }

        public void InitializeDiagram(string datastring)
        {
            string[] buffer = datastring.Split('|');
            clientcount = buffer.Length;
            dataList = new List<User>();
            for (int b = 0; b < buffer.Length; b++)
            {
                dataList.Add(new User());
                string[] valuebuffer = buffer[b].Split(',');
                for (int v = 0; v < valuebuffer.Length; v++)
                {
                    string[] data = valuebuffer[v].Split('-');
                    dataList[b].times.Add(long.Parse(data[0]));
                    dataList[b].values.Add(long.Parse(data[1]));
                    if (long.Parse(data[0]) < lowestdate)
                    {
                        lowestdate = long.Parse(data[0]);
                    }
                    if (long.Parse(data[0]) > highestdate)
                    {
                        highestdate = long.Parse(data[0]);
                    }
                }
            }
        }
        public Point getPointPosition(long date, long value)
        {
            return new Point(diagramposition.X + ((int)((maxdiagramwidth / (highestdate - lowestdate)) * (date - lowestdate))),
                diagramposition.Y + (int)(maxdiagramheight - value * (maxdiagramheight / tbscore.Maximum)));

        }
        private void DiagramForm_Paint(object sender, PaintEventArgs e)
        {
            colorindex = 0;
            Pen pen = new Pen(getnextColor());
            pen.Width = 5;
            int vergroeserung = 5;
            Point[] rahmen = new Point[] 
                {new Point(diagramposition.X-vergroeserung,diagramposition.Y-vergroeserung),
                new Point(maxdiagramwidth+vergroeserung+diagramposition.X, diagramposition.Y-vergroeserung), 
                new Point(maxdiagramwidth+vergroeserung+diagramposition.X, maxdiagramheight+vergroeserung+diagramposition.Y),
                new Point(diagramposition.X-vergroeserung, maxdiagramheight+vergroeserung+diagramposition.Y),
                new Point(diagramposition.X-vergroeserung,diagramposition.Y-vergroeserung),
                new Point(maxdiagramwidth+vergroeserung, diagramposition.Y-vergroeserung) };
            graphic.DrawLines(pen, rahmen);


            pen.Color = getnextColor();

            foreach (var user in dataList)
            {
                Point[] parray = new Point[user.times.Count];
                for (int i = 0; i < user.times.Count; i++)
                {
                    parray[i] = getPointPosition(user.times[i], user.values[i]);
                }
                graphic.DrawLines(pen, parray);
                pen.Width = 2;
                pen.Color = getnextColor();
            }

        }

        private void DiagramForm_Load(object sender, EventArgs e)
        {
            int[] testarray = new int[] { 2, 10, 15, 20, 10, 40, 30, 20, 50, 40, 10 };
        }



        private void tbscore_Scroll(object sender, EventArgs e)
        {
            lblscore.Text = tbscore.Value.ToString();
        }

    }
}
