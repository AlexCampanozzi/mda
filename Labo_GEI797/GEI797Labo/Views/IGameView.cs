using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Views
{
    internal interface IGameView
    {
        void Show();
        void CloseWindow();
        void Render();
    }
}
