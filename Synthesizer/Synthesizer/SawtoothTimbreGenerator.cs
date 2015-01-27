namespace Synthesizer
{
    class SawtoothTimbreGenerator : ITimbreGenerator
    {
        public double value(double normalizedTime, double amptitude)
        {
            return -2 * normalizedTime * amptitude + amptitude;
        }
    }
}
