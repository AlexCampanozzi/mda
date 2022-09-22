using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Explorus.Controller;
using Explorus.Model.GameObjects.Rigidbodies;
using Explorus.Threads;
//using static System.Net.Mime.MediaTypeNames;

namespace Explorus.Model
{
    public class Bubble : RigidBody
    {
        private Dictionary<int, Image> images;
        private Movement bubble_movement;

        private Slimus slimus;
        private int velocity;
        private int dirX;
        private int dirY;
        private Direction direction;

        private simpleAnimator animator;

        private PhysicsThread physics = PhysicsThread.GetInstance();

        private int popped = 0;

        private AudioThread audio = AudioThread.Instance;
        System.Media.SoundPlayer soundHit = new System.Media.SoundPlayer("./Resources/Audio/sound11.wav");
        System.Media.SoundPlayer soundHitWall = new System.Media.SoundPlayer("./Resources/Audio/sound13.wav");
        public Bubble(Point pos, ImageLoader loader, int ID, Slimus _slimus) : base(pos, loader.BubbleImages[0], ID)
        {
            collider = new CircleCollider(this, 24);
            slimus = _slimus;
            velocity = slimus.getSlimeVelocity()*2;
            images = loader.BubbleImages;

            dirX = slimus.getLastSlimeDirX();
            dirY = slimus.getLastSlimeDirY();
            direction = new Direction(dirX, dirY);
            if (direction == null || (direction.X == 0 && direction.Y == 0))
                direction.X = 1;

            animator = new simpleAnimator(this, images, new int[] { 0, 1, 2 });
        }

        public override void update()
        {
            if(popped == 0)
            {
                physics.addMove(new PlayMovement() { obj = this, dir = direction, speed = velocity });
            }
            else if(popped >= 99)
            {
                Console.WriteLine("removed bubble");
                physics.removeFromGame(this);
                physics.clearBuffer(this);
            }
            else if(popped >= 1)
            {

                
                image = animator.Animate(popped);
                popped += 3;
            }
            
        }

        public override void OnCollisionEnter(Collider otherCollider)
        {
            if ((otherCollider.parent.GetType() == typeof(Wall) || (otherCollider.parent.GetType() == typeof(ToxicSlime) || otherCollider.parent.GetType() == typeof(Door)) && popped == 0))
            {
                popped = 1;
                if (otherCollider.parent.GetType() == typeof(ToxicSlime))
                {
                    ((ToxicSlime)otherCollider.parent).loseLife();
                    audio.addSound(soundHit);
                }
                else
                {
                    audio.addSound(soundHitWall);
                }
                collider = null;
            }

        }

    }
}
