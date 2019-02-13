using System.Collections.Generic;

namespace RNG.Library
{
    public interface IRandom
    {
        double Next();
        double IndificateDistributionLaw(List<double> list);
    }
}