namespace Synthesizer
{
    using NAudio.Wave;
    using System;
    using System.IO;

    class SignalGenerator : IWaveProvider
    {
        private int samplingRate;
        private int duration;
        private WaveFormat waveFormat;
        private long currentSample;
        private INotesGenerator notes;
        private ITimbreGenerator timbre;

        public SignalGenerator(int samplingRate, INotesGenerator notes, ITimbreGenerator timbre, int duration)
        {
            this.samplingRate = samplingRate;
            this.notes = notes;
            this.timbre = timbre;
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
                double frequency = 440 * Math.Pow(2, this.notes.Notes(time) / 12.0);
                double period = 1 / frequency;
                int numCompletedPeriods = (int)(time / period);
                double timeWithinPeriod = time - period * numCompletedPeriods;
                double normalizedTime = timeWithinPeriod / period;
                double amptitude = 100;
                buffer[pointer++] = (byte)this.timbre.value(normalizedTime, amptitude);
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
