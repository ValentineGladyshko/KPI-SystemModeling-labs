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
            IRandom random = new ExponentialRandom();
            List<double> list = new List<double>(10000);

            for (int i = 0; i < 10000; i++)
            {
                list.Add(random.Next());
            }

            Show(list);
            Console.WriteLine("Quadratic Chi:" + random.IndificateDistributionLaw(list));

            random = new UniformRandom();
            list = new List<double>(10000);

            for (int i = 0; i < 10000; i++)
            {
                list.Add(random.Next());
            }

            Show(list);
            Console.WriteLine("Quadratic Chi:" + random.IndificateDistributionLaw(list));

            random = new GaussRandom();
            list = new List<double>(10000);

            for (int i = 0; i < 10000; i++)
            {
                list.Add(random.Next());
            }

            Show(list);
            Console.WriteLine("Quadratic Chi:" + random.IndificateDistributionLaw(list));

            Console.WriteLine("Press any key to continue...");
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

                Console.WriteLine(stat.Sum());

                foreach (var some in stat)
                {
                    Console.WriteLine(start.ToString("F") + " - " + (start + step).ToString("F") + " : " + some);
                    start += step;
                }
            }
        }

        //static void DistributionLaw(List<double> list, IRandom random)
        //{
        //    if (list != null)
        //    {
        //        double step = (list.Max() - list.Min()) / 20;
        //        double[] range = new double[21];
        //        double start = list.Min();
        //        for (int i = 0; i < 20; i++)
        //        {
        //            range[i] = start + i * step;
        //        }

        //        range[20] = list.Max();

        //        int[] stat = new int[20];
        //        foreach (var num in list)
        //        {
        //            int i = 1;
        //            while (num > range[i])
        //            {
        //                i++;
        //            }

        //            stat[i - 1]++;
        //        }

        //        start = list.Min();

        //        double phi = 0;
        //        double sum = 0;
        //        foreach (var some in stat)
        //        {
        //            double x1 = random.StatisticCount(start + step/2) /*- random.StatisticCount(start)*/;
        //            sum += x1;
        //            double x2 = (double) some / list.Count;
        //            phi += Math.Pow((x1 - x2), 2) / x1;
        //            start += step;
        //        }

        //        Console.WriteLine(sum +" "+phi);
        //    }
        //}
    }
}
