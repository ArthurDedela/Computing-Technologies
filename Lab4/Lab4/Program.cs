using System;

namespace Lab4
{
    class Program
    {
        const int size = 3;
        static readonly double[,] A = {
            {1.65, -1.76, 0.77},
            {-1.76, 1.04, -2.61},
            {0.77, -2.61, -3.18}
        };

        static readonly double[] B = {2.15, 0.86, -0.73};

        private static void MethodGauss()
        {
            double[] Q = new double[size];
            double[] X = new double[size];

            for (int k = 0; k < size - 1; k++)
            {
                for (int i = 0; i < size - 1; i++)
                    Q[i] = A[i + 1, k] / A[k, k];

                for (int i = 0, j = k; j < size - 1;)
                {
                    A[j + 1, i] = A[j + 1, i] - Q[j] * A[k, i];

                    if (i == size - 1)
                    {
                        B[j + 1] = B[j + 1] - Q[j] * B[k];
                        j++;
                        i = 0;
                    }
                    else i++;
                }
            }

            X[size - 1] = B[size - 1] / A[size - 1, size - 1];

            double s;
            for (int i = size - 2; ; i--)
            {
                s = 0;

                for (int j = i + 1; j < size; j++)
                    s += A[i, j] * X[j];

                X[i] = (B[i] - s) / A[i, i];

                if (i == 0) break;
            }

            Console.WriteLine("Метод Гаусса");

            for (int i = 0; i < size; i++)
                Console.WriteLine($"{i} {X[i] }");
        }

        private static void MethodOfSteepestDescent(double E)
        {

            double[] Xp = new double[size], A_Xp = new double[size], rp = new double[size], A_Transposed_A_r = new double[size], Mu = new double[size], Transposed_A_r = new double[size];
            double[,] Transposed_A = new double[size, size];
            double[] Xp_next = new double[size];
            double[,] A_ = new double[size, size];
            double mu_, for_mu1, for_mu2;

            int[] t = { 0, 0, 0 };
            int it = 0;

            for (int i = 0; i < size; i++)
            {
                Xp[i] = B[i] / A[i, i];
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Transposed_A[i, j] = A[j, i];
                }
            }

            for (it = 0; ; it++)
            {
                for (int i = 0; i < size; i++)
                {
                    A_Xp[i] = 0;

                    for (int j = 0; j < size; j++)
                    {
                        A_Xp[i] += A[i, j] * Xp[j];
                    }

                    rp[i] = A_Xp[i] - B[i];
                }

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        A_[i, j] = 0;

                        for (int k = 0; k < size; k++)
                        {
                            A_[i, j] += A[i, k] * Transposed_A[k, j];
                        }
                    }
                }

                for (int i = 0; i < size; i++)
                {
                    A_Transposed_A_r[i] = 0;

                    for (int j = 0; j < size; j++)
                    {
                        A_Transposed_A_r[i] += A_[i, j] * rp[j];
                    }
                }

                for_mu1 = for_mu2 = 0;

                for (int j = 0; j < size; j++)
                {
                    for_mu1 += rp[j] * A_Transposed_A_r[j];
                    for_mu2 += A_Transposed_A_r[j] * A_Transposed_A_r[j];
                }

                mu_ = for_mu1 / for_mu2;

                for (int i = 0; i < size; i++)
                {
                    Mu[i] = mu_;
                }

                for (int i = 0; i < size; i++)
                {
                    Transposed_A_r[i] = 0;

                    for (int j = 0; j < size; j++)
                        Transposed_A_r[i] += Transposed_A[i, j] * rp[j];
                }

                for (int i = 0; i < size; i++)
                {
                    Xp_next[i] = Xp[i];
                    Xp[i] = Xp[i] - Mu[i] * Transposed_A_r[i];
                }

                int q = 0;

                

                for (int i = 0; i < size; i++)
                {
                    if (Math.Abs(rp[i]) < E && (Xp_next[i] - Xp[i]) < E) t[i] = 1;
                    q += t[i];
                }

                if (q == size) break;
            }

            Console.WriteLine("Метод наискорейшего спуска");

            for (int i = 0; i < size; i++)
                Console.WriteLine($"{i} {Xp[i] }");

            Console.WriteLine($"Кол-во итераций: {it}");
        }

        static void Main(string[] args)
        {
            MethodOfSteepestDescent(Math.Pow(10, -7));

            MethodGauss();

            Console.ReadKey();
        }
    }
}