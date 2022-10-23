/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using System.Drawing;

namespace Explorus.Model
{

    [Serializable]
    public class Wall:GameObject
    { 
        public Wall(Point pos, ImageLoader loader, int ID) : base(pos, loader.WallImage, ID)
        {
            collider = new SquareCollider(this, 48, 48);
        }
    }
}