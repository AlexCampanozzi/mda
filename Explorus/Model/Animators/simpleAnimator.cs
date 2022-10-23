using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Explorus.Model.GameObjects.Rigidbodies
{
    [Serializable]
    internal class simpleAnimator: Animator
    {
        private Dictionary<int, Image> stateImg;
        private int[] state_order;
        public simpleAnimator(GameObject parent, Dictionary<int, Image> states, int[] order)
        {
            stateImg = states;
            state_order = order;
        }

        public override Image Animate(int progress)
        {
            int temp = Math.Min((int)Math.Ceiling(progress/ (100.0 / state_order.Length)), 3);
            int current_state = state_order[temp - 1];

            return stateImg[current_state];
        }
    }
}
