/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Forms;
using Explorus.Controller;

namespace Explorus.Model
{
    public sealed class MenuWindow
    {
        private static readonly MenuWindow instance = new MenuWindow();
        private static readonly object padlock = new object();

        private Font titleFont = new Font("Comic Sans MS", 56);
        private Font optionFont = new Font("Comic Sans MS", 38);
        private SolidBrush brushBlue = new SolidBrush(Color.FromArgb(255, (byte)69, (byte)180, (byte)239));
        private SolidBrush brushYellow = new SolidBrush(Color.Yellow);
        private int nextLine = 60;

        private Graphics g;

        private GameEngine engine = GameEngine.GetInstance();
        private Option currentOption;

        private AudioOption currentAudioOption;

        static MenuWindow()
        {
        }
        private MenuWindow()
        {
        }

        public static MenuWindow Instance
        {
            get
            {
                return instance;
            }
        }

        public Graphics getMenuWindow(PaintEventArgs e)
        {

            g = e.Graphics;
            g.DrawString("Explorus", titleFont, brushBlue, 200, 50);

            if (engine.GetState().GetType() == typeof(StartState))
            {
                startMenu();
            }

            if (engine.GetState().GetType() == typeof(PauseState))
            {
                pauseMenu();
            }

            placeCursor();



            return g;
        }

        public void audioMenu(PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawString("Explorus", titleFont, brushBlue, 200, 50);

            g.DrawString("Music Volume : ", optionFont, brushBlue, 250, 150 + nextLine);
            if (engine.GetAudioState().MusicIsMuted)
            {
                g.DrawString("Mute", optionFont, brushYellow, 650, 150 + nextLine);
            }
            else
            {
                g.DrawString(engine.GetAudioState().MusicVolume.ToString(), optionFont, brushYellow, 650, 150 + nextLine);
            }

            g.DrawString("Sound Volume :", optionFont, brushBlue, 250, 150 + nextLine * 2);
            if (engine.GetAudioState().SoundIsMuted)
            {
                g.DrawString("Mute", optionFont, brushYellow, 650, 150 + nextLine * 2);
            }
            else
            {
                g.DrawString(engine.GetAudioState().SoundVolume.ToString(), optionFont, brushYellow, 650, 150 + nextLine * 2);

            }

            g.DrawString("Back", optionFont, brushBlue, 250, 150 + nextLine * 3);


            AudioOption currentAudioOption = engine.GetAudioState().GetMenuOption();

            if (currentAudioOption == AudioOption.Music)
            {
                g.DrawString("▸", optionFont, brushBlue, 200, 150 + nextLine);
            }

            if (currentAudioOption == AudioOption.Sound)
            {
                g.DrawString("▸", optionFont, brushBlue, 200, 150 + nextLine * 2);
            }

            if (currentAudioOption == AudioOption.Back)
            {
                g.DrawString("▸", optionFont, brushBlue, 200, 150 + nextLine * 3);
            }

        }

        public void startMenu()
        {
            g.DrawString("Options", optionFont, brushBlue, 200, 150);
            g.DrawString("Start Game", optionFont, brushBlue, 250, 150 + nextLine);
            g.DrawString("Audio", optionFont, brushBlue, 250, 150 + nextLine * 2);
            g.DrawString("Level Select", optionFont, brushBlue, 250, 150 + nextLine * 3);
            g.DrawString("Exit Game", optionFont, brushBlue, 250, 150 + nextLine * 4);
        }

        public void pauseMenu()
        {
            g.DrawString("Options", optionFont, brushBlue, 200, 150);
            g.DrawString("Resume Game", optionFont, brushBlue, 250, 150 + nextLine);
            g.DrawString("Audio", optionFont, brushBlue, 250, 150 + nextLine * 2);
            g.DrawString("Exit Game", optionFont, brushBlue, 250, 150 + nextLine * 3);
        }

        public void placeCursor()
        {
            if(currentOption == Option.Start)
            {
                g.DrawString("▸", optionFont, brushBlue, 200, 150 + nextLine);
            }
            if (currentOption == Option.Audio)
            {
                g.DrawString("▸", optionFont, brushBlue, 200, 150 + nextLine * 2);
            }

            if (engine.GetState().GetType() == typeof(StartState))
            {
                if (currentOption == Option.Level)
                {
                    g.DrawString("▸", optionFont, brushBlue, 200, 150 + nextLine * 3);
                }

                if (currentOption == Option.Exit)
                {
                    g.DrawString("▸", optionFont, brushBlue, 200, 150 + nextLine * 4);

                }
            }
            if (engine.GetState().GetType() == typeof(PauseState))
            {
                if (currentOption == Option.Exit)
                {
                    g.DrawString("▸", optionFont, brushBlue, 200, 150 + nextLine * 3);
                }
            }
        }

        public void setOption(Option option)
        {
            currentOption = option;
            Console.WriteLine(currentOption);

        }

        public void setAudioOption(AudioOption option)
        {
            currentAudioOption = option;
        }


    }

}