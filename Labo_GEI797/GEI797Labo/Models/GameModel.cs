using GEI797Labo.Models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Models
{
    internal class GameModel : IGameModel
    {
        private readonly ShapeList oShapes;

        public GameModel()
        {
            oShapes = new ShapeList();
        }

        public ShapeList Shapes
        {
            get { return oShapes; }
        }

        public void Dispose()
        {
            oShapes.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
