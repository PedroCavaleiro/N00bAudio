using NoobAudioLib.Format.Wave;
using System;

namespace NoobAudioLib.FX
{
	/// <summary>
	/// Author: Pedro Cavaleiro
    /// Code Converter: Pedro Cavaleiro
    /// Original Code Language: MatLab
	/// FX Version: 1.2
	/// </summary>
	public class FadeInFadeOut
    {
        double fadeSamples;
        WaveFile waveFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NoobAudioLib.FX.FadeInFadeOut"/> class.
        /// </summary>
        /// <param name="SampleRate">Sample rate.</param>
        /// <param name="waveFile">Wave file.</param>
        /// <param name="fadeDuration">Fade duration in miliseconds.</param>
        /// <param name="valid">If set to <c>true</c> valid.</param>
        /// <remarks>
        /// The <paramref name="valid">valid</paramref> is an out variable, it returns a value before the end of the initialization
        /// It verifies if the FadeIn or FadeOut duration does not exceed the sound length
        /// </remarks>
        public FadeInFadeOut(int SampleRate, WaveFile waveFile, int fadeDuration, out bool valid)
        {
            double fadeDurationS = fadeDuration / (double)1000;

            fadeSamples = Math.Round((double)(fadeDurationS * SampleRate));

            if (waveFile.Data.ProcessedSamples.Length < fadeSamples)
                valid = false;
            else
                valid = true;
            
            this.waveFile = waveFile;
        }

        /// <summary>
        /// Processes the FadeIn FX.
        /// </summary>
        public void FadeIn()
        {
            float[] fadeScale = new float[(int)fadeSamples];
            MatLabFunctions.Linspace ls = new MatLabFunctions.Linspace(0, 1, (float)fadeSamples);
            int i = 0;

            while (ls.hasNext()) {
                try {
                    fadeScale[i] = ls.getNextFloat();
                }
                catch (IndexOutOfRangeException ex)
                { break; }
                i++;
            }

            i = 0;

            foreach(float scale in fadeScale) {
                waveFile.Data.ProcessedSamples[i] = (Int16)Math.Round(waveFile.Data.ProcessedSamples[i] * scale);
                i++;
            }
        }

        /// <summary>
        /// Processes the FadeOut FX
        /// </summary>
        public void FadeOut()
        {
            float[] fadeScale = new float[(int)fadeSamples];
            MatLabFunctions.Linspace ls = new MatLabFunctions.Linspace(0,1, (float)fadeSamples);
            int i = 0;
            while (ls.hasNext())
            {
                try
                {
                    fadeScale[i] = ls.getNextFloat();
                }
                catch (IndexOutOfRangeException ex)
                { break; }
                i++;
            }
            int startFade = (waveFile.Data.ProcessedSamples.Length - (int)fadeSamples) - 1;
            int disposedSamples = 0;
            for(int j = waveFile.Data.ProcessedSamples.Length - 1; j > 0; j--)
            {
                if (waveFile.Data.ProcessedSamples[j] != 0)
                    break;
                disposedSamples++;
            }
            startFade = startFade - disposedSamples;
            for (int j = fadeScale.Length -1; j > -1; j--)
            {
                waveFile.Data.ProcessedSamples[startFade] = (Int16)Math.Round(waveFile.Data.ProcessedSamples[startFade] * fadeScale[j]);
                startFade++;
            }
        }
    }
}
