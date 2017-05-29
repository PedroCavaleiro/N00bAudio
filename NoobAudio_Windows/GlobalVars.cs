using System;
using NoobAudioLib;

namespace NoobAudio_Windows
{
    public static class GlobalVars
    {
        public class DynamicVars
        {
            public StdOps wavOps;
            public NoobAudio mainWindow;
            public FXEditor fxEditor;
            public ProjectSettings projectSettings = new ProjectSettings();

            public class ProjectSettings
            {
                public string fileLocation;
                public FXEditor fxEditor = new FXEditor();
                public string lastEdit;
                public class FXEditor
                {
                    public Fade fadeSettings = new Fade();
                    public Flanger flangerSettings = new Flanger();
                    public Volume volumeSettings = new Volume();
                    public Reverb reverbSettings = new Reverb();
                    public SpeedShift speedShift = new SpeedShift();
                    public Reverse reverse = new Reverse();

                    public class Fade
                    {
                        public bool Enabled = false;
                        public int FadeIn = -1;
                        public int FadeOut = -1;
                    }

                    public class Flanger
                    {
                        public bool Enabled = false;
                        public int DelayTime = -1;
                        public int Rate = -1;
                        public double Amp = -1;
                    }

                    public class Volume
                    {
                        public bool Enabled = false;
                        public double Vol = -1;
                        public bool Override = false;
                    }

                    public class Reverb
                    {
                        public bool Enabled = false;
                        public int Delay = -1;
                        public float Decay = -1;
                    }

                    public class SpeedShift
                    {
                        public bool Enabled = false;
                        public float multiplier = -1;
                        public int currBPM = -1;
                        public int newBPM = -1;
                        public bool bpm = false;
                    }

                    public class Reverse
                    {
                        public bool Enabled = false;
                    }
                }
            }
        }

        

        public static class ConstantVars
        {
            public static class SupportedFormats
            {
                public static class Wave
                {
                    public static string[] BitsPerSample = { "16" };
                    public static string[] Channels = { "1", "2" };
                    public static string[] AudioFormat = { "1" };
                }
            }
        }
    }
}
