using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Unterrichtsbewertungstool
{
    class Diagram
    {
        private int _maxdiagramwidth = 400;
        private int _maxdiagramheight = 200;

        private int _maxvalue = 0;
        private int _colorindex = 0;
        private List<Color> _linecolors = new List<Color>();
        private Graphics _graphic;
        private List<Point[]> _userpoints= new List<Point[]>();

        public Diagram(int maxvalue, Graphics graphics)
        {
            _maxvalue = maxvalue;
            _graphic = graphics;
            InitializeColors();
            _graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }



        public void GenerateDiagram(Dictionary<int, List<Bewertung>> userBewertungen, long start, long ende)
        {
            foreach (var user in userBewertungen)
            {
                List<Point> _pointList = new List<Point>();
                foreach (Bewertung bewertung in user.Value)
                {
                    _pointList.Add(GetPointPosition(bewertung.TimeStampMillis, bewertung.Punkte, start, ende));
                }
                _userpoints.Add(_pointList.ToArray());
               
            }
        }
        public void Draw()
        {
            _colorindex = 0;
            Pen pen = new Pen(GetnextColor())
            {
                Width = 5
            };
            foreach (Point[] pointArray in _userpoints)
            {
                _graphic.DrawLines(pen,pointArray);
                pen.Width = 2;
                pen.Color = GetnextColor();
            }
        }


        private Point GetPointPosition(long date, long value, long start, long ende)
        {
            int x = (int)((date - start) / ((ende - start) / _maxdiagramwidth));
            int y = (int)(_maxdiagramheight - value * (_maxdiagramheight / _maxvalue));
            return new Point(x, y);
        }

        private void InitializeColors()
        {
            _linecolors.Add(Color.Red);
            _linecolors.Add(Color.Blue);
            _linecolors.Add(Color.Green);
            _linecolors.Add(Color.Violet);
            _linecolors.Add(Color.Tomato);
            _linecolors.Add(Color.SeaGreen);
            _linecolors.Add(Color.RoyalBlue);
            _linecolors.Add(Color.Orange);
            _linecolors.Add(Color.Olive);
            _linecolors.Add(Color.Navy);
            _linecolors.Add(Color.Lime);
            _linecolors.Add(Color.Khaki);
            _linecolors.Add(Color.Goldenrod);
            _linecolors.Add(Color.Fuchsia);
            _linecolors.Add(Color.ForestGreen);
            _linecolors.Add(Color.Aqua);
            _linecolors.Add(Color.Beige);
            _linecolors.Add(Color.Chocolate);
            _linecolors.Add(Color.DodgerBlue);
            _linecolors.Add(Color.IndianRed);
        }
        private Color GetnextColor()
        {
            if (_colorindex > _linecolors.Count)
            {
                _colorindex = 0;
            }
            return _linecolors[_colorindex++];
        }
    }
}
