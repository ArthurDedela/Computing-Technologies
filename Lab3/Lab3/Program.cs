using System;
using System.Runtime.InteropServices;

namespace Lab3
{
    class Program
    {
        static double f(double x, double y) => x * x * x + 2 * x * x - 3 * x * y * y + 6 * x * y - x - 2 * y * y + y + 4;
        static double g(double x, double y) => -(y * y * y) + 3 * y * y + 3 * x * x * y + 4 * x * y - y - 3 * x * x - x - 2;

        static double f_x(double x, double y) => 3 * x * x + 4 * x - 3 * y * y + 6 * y - 1;
        static double f_y(double x, double y) => -6 * x * y + 6 * x - 4 * y + 1;
        static double g_x(double x, double y) => 6 * x * y + 4 * y - 6 * x - 1;
        static double g_y(double x, double y) => -3 * y * y + 6 * y + 3 * x * x + 4 * y - 1;

        static double f_x_analytic(double x, double y, double h) => (f(x + h, y) - f(x, y)) / h;
        static double f_y_analytic(double x, double y, double h) => (f(x, y + h) - f(x, y)) / h;
        static double g_x_analytic(double x, double y, double h) => (g(x + h, y) - g(x, y)) / h;
        static double g_y_analytic(double x, double y, double h) => (g(x, y + h) - g(x, y)) / h;

        static void Analytic(double x, double y)
        {
            var k = 0;
            double xn;
            double yn;
            const double E = 0.00001;

            do
            {
                xn = x;
                yn = y;

                var delta = f_x(x, y) * g_y(x, y) - g_x(x, y) * f_y(x, y);

                x = xn - (g_y(x, y) * f(x, y) - f_y(x, y) * g(x, y)) / delta;
                y = yn - (f_x(x, y) * g(x, y) - g_x(x, y) * f(x, y)) / delta;

                k++;
            } while (Math.Abs(xn - x) >= E && Math.Abs(yn - y) >= E);

            Console.WriteLine($"Количество итераций:\t{k}\n\t\tx:\t{x}\n\t\ty:\t{y}\n");
        }

        static void Numeric(double x, double y)
        {
            var k = 0;
            double xn;
            double yn;
            var E = 0.00001;
            var h = 0.001;

            do
            {
                xn = x;
                yn = y;

                double delta = f_x_analytic(x, y, h) * g_y_analytic(x, y, h) - g_x_analytic(x, y, h) * f_y_analytic(x, y, h);

                x = xn - (g_y_analytic(x, y, h) * f(x, y) - f_y_analytic(x, y, h) * g(x, y)) / delta;
                y = yn - (f_x_analytic(x, y, h) * g(x, y) - g_x_analytic(x, y, h) * f(x, y)) / delta;

                k++;
            } while (Math.Abs(xn - x) >= E && Math.Abs(yn - y) >= E);

            Console.WriteLine($"Количество итераций:\t{k}\n\t\tx:\t{x}\n\t\ty:\t{y}\n");
        }


        static void Main()
        {
            Console.WriteLine("Частные производные находятся численно (-2, 2)");
            Numeric(-2.0, 2.0);
            Console.WriteLine("Частные производные находятся аналитически (-2, 2)");
            Analytic(-2.0, 2.0);
            Console.WriteLine("**************************************");


            Console.WriteLine("Частные производные находятся численно (0, 2)");
            Numeric(0.0, 2.0);
            Console.WriteLine("Частные производные находятся аналитически (0, 2)");
            Analytic(0.0, 2.0);
            Console.WriteLine("**************************************");

        
            Console.WriteLine("Частные производные находятся численно (0, -1)");
            Numeric(0.0, -1.0);
            Console.WriteLine("Частные производные находятся аналитически (0, -1)");
            Analytic(0.0, -1.0);
            Console.WriteLine("**************************************");

            Console.ReadKey();
        }
    }
}