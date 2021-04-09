using System;
using System.Collections.Generic;
using System.Text;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;
using Xamarin.Forms;


namespace ChessClock
{
    static class Sound
    {
        public enum SoundType
        {
            Clap,
            Kick,
            KickClap
        }
        private static ISimpleAudioPlayer[] sounds;
        private static int soundsCount;

        static Sound()
        {
            soundsCount = Enum.GetNames(typeof(SoundType)).Length;
            sounds = new ISimpleAudioPlayer[soundsCount];
            LoadSounds();
        }

        public static void PlaySound(SoundType soundType)
        {
            sounds[(int)soundType].Play();
        }

        private static void LoadSounds()
        {
            for (int i = 0; i < soundsCount; i++)
            {
                sounds[i] = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                sounds[i].Load(GetStreamFromFile($"Audio.{Enum.GetName(typeof(SoundType), i)}.wav"));
            }
        }
        private static Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("ChessClock." + filename);

            return stream;
        }
    }
}
