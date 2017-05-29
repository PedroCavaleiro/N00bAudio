using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using static NoobAudioLib.Format.Wave.WaveFile;

namespace NoobAudioLib
{
    /// <summary>
    /// Author: Pedro Cavaleiro,Miguel Brandão
    /// Utils Version: 1.3
    /// </summary>
    /// <remarks>
    /// These methods are static, there is no need for initialization
    /// </remarks>
    public static class Utils
    {
        static string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        /// <summary>
        /// Converts a size (from byte) to other unit depending the size
        /// </summary>
        /// <returns>Ajusted size and suffix.</returns>
        /// <param name="value">Value in bytes.</param>
        /// <example>string s = SizeSuffix(1024); // s will now have the value "1 KB"</example>
        static public string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        /// <summary>
        /// Reads data from a BinaryReader to a byte array.
        /// </summary>
        /// <returns>Byte Array with all the bytes from the BinaryReader.</returns>
        /// <param name="reader">Reader.</param>
        public static byte[] ReadAllBytes(this BinaryReader reader)
        {
            const int bufferSize = 4096;

            using (var ms = new MemoryStream())
            {
                byte[] buffer = new byte[bufferSize];
                int count;

                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                    ms.Write(buffer, 0, count);
                
                return ms.ToArray();
            }
        }
        /// <summary>
        /// Mirrors an fft transformed signal
        /// </summary>
        /// <returns>Returns a new mirrored array.</returns>
        /// <param name="Complex[]">Reader.</param>
        public static Complex[] fftNormalization(Complex[] array)
        {
            Complex[] tmp = new Complex[array.Length / 2];
            for(int a = 0; a < tmp.Length; a++)
            {
                tmp[a] = Complex.Conjugate(array[a]);
            }
            return tmp;
        }

    }

    /// <summary>
    /// Author: Miguel Brandão
    /// MatLabFunctions Version: 1.3
    /// </summary>
    public static class MatLabFunctions
    {
        public class Linspace
        {
            float current;
            float end;
            float step;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:NoobAudioLib.MatLabFunctions.Linspace"/> class.
            /// </summary>
            /// <param name="start">The first number</param>
            /// <param name="end">The last number</param>
            /// <param name="totalCount">The total of numbers needed.</param>
            public Linspace(float start, float end, float totalCount)
            {
                this.current = start;
                this.end = end;
                this.step = (end - start) / totalCount;
            }

            /// <summary>
            /// Checks if there is a next number
            /// </summary>
            /// <returns><c>true</c>, if there is a next number, <c>false</c> otherwise.</returns>
            public bool hasNext()
            {
                return current < (end + step / 2);
            }

            /// <summary>
            /// Gets the next float.
            /// </summary>
            /// <returns>The next float.</returns>
            public float getNextFloat()
            {
                current += step;
                return current;
            }
        }
    }

	/// <summary>
	/// Author: Pedro Cavaleiro
	/// IOOps Version: 1.0
	/// </summary>
	/// <remarks>
	/// These methods are static, there is no need for initialization
	/// </remarks>
	public static class IOOps
    {

        /// <summary>
        /// Reads to memory stream.
        /// </summary>
        /// <returns>The memory stream.</returns>
        /// <param name="inFilePath">The file path.</param>
        static public MemoryStream ReadToMemoryStream(string inFilePath)
        {
            MemoryStream waveStream = new MemoryStream(); ;
            using (FileStream fs = File.OpenRead(inFilePath))
            {
                waveStream.SetLength(fs.Length);
                fs.Read(waveStream.GetBuffer(), 0, (int)fs.Length);
            }
            return waveStream;
        }
    }

    /// <summary>
    /// Author: Pedro Cavaleiro
    /// WaveHeaderCheck Version: 1.0
    /// </summary>
    /// <remarks>
    /// To check witch values are or not valid you need to get the instance values 
    /// ValidBits, ValidChannels, ValidAudioFormat
    /// </remarks>
    public class WaveHeaderCheck
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NoobAudioLib.WaveHeaderCheck"/> class.
        /// </summary>
        /// <param name="inFilepath">In filepath.</param>
        /// <param name="bitsPerSample">An array of the allowed bits per sample</param>
        /// <param name="channels">An array of the allowed channels.</param>
        /// <param name="audioFormat">An array of the allowed audio format.</param>
        public WaveHeaderCheck(string inFilepath, string[] bitsPerSample = null, string[] channels = null, string[] audioFormat = null)
        {
            m_Filepath = inFilepath;
            m_FileInfo = new FileInfo(inFilepath);
            m_FileStream = IOOps.ReadToMemoryStream(inFilepath);

            m_Riff = new RiffBlock();
            m_Fmt = new FmtBlock();

            Read();

            Verify(bitsPerSample, channels, audioFormat);
        }

        /// <summary>
        /// Reads the Riff Block and the Fmt (format) block data.
        /// </summary>
        private void Read()
        {
            m_Riff.ReadRiff(m_FileStream);
            m_Fmt.ReadFmt(m_FileStream);
        }

        /// <summary>
        /// Verify the specified bitsPerSample, channels and audioFormat.
        /// </summary>
        /// <returns>The verify.</returns>
        /// <param name="bitsPerSample">Bits per sample.</param>
        /// <param name="channels">Channels.</param>
        /// <param name="audioFormat">Audio format.</param>
        private void Verify(string[] bitsPerSample = null, string[] channels = null, string[] audioFormat = null)
        {
            if (bitsPerSample != null)
                for (int i = 0; i < bitsPerSample.Length; i++)
                {
                    if (m_Fmt.BitsPerSample.ToString() == bitsPerSample[i])
                    {
                        m_ValidBits = true;
                        break;
                    }
                }
            else
                m_ValidBits = true;
            if (channels != null)
                for (int i = 0; i < channels.Length; i++)
                {
                    if (m_Fmt.Channels.ToString() == channels[i])
                    {
                        m_ValidChannels = true;
                        break;
                    }
                }
            else
                m_ValidChannels = true;
            if (audioFormat != null)
                for (int i = 0; i < audioFormat.Length; i++)
                {
                    if (m_Fmt.FmtTag.ToString() == audioFormat[i])
                    {
                        m_ValidAudioFormat = true;
                        break;
                    }
                }
            else
                m_ValidAudioFormat = true;
        }

        private string m_Filepath;
        private FileInfo m_FileInfo;
        private MemoryStream m_FileStream;

        private RiffBlock m_Riff;
        private FmtBlock m_Fmt;

        private bool m_ValidBits = false;
        private bool m_ValidChannels = false;
        private bool m_ValidAudioFormat = false;

        public bool ValidBits
        {
            get { return m_ValidBits; }
        }

        public bool ValidChannels
        {
            get { return m_ValidChannels; }
        }

        public bool ValidAudioFormat
        {
            get { return m_ValidAudioFormat; }
        }
    }
}
