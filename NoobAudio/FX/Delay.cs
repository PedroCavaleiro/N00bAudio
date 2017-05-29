using System;
using NoobAudioLib.Format.Wave;
namespace NoobAudioLib.FX
{
	/// <summary>
	/// Authors: José Pires
	/// Code Converter: Pedro Cavaleiro
	/// Original Code Language: MatLab
	/// FX Version: 1.5
	/// Fixes applyed by: Pedro Cavaleiro, Miguel Brandão
    /// NOTE: This FX is not available on GUI
    /// STATUS: FX Not working
	/// </summary>
	public class Delay
    {
        WaveFile waveFile;
        int sampleRate;

        public Delay(WaveFile waveFile)
        {
            this.waveFile = waveFile;
            sampleRate = (int)this.waveFile.Format.SamplesPerSec;
        }

        public void ProcessDelay(int start, int sectionStart, int sectionEnd, int delay_time, int times, double gain = 0.5)
        {
            int s = (int)Math.Floor(start * (double)sampleRate);
            int sdst = (int)Math.Floor(sectionStart * (double)sampleRate);
            int sdend = (int)Math.Floor(sectionEnd * (double)sampleRate);
            int originalProcessedAudioLength = waveFile.Data.ProcessedSamples.Length;
            int sd = sdend - sdst;
            int dt = (int)Math.Floor(delay_time * (double)sampleRate);
            int m = dt + sd;
            int delSize = (sd + (dt + sd) * (times - 1));
            int fim = s + delSize;
            Int16[] section = new Int16[sdend - sdst];
            int z = 0;
            for (int i = sdst; i < sdend; i++)
            {
                section[z] = waveFile.Data.ProcessedSamples[i];
                z++;
            }
            Int16[] temp;
            if (fim > originalProcessedAudioLength)
                temp = new Int16[originalProcessedAudioLength + (fim - originalProcessedAudioLength)];
            else
                temp = new Int16[originalProcessedAudioLength];

            for (int n = s; s >= fim; n = n + m)
            {
                for (int i = n; i <= m; i++)
                {
                    temp[i] = (Int16)(waveFile.Data.ProcessedSamples[i] * 0.5 + gain * section[i]);
                }
            }
            waveFile.Data.ProcessedSamples = new Int16[temp.Length];
            waveFile.Data.ProcessedSamples = temp;
        }

    }
}
