using System.Drawing;

namespace GEI797Labo.Models.Definitions
{
    internal abstract class Shape
    {
        private float posX = 0;
        private float posY = 0;

        public Shape(Color color, int width, int height, float position_X = 0, float position_Y = 0)
        {
            Color = color;
            Width = width > 1 ? width : 1;
            Height = height > 1 ? width : 1;
            Position_X = position_X;
            Position_Y = position_Y;
        }

        public abstract ShapeType GetShapeType { get; }

        public Color Color { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public float Position_X {
            get { return posX; }
            set { posX = value > 0 ? value : 0; } 
        }
        public float Position_Y {
            get { return posY; }
            set { posY = value > 0 ? value : 0; }
        }

        //************************************************
        //                À COMPLÉTER
        //************************************************
        public bool Collision(Shape shape)
        {
            // COMPLETER LE CODE

            return false;
        }
    }
}
