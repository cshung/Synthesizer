namespace NAudioLab
{
    using NAudio.Wave;
    using System;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            String filename = @"c:\temp\sound.wav";
            WaveFileWriter.CreateWaveFile(filename, new SignalGenerator(/*samplingRate */44100, new NotesGenerator(), /* duration */ 8));
        }
    }

    interface INotesGenerator
    {
        int Notes(double time);
    }

    class NotesGenerator : INotesGenerator
    {
        public int Notes(double time)
        {
            // -9 -8 -7 -6 -5 -4  -3 -2 -1  0  1  2  3
            //  c c#  d d#  e  f  f#  g g#  a a#  b  c
            return (new int[] { -9, -7, -5, -4, -2, 0, 2, 3 })[(int)time] - 12;
        }
    }

    class SignalGenerator : IWaveProvider
    {
        private int samplingRate;
        private int duration;
        private WaveFormat waveFormat;
        private long currentSample;
        private INotesGenerator generator;

        public SignalGenerator(int samplingRate, INotesGenerator generator, int duration)
        {
            this.samplingRate = samplingRate;
            this.generator = generator;
            this.duration = duration;
            this.waveFormat = new WaveFormat(samplingRate, 1);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            int written = 0;
            int pointer = offset;
            for (int i = 0; i < count && currentSample < this.duration * this.samplingRate; i++, currentSample++)
            {
                double time = ((double)currentSample) / this.samplingRate;
                double frequency = 440 * Math.Pow(2, this.generator.Notes(time) / 12.0);
                double period = 1 / frequency;
                int completed_periods = (int)(time / period);
                double time_within_period = time - period * completed_periods;
                double normalized_time = time_within_period / period;
                double amptitude = 100;
                buffer[pointer++] = (byte)(-amptitude * normalized_time + amptitude);
                // buffer[pointer++] = (byte)(Math.Sin(frequency * 2 * Math.PI * currentSample / samplingRate) * 100);
                written++;
            }
            return written;
        }

        public WaveFormat WaveFormat
        {
            get { return this.waveFormat; }
        }
    }
}
