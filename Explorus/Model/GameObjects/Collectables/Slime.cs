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
    public class Slime : Collectable
    {
        public Slime(Point pos, ImageLoader loader, int ID) : base(pos, loader.SlimeImage, ID)
        {
            collider = new CircleCollider(this, 24);
        }
    }
}