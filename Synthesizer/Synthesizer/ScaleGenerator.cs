namespace Synthesizer
{
    using NAudio.Wave;
    using System;
    using System.IO;

    class ScaleGenerator : INotesGenerator
    {
        public int Notes(double time)
        {
            // -9 -8 -7 -6 -5 -4  -3 -2 -1  0  1  2  3
            //  c c#  d d#  e  f  f#  g g#  a a#  b  c
            return (new int[] { -9, -7, -5, -4, -2, 0, 2, 3 })[(int)time] - 12;
        }
    }
}