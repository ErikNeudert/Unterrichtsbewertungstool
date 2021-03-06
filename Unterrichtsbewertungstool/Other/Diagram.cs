﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Unterrichtsbewertungstool
{
    class Diagram
    {
        //Lokale Variablen
        private int _maxdiagramwidth = 400;
        private int _maxdiagramheight = 200;
        private int _maxvalue = 0;
        private int _colorindex = 0;
        private List<Color> _linecolors = new List<Color>();
        private Graphics _graphic;
        private Dictionary<int, Point[]> _userpoints = new Dictionary<int, Point[]>();

        /// <summary>
        /// Initiiert die Farben, legt Kantenglätung fest 
        /// </summary>
        /// <param name="maxvalue">Die höchst mögliche Bewertung</param>
        /// <param name="graphics">Das Grafik objekt auf dem gezeichnet werden soll</param>
        public Diagram(int maxvalue, Graphics graphics)
        {
            _maxvalue = maxvalue;
            _graphic = graphics;
            InitializeColors();
            _graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        /// <summary>
        /// Generiert das Aktuelle Diagramm
        /// </summary>
        /// <param name="userBewertungen">Liste an Bewertungen</param>
        /// <param name="starttime">Startzeit</param>
        /// <param name="endtime">Endzeit</param>
        public void GenerateDiagram(Dictionary<int, List<Bewertung>> userBewertungen, long starttime, long endtime)
        {
            _userpoints.Clear();
            foreach (var user in userBewertungen)
            {
                int userId = user.Key;
                List<Point> _pointList = new List<Point>();
                foreach (Bewertung bewertung in user.Value)
                {
                    _pointList.Add(GetPointPosition(bewertung.TimeStampTicks, bewertung.Punkte, starttime, endtime));
                }
                _userpoints[userId] = _pointList.ToArray();
            }
        }

        /// <summary>
        /// Zeichnet das Aktuell generierte Diagram
        /// </summary>
        public void Draw()
        {
            //Zurücksetzen des Grafikelements 
            try
            {
                //Fehler passieren nur selten, haben kaum Auswirkung und sollen ignoriert werden.
                _graphic.Clear(Color.LightGray);

                //Setzt die Farbreihenfolge zurück und Zeichnet mit Linienbreite 2
                _colorindex = 0;
                Pen pen = new Pen(GetnextColor())
                {
                    Width = 2
                };

                //Zeichnet das Aktuelle PointArray
                foreach (Point[] pointArray in _userpoints.Values)
                {
                    if (pointArray.Length == 1)
                    {
                        continue;
                    }
                    else
                    {
                        _graphic.DrawLines(pen, pointArray);
                        pen.Width = 2;
                        pen.Color = GetnextColor();
                    }
                }
            }
            catch (Exception e)
            {
                //Macht manchmal komisches Zeug
                Debug.WriteLine("Fehler beim Zeichnen (wird irgnoriert, da es bei häufigen Aufrufen manchmal vorkommt): " + e);
            }
        }

        /// <summary>
        /// Liefert den Point mit X und Y in abhängigkeit zur Diagramauflösung zurück
        /// </summary>
        /// <param name="time">X Achse</param>
        /// <param name="value">Y Achse</param>
        /// <param name="start">Startzeit</param>
        /// <param name="ende">Endzeit</param>
        /// <returns></returns>
        private Point GetPointPosition(long time, long value, long start, long ende)
        {
            int x = (int)((time - start) / ((ende - start) / _maxdiagramwidth));
            int y = (int)(_maxdiagramheight - value * (_maxdiagramheight / _maxvalue));
            return new Point(x, y);
        }

        /// <summary>
        /// Hinzufügen der in frage kommenden Farben für das Linien Diagramm
        /// </summary>
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

        /// <summary>
        /// Liefert die nächste Farbe zurück
        /// </summary>
        /// <returns>Color</returns>
        private Color GetnextColor()
        {
            return _linecolors[_colorindex++ % 20];
        }
    }
}
