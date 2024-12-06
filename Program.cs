using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace GraphDrawer
{
    public class MainForm : Form
    {
        public MainForm()
        {
            this.Text = "Графік функції";
            this.Resize += (sender, args) => this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            Pen axisPen = new Pen(Color.Black, 2);
            g.DrawLine(axisPen, 50, height - 50, 50, 50);
            g.DrawLine(axisPen, 50, height - 50, width - 50, height - 50);

            double xmin = 4.5, xmax = 16.4, step = 2.2;
            double ymin = double.MaxValue, ymax = double.MinValue;

            for (double x = xmin; x <= xmax; x += step)
            {
                double y = (Math.Pow(x, 3) - 2) / (3 * Math.Log(x));
                ymin = Math.Min(ymin, y);
                ymax = Math.Max(ymax, y);
            }

            double xScale = (width - 100) / (xmax - xmin);
            double yScale = (height - 100) / (ymax - ymin);

            PointF? prevPoint = null;
            for (double x = xmin; x <= xmax; x += step)
            {
                double y = (Math.Pow(x, 3) - 2) / (3 * Math.Log(x));

                float screenX = (float)(50 + (x - xmin) * xScale);
                float screenY = (float)(height - 50 - (y - ymin) * yScale);

                PointF currentPoint = new PointF(screenX, screenY);

                if (prevPoint != null)
                {
                    g.DrawLine(Pens.Red, prevPoint.Value, currentPoint);
                }

                prevPoint = currentPoint;
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}