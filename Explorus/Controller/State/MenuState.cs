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
            menuOption = Option.Music;
        }

        public void ChangeVolume(int volume)
        {

            if (menuOption == Option.Music)
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
                    musicVolume = 0;
                }

                Console.WriteLine(menuOption.ToString() + " volume is " + musicVolume);


            }
            else if (menuOption == Option.Sound)
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
                    soundVolume = 0;
                }
                Console.WriteLine(menuOption.ToString() + " volume is " + soundVolume);

            }



            //le set dans audio thread
        }

        public void SetOption(Option option)
        {
            Console.WriteLine("current option is " + option.ToString());
            menuOption = option;
        }

        public Option GetMenuOption()
        {
            return menuOption;
        }




    }
}
