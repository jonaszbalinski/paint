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
        private Point lineStartPoint;

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
                lineStartPoint = e.Location;
            }
            else if(radioButtonRectangle.Checked)
            {

            }
            else if(radioButtonEllipse.Checked)
            {

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
                pathPoints = new List<PointF>();

                pictureBox.Refresh();
            }
            else if (radioButtonLine.Checked)
            {
                g.DrawLine(new Pen(Color.Red, 5), lineStartPoint, e.Location);
                pictureBox.Refresh();
            }
            else if (radioButtonRectangle.Checked)
            {

            }
            else if (radioButtonEllipse.Checked)
            {

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
