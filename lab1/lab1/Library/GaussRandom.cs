using System;
using System.Collections.Generic;
using System.Linq;

namespace RNG.Library
{
    public class GaussRandom : IRandom
    {
        private double sigma;
        private double alpha;
        private Random random = new Random();

        public GaussRandom()
        {
            sigma = 1;
            alpha = 6;
        }

        public GaussRandom(double sigma, double alpha)
        {
            this.sigma = sigma;
            this.alpha = alpha;
        }

        public double Next()
        {
            double num = -6;
            for (int i = 0; i < 12; i++)
            {
                num += random.NextDouble();
            }

            return sigma * num + alpha;
        }

        public double StatisticCount(double x)
        {
            double i1 = Math.Pow((x - alpha), 2);
            double i2 = (2 * Math.Pow(sigma, 2));
            double i3 = 1.0 / (sigma * Math.Sqrt(2 * Math.PI));
            double i4 = i3;
            return 1.0 / (sigma * Math.Sqrt(2 * Math.PI)) *
                   Math.Exp(-(i1 / i2));
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
                    double x2 = (double)some / list.Count;
                    phi += Math.Pow((x1 - x2), 2) / x1;
                    start += step;
                }

                return phi;
            }

            return 10;
        }
    }
}