using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Media;

namespace NoobAudioLib
{
    /// <summary>
    /// Author: Pedro Cavaleiro
    /// Version: 1.0
    /// </summary>
    public class StdOps
    {
        MemoryStream waveStream;
        string FilePath;
        Format.Wave.WaveFile waveFile;

        public MemoryStream GetWaveStream
        {
            get { return waveStream; }
        }

        public Format.Wave.WaveFile GetWaveFile
        {
            get { return waveFile; }
        }

        public string GetFilePath
        {
            get { return FilePath; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NoobAudioLib.StdOps"/> class.
        /// </summary>
        /// <param name="WaveFile">Wave file path</param>
        public StdOps(string WaveFile)
        {
            using (FileStream fs = File.OpenRead(WaveFile))
            {
                waveStream = new MemoryStream();
                waveStream.SetLength(fs.Length);
                fs.Read(waveStream.GetBuffer(), 0, (int)fs.Length);
            }
            FilePath = WaveFile;
            waveFile = new Format.Wave.WaveFile(FilePath, waveStream);
            waveFile.Read();
        }

        /// <summary>
        /// Releases all resource used by the <see cref="T:NoobAudioLib.StdOps"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:NoobAudioLib.StdOps"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:NoobAudioLib.StdOps"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the <see cref="T:NoobAudioLib.StdOps"/> so
        /// the garbage collector can reclaim the memory that the <see cref="T:NoobAudioLib.StdOps"/> was occupying.</remarks>
        public void Dispose()
        {
            waveStream.Dispose();
        }
    }
}
