using System;
using System.Media;

namespace PotterGame.Utils.AudioPlayer
{
    public static class AudioWrapper
    {
        static SoundPlayer musicPlayer = new SoundPlayer();
        static SoundPlayer sfxPlayer = new SoundPlayer();

        public static void PlayMusicWithFilename(string filename)
        {
            //musicPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Music\\" + filename;
            //musicPlayer.PlayLooping();
        }

        public static void PlaySFXWithFilename(string filename)
        {
            sfxPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\SFX\\" + filename;
            sfxPlayer.Play();
        }
    }
}