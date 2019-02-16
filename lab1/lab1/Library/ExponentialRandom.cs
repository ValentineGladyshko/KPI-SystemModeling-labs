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

        public void DistributionStatistics()
        {
            Console.WriteLine("Expected:\n\taverage: " + (1.0 / lambda).ToString("F3") + " dispersion: " 
                              + (1.0 / Math.Pow(lambda, 2)).ToString("F3") + " critical chi-square: 28,869");
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