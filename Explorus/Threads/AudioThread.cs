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

        private int soundVolume = 50;
        private int musicVolume = 50;


        

        List<Sound> soundBuffer = new List<Sound>() { };
        


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
                            soundHit.Play();
                            break;
                        case Sound.soundDead:
                            MediaPlayer soundDead = new MediaPlayer();
                            soundDead.Open(new System.Uri("Resources/Audio/sound08.wav", UriKind.Relative));
                            soundDead.Play();
                            break;
                        case Sound.soundDirectionChange:
                            MediaPlayer soundDirectionChange = new MediaPlayer();
                            soundDirectionChange.Open(new System.Uri("Resources/Audio/sound13.wav", UriKind.Relative));
                            soundDirectionChange.Play();
                            break;
                        case Sound.soundMoveS:
                            MediaPlayer soundMoveS = new MediaPlayer();
                            soundMoveS.Open(new System.Uri("Resources/Audio/sound01.wav", UriKind.Relative));
                            soundMoveS.Play();
                            break;
                        case Sound.soundHitS:
                            MediaPlayer soundHitS = new MediaPlayer();
                            soundHitS.Open(new System.Uri("Resources/Audio/sound03.wav", UriKind.Relative));
                            soundHitS.Play();
                            break;
                        case Sound.soundGemS:
                            MediaPlayer soundGemS = new MediaPlayer();
                            soundGemS.Open(new System.Uri("Resources/Audio/sound08.wav", UriKind.Relative));
                            soundGemS.Play();
                            break;
                        case Sound.soundDoorS:
                            MediaPlayer soundDoorS = new MediaPlayer();
                            soundDoorS.Open(new System.Uri("Resources/Audio/sound09.wav", UriKind.Relative));
                            soundDoorS.Play();
                            break;
                        case Sound.soundWinS:
                            MediaPlayer soundWinS = new MediaPlayer();
                            soundWinS.Open(new System.Uri("Resources/Audio/sound10.wav", UriKind.Relative));
                            soundWinS.Play();
                            break;
                        case Sound.soundShootS:
                            MediaPlayer soundShootS = new MediaPlayer();
                            soundShootS.Open(new System.Uri("Resources/Audio/sound15.wav", UriKind.Relative));
                            soundShootS.Play();
                            break;
                        case Sound.soundHitB:
                            MediaPlayer soundHitB = new MediaPlayer();
                            soundHitB.Open(new System.Uri("Resources/Audio/sound11.wav", UriKind.Relative));
                            soundHitB.Play();
                            break;
                        case Sound.soundHitWallB:
                            MediaPlayer soundHitWallB = new MediaPlayer();

                            soundHitWallB.Open(new System.Uri("Resources/Audio/sound13.wav", UriKind.Relative));
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
            SoundPlayer soundtrack = new SoundPlayer("./Resources/Audio/soundtrack.wav");
            while (true)
            {
                soundtrack.PlaySync();
            }
        }

        public void setMusicVolume(int volume)
        {

        }

        public void setSoundVolume(int volume)
        {

        }
    }
}
