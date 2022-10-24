using Explorus.Controller;
using Explorus.Model;
using Explorus.Threads;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using Explorus;

namespace ExplorusTests3
{
    /// <summary>
    /// Description résumée pour ControllerTests
    /// </summary>
    [TestClass]
    public class ControllerTests
    {
        //IGameEngine ge;
        GameEngine engine = new GameEngine();
        PhysicsThread physics = PhysicsThread.GetInstance();
        ImageLoader loader = new ImageLoader();
        Slimus slimus;

        [TestInitialize()]
        public void StartGame()
        {
            engine = GameEngine.GetInstance();
            engine.levelState = new LevelState(engine);
            Map map = Map.Instance;
            Assert.IsNotNull(map); 
            slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));

            engine.Start(true);
            engine.levelState.chosenLevel = "test.png";
            engine.processInput(Keys.Space);
            Assert.AreEqual("Play", engine.GetState().Name());
        }

        [TestCleanup()]
        public void StopGame()
        {
            //ge.Stop();
            //physicsThread.Abort();
        }

        [TestMethod]
        public void moveDown()
        {
            slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));

            Point originalPos = slimus.GetGridPosition();
            Trace.WriteLine(originalPos);
            Point goal = originalPos;
            goal.Y += 1;
            bool same_pos = true;
            GameView oview = GameView.Instance;
            oview.inputSubscription(Keys.Down);
            while (same_pos)
            {
                slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));
                if (slimus.GetGridPosition() != originalPos)
                {
                    same_pos = false;
                }
                Thread.Sleep(100);
            }
            Assert.AreEqual(goal, slimus.GetGridPosition());
        }

        [TestMethod]
        public void moveUp()
        {
            slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));
            Point originalPos = slimus.GetGridPosition();
            Trace.WriteLine(originalPos);
            Point goal = originalPos;
            goal.Y -= 1;
            bool same_pos = true;
            GameView oview = GameView.Instance;
            oview.inputSubscription(Keys.Up);
            while (same_pos)
            {
                slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));
                if (slimus.GetGridPosition() != originalPos)
                {
                    same_pos = false;
                }
                Thread.Sleep(100);
            }
            Assert.AreEqual(goal, slimus.GetGridPosition());
        }

        [TestMethod]
        public void moveLeft()
        {
            slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));

            Point originalPos = slimus.GetGridPosition();
            Trace.WriteLine(originalPos);
            Point goal = originalPos;
            goal.X -= 1;
            bool same_pos = true;
            GameView oview = GameView.Instance;
            oview.inputSubscription(Keys.Left);
            while (same_pos)
            {
                slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));
                if (slimus.GetGridPosition() != originalPos)
                {
                    same_pos = false;
                }
                Thread.Sleep(100);
            }
            Assert.AreEqual(goal, slimus.GetGridPosition());
        }

        [TestMethod]
        public void moveRight()
        {
            slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));

            Point originalPos = slimus.GetGridPosition();
            Trace.WriteLine(originalPos);
            Point goal = originalPos;
            goal.X += 1;
            bool same_pos = true;
            GameView oview = GameView.Instance;
            oview.inputSubscription(Keys.Right);
            while (same_pos)
            {
                slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));
                if (slimus.GetGridPosition() != originalPos)
                {
                    same_pos = false;
                }
                Thread.Sleep(100);
            }
            Assert.AreEqual(goal, slimus.GetGridPosition());
        }

        [TestMethod]
        public void wallCollision()
        {
            slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));

            Point originalPos = slimus.GetGridPosition();
            Point goal = originalPos;
            goal.X -= 1;
            bool same_pos = true;
            GameView oview = GameView.Instance;
            oview.inputSubscription(Keys.Left);
            while (same_pos)
            {
                slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));
                if (slimus.GetGridPosition() != originalPos)
                {
                    same_pos = false;
                }
                Thread.Sleep(100);
            }
            Assert.AreEqual(goal, slimus.GetGridPosition());
            // slimus now next to wall in test.png
            objectTypes[,] gridMap = Map.Instance.GetTypeMap();
            Assert.AreEqual(objectTypes.Wall, gridMap[goal.X - 1, goal.Y]);
            oview.inputSubscription(Keys.Left);
            Thread.Sleep(1000);

            Assert.AreEqual(goal, slimus.GetGridPosition());
        }

        [TestMethod]
        public void gemCollision()
        {
            slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));
            Point originalPos = slimus.GetGridPosition();
            Point goal1 = originalPos;
            goal1.X += 1;
            Point goal2 = originalPos;
            goal2.X += 2;
            bool same_pos = true;
            // check for gem
            objectTypes[,] gridMap = Map.Instance.GetTypeMap();
            Assert.AreEqual(objectTypes.Gem, gridMap[goal2.X, goal2.Y]);

            GameView oview = GameView.Instance;
            oview.inputSubscription(Keys.Right);
            while (same_pos)
            {
                slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));
                if (slimus.GetGridPosition() != originalPos)
                {
                    same_pos = false;
                }
                Thread.Sleep(100);
            }
            Assert.AreEqual(goal1, slimus.GetGridPosition());
            // second movement
            same_pos = true;
            oview.inputSubscription(Keys.Right);
            while (same_pos)
            {
                slimus = (Slimus)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Slimus));
                if (slimus.GetGridPosition() != goal1)
                {
                    same_pos = false;
                }
                Thread.Sleep(100);
            }
            Assert.AreEqual(goal2, slimus.GetGridPosition());
            // check if gem removed
            Collectable gem = (Collectable)Map.Instance.GetObjectList().Find(obj => obj.GetCollider().parent.GetType() == typeof(Gem));
            //16 because there is 6 toxic slime and gems
            Assert.AreEqual(16,GameMaster.Instance.getGemStatus());
            
        }
    }
}

