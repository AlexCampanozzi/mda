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
    public class Gem : Collectable
    {
        public Gem(Point pos, ImageLoader loader, int ID) : base(pos, loader.GemImage, ID)
        {
            collider = new CircleCollider(this, 21);
        }
    }

}