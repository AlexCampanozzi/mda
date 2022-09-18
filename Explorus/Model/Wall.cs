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
    public class Wall:GameObject
    {
        public Wall(Point pos, ImageLoader loader) : base(pos, loader.WallImage)
        {
            radius = 48;
        }
    }
}