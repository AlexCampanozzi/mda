﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Explorus.Model.GameObjects.Rigidbodies
{
    internal class directionAnimator : Animator
    {
        private Dictionary<int, Image> stateImg;
        private int[] state_order;
        public directionAnimator(Dictionary<int, Image> states, int[] order)
        {
            stateImg = states;
            state_order = order;
        }

        public override Image Animate(int progress, int DirX, int DirY)
        { 
            int temp = (int)Math.Floor(progress / (100.0 / state_order.Length));
            int current_state = state_order[temp];
            int direction;
            if (DirX == 1 && DirY == 0) direction = 10;
            else if (DirX == 0 && DirY == -1) direction = 20;
            else if (DirX == -1 && DirY == 0) direction = 30;
            else  direction = 10;

            return stateImg[direction + current_state];
        }
    }
}
