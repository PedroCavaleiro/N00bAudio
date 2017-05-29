using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace NoobAudioLib.Format.Wave
{
    /// <summary>
    /// Ref to the base document http://csserver.evansville.edu/~blandfor/EE356/WavFormatDocs.pdf
    /// Ref to the base document NoobAudioLib/Docs/WaveStandardFormat.txt
    /// Authors: Pedro Cavaleiro, Andr¨¦ Ricardo, Jos¨¦ Pires, Miguel Brand?o
    /// Wave File Version: 4.3
    /// </summary>
    public class WaveFile
    {
        /// <summary>
        /// RIFF Header 12 bytes
        /// </summary>
        public class RiffBlock
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="T:NoobAudioLib.Format.Wave.WaveFile.RiffBlock"/> class.
            /// </summary>
            public RiffBlock()
            {
                m_RiffID = new byte[4];
                m_RiffFormat = new byte[4];
            }

            /// <summary>
            /// Reads the riff data block.
            /// </summary>
            /// <param name="inFS">MemoryStream containing the stream of the .wav file</param>
            public void ReadRiff(MemoryStream inFS)
            {
                inFS.Read(m_RiffID, 0, 4);

                Debug.Assert(m_RiffID[0] == 82, "RiffID Inv¨¢ido");

                BinaryReader binRead = new BinaryReader(inFS);

                m_RiffSize = binRead.ReadUInt32();

                inFS.Read(m_RiffFormat, 0, 4);
            }

            public byte[] RiffID
            {
                get { return m_RiffID; }
            }

            public uint RiffSize
            {
                get { return (m_RiffSize); }
            }

            public byte[] RiffFormat
            {
                get { return m_RiffFormat; }
            }

            private byte[] m_RiffID;
            private uint m_RiffSize;
            private byte[] m_RiffFormat;
        }

        /// <summary>
        /// FMT (format) header 24 bytes
        /// </summary>
        public class FmtBlock
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="T:NoobAudioLib.Format.Wave.WaveFile.FmtBlock"/> class.
            /// </summary>
            public FmtBlock()
            {
                m_FmtID = new byte[4];
            }

			/// <summary>
			/// Reads the FMT (format) header
			/// </summary>
			/// <param name="inFS">MemoryStream containing the stream of the .wav file</param>
			public void ReadFmt(MemoryStream inFS)
            {
                inFS.Read(m_FmtID, 0, 4);

                Debug.Assert(m_FmtID[0] == 102, "FormatID Inv¨¢ido");

                BinaryReader binRead = new BinaryReader(inFS);

                m_FmtSize = binRead.ReadUInt32();
                m_FmtTag = binRead.ReadUInt16();
                m_Channels = binRead.ReadUInt16();
                m_SamplesPerSec = binRead.ReadUInt32();
                m_AverageBytesPerSec = binRead.ReadUInt32();
                m_BlockAlign = binRead.ReadUInt16();
                m_BitsPerSample = binRead.ReadUInt16();

                inFS.Seek(m_FmtSize + 20, SeekOrigin.Begin);
            }

            public byte[] FmtID
            {
                get { return m_FmtID; }
            }

            public uint FmtSize
            {
                get { return m_FmtSize; }
            }

            public ushort FmtTag
            {
                get { return m_FmtTag; }
            }

            public ushort Channels
            {
                get { return m_Channels; }
            }

            public uint SamplesPerSec
            {
                get { return m_SamplesPerSec; }
                set { m_SamplesPerSec = value; }
            }

            public uint AverageBytesPerSec
            {
                get { return m_AverageBytesPerSec; }
            }

            public ushort BlockAlign
            {
                get { return m_BlockAlign; }
            }

            public ushort BitsPerSample
            {
                get { return m_BitsPerSample; }
            }

            private byte[] m_FmtID;
            private uint m_FmtSize;
            private ushort m_FmtTag;
            private ushort m_Channels;
            private uint m_SamplesPerSec;
            private uint m_AverageBytesPerSec;
            private ushort m_BlockAlign;
            private ushort m_BitsPerSample;
        }

        /// <summary>
        /// Data block 8 bytes + ????
        /// </summary>
        public class DataBlock
        {
            #region ChannelSeparator
            //Not working -> Objective separate the audio channels
            /*public static byte[] GetChannelBytes(byte[] audioBytes, uint speakerMask, Channels channelToRead, int bitDepth, uint sampleStartIndex, uint sampleEndIndex)
            {
                var channels = FindExistingChannels(speakerMask);
                var ch = GetChannelNumber(channelToRead, channels);
                var byteDepth = bitDepth / 8;
                var chOffset = ch * byteDepth;
                var frameBytes = byteDepth * channels.Length;
                var startByteIncIndex = sampleStartIndex * byteDepth * channels.Length;
                var endByteIncIndex = sampleEndIndex * byteDepth * channels.Length;
                var outputBytesCount = endByteIncIndex - startByteIncIndex;
                var outputBytes = new byte[outputBytesCount / channels.Length];
                var i = 0;

                startByteIncIndex += chOffset;

                for (var j = startByteIncIndex; j < endByteIncIndex; j += frameBytes)
                {
                    for (var k = j; k < j + byteDepth; k++)
                    {
                        outputBytes[i] = audioBytes[(k - startByteIncIndex) + chOffset];
                        i++;
                    }
                }

                return outputBytes;
            }

            private static Channels[] FindExistingChannels(uint speakerMask)
            {
                var foundChannels = new List<Channels>();

                foreach (var ch in Enum.GetValues(typeof(Channels)))
                {
                    if ((speakerMask & (uint)ch) == (uint)ch)
                    {
                        foundChannels.Add((Channels)ch);
                    }
                }

                return foundChannels.ToArray();
            }

            private static int GetChannelNumber(Channels input, Channels[] existingChannels)
            {
                for (var i = 0; i < existingChannels.Length; i++)
                {
                    if (existingChannels[i] == input)
                    {
                        return i;
                    }
                }

                return -1;
            }*/
            #endregion

            public DataBlock()
            {
                m_DataID = new byte[4];
            }

            public void ReadData(MemoryStream inFS, int channels)
            {
                inFS.Read(m_DataID, 0, 4);

                Debug.Assert(m_DataID[0] == 100, "ID de Dados Inválido. Verifique a documentação para ver a origem deste erro.");

                BinaryReader binRead = new BinaryReader(inFS);

                m_DataSize = binRead.ReadUInt32();

                m_Data = new Int16[m_DataSize];

                inFS.Seek(40, SeekOrigin.Begin);

                m_NumSamples = (int)(m_DataSize / 2);

                for (int i = 0; i < m_NumSamples; i++)
                {
                    m_Data[i] = binRead.ReadInt16();
                }
            }

            public byte[] DataID
            {
                get { return m_DataID; }
            }

            public uint DataSize
            {
                get { return m_DataSize; }
            }

            public Int16 this[int pos]
            {
                get { return m_Data[pos]; }
            }

            public Int16[] Samples
            {
                get { return m_Data; }
            }

            public Int16[] ProcessedSamples
            {
                get { return m_ProcessedData; }
                set { m_ProcessedData = value; }
            }

            public int NumSamples
            {
                get { return m_NumSamples; }
                set { m_NumSamples = value; }
            }

            private byte[] m_DataID;
            private uint m_DataSize;
            private Int16[] m_Data;
            private Int16[] m_ProcessedData;
            private int m_NumSamples;
        }

        public WaveFile(string inFilepath, MemoryStream waveStream)
        {
            m_Filepath = inFilepath;
            m_FileInfo = new FileInfo(inFilepath);
            m_FileStream = waveStream;

            m_Riff = new RiffBlock();
            m_Fmt = new FmtBlock();
            m_Data = new DataBlock();
        }

        public void Read()
        {
            m_Riff.ReadRiff(m_FileStream);
            m_Fmt.ReadFmt(m_FileStream);
            m_Data.ReadData(m_FileStream, m_Fmt.Channels);
            time = (((long)m_Data.DataSize - 44) / (m_Fmt.SamplesPerSec * (m_Fmt.BitsPerSample / 8))) / m_Fmt.Channels;
            int disposedsamples = 0;
            for (int j = m_Data.Samples.Length - 1; j > 0; j--)
            {
                if (m_Data[j] != 0)
                    break;
                disposedsamples++;
            }
            Int16[] newps = new Int16[m_Data.Samples.Length - disposedsamples];
            for (int i = 0; i < newps.Length; i++)
                newps[i] = m_Data.Samples[i];
            m_Data.ProcessedSamples = (Int16[])newps.Clone();
        }

        public MemoryStream Write()
        {
            int disposedSamples = 0;
            for (int j = Data.ProcessedSamples.Length - 1; j > 0; j--)
            {
                if (Data.ProcessedSamples[j] != 0)
                    break;
                disposedSamples++;
            }
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            try
            {
                bw.Write(Riff.RiffID);
                bw.Write(Riff.RiffSize);
                bw.Write(Riff.RiffFormat);
                bw.Write(Format.FmtID);
                bw.Write(Format.FmtSize);
                bw.Write(Format.FmtTag);
                bw.Write(Format.Channels);
                bw.Write(Format.SamplesPerSec);
                bw.Write(Format.AverageBytesPerSec);
                bw.Write(Format.BlockAlign);
                bw.Write(Format.BitsPerSample);
                bw.Write(Data.DataID);
                bw.Write(sizeof(Int16) * Data.ProcessedSamples.Length);
                for (int i = 0; i < Data.ProcessedSamples.Length - disposedSamples; i++)
                {
                    bw.Write((ushort)Data.ProcessedSamples[i]);
                }
                return ms;
            }
            finally
            {

            }
        }

        public DataBlock Data
        {
            get { return m_Data; }
        }

        public FmtBlock Format
        {
            get { return m_Fmt; }
        }

        public RiffBlock Riff
        {
            get { return m_Riff; }
        }

        private string m_Filepath;
        private FileInfo m_FileInfo;
        private MemoryStream m_FileStream;

        private RiffBlock m_Riff;
        private FmtBlock m_Fmt;
        private DataBlock m_Data;
        private long time;

        public long GetTrackLength
        {
            get { return time; }
        }

        [Flags]
        public enum Channels : uint
        {
            FrontLeft = 0x1,
            FrontRight = 0x2,
            FrontCenter = 0x4,
            Lfe = 0x8,
            BackLeft = 0x10,
            BackRight = 0x20,
            FrontLeftOfCenter = 0x40,
            FrontRightOfCenter = 0x80,
            BackCenter = 0x100,
            SideLeft = 0x200,
            SideRight = 0x400,
            TopCenter = 0x800,
            TopFrontLeft = 0x1000,
            TopFrontCenter = 0x2000,
            TopFrontRight = 0x4000,
            TopBackLeft = 0x8000,
            TopBackCenter = 0x10000,
            TopBackRight = 0x20000
        }
    }
}