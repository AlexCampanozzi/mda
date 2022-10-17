using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Explorus.Controller

{
    public enum AudioOption
    {
        Music,
        Sound,
        Back
    }


    public class AudioState : PauseState
    {
        private int musicVolume = 50;
        private int soundVolume = 50;

        private int musicVolumeMemory = 50;
        private int soundVolumeMemory = 50;
        private bool musicIsMuted = false;
        private bool soundIsMuted = false;


        private AudioOption menuOption;
        AudioThread audio = AudioThread.Instance;

        public int MusicVolume { get => musicVolume; set => musicVolume = value; }
        public int SoundVolume { get => soundVolume; set => soundVolume = value; }
        public bool MusicIsMuted { get => musicIsMuted; set => musicIsMuted = value; }
        public bool SoundIsMuted { get => soundIsMuted; set => soundIsMuted = value; }

        public AudioState(GameEngine engine) : base(engine)
        {
            this.engine = engine;
            menuOption = AudioOption.Music;
        }

        public void ChangeVolume(int volume)
        {

            if (menuOption == AudioOption.Music)
            {
                if (!musicIsMuted)
                {
                    if (musicVolume >= 0 && musicVolume <= 100)
                    {
                        musicVolume = musicVolume + volume;
                    }
                    if (musicVolume < 0)
                    {
                        musicVolume = 0;
                    }
                    if (musicVolume > 100)
                    {
                        musicVolume = 100;
                    }
                    if (volume == 0)
                    {
                        musicIsMuted = true;
                        musicVolumeMemory = musicVolume;
                        musicVolume = 0;
                    }
                }
                
                else if (volume == 0 )
                {
                    musicIsMuted = false;
                    musicVolume = musicVolumeMemory;
                }

                audio.setMusicVolume(musicVolume);
                Console.WriteLine(menuOption.ToString() + " volume is " + musicVolume);


            }
            else if (menuOption == AudioOption.Sound)
            {
                if (!soundIsMuted)
                {
                    if (soundVolume >= 0 && soundVolume <= 100)
                    {
                        soundVolume = soundVolume + volume;
                    }
                    if (soundVolume < 0)
                    {
                        soundVolume = 0;
                    }
                    if (soundVolume > 100)
                    {
                        soundVolume = 100;
                    }
                    if (volume == 0)
                    {
                        soundIsMuted = true;
                        soundVolumeMemory = soundVolume;
                        soundVolume = 0;
                    }
                }

                else if (volume == 0)
                {
                    soundIsMuted = false;
                    soundVolume = soundVolumeMemory;
                }
                Console.WriteLine(menuOption.ToString() + " volume is " + soundVolume);
                audio.setSoundVolume(soundVolume);
            }



            //le set dans audio thread
        }

        public void SetOption(AudioOption option)
        {
            menuOption = option;
        }

        public AudioOption GetMenuOption()
        {
            return menuOption;
        }

        public override string Name()
        {
            return "Audio";
        }


    }
}
