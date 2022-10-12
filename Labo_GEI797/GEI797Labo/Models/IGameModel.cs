using GEI797Labo.Models.Collections;

namespace GEI797Labo.Models
{
    internal interface IGameModel
    {
        void Dispose();
        ShapeList Shapes { get; }
    }
}
