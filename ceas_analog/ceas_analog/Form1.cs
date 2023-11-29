using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ceas_analog
{
    public partial class Form1 : Form
    {
        double am = (Math.PI * 2) / 60;
        double ah = (Math.PI * 2) / 12;
        double h, m, s;
        double hs = 90, hm = 70, hh = 50, nh = 100, displacement = 9;
        PointF C, H, M, S;
        Bitmap b;
        Graphics g;
        string[] numbers = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII"};

        public Form1()
        {
            InitializeComponent();
            C = new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2);

            h = DateTime.Now.Hour;
            m = DateTime.Now.Minute;
            s = DateTime.Now.Second;

            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.White);

            s++;
            if (s == 60)
            {
                m++;
                s = 0;
            }
            if (m == 60)
            {
                h++;
                m = 0;
            }
            if (h == 12)
            {
                h = 0;
            }

            H.X = C.X + (float)(hh * Math.Cos(ah * (h - 3)));
            H.Y = C.Y + (float)(hh * Math.Sin(ah * (h - 3)));

            M.X = C.X + (float)(hm * Math.Cos(am * (m - 15)));
            M.Y = C.Y + (float)(hm * Math.Sin(am * (m - 15)));

            S.X = C.X + (float)(hs * Math.Cos(am * (s - 15)));
            S.Y = C.Y + (float)(hs * Math.Sin(am * (s - 15)));


            g.DrawEllipse(Pens.Gold, C.X - 110, C.Y - 110, 220, 220);
            g.DrawString("Rolex", new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Black), C);
            g.DrawLine(Pens.Black, C, H);
            g.DrawLine(Pens.Gray, C, M);
            g.DrawLine(Pens.Red, C, S);

            for (int i = 1; i <= 12; ++i)
            {
                PointF number_place = new PointF((float) (pictureBox1.Width / 2 - displacement + nh * Math.Cos(ah * (i - 3))), (float) (pictureBox1.Height / 2 - displacement + nh * Math.Sin(ah * (i - 3))));
                g.DrawString(numbers[i - 1], new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Black), number_place);
            }

            pictureBox1.Image = b;
        }
    }
}
