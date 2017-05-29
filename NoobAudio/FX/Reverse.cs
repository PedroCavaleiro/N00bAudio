using System;
using NoobAudioLib.Format.Wave;

namespace NoobAudioLib.FX
{
    public class Reverse
    {
        WaveFile waveFile;

        public Reverse(WaveFile waveFile)
        {
            this.waveFile = waveFile;
        }

        public void Invert()
        {
            Int16[] inverted = new Int16[waveFile.Data.ProcessedSamples.Length];
            int n = waveFile.Data.ProcessedSamples.Length - 1;
            for (int i = 0; i < waveFile.Data.ProcessedSamples.Length; i++)
            {
                inverted[i] = waveFile.Data.ProcessedSamples[n];
                n--;
            }
            waveFile.Data.ProcessedSamples = inverted;
        }
    }
}
