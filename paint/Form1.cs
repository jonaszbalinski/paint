using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paint
{
    public partial class Form1 : Form
    {
        private List<PointF> pathPoints = new List<PointF>();
        private bool shouldDrawPath = false;
        private Point drawStartPoint;

        public Form1()
        {
            InitializeComponent();

            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(pictureBox.Image);
            g.Clear(Color.White);
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox.Image);
            if (radioButtonPencil.Checked)
            {
                shouldDrawPath = true;
                pathPoints.Add(e.Location);
            }
            else if(radioButtonLine.Checked)
            {
                drawStartPoint = e.Location;
            }
            else if(radioButtonRectangle.Checked)
            {
                drawStartPoint = e.Location;
            }
            else if(radioButtonEllipse.Checked)
            {
                drawStartPoint = e.Location;
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox.Image);
            if (radioButtonPencil.Checked)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawCurve(new Pen(Color.Red, 5), pathPoints.ToArray());
                shouldDrawPath = false;
                pathPoints.Clear();

                pictureBox.Refresh();
            }
            else if (radioButtonLine.Checked)
            {
                g.DrawLine(new Pen(Color.Red, 5), drawStartPoint, e.Location);
                pictureBox.Refresh();
            }
            else if (radioButtonRectangle.Checked)
            {
                int sizeX = e.X - drawStartPoint.X;
                int sizeY = e.Y - drawStartPoint.Y;
                int startX, startY;

                if (sizeX > 0)
                {
                    startX = drawStartPoint.X;
                }
                else
                {
                    startX = e.X;
                }

                if (sizeY > 0)
                {
                    startY = drawStartPoint.Y;
                }
                else
                {
                    startY = e.Y;
                }

                g.DrawRectangle(new Pen(Color.Red, 5), new Rectangle(startX, startY, Math.Abs(sizeX), Math.Abs(sizeY)));
                pictureBox.Refresh();
            }
            else if (radioButtonEllipse.Checked)
            {
                int sizeX = e.X - drawStartPoint.X;
                int sizeY = e.Y - drawStartPoint.Y;
                int startX, startY;

                if (sizeX > 0)
                {
                    startX = drawStartPoint.X;
                }
                else
                {
                    startX = e.X;
                }

                if (sizeY > 0)
                {
                    startY = drawStartPoint.Y;
                }
                else
                {
                    startY = e.Y;
                }

                g.DrawEllipse(new Pen(Color.Red, 5), new Rectangle(startX, startY, Math.Abs(sizeX), Math.Abs(sizeY)));
                pictureBox.Refresh();
            }    
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox.Image);
            if (radioButtonPencil.Checked)
            {
                if (shouldDrawPath)
                {
                    if (e.Location.X > pathPoints.Last().X + 5 ||
                       e.Location.X < pathPoints.Last().X - 5 ||
                       e.Location.Y > pathPoints.Last().X + 5 ||
                       e.Location.Y < pathPoints.Last().X - 5)
                    {
                        if (pathPoints.Count > 5)
                        {
                            g.DrawCurve(new Pen(Color.Red, 5), pathPoints.ToArray());
                            pictureBox.Refresh();
                        }
                        pathPoints.Add(e.Location);
                    }
                }
            }
            else if (radioButtonLine.Checked)
            {

            }
            else if (radioButtonRectangle.Checked)
            {

            }
            else if (radioButtonEllipse.Checked)
            {

            }
        }
    }
}
