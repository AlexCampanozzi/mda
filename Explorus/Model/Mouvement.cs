using System;
using System.Windows.Forms;

namespace Explorus
{
    public class Mouvement
    {
        public bool currentlyMoving = false;
        private Position currentPosition;

        public Mouvement(Position pos)
        {
            currentPosition = pos;
            currentlyMoving = false;
        }

        public void KeyPress(Keys currentInput)
        {
            currentlyMoving = true;
            //MouvementEnded += move_MouvementEnded;
            

        }

        public void move_MouvementEnded(object sender, EventArgs e)
        {
            currentlyMoving = false;
            Console.WriteLine("finished movement");
        }

        public event EventHandler MouvementEnded;

        public virtual void OnMouvementEnd(EventArgs e)
        {
            MouvementEnded?.Invoke(this, e);
        }

        public bool GetCurrentlyMoving()
        {
            return currentlyMoving;
        }

    }
}
