//SOUNDBUFFER(N = 2) = SOUNDBUFFER[0],
//SOUNDBUFFER[i: 0..N] = (when(i < N) addSound->SOUNDBUFFER[i + 1]
//| when(i > 0) playSound->clearBuffer->SOUNDBUFFER[i - 1]
//).

//MAIN = (addSound->MAIN).
//SOUNDTHREAD = (playSound->SOUNDTHREAD).
//MUSIC = (playMusic->MUSIC).

//|| BUFFERS = (MAIN || SOUNDBUFFER || SOUNDTHREAD || MUSIC).
using Explorus.Controller;
using Explorus.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Forms;



namespace Explorus.Threads
{
    public enum Sound
    {
        soundHit,
        soundDead,
        soundDirectionChange,
        soundMoveS,
        soundHitS,
        soundGemS,
        soundDoorS,
        soundWinS,
        soundShootS,
        soundHitB,
        soundHitWallB
    }

     public sealed class AudioThread
     {
        private static AudioThread instance = new AudioThread();
        private static readonly object padlock = new object();

        private double soundVolume = 0.5;
        private double musicVolume = 0.5;

        private bool changeMusic = false;

        private string song = "Resources/Audio/soundtrackMenu.wav";



        List<Sound> soundBuffer = new List<Sound>() { };

        private MediaPlayer soundtrack;
        private MediaPlayer soundtrackMenu;

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

        public void addSound(Sound sound)
        {
                soundBuffer.Add(sound);

        }
        
        
        public void Run()
        {
            MediaPlayer soundHit = new MediaPlayer();
            MediaPlayer soundDead = new MediaPlayer();
            MediaPlayer soundDirectionChange = new MediaPlayer();
            MediaPlayer soundMoveS = new MediaPlayer();
            MediaPlayer soundHitS = new MediaPlayer();
            MediaPlayer soundGemS = new MediaPlayer();
            MediaPlayer soundDoorS = new MediaPlayer();
            MediaPlayer soundWinS = new MediaPlayer();
            MediaPlayer soundShootS = new MediaPlayer();
            MediaPlayer soundHitB = new MediaPlayer();
            MediaPlayer soundHitWallB = new MediaPlayer();

            soundDead.Open(new System.Uri("Resources/Audio/sound08.wav", UriKind.Relative));
            soundHit.Open(new System.Uri("Resources/Audio/sound19.wav", UriKind.Relative));
            soundDirectionChange.Open(new System.Uri("Resources/Audio/sound13.wav", UriKind.Relative));
            soundMoveS.Open(new System.Uri("Resources/Audio/sound01.wav", UriKind.Relative));
            soundHitS.Open(new System.Uri("Resources/Audio/sound03.wav", UriKind.Relative));
            soundGemS.Open(new System.Uri("Resources/Audio/sound08.wav", UriKind.Relative));
            soundDoorS.Open(new System.Uri("Resources/Audio/sound09.wav", UriKind.Relative));
            soundWinS.Open(new System.Uri("Resources/Audio/sound10.wav", UriKind.Relative));
            soundShootS.Open(new System.Uri("Resources/Audio/sound15.wav", UriKind.Relative));
            soundHitB.Open(new System.Uri("Resources/Audio/sound11.wav", UriKind.Relative));
            soundHitWallB.Open(new System.Uri("Resources/Audio/sound13.wav", UriKind.Relative));

            new Thread(music).Start();
            Thread.Sleep(1000);
            while (true)
            {
                if(soundBuffer.Count > 0)
                {
                    switch(soundBuffer.First())
                    {
                        case Sound.soundHit:
                            soundHit.Position = System.TimeSpan.FromSeconds(0);
                            soundHit.Volume = soundVolume;
                            soundHit.Play();
                            break;
                        case Sound.soundDead:
                            soundDead.Position = System.TimeSpan.FromSeconds(0);
                            soundDead.Volume = soundVolume;
                            soundDead.Play();
                            break;
                        case Sound.soundDirectionChange:
                            soundDirectionChange.Position = System.TimeSpan.FromSeconds(0);
                            soundDirectionChange.Volume = soundVolume;
                            soundDirectionChange.Play();
                            break;
                        case Sound.soundMoveS:
                            soundMoveS.Position = System.TimeSpan.FromSeconds(0);
                            soundMoveS.Volume = soundVolume;
                            soundMoveS.Play();
                            break;
                        case Sound.soundHitS:
                            soundHitS.Position = System.TimeSpan.FromSeconds(0);
                            soundHitS.Volume = soundVolume;
                            soundHitS.Play();
                            break;
                        case Sound.soundGemS:
                            soundGemS.Position = System.TimeSpan.FromSeconds(0);
                            soundGemS.Volume = soundVolume;
                            soundGemS.Play();
                            break;
                        case Sound.soundDoorS:
                            soundDoorS.Position = System.TimeSpan.FromSeconds(0);
                            soundDoorS.Volume = soundVolume;
                            soundDoorS.Play();
                            break;
                        case Sound.soundWinS:
                            soundWinS.Position = System.TimeSpan.FromSeconds(0);
                            soundWinS.Volume = soundVolume;
                            soundWinS.Play();
                            break;
                        case Sound.soundShootS:
                            soundShootS.Position = System.TimeSpan.FromSeconds(0);
                            soundShootS.Volume = soundVolume;
                            soundShootS.Play();
                            break;
                        case Sound.soundHitB:
                            soundHitB.Position = System.TimeSpan.FromSeconds(0);
                            soundHitB.Volume = soundVolume;
                            soundHitB.Play();
                            break;
                        case Sound.soundHitWallB:
                            soundHitWallB.Position = System.TimeSpan.FromSeconds(0);
                            soundHitWallB.Volume = soundVolume;
                            soundHitWallB.Play();
                            break;
                    }
                    soundBuffer.RemoveAt(0);
                }
                else
                {

                    Thread.Sleep(500);
                }

            }
        }

        private void music()
        {
            soundtrack = new MediaPlayer();
            soundtrack.Open(new System.Uri("Resources/Audio/soundtrackMenu.wav", UriKind.Relative));
            //soundtrack.Volume = musicVolume;

            soundtrack.ScrubbingEnabled = true;
            soundtrack.Play();
            int i = 0;
            while (true)
            {
                i++;
                soundtrack.Volume = musicVolume;
                if (soundtrack.Position >= soundtrack.NaturalDuration)
                    soundtrack.Position = TimeSpan.FromSeconds(0);

                if (changeMusic)
                {
                    soundtrack.Stop();
                    soundtrack.Close();
                    soundtrack = new MediaPlayer();
                    soundtrack.Open(new System.Uri(song, UriKind.Relative));
                    soundtrack.Play();
                    changeMusic = false;
                }
            }
        }

        public void setMusicVolume(int volume)
        {
            changeMusic = true;
            musicVolume = (double)volume/100;
        }

        public void setSoundVolume(int volume)
        {
            soundVolume = (double)volume/100;
        }

        public void setJazzMusic()
        {
            song = "Resources/Audio/soundtrackJazz.wav";
            changeMusic = true;
        }

        public void setGameMusic()
        {
            song = "Resources/Audio/soundtrack.wav";
            changeMusic = true;
        }

        public void setSlamMusic()
        {
            song = "Resources/Audio/soundtrackSlam.wav";
            changeMusic = true;
        }

        public void setMenuMusic()
        {
            song = "Resources/Audio/soundtrackMenu.wav";
            changeMusic = true;
        }
    }
}
