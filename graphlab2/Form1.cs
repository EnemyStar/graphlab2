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
            //Останавливаем все таймеры на форме
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Очищаем pictureBox1
            pictureBox1.Image = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            g = Graphics.FromHwnd(pictureBox1.Handle);//создаем графику для рисования
            x2 = pictureBox1.Width / 2;//задаем начальные координаты большой палки
            y2 = (pictureBox1.Height / 2) - 100;
            
            drawLine(pictureBox1.Width / 2, pictureBox1.Height / 2, x2, y2, g);//рисуем начальное положение большой палки
            x3 = x2;//задаем координаты маленькой палки
            y3 = y2 - 50;
            drawLine(x2, y2, x3, y3, g);//рисуем маленькую палку
            timer2.Enabled = true;//запускаем таймер
            


        }

        private void timer3_Tick(object sender, EventArgs e)
        {

        }
        private void drawsmall()
        {
            Tuple<int, int> j = povorot(x3, y3, x2, y2, 15);//поворачиваем дальнюю от центра точку на 15 градусов
            x3 = j.Item1;
            y3 = j.Item2;
            drawLine(x2, y2, x3, y3, g);//рисуем линию от конца большой палки до конца маленькой


        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
          
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);//закрашиваем pictureBox1 белым чтобы создать видимость анимации
            Tuple<int, int> h = povorot(x2, y2, pictureBox1.Width / 2, pictureBox1.Height / 2,1);//поворачиваем верхнюю точку большой палки на 1 градус(чем больше градус,тем быстрее будет анимация)

            x2 = h.Item1;
            y2 = h.Item2;
            drawsmall();//вызываем метод который поворачивает и рисует маленькую палку
            drawLine(pictureBox1.Width / 2, pictureBox1.Height / 2, x2, y2, g);//рисуем большую палку от центра до точки которую мы поворачивали
           

        }

        private void button6_Click(object sender, EventArgs e)
        {
           

        bp = new Bitmap(922, 423);
            Graphics g = Graphics.FromImage(bp);
            drawPolygon(g);
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
          
            g = Graphics.FromHwnd(pictureBox1.Handle);//создаем графику
         
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);//закрашиваем белым
            timer1.Enabled = true;//запускаем таймер



        }
        void drawLine(int x1, int y1, int x2, int y2, Graphics g)
        {
            //Метод брезенхема здесь просто скопирован с первого попавшегося кода. Я сам не до конца понимаю что здесь делают поэтому
            //я ему объяснял теорию как понял,а код он не смотрел. Рекомендую найти вариант где есть код и хорошее к нему объяснение или написать самому или разобраться с этим(https://ru.wikibooks.org/wiki/%D0%A0%D0%B5%D0%B0%D0%BB%D0%B8%D0%B7%D0%B0%D1%86%D0%B8%D0%B8_%D0%B0%D0%BB%D0%B3%D0%BE%D1%80%D0%B8%D1%82%D0%BC%D0%BE%D0%B2/%D0%90%D0%BB%D0%B3%D0%BE%D1%80%D0%B8%D1%82%D0%BC_%D0%91%D1%80%D0%B5%D0%B7%D0%B5%D0%BD%D1%85%D1%8D%D0%BC%D0%B0)
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

     

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            x1 = rnd.Next(pictureBox1.Width);
            x2 = rnd.Next(pictureBox1.Width);
            y1 = rnd.Next(pictureBox1.Height);
            y2 = rnd.Next(pictureBox1.Height);

            drawLine(x1, y1, x2, y2, g);//генерируем 4 случайных переменных и рисуем по ним линию с помощью нашего алгоритма брезенхема


        }
        void drawPolygon(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);//закрашиваем белым
            Random rnd = new Random();
           
            x1 = rnd.Next(pictureBox1.Width);//генерируем случайные x1 и x2 для наших будущих начальных точек
            x2 = rnd.Next(pictureBox1.Width);
            if (x1 > x2) { int k = x2; x2 = x1; x1 = k; }//делаем так чтобы x1 всегда было слева от x2 

            y1 = rnd.Next(pictureBox1.Height);//к нашим x1 и x2 генерируем y,они должны быть равны для работы этого алгоритма
            y2 = y1;
            int d = x2 - x1; //вычисляем длинну нашей прямой которая получилось от этих 2 точек
            int numPoints = rnd.Next(1, 7); //генерируем случайное число вершин которые будем добавлять(их будет в два раза больше чем сгенерирует)
            int dx = Convert.ToInt32(d / (numPoints + 1)) + 1;//получем шаг с которым надо будет идти от x1 до x2 
            int ampY;
            Point[] Points = new Point[2+ numPoints * 2];//создаем массив точек 2 точки начальные (x1,y1),(x2,y2) и numPoints*2 так как точки будут ставить выше и ниже прямой (x1,y1),(x2,y2)

            Points[0].X =x1;//запихиваем нашу левую  начальную точку
            Points[0].Y= y1;
            int i = 1;//начинаем с 1 т.к. 0 у нас уже занят

            for (int xi = (x1 + dx); xi < x2; xi += dx)//обходим нашу прямую от x1 до x2 с шагом dx и на каждом шаге ставим точку
            {

                Points[i].X = xi; //добавляем x к точке которую будем ставить
                ampY = rnd.Next(0, y1 - 1);//генерируем такую переменную чтобы она не вылезла за границы pictureBox
                Points[i].Y = y1 - ampY;//у нашего начального y отнимаем переменную ampY,тогда получиться что эта точка будет выше прямой по которой мы идем

                i++;
            }
            Points[i].X = x2;//теперь когда мы прошли прямую "по верху " нужно будет пройти по низу но перед этим нужно добавить вторую точку прямой,чтобы в конце когда мы будем рисовать у нас прямые не пересекались и мы могли нормально обойти наши точки по часовой 
            Points[i].Y = y2;
            i++;
            for (int xi = (x2 - dx); xi > x1; xi -= dx)//делаем точно также как и в верхнем цикле только идем уже от x2 до x1 
            {
                Points[i].X = xi;
                ampY = rnd.Next(0, pictureBox1.Height - y1);
                Points[i].Y = y1 + ampY;//прибавляем ampY чтобы точка была снизу прямой 

                i++;
            }
         
            int l;
            for (l = 0; l < (1 + numPoints * 2); l++)//теперь просто обходим наш массив и рисуем прямые между всеми точками массива
            {

                drawLine( Points[l].X, Points[l].Y, Points[l+1].X, Points[l+1].Y, g);
            }
           
            drawLine(Points[l].X, Points[l].Y, x1, y1, g);//также чтобы полигон был цельным нам надо закрыть его от последнего элемента массива с (x1,y1)


        }
        private void FloodFill(Bitmap bmp, Point pt, Color targetColor, Color replacementColor)
        {
            //читайте здесь
            //https://simpledevcode.wordpress.com/2015/12/29/flood-fill-algorithm-using-c-net/
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
                    // Для наглядности метода растеризации раскомментировать 
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
         
         

            FloodFill(bp, location, Color.Black, Color.Red);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

     
    }

}
