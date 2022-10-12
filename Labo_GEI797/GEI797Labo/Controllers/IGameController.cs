using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEI797Labo.Controllers
{
    internal interface IGameController
    {
        int DefaultSize { get; }
        void User_KeyDown(KeyEventArgs e);
        void User_KeyUp(KeyEventArgs e);
        void StartGame();
        void StopGame();
        bool IsRunning { get; }
    }
}
