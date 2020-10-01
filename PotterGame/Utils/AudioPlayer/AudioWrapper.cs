using System;
using System.Media;

namespace PotterGame.Utils.AudioPlayer
{
    public static class AudioWrapper
    {
        static SoundPlayer player = new SoundPlayer();

        public static void PlayAudioWithFilename(string filename)
        {
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\mainmenu.wav";
            player.PlayLooping();
        }
    }
}