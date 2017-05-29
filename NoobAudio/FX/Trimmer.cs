using System;
using NoobAudioLib.Format.Wave;

namespace NoobAudioLib.FX
{
    /// <summary>
    /// Author: Miguel Brandão
    /// FX Version: 1.5
    /// Fixes Applied by: Pedro Cavaleiro, André Ricardo
    /// </summary>
    public class Trimmer
    {
        WaveFile waveFile;
        int SampleRate;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NoobAudioLib.FX.Trimmer"/> class.
        /// </summary>
        /// <param name="SampleRate">Sample rate.</param>
        /// <param name="waveFile">Wave file.</param>
        public Trimmer(int SampleRate, WaveFile waveFile)
        {
            this.waveFile = waveFile;
            this.SampleRate = SampleRate;
        }

        /// <summary>
        /// Cuts the begining of the sound
        /// </summary>
        /// <param name="start">Time in miliseconds to cut from the bigining</param>
        public void CutStart(int start)
        {
            int startSample = SampleRate * (start / 1000);
            Int16[] cut = new Int16[waveFile.Data.ProcessedSamples.Length - startSample];
            for (int i = startSample; i < waveFile.Data.ProcessedSamples.Length; i++)
                cut[i - startSample] = waveFile.Data.ProcessedSamples[i];
            waveFile.Data.NumSamples = waveFile.Data.NumSamples - startSample;
            waveFile.Data.ProcessedSamples = new Int16[cut.Length];
            waveFile.Data.ProcessedSamples = cut;
        }

        /// <summary>
        /// Cuts the end of the sound
        /// </summary>
        /// <param name="end">Time in miliseconds to cut from the end</param>
        public void CutEnd(int end)
        {
            int endSample = SampleRate * (end / 1000);
            Int16[] cut = new Int16[waveFile.Data.ProcessedSamples.Length - endSample];

            for (int i = 0; i < waveFile.Data.ProcessedSamples.Length - endSample; i++)
                cut[i] = waveFile.Data.ProcessedSamples[i];
            waveFile.Data.NumSamples = waveFile.Data.NumSamples - endSample;
            waveFile.Data.ProcessedSamples = new Int16[cut.Length];
            waveFile.Data.ProcessedSamples = cut;
        }
    }
}
