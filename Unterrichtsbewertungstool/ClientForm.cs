﻿using System;
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
        private List<User> _userList = new List<User>();
        private long _lowestdate = long.MaxValue;
        private long _highestdate = long.MinValue;
        private Diagram _diagram = null;
        private int _colorindex = 0;
        private int _scrollbarvalue = 0;

        public ClientForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            lbldiatitle.Text = "Bewertungen";
            lblscore.Text = tbscore.Value.ToString();

            InitializeDiagram("3-6,5-5,12-7,17-8|2-1,5-4|3-1,4-3,5-7,9-3,10-1,14-23,15-17|1-5,2-10,3-5,4-10,5-5,6-10,7-5,8-10,9-5,10-10,11-5,12-10,13-5,14-10,15-5,16-10,17-5,18-10,19-5,20-10");
        }

        public void InitializeDiagram(string datastring)
        {
            string[] buffer = datastring.Split('|');
            _userList = new List<User>();
            for (int b = 0; b < buffer.Length; b++)
            {
                _userList.Add(new User());
                string[] valuebuffer = buffer[b].Split(',');
                for (int v = 0; v < valuebuffer.Length; v++)
                {
                    string[] data = valuebuffer[v].Split('-');
                    _userList[b].Data.Add(long.Parse(data[0]), long.Parse(data[1]));
                    if (long.Parse(data[0]) < _lowestdate)
                    {
                        _lowestdate = long.Parse(data[0]);
                    }
                    if (long.Parse(data[0]) > _highestdate)
                    {
                        _highestdate = long.Parse(data[0]);
                    }
                }
            }

            _diagram = new Diagram(_highestdate, _lowestdate, tbscore.Maximum, pbdiagram.CreateGraphics(), _userList);
            _diagram.Draw();
        }


        private void DiagramForm_Paint(object sender, PaintEventArgs e)
        {
            _diagram.Draw();
        }

        private void tbscore_Scroll(object sender, EventArgs e)
        {
            _scrollbarvalue = tbscore.Value;
            lblscore.Text = _scrollbarvalue.ToString();
        }

        private void pbdiagram_Paint(object sender, PaintEventArgs e)
        {
            _diagram.Draw();
        }
    }
}
