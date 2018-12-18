using System;

namespace Lab_2
{
    class Program
    {
        static double Equation(double x)
        {
            return Math.Pow(1.4, x) - 2 * x - 0.5;
        }

        static double EquationDerivative(double x)
        {
            return Math.Pow(1.4, x) * Math.Log(1.4) - 2;
        }

        static double EquationSecondDerivative(double x)
        {
            var log = Math.Log(1.4);
            return log * log * Math.Pow(1.4, x);
        }

        static void Main()
        {
            var a1 = 0.0;
            var b1 = 1.0;

            var a2 = 8;
            var b2 = 9;

            Console.WriteLine("Метод Ньютона");
            Newton(a1, b1);
            Newton(a2, b2);
            Console.WriteLine();

            Console.WriteLine("Метод релаксации");
            Relax(a1, b1);
            Relax(a2, b2);
            Console.ReadLine();
        }
      
        static void Relax(double a, double b)
        {
            var E = 1e-5;
            var i = 0;
            var x = (a + b) / 2.0;
            var inf = Math.Min(EquationDerivative(a), EquationDerivative(b));
            var sup = Math.Max(EquationDerivative(a), EquationDerivative(b));
            var tau = -2.0 / (inf + sup);

            var x1 = x + Equation(x) * tau;
            do
            {
                x = x1;
                x1 = x + Equation(x) * tau;
                i++;
            } while (Math.Abs(x1 - x) > E);

            Console.WriteLine($"Количество итераций: \t{i}");
            Console.WriteLine($"Корень: \t\t{x}");
        }

        static void Newton(double a, double b)
        {
            var i = 0;
            var E = 1e-5;

            var middleValue = Equation(a) * EquationSecondDerivative(b) > 0 ? a : b;
            double x;

            do
            {
                x = middleValue;
                middleValue = x - Equation(x) / EquationDerivative(x);
                i++;
            } while (Math.Abs(x - middleValue) > E);

            Console.WriteLine($"Количество итераций: \t{i}");
            Console.WriteLine($"Корень: \t\t{x}");
        }
    }
}