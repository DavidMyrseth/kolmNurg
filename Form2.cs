using System;
using System.Drawing;
using System.Windows.Forms;

namespace triangle
{
    public partial class Form2 : Form
    {
        private TextBox txtAngleA, txtAngleB, txtAngleC;
        private Button btnCalculate;
        private Panel drawPanel;
        private const double defaultSideA = 100; // Дефолтное значение для стороны A

        public Form2(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Text = "Форма 3 - Углы в градусах";

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.BackColor = Color.Pink;

            // Поля ввода для углов
            Label lblAngleA = new Label { Text = "Угол A (град):", Location = new Point(20, 20), AutoSize = true };
            txtAngleA = new TextBox { Location = new Point(120, 20), Width = 100 };
            Controls.Add(lblAngleA);
            Controls.Add(txtAngleA);

            Label lblAngleB = new Label { Text = "Угол B (град):", Location = new Point(20, 60), AutoSize = true };
            txtAngleB = new TextBox { Location = new Point(120, 60), Width = 100 };
            Controls.Add(lblAngleB);
            Controls.Add(txtAngleB);

            Label lblAngleC = new Label { Text = "Угол C (град):", Location = new Point(20, 100), AutoSize = true };
            txtAngleC = new TextBox { Location = new Point(120, 100), Width = 100, ReadOnly = true }; // Только для чтения
            Controls.Add(lblAngleC);
            Controls.Add(txtAngleC);

            // Кнопка для вычислений
            btnCalculate = new Button { Text = "Рассчитать и нарисовать", Location = new Point(20, 140), Width = 200 };
            btnCalculate.Click += BtnCalculate_Click;
            Controls.Add(btnCalculate);

            // Панель для рисования треугольника
            drawPanel = new Panel { Location = new Point(250, 20), Size = new Size(300, 300), BackColor = Color.White };
            Controls.Add(drawPanel);
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                double angleA = Convert.ToDouble(txtAngleA.Text);
                double angleB = Convert.ToDouble(txtAngleB.Text);
                double angleC = 180 - angleA - angleB; // Вычисляем угол C
                txtAngleC.Text = angleC.ToString();    // Показываем угол C в интерфейсе

                // Проверка суммы углов
                if (angleC <= 0)
                {
                    MessageBox.Show("Сумма углов A и B должна быть меньше 180 градусов!");
                    return;
                }

                // Вычисляем стороны B и C
                double sideA = defaultSideA; // Используем дефолтное значение для стороны A
                double sideB = sideA * Math.Sin(DegreeToRadian(angleB)) / Math.Sin(DegreeToRadian(angleA));
                double sideC = sideA * Math.Sin(DegreeToRadian(angleC)) / Math.Sin(DegreeToRadian(angleA));

                // Очистка панели и рисование треугольника
                drawPanel.Invalidate();
                drawPanel.Paint += (s, pe) => DrawTriangle(pe.Graphics, sideA, sideB, sideC, angleA, angleB, angleC);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка ввода: " + ex.Message);
            }
        }

        private void DrawTriangle(Graphics g, double a, double b, double c, double angleA, double angleB, double angleC)
        {
            Pen pen = new Pen(Color.Black, 2);

            // Получаем размеры панели для рисования
            float panelWidth = drawPanel.ClientSize.Width;
            float panelHeight = drawPanel.ClientSize.Height;

            // Рассчитываем максимальную длину стороны, чтобы масштабировать треугольник, сохраняя пропорции
            double maxSide = Math.Max(a, Math.Max(b, c));
            float scale = Math.Min(panelWidth, panelHeight) / (float)maxSide;

            // Устанавливаем начальные координаты для первой точки
            float x1 = 20, y1 = panelHeight - 20;  // Начинаем от нижнего левого угла
            float x2 = x1 + (float)(a * scale), y2 = y1;

            // Вычисляем координаты третьей точки по углу B
            float x3 = x1 + (float)(b * scale * Math.Cos(DegreeToRadian(angleB)));
            float y3 = y1 - (float)(b * scale * Math.Sin(DegreeToRadian(angleB)));

            // Рисуем треугольник
            g.DrawLine(pen, x1, y1, x2, y2);
            g.DrawLine(pen, x2, y2, x3, y3);
            g.DrawLine(pen, x3, y3, x1, y1);

            // Метки сторон
            g.DrawString("A", new Font("Arial", 10), Brushes.Black, x1 - 15, y1);
            g.DrawString("B", new Font("Arial", 10), Brushes.Black, x2 + 5, y2);
            g.DrawString("C", new Font("Arial", 10), Brushes.Black, x3, y3 - 15);
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}