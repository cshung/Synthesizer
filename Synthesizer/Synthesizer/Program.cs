namespace Synthesizer
{
    using NAudio.Wave;
    using System;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            String filename = @"c:\temp\sound.wav";
            WaveFileWriter.CreateWaveFile(filename, new SignalGenerator(/*samplingRate */44100, new ScaleGenerator(), new SineTimbreGenerator(), /* duration */ 8));
        }
    }
}