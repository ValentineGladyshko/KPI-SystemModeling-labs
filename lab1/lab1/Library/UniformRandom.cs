using System;
using System.Collections.Generic;
using System.Linq;

namespace RNG.Library
{
    public class UniformRandom : IRandom
    {
        private readonly long a = 1220703125;
        private readonly long c = 2147483648;

        private long current;

        public UniformRandom()
        {
            Random random = new Random();
            current = random.Next();
        }

        public UniformRandom(long current)
        {
            this.current = current;
        }

        public double Next()
        {
            current = (a * current) % c;
            return (double) current / c;
        }

        public double StatisticCount(double x)
        {
            return x;
        }

        public double IndificateDistributionLaw(List<double> list)
        {
            if (list != null)
            {
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

                double сhi = 0;

                foreach (var some in stat)
                {
                    double x1 = (StatisticCount(start + step) - StatisticCount(start)) * list.Count;
                    double x2 = some;
                    сhi += Math.Pow((x1 - x2), 2) / x1;
                    start += step;
                }

                return сhi;
            }

            return 10;
        }

        public void DistributionStatistics()
        {
            Console.WriteLine("Expected:\n\taverage 0,500 dispersion: 0,000 critical chi-square: 27,587");
        }
    }
}