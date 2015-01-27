namespace Synthesizer
{
    using NAudio.Wave;
    using System;
    using System.IO;

    interface INotesGenerator
    {
        int Notes(double time);
    }
}
