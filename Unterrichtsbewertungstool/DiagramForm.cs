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
    public partial class DiagramForm : Form
    {
        Graphics graphic;
        int maxdiagramwidth = 400;
        public DiagramForm()
        {
            InitializeComponent();
            graphic = this.CreateGraphics();
            StartPosition = FormStartPosition.CenterScreen;
            int[] testarray = new int[] { 2, 10, 15, 20, 10, 40, 30, 20, 50, 40, 10 };
            DrawDiagramm(5, testarray);
        }
        private void DrawDiagramm(int resolution, params int[] array)
        {
            Pen pen = new Pen(Color.Green);
            for (int i = 0; i < array[0]; i++)
            {
                Point[] pointarray = new Point[resolution];
                for (int a = 0; a < resolution; a++)
                {
                    pointarray[a] = new Point(((maxdiagramwidth / resolution) * a)+40, array[1 + a + i * resolution]*5+300);
                }
                graphic.DrawLines(pen, pointarray);
            }
            this.Refresh();
        }

        private void DiagramForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DiagramForm_Load(object sender, EventArgs e)
        {
            int[] testarray = new int[] { 2, 10, 15, 20, 10, 40, 30, 20, 50, 40, 10 };
            DrawDiagramm(5, testarray);
        }

    }
}
