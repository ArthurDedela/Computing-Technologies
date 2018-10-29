using System;
using System.Diagnostics;

namespace Lab_1
{
    class Program
    {
        private static double OriginalFunc(double x)
        {
            return Math.Sqrt((1 + Math.Sqrt(1 + 16 * x * x)) / (2 * (1 + 16 * x * x)));
        }

        private static double Calculate(double x, double E = 0.1)
        {
            if (Math.Abs(x) >= 0.25) throw new ArgumentOutOfRangeException(nameof(x));
     
            var original = OriginalFunc(x);
            Console.WriteLine($"Истинное значение: {original}");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var sum = 0.0;
            long k = 0;
            double sumMember = 1;
 
            do
            {
                var sign = k % 2 == 0 ? 1 : -1;
                sum += sumMember * sign;
                ++k;

                sumMember *= (4.0 * k - 3.0) / (2.0 * k - 1.0) * (4.0 * k - 2.0) / (2.0 * k) * (4.0 * k - 1.0) / (2.0 * k - 1.0) * (4.0 * k) / (2.0 * k) * x * x;
            } while (Math.Abs(original - sum) >= E);
            sw.Stop();

            Console.WriteLine($"Время вычислений: {sw.Elapsed.ToString()}");
            Console.WriteLine($"Итераций(k): {k}");

            return sum;
        }

        static void Main(string[] args)
        {
            var res = Calculate(0.03, 1e-15);

            Console.WriteLine($"Ответ: {res:F15}");
            Console.ReadKey();
        }
    }
}
