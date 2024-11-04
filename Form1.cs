using System;
using System.Windows.Forms;

namespace kolmNurg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        class kolmNurg
        {
            public double a;
            public double b;
            public double c;
            public string name;

            // Конструктор с параметрами
            public kolmNurg(double A, double B, double C, string Name)
            {
                a = A;
                b = B;
                c = C;
                name = Name;
            }

            // Конструктор без параметров
            public kolmNurg()
            {
                a = 3;
                b = 4;
                c = 5;
                name = "DefaultTriangle";
            }

            public string outputA()
            {
                return Convert.ToString(a);
            }

            public string outputB()
            {
                return Convert.ToString(b);
            }

            public string outputC()
            {
                return Convert.ToString(c);
            }

            public double Perimeter()
            {
                double p = 0;
                p = a + b + c;
                return p;
            }

            public double surFace()
            {
                double s = 0;
                double p = 0;
                p = (a + b + c) / 2;
                s = Math.Sqrt((p * (p - a) * (p - b) * (p - c)));
                return s;
            }

            public double GetSetA
            {
                get
                { return a; }
                set
                { a = value; }
            }

            public double GetSetB
            {
                get
                { return b; }
                set
                { b = value; }
            }
            public double GetSetC
            {
                get
                { return c; }
                set
                { c = value; }
            }

            // Новое свойство для имени треугольника
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            // Новый метод для нахождения высоты, опущенной на сторону a
            public double HeightFromA()
            {
                return 2 * surFace() / a;
            }

            public bool ExistTriangle
            {
                get
                {
                    if ((a > b) && (b > a + c) && (c > a + b))
                        return false;
                    else return true;
                }
            }
        }

        // Обработчик события для кнопки "Calculate"
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // Создаем экземпляр треугольника
            kolmNurg triangle = new kolmNurg(3, 4, 5, "MyTriangle");

            // Проверяем существование треугольника
            if (triangle.ExistTriangle)
            {
                // Вычисляем периметр, площадь и высоту
                double perimeter = triangle.Perimeter();
                double area = triangle.surFace();
                double heightFromA = triangle.HeightFromA();

                // Выводим результат
                lblResult.Text = $"Треугольник {triangle.Name}:\n" +
                    $"Стороны: a={triangle.a}, b={triangle.b}, c={triangle.c}\n" +
                    $"Периметр: {perimeter}\n" +
                    $"Площадь: {area:F2}\n" +
                    $"Высота на сторону a: {heightFromA:F2}";
            }
            else
            {
                lblResult.Text = "Такой треугольник не существует!";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}