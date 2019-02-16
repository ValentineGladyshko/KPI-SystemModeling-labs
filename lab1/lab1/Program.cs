using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RNG.Library;

namespace RNG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Exponential Random:\n");

            Console.Write("Enter lambda: ");
            string lambdaLine = Console.ReadLine();
            IRandom random = new ExponentialRandom();
            if (double.TryParse(lambdaLine, out double lambda))
            {
                random = new ExponentialRandom(lambda);
            }
            
            List<double> list = new List<double>(10000);

            for (int i = 0; i < 10000; i++)
            {
                list.Add(random.Next());
            }

            Show(list);
            random.DistributionStatistics();
            DistributionStatistics(list);
            Console.WriteLine("Quadratic Chi: " + random.IndificateDistributionLaw(list));

            Console.WriteLine("\nUniform Random:\n");

            random = new UniformRandom();
            list = new List<double>(10000);

            for (int i = 0; i < 10000; i++)
            {
                list.Add(random.Next());
            }

            Show(list);
            random.DistributionStatistics();
            DistributionStatistics(list);
            Console.WriteLine("Quadratic Chi: " + random.IndificateDistributionLaw(list));

            Console.WriteLine("\nGauss Random:\n");

            Console.Write("Enter sigma: ");
            string sigmaLine = Console.ReadLine();
            Console.Write("Enter alpha: ");
            string alphaLine = Console.ReadLine();

            random = new GaussRandom();
            if (double.TryParse(sigmaLine, out double sigma) && double.TryParse(alphaLine, out double alpha))
            {
                random = new GaussRandom(sigma, alpha);
            }

            list = new List<double>(10000);

            for (int i = 0; i < 10000; i++)
            {
                list.Add(random.Next());
            }

            Show(list);
            random.DistributionStatistics();
            DistributionStatistics(list);
            Console.WriteLine("Quadratic Chi: " + random.IndificateDistributionLaw(list));

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void Show(List<double> list)
        {
            if (list != null)
            {
                Console.WriteLine("min: " + list.Min().ToString("F") + " max: " + list.Max().ToString("F"));

                double step = (list.Max() - list.Min()) / 20;
                double[] range = new double[21];
                double start = list.Min();
                for (int i = 0; i < 20; i++)
                {
                    range[i] = start + i * step;
                }

                range[20] = list.Max();

                int[] stat = new int[20];
                foreach (var num in list)
                {
                    int i = 1;
                    while (num > range[i])
                    {
                        i++;
                    }

                    stat[i - 1]++;
                }

                start = list.Min();

                foreach (var some in stat)
                {
                    Console.WriteLine(start.ToString("F") + " - " + (start + step).ToString("F") + " : " + some);
                    start += step;
                }
            }
        }

        public static void DistributionStatistics(List<double> list)
        {
            double average = list.Sum() / list.Count;
            double dispersion = 0;
            foreach (var num in list)
            {
                dispersion += Math.Pow((num - average), 2);
            }

            dispersion = dispersion / list.Count;

            Console.WriteLine("Real:\n\taverage: " + average.ToString("F4") + " dispersion: " + dispersion.ToString("F4"));
        }
    }
}
