using System;
using System.Drawing;
using System.Windows.Forms;
/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System.Collections.Generic;
using Explorus.Controller;
using Explorus.Model.GameObjects.Rigidbodies;
using System.Drawing;
using Explorus.Threads;

namespace Explorus.Model
{
    public class ToxicSlime : RigidBody
    {
        private Animator animator;
        private Image image;

        private int last_slimeDirX = 0;
        private int last_slimeDirY = 0;

<<<<<<< Updated upstream
        private Dictionary<int, Image> states;
=======
        private PhysicsThread physics = PhysicsThread.GetInstance();

        private Direction direction = new Direction(0, 1);

        private int slimeVelocity = 1;

        private static readonly Dictionary<int, Image> states = new Dictionary<int, Image>()
        {
            {1, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {2, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(96, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {3, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(192, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {11, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(288, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {12, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(384, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {13, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 288, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {21, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(0, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {22, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(96, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {23, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(192, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {31, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(288, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {32, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(384, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
            {33, new Bitmap("./Resources/TilesSheet.png").Clone(new Rectangle(480, 384, 96, 96), new Bitmap("./Resources/TilesSheet.png").PixelFormat)},
        };
>>>>>>> Stashed changes

        //Map map = Map.GetInstance();
        public ToxicSlime(Point pos, ImageLoader loader, int ID) : base(pos, loader.ToxicSlimeImage, ID)
        {
            int[] order = { 2, 3, 2, 1 };
            collider = new CircleCollider(this, 39);
            animator = new directionAnimator(states, order);
            states = loader.ToxicSlimeImages;

        }
        private void SetImage()
        {
            int progress = (int)(position.X % 96.0 + position.Y % 96.0);
            image = animator.Animate(progress, last_slimeDirX, last_slimeDirY);
        }

        public override void update()
        {
            objectTypes[,] gridMap = Map.Instance.GetTypeMap();

            objectTypes nextGrid = gridMap[gridPosition.X + direction.X, gridPosition.Y + direction.Y];

            Point newPosition = GetPosition();

            GameMaster gameMaster = GameMaster.GetInstance();
            Map oMap = Map.Instance;
            List<GameObject> compoundGameObjectList = Map.Instance.GetCompoundGameObject().getComponentGameObjetList();

            if (direction.X != 0 || direction.Y != 0)
            {
                if (nextGrid != objectTypes.Wall && nextGrid != objectTypes.Door) //Collision
                {
                    if (direction.X + direction.Y > 0 && position.X >= (gridPosition.X + direction.X) * 96 && position.Y >= (gridPosition.Y + direction.Y) * 96)
                    {
                        gridPosition.X += direction.X;
                        gridPosition.Y += direction.Y;
                    }
                    else if (direction.X + direction.Y < 0 && position.X <= (gridPosition.X + direction.X) * 96 && position.Y <= (gridPosition.Y + direction.Y) * 96)
                    {
                        gridPosition.X += direction.X;
                        gridPosition.Y += direction.Y;
                    }
                    else
                        physics.addMove(new PlayMovement() { obj = this, dir = direction, speed = slimeVelocity });
                }
                else
                {
                    newRndDir();
                }
                SetImage();
            }
        }

        public void newRndDir()
        {
            Random rnd = new Random();
            int newDirID = rnd.Next(0, 3);
            Direction newDir = new Direction(0, 0);
            switch (newDirID)
            {
                case 0:
                    newDir = new Direction(0, 1);
                    break;
                case 1:
                    newDir = new Direction(1, 0);
                    break;
                case 2:
                    newDir = new Direction(0, -1);
                    break;
                case 3:
                    newDir = new Direction(-1, 0);
                    break;
            }
            direction = newDir;
        }

        public void invertDir()
        {
            direction.X = direction.X * -1;
            direction.Y = direction.Y * -1;
        }

        public override void OnCollisionEnter(Collider otherCollider)
        {
            if(otherCollider.parent.GetType() == typeof(ToxicSlime))
            {
                ((ToxicSlime)otherCollider.parent).invertDir();
                this.invertDir();

            }
        }
    }
}
