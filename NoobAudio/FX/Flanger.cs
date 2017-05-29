using System;
using NoobAudioLib.Format.Wave;

namespace NoobAudioLib.FX
{
    /// <summary>
    /// Authors: André Ricardo, Miguel Brandão, José Pires
    /// Code Converter: Pedro Cavaleiro
    /// Original Code Language: MatLab
    /// FX Version: 2.8
    /// Fixes applyed by: Pedro Cavaleiro, André Ricardo
    /// </summary>
    public class Flanger
    {
        WaveFile waveFile;
		double m_maxTimeDelay;
		double rate;
		float[] sinRef;
		int maxSampleDelay;
		double amp;

		/// <summary>
        /// Initializes a new instance of the <see cref="T:NoobAudioLib.FX.Flanger"/> class.
        /// </summary>
        /// <param name="waveFile">Wave file.</param>
        /// <param name="maxTimeDelay">Max time delay.</param>
        /// <param name="rate">Rate.</param>
        /// <param name="Amp">Amp.</param>
        /// <remarks>
        /// The recomended value for <paramref name="Amp">Amp</paramref> is 0.7
        /// </remarks>
        public Flanger(WaveFile waveFile, int maxTimeDelay, double rate, double Amp = 0.7)
        {
            this.waveFile = waveFile;

			m_maxTimeDelay = maxTimeDelay / (double)1000;
			this.rate = rate;
			sinRef = new float[waveFile.Data.Samples.Length];

			for (int i = 0; i < sinRef.Length; i++)
				sinRef[i] = (float)Math.Sin(2 * Math.PI * (i + 1) * (rate / waveFile.Format.SamplesPerSec));

			maxSampleDelay = (int)Math.Round(m_maxTimeDelay * waveFile.Format.SamplesPerSec);
			amp = Amp;
        }

        /// <summary>
        /// Processes the flanger FX.
        /// </summary>
		public void ProcessFlanger()
		{
            Int16[] rtn = new Int16[waveFile.Data.ProcessedSamples.Length];
            for (int i = 0; i < waveFile.Data.ProcessedSamples.Length; i++)
                rtn[i] = waveFile.Data.ProcessedSamples[i];

            for (int i = maxSampleDelay; i < waveFile.Data.ProcessedSamples.Length; i++)
			{
                double currSin = Math.Abs(sinRef[i]);
                int currDelay = (int)Math.Ceiling(currSin * maxSampleDelay);
                rtn[i] = (Int16)((amp * waveFile.Data.ProcessedSamples[i]) + (amp * waveFile.Data.ProcessedSamples[i - currDelay]));
			}

            for (int i = 0; i < rtn.Length; i++)
                waveFile.Data.ProcessedSamples[i] = rtn[i];
		}
    }
}
