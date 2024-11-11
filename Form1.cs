using System;
using System.Drawing;
using System.Windows.Forms;

namespace triangle
{
    public partial class Form1 : Form
    {
        private double a, b, c;
        private Panel drawPanel;

        public Form1(int w, int h, double sideA, double sideB, double sideC)
        {
            this.Width = w;
            this.Height = h;
            a = sideA;
            b = sideB;
            c = sideC;

            this.Text = "Рисование треугольника";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            drawPanel = new Panel
            {
                Size = new Size(200, 150),
                Location = new Point(50, 50),
                BackColor = Color.White
            };
            this.Controls.Add(drawPanel);

            // Подписываемся на событие перерисовки панели
            drawPanel.Paint += DrawPanel_Paint;
        }

        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (a + b <= c || a + c <= b || b + c <= a)
            {
                MessageBox.Show("Треугольник с такими сторонами не существует");
                return;
            }

            // Масштабирование, чтобы треугольник вписывался в панель
            double maxSide = Math.Max(a, Math.Max(b, c));  // Находим самую длинную сторону
            double scale = Math.Min(drawPanel.Width, drawPanel.Height) / maxSide * 0.8; // 80% от меньшего размера панели

            // Масштабируем стороны
            a *= scale;
            b *= scale;
            c *= scale;

            // Начальная точка для стороны a
            PointF pointA = new PointF(10, drawPanel.Height - 30);
            PointF pointB = new PointF(pointA.X + (float)a, pointA.Y);

            // Вычисляем угол для третьей вершины
            double angleC = Math.Acos((a * a + b * b - c * c) / (2 * a * b));
            float xC = (float)(pointA.X + b * Math.Cos(angleC));
            float yC = (float)(pointA.Y - b * Math.Sin(angleC));

            PointF pointC = new PointF(xC, yC);

            float centerX = drawPanel.Width / 2;
            float centerY = drawPanel.Height / 2;

            float offsetX = centerX - (pointA.X + pointB.X + pointC.X) / 3;
            float offsetY = centerY - (pointA.Y + pointB.Y + pointC.Y) / 3;

            offsetY += 10;

            pointA.X += offsetX;
            pointA.Y += offsetY;
            pointB.X += offsetX;
            pointB.Y += offsetY;
            pointC.X += offsetX;
            pointC.Y += offsetY;

            // Рисуем треугольник
            Pen pen = new Pen(Color.Blue, 2);
            g.DrawLine(pen, pointA, pointB);
            g.DrawLine(pen, pointB, pointC);
            g.DrawLine(pen, pointC, pointA);

            // Метки для сторон
            g.DrawString("A", new Font("Arial", 12), Brushes.Black, pointA.X - 20, pointA.Y);
            g.DrawString("B", new Font("Arial", 12), Brushes.Black, pointB.X + 10, pointB.Y);
            g.DrawString("C", new Font("Arial", 12), Brushes.Black, pointC.X, pointC.Y - 20);
        }
    }
}