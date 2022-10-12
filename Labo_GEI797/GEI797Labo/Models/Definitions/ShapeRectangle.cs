using System.Drawing;

namespace GEI797Labo.Models.Definitions
{
    internal class ShapeRectangle : Shape
    {
        public ShapeRectangle(Color color, int width, int height, int position_X = 0, int position_Y = 0) : 
            base(color, width, height, position_X, position_Y)
        { }

        public override ShapeType GetShapeType
        {
            get { return ShapeType.Rectangle; }
        }
    }
}
