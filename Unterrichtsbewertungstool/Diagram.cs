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
        private long _highestdate = 0;
        private long _lowestdate = 0;
        private int _maxvalue = 0;
        private int _colorindex = 0;
        private List<Color> _linecolors = new List<Color>();
        private Graphics _graphic;
        private ServerData _userList = new List<Bewertung>();


        public Diagram(long hightestdate, long lowestdate, int maxvalue, Graphics graphics, List<User> users)
        {
            _highestdate = hightestdate;
            _highestdate = hightestdate;
            _lowestdate = lowestdate;
            _maxvalue = maxvalue;
            _graphic = graphics;
            InitializeColors();
            _userList = users;

            _graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        public void Draw()
        {
            _colorindex = 0;
            Pen pen = new Pen(getnextColor());
            foreach (var user in _userList)
            {
                List<Point> _pointList = new List<Point>();
                foreach (KeyValuePair<long, long> pair in user.Data)
                {
                    _pointList.Add(GetPointPosition(pair.Key, pair.Value));
                }
                _graphic.DrawLines(pen, _pointList.ToArray());
                pen.Width = 2;
                pen.Color = getnextColor();
            }
        }


        private Point GetPointPosition(long date, long value)
        {
            return new Point(((int)((_maxdiagramwidth / (_highestdate - _lowestdate)) * (date - _lowestdate))),
                (int)(_maxdiagramheight - value * (_maxdiagramheight / _maxvalue)));
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
        private Color getnextColor()
        {
            if (_colorindex > _linecolors.Count)
            {
                _colorindex = 0;
            }
            return _linecolors[_colorindex++];
        }
    }
}
