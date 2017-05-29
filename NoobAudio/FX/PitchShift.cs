using System;         
using System.Numerics;
using NoobAudioLib.Format.Wave;

namespace NoobAudioLib.FX
{
	/// <summary>
	/// Author: Miguel Brandão
	/// FX Version: 0.1 
	/// NOTE: This FX is not available on GUI
   	/// STATUS: FX Not working
	/// </summary>
    public class PitchShift
    {
        WaveFile waveFile;
        int SampleRate;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NoobAudioLib.FX.PitchShift"/> class.
        /// </summary>
        /// <param name="SampleRate">Sample rate.</param>
        /// <param name="waveFile">Wave file.</param>
        public PitchShift(int SampleRate, WaveFile waveFile,int pitchModifier)
        {
            this.waveFile = waveFile;
            this.SampleRate = SampleRate;
        }
        public void ProcessPitchShift()
        {
            Complex[] array = new Complex[waveFile.Data.ProcessedSamples.Length];
            Complex[] absarray = new Complex[array.Length];
            Double[] DData = new double[waveFile.Data.ProcessedSamples.Length];
            for (int a = 0; a < DData.Length; a++)   //Conversion of data to Double
                DData[a] = (double)waveFile.Data.ProcessedSamples[a];
            //FourierTransform fft = new FourierTransform();
            //fft.FFT(DData, true);                    //Get fourier transform
            for (int a = 0; a < array.Length; a++)
                absarray[a] = Complex.Abs(array[a]);
            bool[] keyFreq = new bool[array.Length];
            for (int a = 0; a < array.Length / 2; a++)
            { //populate array that stores key frequencies
                if (absarray[a].Imaginary > 20)
                    keyFreq[a] = true;
            }
            for (int a = 0; a < array.Length / 2; a++)
            {

            }
            //waveFile.Data.ProcessedSamples;
        }
    }
}

