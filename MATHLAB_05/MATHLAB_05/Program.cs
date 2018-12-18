using System;

namespace Lab5
{
    class Program
    {

        private static double a = 0.0;
        private static double b = Math.PI;

        static double f(double x)
        {
            var result = x.Equals(0.0) ? 0.0 : x * Math.Sin(x) / (1 + Math.Pow(Math.Cos(x), 2));
            return result;
        }

        static void Trapezoid_Method(int n)
        {
            double Result_1, Result_2;

            double E = 0.00005;

            do
            {
                Result_1 = Result_For_T(n);
                Result_2 = Result_For_T(n * 2);

                n *= 2;

            } while (Math.Abs(Result_1 - Result_2) > E);

            Console.WriteLine(Result_1);
        }

        static double Result_For_T(int n)
        {
            double sum = 0;

            for (var i = 0; i <= n; i++)
            {
                var koef = (i == 0 || i == n) ? 1 : 2;
                sum += f(i * ((b - a) / n)) * koef;
            }

            return ((b - a) / (2.0 * n)) * sum;
        }

        static void Method_Simpson(int n)
        {
            double Result_1, Result_2;

            double E = 0.00005;

            do
            {
                Result_1 = Result_For_S(n);
                Result_2 = Result_For_S(n * 2);

                n *= 2;

            } while (Math.Abs(Result_1 - Result_2) > E);

            Console.WriteLine(Result_1);
        }

        static double Result_For_S(int n)
        {
            double sum = 0;

            for (var i = 0; i <= n; i++)
            {
                var koef = (i == 0 || i == n) ? 1 : (i % 2 == 1) ? 4 : 2;
                sum += f(i * ((b - a) / n)) * koef;
            }

            return ((b - a) / (3.0 * n)) * sum;
        }

        static void Main(string[] args)
        {
            var n = 2;

            Console.WriteLine("Метод трапеций");
            Trapezoid_Method(n);

            Console.WriteLine("\nМетод Симпсона");
            Method_Simpson(n);

            Console.ReadLine();
        }
    }
}