using Explorus.Controller;
using Explorus.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;

namespace Explorus.Threads
{
     public sealed class AudioThread
     {
        private static AudioThread instance = new AudioThread();
        private static readonly object padlock = new object();

        List<SoundPlayer> soundBuffer = new List<SoundPlayer>() { };

        /*System.Media.SoundPlayer music1 = new System.Windows.Media.MediaPlayer("./Resources/Audio/sound06.wav");
        System.Media.SoundPlayer music2 = new System.Media.MediaPlayer("./Resources/Audio/sound07.wav");
        System.Media.SoundPlayer music3 = new System.Media.MediaPlayer("./Resources/Audio/sound10.wav");*/

        static AudioThread()
        {

        }

        private AudioThread()
        {

        }

        public static AudioThread Instance
        {
            get
            {
                return instance;
            }
        }

        public void addSound(SoundPlayer sound)
        {
                soundBuffer.Add(sound);
        }
        
        
        public void Run()
        {
            //new Thread(music).Start();
            while (true)
            {
                Thread.Sleep(50);
                if(soundBuffer.Count > 0)
                {
                    SoundPlayer tmp = soundBuffer.First();
                    Thread newthread = new Thread(tmp.Play);
                    newthread.Start();
                    soundBuffer.RemoveAt(0);
                }
            }
        }

        private void music()
        {
            while(true)
            {
                /*music1.PlaySync();
                music2.PlaySync();
                music3.PlaySync();*/
            }
        }
    }
}
