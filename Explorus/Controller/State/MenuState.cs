using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Controller

{
    public enum Option
    {
        Music,
        Sound
    }

    class MenuState : PauseState
    {
        private int musicVolume = 50;
        private int soundVolume = 50;

        private Option menuOption;

        public MenuState(GameEngine engine) : base(engine)
        {
            this.engine = engine;
            Console.WriteLine("MenuState state");
        }

        public void ChangeVolume(int volume)
        {
            int currentMenuVolume;
            if (menuOption == Option.Music)
            {
                currentMenuVolume = musicVolume + volume;
            }
            else if (menuOption == Option.Sound)
            {
                currentMenuVolume= soundVolume + volume;
            }



            //le set dans audio thread
        }

        public void SetOption(Option option)
        {
            Console.WriteLine("current option is " + option.ToString());
            menuOption = option;
        }




    }
}
