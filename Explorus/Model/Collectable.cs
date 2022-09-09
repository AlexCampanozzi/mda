/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System.Drawing;

namespace Explorus
{
    public class Collectable : GameObject
    {
        public Collectable(Point pos, Image img) : base(pos, img)
        {

        }

        public Collectable(int x, int y, Image img) : base(x, y, img)
        {

        }
    }
}