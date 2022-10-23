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
        private SolidBrush brushWhite = new SolidBrush(Color.White);
        private SolidBrush brushYellow = new SolidBrush(Color.Yellow);
        private int nextLine = 60;

        private Graphics g;

        private GameEngine engine = GameEngine.GetInstance();
        
        private Option currentOption;
        private bool isChanged = true;


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

        public bool IsChanged { get => isChanged; set => isChanged = value; }

        public System.Drawing.Image getMenuWindow(PaintEventArgs e)
        {

            System.Drawing.Image bitmap = new Bitmap(1000, 1000);
            using (Graphics graph = Graphics.FromImage(bitmap))
            {
                g = graph;
                g.DrawString("Explorus", titleFont, brushBlue, 200, 50);

                if (engine.GetState().GetType() == typeof(StartState))
                {
                    startMenu();
                }

                if (engine.GetState().GetType() == typeof(PauseState))
                {
                    pauseMenu();
                }

                if (engine.GetState().GetType() == typeof(AudioState))
                {
                    audioMenu();
                }

                if (engine.GetState().GetType() == typeof(LevelState))
                {
                    levelMenu();
                }

                placeCursor();
            }

            isChanged = false;
            return bitmap;
        }

        public void audioMenu()
        {
            g.DrawString("Music Volume : ", optionFont, brushWhite, 250, 150 + nextLine);

            if (engine.GetAudioState().MusicIsMuted)
            {
                g.DrawString("Mute", optionFont, brushYellow, 650, 150 + nextLine);
            }
            else
            {
                g.DrawString(engine.GetAudioState().MusicVolume.ToString(), optionFont, brushYellow, 650, 150 + nextLine);
            }

            g.DrawString("Sound Volume :", optionFont, brushWhite, 250, 150 + nextLine * 2);
            if (engine.GetAudioState().SoundIsMuted)
            {
                g.DrawString("Mute", optionFont, brushYellow, 650, 150 + nextLine * 2);
            }

            else
            {
                g.DrawString(engine.GetAudioState().SoundVolume.ToString(), optionFont, brushYellow, 650, 150 + nextLine * 2);

            }

            g.DrawString("Back", optionFont, brushWhite, 250, 150 + nextLine * 3);


        }

        public void levelMenu()
        {
            g.DrawString("Explorus", titleFont, brushBlue, 200, 50);

            g.DrawString("Slimes Quantity : ", optionFont, brushWhite, 250, 150 + nextLine);
            g.DrawString(engine.GetLevelState().Slimes.ToString(), optionFont, brushYellow, 680, 150 + nextLine);

            g.DrawString("Level Select : ", optionFont, brushWhite, 250, 150 + nextLine * 2);
            g.DrawString(engine.GetLevelState().chosenLevelName().Split('.')[0], optionFont, brushYellow, 350, 150 + nextLine * 3);

            g.DrawString("Back", optionFont, brushWhite, 250, 150 + nextLine * 4);

        }

        public void startMenu()
        {
            g.DrawString("Options", optionFont, brushWhite, 200, 150);
            g.DrawString("Start Game", optionFont, brushWhite, 250, 150 + nextLine);
            g.DrawString("Audio", optionFont, brushWhite, 250, 150 + nextLine * 2);
            g.DrawString("Level Select", optionFont, brushWhite, 250, 150 + nextLine * 3);
            g.DrawString("Exit Game", optionFont, brushWhite, 250, 150 + nextLine * 4);
        }

        public void pauseMenu()
        {
            g.DrawString("Options", optionFont, brushWhite, 200, 150);
            g.DrawString("Resume Game", optionFont, brushWhite, 250, 150 + nextLine);
            g.DrawString("Audio", optionFont, brushWhite, 250, 150 + nextLine * 2);
            g.DrawString("Exit Game", optionFont, brushWhite, 250, 150 + nextLine * 3);
        }

        public void placeCursor()
        {
            if (currentOption == Option.Start)
            {
                g.DrawString("▸", optionFont, brushWhite, 200, 150 + nextLine);
            }

            else if (currentOption == Option.Audio && engine.GetState().GetType() != typeof(AudioState))
            {
                g.DrawString("▸", optionFont, brushWhite, 200, 150 + nextLine * 2);
            }

            if (engine.GetState().GetType() == typeof(StartState))
            {
                if (currentOption == Option.Level)
                {
                    g.DrawString("▸", optionFont, brushWhite, 200, 150 + nextLine * 3);
                }

                if (currentOption == Option.Exit)
                {
                    g.DrawString("▸", optionFont, brushWhite, 200, 150 + nextLine * 4);

                }
            }

            else if (engine.GetState().GetType() == typeof(PauseState))
            {
                if (currentOption == Option.Exit)
                {
                    g.DrawString("▸", optionFont, brushWhite, 200, 150 + nextLine * 3);
                }
            }

            else if (engine.GetState().GetType() == typeof(AudioState))
            {
                AudioOption currentAudioOption = engine.GetAudioState().GetMenuOption();

                if (currentAudioOption == AudioOption.Music)
                {
                    g.DrawString("▸", optionFont, brushWhite, 200, 150 + nextLine);
                }

                else if (currentAudioOption == AudioOption.Sound)
                {
                    g.DrawString("▸", optionFont, brushWhite, 200, 150 + nextLine * 2);
                }

                else if (currentAudioOption == AudioOption.Back)
                {
                    g.DrawString("▸", optionFont, brushWhite, 200, 150 + nextLine * 3);
                }
            }

            else if (engine.GetState().GetType() == typeof(LevelState))
            {

                LevelOption currentLevelOption = engine.GetLevelState().GetMenuOption();

                if (currentLevelOption == LevelOption.Slimes)
                {
                    g.DrawString("▸", optionFont, brushWhite, 200, 150 + nextLine);
                }

                if (currentLevelOption == LevelOption.Level)
                {
                    g.DrawString("▸", optionFont, brushWhite, 200, 150 + nextLine * 2);
                }

                if (currentLevelOption == LevelOption.Back)
                {
                    g.DrawString("▸", optionFont, brushWhite, 200, 150 + nextLine * 4);
                }
            }
        }

        public void setOption(Option option)
        {
            currentOption = option;
        }

        


    }

}