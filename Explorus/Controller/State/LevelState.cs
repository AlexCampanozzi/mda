using Explorus.Model;
using Explorus.Threads;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Explorus.Controller

{
    public enum LevelOption
    {
        Slimes,
        Level,
        Back
    }

    public class LevelState : PauseState
    {
        private LevelOption menuOption;
        private List<string> listLevel = new List<string>();// = new string[4] {"dungeon","sand","lava","underwater"};
        private int slimes = 6;
        private int levelIndex = 0;
        public string chosenLevel = null;

        public int Slimes { get => slimes; set => slimes = value; }

        public LevelState(GameEngine engine) : base(engine)
        {
            this.engine = engine;

            string[] files = Directory.GetFiles("./Resources/Maps");
            foreach (string file in files)
            {
                bool flag = true;
                string[] words = file.Split('.');
                if (words[words.Count() - 1] != "png")
                {
                    flag = false;
                }
                else
                {
                    Bitmap tempImg = new Bitmap(file);
                    GraphicsUnit pixelRef = GraphicsUnit.Pixel;
                    RectangleF mapSizeFloat;
                    mapSizeFloat = tempImg.GetBounds(ref pixelRef);
                    int[] mapSize = new int[2] { (int)mapSizeFloat.Width, (int)mapSizeFloat.Height };

                    for (int x = 0; x < mapSize[0]; x++)
                    {
                        if(tempImg.GetPixel(x, 0).Name != "ff000000" || tempImg.GetPixel(x, mapSize[1] - 1).Name != "ff000000")
                        {
                            flag = false;
                        }

                    }

                    for (int y = 0; y < mapSize[1]; y++)
                    {
                        if (tempImg.GetPixel(0, y).Name != "ff000000" || tempImg.GetPixel(mapSize[0] - 1, y).Name != "ff000000")
                        {
                            flag = false;
                        }

                    }

                    int slimeCount = 0;
                    int toxicSlimeCount = 0;
                    int slimusCount = 0;

                    for (int x = 0; x < mapSize[0]; x++)
                    {
                        for (int y = 0; y < mapSize[1]; y++)
                        {
                            string pixelname = tempImg.GetPixel(x, y).Name;
                            if (pixelname == "ffffff00")
                                slimeCount++;
                            else if (pixelname == "ff0000ff")
                                slimusCount++;
                            else if (pixelname == "ff00ff00")
                                toxicSlimeCount++;
                        }

                    }

                    if (slimeCount < 1 || slimusCount < 1 || slimusCount > 1 || toxicSlimeCount < 8)
                        flag = false;
                }
                
                if(flag)
                    listLevel.Add(Path.GetFileName(file));
            }
        }

        public void Increment(int i)
        {
            if (menuOption == LevelOption.Slimes)
            {

                if (slimes >= 1 && slimes <= 8)
                {
                    slimes = slimes + i;
                }
                if (slimes < 1)
                {
                    slimes = 1;
                }
                if (slimes > 8)
                {
                    slimes = 8;
                }
            }
            if (menuOption == LevelOption.Level)
            {
                if (levelIndex >= 0 && levelIndex <= listLevel.Count-1)
                {
                    levelIndex = levelIndex + i;
                }
                if (levelIndex < 0)
                {
                    levelIndex = 0;
                }
                if (levelIndex > listLevel.Count - 1)
                {
                    levelIndex = listLevel.Count - 1;
                }

            }
        }
        public void SetOption(LevelOption option)
        {
            menuOption = option;
        }

        public LevelOption GetMenuOption()
        {
            return menuOption;
        }

        public string chosenLevelName()
        {
            if(chosenLevel != null)
            {
                return chosenLevel;
            }
            return listLevel[levelIndex];
        }

        public override string Name()
        {
            return "Level";
        }


    }
}
