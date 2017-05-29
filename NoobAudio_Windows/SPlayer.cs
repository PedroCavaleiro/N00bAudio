using System;
using System.IO;
using System.Media;

namespace NoobAudio_Windows
{
    public class SPlayer
    {
        SoundPlayer soundPlayer;

        public SPlayer()
        {            
        }

        public void Stop()
        {
            soundPlayer.Stop();
        }

        public long StreamLength(MemoryStream ms)
        {
            soundPlayer = new SoundPlayer(ms);
            long l = soundPlayer.Stream.Length;
            soundPlayer.Dispose();
            return l;
        }

        public void Play(MemoryStream ms)
        {
            soundPlayer = new SoundPlayer(ms);
            soundPlayer.Stream.Position = 0;
            soundPlayer.Play();
        }

        public void Play(MemoryStream ms, int pos)
        {
            soundPlayer = new SoundPlayer(ms);
            soundPlayer.Stream.Position = pos;
        }
    }
}
