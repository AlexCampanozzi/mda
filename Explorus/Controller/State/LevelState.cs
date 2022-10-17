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
    public enum LevelOption
    {
        Slimes,
        Level,
        Back
    }

    public class LevelState : PauseState
    {
        private LevelOption menuOption;
        private string[] listLevel = new string[4] {"dungeon","sand","lava","underwater"};
        private int slimes = 6;
        private int levelIndex = 0;
        private string chosenLevel;

        public int Slimes { get => slimes; set => slimes = value; }

        public LevelState(GameEngine engine) : base(engine)
        {
            this.engine = engine;
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
                if (levelIndex >= 0 && levelIndex <= listLevel.Length-1)
                {
                    levelIndex = levelIndex + i;
                }
                if (levelIndex < 0)
                {
                    levelIndex = 0;
                }
                if (levelIndex > listLevel.Length - 1)
                {
                    levelIndex = listLevel.Length - 1;
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
            return listLevel[levelIndex];
        }

        public override string Name()
        {
            return "Level";
        }


    }
}
