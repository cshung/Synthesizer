namespace Synthesizer
{
    using NAudio.Wave;
    using System;
    using System.IO;

    interface ITimbreGenerator
    {
        double value(double normalizedTime, double amptitude);
    }
}
