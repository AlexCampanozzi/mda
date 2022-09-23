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


        

        List<Sound> soundBuffer = new List<Sound>() { };

        private MediaPlayer soundtrack;

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
            
            new Thread(music).Start();
            while (true)
            {
                if(soundBuffer.Count > 0)
                {
                    switch(soundBuffer.First())
                    {
                        case Sound.soundHit:
                            MediaPlayer soundHit = new MediaPlayer();
                            soundHit.Open(new System.Uri("Resources/Audio/sound19.wav", UriKind.Relative));
                            soundHit.Volume = soundVolume;
                            soundHit.Play();
                            break;
                        case Sound.soundDead:
                            MediaPlayer soundDead = new MediaPlayer();
                            soundDead.Open(new System.Uri("Resources/Audio/sound08.wav", UriKind.Relative));
                            soundDead.Volume = soundVolume;
                            soundDead.Play();
                            break;
                        case Sound.soundDirectionChange:
                            MediaPlayer soundDirectionChange = new MediaPlayer();
                            soundDirectionChange.Open(new System.Uri("Resources/Audio/sound13.wav", UriKind.Relative));
                            soundDirectionChange.Volume = soundVolume;
                            soundDirectionChange.Play();
                            break;
                        case Sound.soundMoveS:
                            MediaPlayer soundMoveS = new MediaPlayer();
                            soundMoveS.Open(new System.Uri("Resources/Audio/sound01.wav", UriKind.Relative));
                            soundMoveS.Volume = soundVolume;
                            soundMoveS.Play();
                            break;
                        case Sound.soundHitS:
                            MediaPlayer soundHitS = new MediaPlayer();
                            soundHitS.Open(new System.Uri("Resources/Audio/sound03.wav", UriKind.Relative));
                            soundHitS.Volume = soundVolume;
                            soundHitS.Play();
                            break;
                        case Sound.soundGemS:
                            MediaPlayer soundGemS = new MediaPlayer();
                            soundGemS.Open(new System.Uri("Resources/Audio/sound08.wav", UriKind.Relative));
                            soundGemS.Volume = soundVolume;
                            soundGemS.Play();
                            break;
                        case Sound.soundDoorS:
                            MediaPlayer soundDoorS = new MediaPlayer();
                            soundDoorS.Open(new System.Uri("Resources/Audio/sound09.wav", UriKind.Relative));
                            soundDoorS.Volume = soundVolume;
                            soundDoorS.Play();
                            break;
                        case Sound.soundWinS:
                            MediaPlayer soundWinS = new MediaPlayer();
                            soundWinS.Open(new System.Uri("Resources/Audio/sound10.wav", UriKind.Relative));
                            soundWinS.Volume = soundVolume;
                            soundWinS.Play();
                            break;
                        case Sound.soundShootS:
                            MediaPlayer soundShootS = new MediaPlayer();
                            soundShootS.Open(new System.Uri("Resources/Audio/sound15.wav", UriKind.Relative));
                            soundShootS.Volume = soundVolume;
                            soundShootS.Play();
                            break;
                        case Sound.soundHitB:
                            MediaPlayer soundHitB = new MediaPlayer();
                            soundHitB.Open(new System.Uri("Resources/Audio/sound11.wav", UriKind.Relative));
                            soundHitB.Volume = soundVolume;
                            soundHitB.Play();
                            break;
                        case Sound.soundHitWallB:
                            MediaPlayer soundHitWallB = new MediaPlayer();

                            soundHitWallB.Open(new System.Uri("Resources/Audio/sound13.wav", UriKind.Relative));
                            soundHitWallB.Volume = soundVolume;
                            soundHitWallB.Play();
                            break;
                    }
                    soundBuffer.RemoveAt(0);
                }
                else
                {

                    Thread.Sleep(50);
                }

            }
        }

        private void music()
        {

            while (true)
            {

                soundtrack = new MediaPlayer();
                soundtrack.Open(new System.Uri("Resources/Audio/soundtrack.wav", UriKind.Relative));
                //soundtrack.Volume = musicVolume;
                soundtrack.Play();
                int i = 0;
                while (i < 300)
                {
                    Thread.Sleep(100);
                    i++;
                    soundtrack.Volume = musicVolume;
                }
            }
        }

        public void setMusicVolume(int volume)
        {
            musicVolume = (double)volume/100;
        }

        public void setSoundVolume(int volume)
        {
            soundVolume = (double)volume/100;
        }
    }
}
