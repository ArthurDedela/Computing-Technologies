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
//            if (Math.Abs(x) >= 0.25) throw new ArgumentOutOfRangeException();

            var sum = 0.0;      
            var original = OriginalFunc(x);
            long k = 0;
            double fac = 1;
//            double upperFac = 1;
//            double lowerFac = 1;

            do
            {
                var sign = k % 2 == 0 ? 1 : -1;
                var member = sign * fac;
//                var member = sign * fac * Math.Pow(x, 2 * k);
                
                sum += member;
                k++;

//                upperFac *= (4d * k - 3d) * (4d * k - 2d) * (4d * k - 1d) * (4d * k);
//                lowerFac *= (2d * k - 1d) * (2d * k) * (2d * k - 1d) * (2d * k);

                fac *= (4.0 * k - 3.0) / (2.0 * k - 1.0) * (4.0 * k - 2.0) / (2.0 * k) * (4.0 * k - 1.0) / (2.0 * k - 1.0) * (4.0 * k) / (2.0 * k) * x * x;
            } while (Math.Abs(original - sum) >= E);

            Console.WriteLine(original);
            Console.WriteLine($"Итераций(k): {k}");

            return sum;
        }

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            var res = Calculate(-0.25, 1e-15);

            sw.Stop();

            Console.WriteLine($"Ответ: {res:F15}\nВремя работы: {sw.Elapsed.ToString()}");
            Console.ReadKey();
        }
    }
}
