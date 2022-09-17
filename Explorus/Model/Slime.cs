/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System.Drawing;

namespace Explorus.Model
{
    public class Slime : Collectable
    {
        public Slime(Point pos) : base(pos, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(528, 48, 48, 48), new Bitmap("./Resources/TilesSheet.png").PixelFormat))
        {
            radius = 24;
        }
    }
}