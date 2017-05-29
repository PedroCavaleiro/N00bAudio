using System;
using NoobAudioLib.Format.Wave;

namespace NoobAudioLib.FX
{
	/// <summary>
	/// Author: José Pires
	/// Code Converter: Pedro Cavaleiro
	/// Original Code Language: MatLab
	/// FX Version: 1.1
	/// </summary>
	public class SpeedShift
    {
		WaveFile waveFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NoobAudioLib.FX.SpeedShift"/> class.
        /// </summary>
        /// <param name="waveFile">Wave file.</param>
		public SpeedShift(WaveFile waveFile)
        {
            this.waveFile = waveFile;
        }

        /// <summary>
        /// Speeds up or down the speed of the audio by changing the sample rate
        /// </summary>
        /// <param name="speed">Factor by witch the speed will be changed</param>
        public void ShiftSpeed(float speed)
        {
            int currSpeed = (int)waveFile.Format.SamplesPerSec;
            waveFile.Format.SamplesPerSec = (uint)(currSpeed * speed);
        }

        public void SpeedShiftBPM(int currBPM, int newBPM)
        {
            double multiplier = newBPM / (double)currBPM;
            int currSpeed = (int)waveFile.Format.SamplesPerSec;
            waveFile.Format.SamplesPerSec = (uint)Math.Floor(currSpeed * multiplier);
        }

    }
}
