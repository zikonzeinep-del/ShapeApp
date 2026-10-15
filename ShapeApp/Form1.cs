using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeApp
{
    public partial class Form1 : Form
    {
        private bool isDrawing = false;
        private Point currentPoint;
        private Pen drawingPen;
        private Bitmap drawingBitmap;
        private Graphics bmpGraphics;

        public Form1()
        {
            InitializeComponent();

            // Үзэгний анхдагч тохиргоо (Хар өнгө, 3 өргөн)
            drawingPen = new Pen(Color.Black, 3);
            drawingPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            drawingPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            this.DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Формын хэмжээгээр цагаан дэвсгэртэй зураг санах ойд бэлтгэх
            drawingBitmap = new Bitmap(Math.Max(this.ClientSize.Width, 800), Math.Max(this.ClientSize.Height, 600));
            bmpGraphics = Graphics.FromImage(drawingBitmap);
            bmpGraphics.Clear(Color.White);

            // Хулганы болон бусад эвэнтүүдийг Form дээр шууд холбох
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            this.Paint += Form1_Paint;

            if (this.numericUpDown1 != null)
            {
                this.numericUpDown1.Minimum = 1;
                this.numericUpDown1.Value = 3;
                this.numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            }

            if (this.button1 != null)
            {
                this.button1.Text = "Цэвэрлэх";
                this.button1.Click += button1_Click;
            }
        }

        // Зурсан зургаа форм дээр гаргаж харуулах
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (drawingBitmap != null)
            {
                e.Graphics.DrawImage(drawingBitmap, 0, 0);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                currentPoint = e.Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                // Заагч зурах үйлдэл
                bmpGraphics.DrawLine(drawingPen, currentPoint, e.Location);
                currentPoint = e.Location;

                // Дэлгэцийг шинэчилж шууд харуулах
                this.Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0)
            {
                drawingPen.Width = (float)numericUpDown1.Value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bmpGraphics != null)
            {
                bmpGraphics.Clear(Color.White);
                this.Invalidate();
            }
        }
    }
}