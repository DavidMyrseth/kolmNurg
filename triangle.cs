using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kolmNurg
{
    class Triangle
    {
        public double a;
        public double b;
        public double c;

        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
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
            return a + b + c;
        }

        public double Surface()
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public double GetSetA
        {
            get { return a; }
            set { a = value; }
        }

        public double GetSetB
        {
            get { return b; }
            set { b = value; }
        }

        public double GetSetC
        {
            get { return c; }
            set { c = value; }
        }

        public bool ExistTriangle
        {
            get
            {
                return a + b > c && a + c > b && b + c > a;
            }
        }

        public string TriangleType
        {
            get
            {
                if (ExistTriangle)
                {
                    if (a == b && b == c)
                    {
                        return "võrdkülgne"; // Equilateral
                    }
                    else if (a == b || b == c || a == c)
                    {
                        return "võrdhaarne"; // Isosceles
                    }
                    else
                    {
                        return "erikülgne"; // Scalene
                    }
                }
                else
                {
                    return "tundmatu tüüp"; // Unknown type
                }
            }
        }
    }
}
