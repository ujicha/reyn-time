using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using Advanced_Combat_Tracker;
using WMPLib;

namespace ReynTime
{
    internal class SoundLibrary
    {
        #region Members

        WindowsMediaPlayer player = new WindowsMediaPlayer();

        Random Randomizer
        {
            get;
            set;
        }

        public IList<string> Library
        {
            get;
            private set;
        }

        public string SoundDir
        {
            get;
            set;
        }

        public int Volume
        {
            get
            {
                return player.settings.volume;
            }
            set
            {
                player.settings.volume = GetAdjustedVolume(value);
            }
        }

        #endregion

        #region Constructors

        public SoundLibrary(string directory)
        {
            Randomizer = new Random();
            SoundDir = directory;
            Library = new List<string>();
            Volume = 25;
            player.uiMode = "invisible";
        }

        #endregion

        #region Public Methods

        public void Add(string path)
        {
            string fullPath = Path.Combine(SoundDir, path);

            if (path != string.Empty && !Library.Contains(path) && File.Exists(fullPath))
                Library.Add(path);
        }

        public void PlayRandom()
        {
            string sound = GetRandomSound();
            PlayMp3(sound);
        }

        public void AddAllInDirectory()
        {
            if (!Directory.Exists(SoundDir))
                return;

            foreach (string file in Directory.GetFiles(SoundDir, "*.mp3"))
            {
                Add(file);
            }
        }

        /// <summary>
        /// Play sound using WMP directly. Supports MP3 and possibly other formats.
        /// </summary>
        /// <param name="path"></param>
        public void PlayMp3(string path)
        {
            ThreadStart playback = new ThreadStart(delegate
            {
                try
                {
                    if (player.playState != WMPPlayState.wmppsUndefined
                        && player.playState != WMPPlayState.wmppsStopped)
                    {
                        return;
                    }

                    player.settings.volume = Volume;
                    player.URL = path;
                }
                catch
                {
                    // Ignore playback errors
                }
            });

            Thread soundThread = new Thread(playback);
            soundThread.Start();
        }

        #endregion

        #region Private Methods

        int GetAdjustedVolume(int volume)
        {
            return Math.Min(Math.Max(volume, 0), 100);
        }

        string GetRandomSound()
        {
            if (Library == null | Library.Count == 0)
                return string.Empty;

            int index = Randomizer.Next(Library.Count);
            return Path.Combine(SoundDir, Library[index]);
        }

        /// <summary>
        /// Play sound using WMP API within ACT. Supports WAV only.
        /// </summary>
        /// <param name="path"></param>
        void PlaySound(string path)
        {
            ActGlobals.oFormActMain.PlaySoundWmpApi(path, 50);
        }

        #endregion
    }
}
