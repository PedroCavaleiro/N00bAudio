using System;
using NoobAudioLib.Format.Wave;

namespace NoobAudioLib.FX
{
    /// <summary>
    /// Author: Pedro Cavaleiro
    /// FX Version: 1.0
    /// </summary>
    public class Volume
    {
        WaveFile waveFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NoobAudioLib.FX.Volume"/> class.
        /// </summary>
        /// <param name="waveFile">Wave file.</param>
        public Volume(WaveFile waveFile)
        {
            this.waveFile = waveFile;
        }

        /// <summary>
        /// Processes the volume change.
        /// </summary>
        /// <param name="vol">New volume</param>
        public void ProcessVolumeChange(double vol)
        {
            for (int i = 0; i < waveFile.Data.ProcessedSamples.Length; i++)
            {
                waveFile.Data.ProcessedSamples[i] = (Int16)Math.Round(waveFile.Data.ProcessedSamples[i] * vol);
                i++;
            }
        }
    }
}
