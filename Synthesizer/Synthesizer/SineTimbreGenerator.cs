namespace Synthesizer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class SineTimbreGenerator : ITimbreGenerator
    {
        public double value(double normalizedTime, double amptitude)
        {
            return Math.Sin(normalizedTime * 2 * Math.PI) * amptitude;
        }
    }
}
