using System;
using NoobAudioLib.Format.Wave;

namespace NoobAudioLib.FX
{
    public class Invert
    {
        WaveFile waveFile;
        public Invert(WaveFile waveFile)
        {
            this.waveFile = waveFile;
        }

        public void  performInvert()
        {
            Int16[] invert = new Int16[waveFile.Data.ProcessedSamples.Length];
            for (int i = 0; i < waveFile.Data.ProcessedSamples.Length; i++)
                invert[i] = (Int16)(waveFile.Data[i] * -1);
            waveFile.Data.ProcessedSamples = new Int16[waveFile.Data.Samples.Length];
            waveFile.Data.ProcessedSamples = invert;
        }
    }
}
