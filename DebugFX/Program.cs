using System;
using NoobAudioLib;
using NoobAudioLib.FX;
using System.IO;

namespace DebugFX
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("==== FX Debug ====");
            Console.WriteLine("==== Delay FX ====");
            string audioPath, exportPath;
            Console.Write("Enter file path: ");
            audioPath = Console.ReadLine();
            Console.Write("Enter export path: ");
            exportPath = Console.ReadLine();
            StdOps stdOps = new StdOps(audioPath);

            Delay delay = new Delay(stdOps.GetWaveFile);
            delay.ProcessDelay(1, 0, 1, 2, 10, 0.75);

			FileStream file = new FileStream(exportPath, FileMode.OpenOrCreate, FileAccess.Write);
			MemoryStream ms = new MemoryStream();
			ms = stdOps.GetWaveFile.Write();
			ms.WriteTo(file);
			ms.Close();
			file.Close();
            Console.WriteLine("File Processed and Exported");

        }
    }
}
