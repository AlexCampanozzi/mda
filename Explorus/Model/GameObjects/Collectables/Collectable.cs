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
    public class Collectable : GameObject
    {
        public Collectable(Point pos, Image img, int ID) : base(pos, img, ID)
        {

        }
    }
}