using System;
using System.Collections.Generic;
using System.Linq;

namespace RNG.Library
{
    public class ExponentialRandom : IRandom
    {
        private double lambda;
        private Random random = new Random();

        public ExponentialRandom()
        {
            lambda = 1;
        }

        public ExponentialRandom(double lambda)
        {
            this.lambda = lambda;
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

                double phi = 0;

                foreach (var some in stat)
                {
                    double x1 = StatisticCount(start + step) - StatisticCount(start);
                    double x2 = (double) some / list.Count;
                    phi += Math.Pow((x1 - x2), 2) / x1;
                    start += step;
                }

                return phi;
            }

            return 10;
        }

        public double Next()
        {
            return -1 / lambda * Math.Log(random.NextDouble());
        }

        public double StatisticCount(double x)
        {
            return 1 - Math.Exp(-lambda * x);
        }
    }
}