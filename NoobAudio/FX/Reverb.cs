using System;
using NoobAudioLib.Format.Wave;

namespace NoobAudioLib.FX
{
    /// <summary>
    /// Author: José Pires
    /// FX Version: 1.2
    /// </summary>
    public class Reverb
    {
        WaveFile waveFile;
        int DelayMS;
        float Decay;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NoobAudioLib.FX.Reverb"/> class.
        /// </summary>
        /// <param name="waveFile">Wave file.</param>
        /// <param name="DelayMS">Delay in miliseconds.</param>
        /// <param name="Decay">Decay.</param>
        public Reverb(WaveFile waveFile, int DelayMS, float Decay)
        {
            this.DelayMS = DelayMS;
            this.Decay = Decay;
            this.waveFile = waveFile;
        }

        /// <summary>
        /// Processes the reverb.
        /// </summary>
        public void ProcessReverb()
        {
            int delaySamples = (int)((float)DelayMS * (waveFile.Format.SamplesPerSec / (float)1000));
            for (int i = 0; i < waveFile.Data.ProcessedSamples.Length - delaySamples; i++)
                waveFile.Data.ProcessedSamples[i + delaySamples] += (Int16)(waveFile.Data.ProcessedSamples[i] * Decay);
        }
    }
}
