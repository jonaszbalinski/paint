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
        private Pen pen = new Pen(Color.Black, 3);
        public Form1()
        {
            InitializeComponent();

            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(pictureBox.Image);
            g.Clear(Color.White);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;

            pictureBoxColorShow.Image = new Bitmap(pictureBoxColorShow.Width, pictureBoxColorShow.Height);
            g = Graphics.FromImage(pictureBoxColorShow.Image);
            g.Clear(pen.Color);
            pictureBoxColorShow.Refresh();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox.Image);
            if (radioButtonPencil.Checked)
            {
                shouldDrawPath = true;
                pathPoints.Add(e.Location);
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
                List<PointF>fixedPathPoints = new List<PointF>();
                int precision = 5;
                for (int i = 0; i < pathPoints.Count; i += precision)
                {
                    float avgX = 0;
                    int itX = 0;

                    float avgY = 0;
                    int itY = 0;

                    for (int j = 0; j < precision; j++)
                    {
                        if((i + j) < pathPoints.Count)
                        {
                            avgX += pathPoints[i + j].X;
                            itX++;
                        }
                        else break;
                    }

                    for (int j = 0; j < precision; j++)
                    {
                        if ((i + j) < pathPoints.Count)
                        {
                            avgY += pathPoints[i + j].Y;
                            itY++;
                        }
                        else break;
                    }

                    fixedPathPoints.Add(new PointF((avgX/itX), (avgY/itY)));
                }
                fixedPathPoints.Add(e.Location);

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Color buff = pen.Color;
                pen.Color = Color.Green;
                g.DrawCurve(pen, fixedPathPoints.ToArray());
                pen.Color = buff;

                shouldDrawPath = false;
                pathPoints.Clear();
                fixedPathPoints.Clear();

                

                pictureBox.Refresh();
            }
            else if (radioButtonLine.Checked)
            {
                g.DrawLine(pen, drawStartPoint, e.Location);
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

                g.DrawRectangle(pen, new Rectangle(startX, startY, Math.Abs(sizeX), Math.Abs(sizeY)));
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

                g.DrawEllipse(pen, new Rectangle(startX, startY, Math.Abs(sizeX), Math.Abs(sizeY)));
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
                            g.DrawCurve(pen, pathPoints.ToArray());
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

        private void buttonChangeColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                pen.Color = cd.Color;
                Graphics g = Graphics.FromImage(pictureBoxColorShow.Image);
                g.Clear(pen.Color);
                pictureBoxColorShow.Refresh();
            }
        }

        private void trackBarPenSize_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBarPenSize.Value;
        }
    }
}
