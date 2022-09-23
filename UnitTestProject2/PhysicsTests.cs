/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

/*********************************
 * Note pour exécuter les tests, retirer les conditions gameState = play
 * des méthodes AddMove (68) et removeFromGame (170) du physicsThread
 * On a pas trouvé de façon de contourner la condition, et si on l'enlève
 * les toxic slimes se téléportent lorsque qu'on fait pause
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Explorus;
using System.Drawing;
using System.Windows.Forms;
using Explorus.Controller;
using Explorus.Model;
using Explorus.Threads;
using System.Net.NetworkInformation;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Threading.Tasks;

namespace ExplorusTests

{
    [TestClass]
    public class PhysicsTests
    {
        GameEngine engine = new GameEngine();
        public PhysicsThread physics = PhysicsThread.GetInstance();
        public Map oMap = Map.Instance;
        ImageLoader loader = new ImageLoader();

        [TestInitialize]
        public void testInitialize()
        {
            engine.Start();
        }
        [TestCleanup]
        public void testClean()
        {
            engine.Stop();
        }
        
        /* physics methods:
         * getInstance: not null
         * getBuffer: empty at init, Count=1 ater valid add, empty after remove
         * addMove: PlayMovement: null obj, null/0 dir, 0/negative speed; 
         * - add every Directions
         * clearBuffer: null obj, clear with Slimus, 
         * clear with toxic, clear with Bubble, clear while empty
         * moveObject (private) : check position after valid movement with wall and others
         * checkCollision (private): add Slimus-toxic/toxic-Bubble/Slimus-gem
         * add Slimus-wall/toxic-wall/Bubble-wall
         */
        [TestMethod]
        public void threadInstance()
        {
            Assert.AreNotEqual(null, physics);
        }

        [TestMethod]
        public void EmptyThread ()
        {
            Assert.AreEqual(0, (physics.getBuffer().Count));
        }

        [TestMethod]
        public void AddedObjectThread()
        {
            int x = 12;
            int y = 15;

            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());

            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(1, (physics.getBuffer().Count));
            physics.clearBuffer(entity);
        }

        [TestMethod]
        public void addNull()
        {
            int x = 12;
            int y = 15;

            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());

            physics.addMove(new PlayMovement() { obj = null, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(0, (physics.getBuffer().Count));

            physics.addMove(new PlayMovement() { obj = entity, dir = null, speed = 2 });
            Assert.AreEqual(0, (physics.getBuffer().Count));
        }
        [TestMethod]
        public void addInvalidDir()
        {
            int x = 12;
            int y = 15;

            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());

            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(0, 0), speed = 2 });
            Assert.AreEqual(0, (physics.getBuffer().Count));
            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(3, 0), speed = 2 });
            Assert.AreEqual(0, (physics.getBuffer().Count));
            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(-2, 0), speed = 2 });
            Assert.AreEqual(0, (physics.getBuffer().Count));
            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(0, -4), speed = 2 });
            Assert.AreEqual(0, (physics.getBuffer().Count));
        }

        [TestMethod]
        public void addAllDir()
        {
            int x = 12;
            int y = 15;

            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());


            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(1, (physics.getBuffer().Count));
            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(-1, 0), speed = 2 });
            Assert.AreEqual(2, (physics.getBuffer().Count));
            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(0, 1), speed = 2 });
            Assert.AreEqual(3, (physics.getBuffer().Count));
            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(0, -1), speed = 2 });
            Assert.AreEqual(4, (physics.getBuffer().Count));
            physics.clearBuffer(entity);
        }
        [TestMethod]
        public void addInvalidSpeed()
        {
            int x = 12;
            int y = 15;

            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());

            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(1, 0), speed = -1 });
            Assert.AreEqual(0, (physics.getBuffer().Count));
            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(1, 0), speed = 0 });
            Assert.AreEqual(0, (physics.getBuffer().Count));

        }
        [TestMethod]
        public void addtypesobjects()
        {
            int x = 12;
            int y = 15;

            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());
            RigidBody entity2 = new ToxicSlime(new Point(x * 96, (y - 1) * 96), loader, oMap.getID());
            RigidBody entity3 = new Bubble(new Point(x * 96, (y) * 96), loader, oMap.getID(), new Slimus(new Point(x * 96, y * 96), loader, oMap.getID()));

            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(1, (physics.getBuffer().Count));
            physics.addMove(new PlayMovement() { obj = entity2, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(2, (physics.getBuffer().Count));
            physics.addMove(new PlayMovement() { obj = entity3, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(3, (physics.getBuffer().Count));
            physics.clearBuffer(entity);
            physics.clearBuffer(entity2);
            physics.clearBuffer(entity3);
        }

        [TestMethod]
        public void cleartypesbuffer()
        {
            int x = 12;
            int y = 15;

            Slimus entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());
            RigidBody entity2 = new ToxicSlime(new Point(x * 96, (y - 1) * 96), loader, oMap.getID());
            RigidBody entity3 = new Bubble(new Point(x * 96, (y) * 96), loader, oMap.getID(), entity);

            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(1, (physics.getBuffer().Count));
            physics.addMove(new PlayMovement() { obj = entity2, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(2, (physics.getBuffer().Count));
            physics.addMove(new PlayMovement() { obj = entity3, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(3, (physics.getBuffer().Count));

            physics.clearBuffer(entity3);
            Assert.AreEqual(2, (physics.getBuffer().Count));
            physics.clearBuffer(entity);
            Assert.AreEqual(1, (physics.getBuffer().Count));
            physics.clearBuffer(entity2);
            Assert.AreEqual(0, (physics.getBuffer().Count));

        }

        [TestMethod]
        public void clearemptybuffer()
        {
            int x = 12;
            int y = 15;
            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());
            physics.clearBuffer(entity);
            Assert.AreEqual(0, (physics.getBuffer().Count));
        }
        [TestMethod]
        public void clearnullbuffer()
        {
            int x = 12;
            int y = 15;
            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());

            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(1, (physics.getBuffer().Count));
            physics.clearBuffer(null);
            Assert.AreEqual(1, (physics.getBuffer().Count));
            physics.clearBuffer(entity);
        }
        



    }
}
