using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphlab2
{
    public partial class Form1 : Form
    {
       
        int x1, x2, y1, y2, x3, y3;
        Bitmap bp;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            g = Graphics.FromHwnd(pictureBox1.Handle);
            x2 = pictureBox1.Width / 2;
            y2 = (pictureBox1.Height / 2) - 100;
            drawLine(pictureBox1.Width / 2, pictureBox1.Height / 2, x2, y2, g);
            x3 = x2;
            y3 = y2 - 50;
            drawLine(x2, y2, x3, y3, g);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);
            timer2.Enabled = true;
            // timer3.Enabled = true;


        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Tuple<int, int> j = povorot(x3, y3, x2, y2, 15);
            x3 = j.Item1;
            y3 = j.Item2;
            drawLine(x2, y2, x3, y3, g);


        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int angl = 1;
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);
            Tuple<int, int> h = povorot(x2, y2, pictureBox1.Width / 2, pictureBox1.Height / 2, angl);

            x2 = h.Item1;
            y2 = h.Item2;
            timer3_Tick(sender, e);
            drawLine(pictureBox1.Width / 2, pictureBox1.Height / 2, x2, y2, g);
            angl++;

        }

        private void button6_Click(object sender, EventArgs e)
        {
           // g = Graphics.FromHwnd(pictureBox1.Handle);
           // drawPolygon(g);

        bp = new Bitmap(922, 423);
            Graphics g = Graphics.FromImage(bp);
            drawPolygon(g,bp);
            pictureBox1.Image = bp;


        }

        private Tuple<int, int> povorot(int xq, int yq, int xc, int yc, int angl)
        {
            double angleRadian = angl * Math.PI / 180;
            int x = Convert.ToInt32(((xq - xc) * Math.Cos(angleRadian) - (yq - yc) * Math.Sin(angleRadian) + xc));
            int y = Convert.ToInt32(((xq - xc) * Math.Sin(angleRadian) + (yq - yc) * Math.Cos(angleRadian) + yc));
            return Tuple.Create(x, y);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //myPen = new Pen(Color.Black, 1);
            g = Graphics.FromHwnd(pictureBox1.Handle);


            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);
            timer1.Enabled = true;



        }
        void drawLine(int x1, int y1, int x2, int y2, Graphics g)
        {
            int deltaX = Math.Abs(x2 - x1);
            int deltaY = Math.Abs(y2 - y1);
            int signX = x1 < x2 ? 1 : -1;
            int signY = y1 < y2 ? 1 : -1;
            //
            int error = deltaX - deltaY;
            //
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(x2, y2, 1, 1));

            while (x1 != x2 || y1 != y2)
            {
                g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(x1, y1, 1, 1));

                int error2 = error * 2;
                //
                if (error2 > -deltaY)
                {
                    error -= deltaY;
                    x1 += signX;
                }
                if (error2 < deltaX)
                {
                    error += deltaX;
                    y1 += signY;
                }
            }

        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            x1 = rnd.Next(pictureBox1.Width);
            x2 = rnd.Next(pictureBox1.Width);
            y1 = rnd.Next(pictureBox1.Height);
            y2 = rnd.Next(pictureBox1.Height);

            drawLine(x1, y1, x2, y2, g);


        }
        void drawPolygon(Graphics g,Bitmap bp)
        {
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);
            Random rnd = new Random();
           
            x1 = rnd.Next(pictureBox1.Width);
            x2 = rnd.Next(pictureBox1.Width);
            if (x1 > x2) { int k = x2; x2 = x1; x1 = k; }

            y1 = rnd.Next(pictureBox1.Height);
            y2 = y1;
            int d = x2 - x1;
            int numPoints = rnd.Next(1, 7);
            int dx = Convert.ToInt32(d / (numPoints + 1)) + 1;
            int ampY;
            Point[] Points = new Point[2+ numPoints * 2];
            
            Points[0].X =x1;
            Points[0].Y= y1;
            int i = 1;

            for (int xi = (x1 + dx); xi < x2; xi += dx)
            {
                Points[i].X = xi;
                ampY = rnd.Next(0, y1 - 1);
                Points[i].Y = y1 - ampY;

                i++;
            }
            Points[i].X = x2;
            Points[i].Y = y2;
            i++;
            for (int xi = (x2 - dx); xi > x1; xi -= dx)
            {
                Points[i].X = xi;
                ampY = rnd.Next(0, pictureBox1.Height - y1);
                Points[i].Y = y1 + ampY;

                i++;
            }
         
            int l;
            for (l = 0; l < (1 + numPoints * 2); l++)
            {
                drawLine( Points[l].X, Points[l].Y, Points[l+1].X, Points[l+1].Y, g);
            }
           
            drawLine(Points[l].X, Points[l].Y, x1, y1, g);
         

        }
        private void FloodFill(Bitmap bmp, Point pt, Color targetColor, Color replacementColor, object sender, EventArgs e)
        {
            targetColor = bmp.GetPixel(pt.X, pt.Y);
            if (targetColor.ToArgb().Equals(replacementColor.ToArgb()))
            {
                return;
            }

            Stack<Point> pixels = new Stack<Point>();

            pixels.Push(pt);
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop();
                int y1 = temp.Y;
                while (y1 >= 0 && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    y1--;
                }
                y1++;
                bool spanLeft = false;
                bool spanRight = false;
                while (y1 < bmp.Height && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    bmp.SetPixel(temp.X, y1, replacementColor);
                  
                  //  pictureBox1.Refresh();
                    if (!spanLeft && temp.X > 0 && bmp.GetPixel(temp.X - 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X - 1, y1));
                        spanLeft = true;
                    }
                    else if (spanLeft && temp.X - 1 == 0 && bmp.GetPixel(temp.X - 1, y1) != targetColor)
                    {
                        spanLeft = false;
                    }
                    if (!spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X + 1, y1));
                        spanRight = true;
                    }
                    else if (spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) != targetColor)
                    {
                        spanRight = false;
                    }
                    y1++;
                }

            }
            pictureBox1.Refresh();

        }

        private void MouseClick1(Object sender, MouseEventArgs e)
        {
            var location = e.Location;
         
         

            FloodFill(bp, location, Color.Black, Color.Red,sender,e);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

     
    }

}
