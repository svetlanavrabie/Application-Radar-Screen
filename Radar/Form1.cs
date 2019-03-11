using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Radar
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();
        int width = 300, height = 300, hand = 150;
        int u; //in degree
        int cx, cy; // center of the circle

        int x, y; // hand coordinate

        int tx, ty, lim = 20;

        Bitmap bmp;
        Pen p;
        Graphics g;




        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // create Bitmap
            bmp = new Bitmap(width+1, height+1);

            //background color
            this.BackColor = Color.Black;

            //center
            cx = width / 2;
            cy = height / 2;

            //initial degree og hand
            u = 0;

            //timer
            t.Interval = 5; // in millisecond
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

        }


        private void t_Tick(object sender, EventArgs e) {
            //pen

            p = new Pen(Color.Green, 1f);
            // graphics

            g = Graphics.FromImage(bmp);

            //calculate x, y coordinate of hand;
            int tu = (u - lim) % 360;

            if (u>=0 && u<=180)
            {
                //right half
                //u in degree is converted into radian

                x = cx + (int)(hand * Math.Sin(Math.PI * u / 180));
                y = cy - (int)(hand * Math.Cos(Math.PI * u / 180));

            }
            else
            {
                x = cx - (int)(hand * -Math.Sin(Math.PI * u / 180));
                y = cy - (int)(hand * Math.Cos(Math.PI * u / 180));

            }


            if (tu >= 0 && tu <= 180)
            {
                //right half
                //u in degree is converted into radian

                tx = cx + (int)(hand * Math.Sin(Math.PI * tu / 180));
                ty = cy - (int)(hand * Math.Cos(Math.PI * tu / 180));

            }
            else
            {
                tx = cx - (int)(hand * -Math.Sin(Math.PI * tu / 180));
                ty = cy - (int)(hand * Math.Cos(Math.PI * tu / 180));

            }

            //draw circle
            g.DrawEllipse(p,0,0, width, height); // bigger circle
            g.DrawEllipse(p, 80, 80, width - 160, height - 160); //small circle

            // draw perpendicular line
            g.DrawLine(p, new Point(cx, 0), new Point(cx, height));//up-down
            g.DrawLine(p, new Point(0, cy), new Point(width, cy));//right-left


            //draw hand
            g.DrawLine(new Pen(Color.Black, 1f), new Point(cx, cy), new Point(tx, ty));
            g.DrawLine(p, new Point(cx, cy), new Point(x, y));

            // load bitmap in picturebox1
            pictureBox1.Image = bmp;

            //dispose
            p.Dispose();
            g.Dispose();

            //update
            u++;
            if (u==360)
            {
                u = 0;
            }



        }
    }
}
