/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
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


namespace ExplorusTests

{
    [TestClass]
    public class ThreadTests
    {
        GameEngine engine = new GameEngine();
        public PhysicsThread physics = PhysicsThread.GetInstance();
        public Map oMap = Map.Instance;
        ImageLoader loader = new ImageLoader();

        /* physics methods:
         * getInstance: not null
         * getBuffer: empty at init, count=1 ater valid add, empty after remove
         * addMove: playmovement: null obj, null/0 dir, 0/negative speed; 
         * - add every directions
         * clearBuffer: null obj, clear with slimus, 
         * clear with toxic, clear with bubble, clear while empty
         * removeFromGame: remove slimus, toxic, bubble, null
         * moveObject (private) : check position after valid movement with wall and others
         * checkCollision (private): add slimus-toxic/toxic-bubble/slimus-gem
         * add slimus-wall/toxic-wall/bubble-wall
         */
        [TestMethod]
        public void instance()
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
            PlayMovement mvmt = new PlayMovement()
            {
                obj = entity,
                dir = new Direction(1, 0),
                speed = 2
            };
            physics.addMove(mvmt);
            Assert.AreEqual(1, (physics.getBuffer().Count));
        }
        [TestMethod]
        public void RemoveObjectThread()
        {
            int x = 12;
            int y = 15;

            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());

            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(1, 0), speed = 2 });
            physics.removeFromGame(entity);
            Assert.AreEqual(0, (physics.getBuffer().Count));
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
        public void addTypesObjects()
        {
            int x = 12;
            int y = 15;

            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());
            //RigidBody entity2 = new ToxicSlime(new Point(x * 96, (y-1) * 96), loader, oMap.getID());
            //RigidBody entity3 = new Bubble(new Point(x * 96, (y) * 96), loader, oMap.getID(), new Slimus(new Point(x * 96, y * 96), loader, oMap.getID()));

            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(1, (physics.getBuffer().Count));
            //physics.addMove(new PlayMovement() { obj = entity2, dir = new Direction(1, 0), speed = 2 });
            //Assert.AreEqual(2, (physics.getBuffer().Count));
            //physics.addMove(new PlayMovement() { obj = entity3, dir = new Direction(1, 0), speed = 2 });
            //Assert.AreEqual(3, (physics.getBuffer().Count));
        }
        [TestMethod]
        public void clearBuffer()
        {
            int x = 12;
            int y = 15;
            physics.clearBuffer(new Slimus(new Point(x * 96, y * 96), loader, oMap.getID()));
        }

        [TestMethod]
        public void clearTypesBuffer()
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
        public void clearEmptyBuffer()
        {
            int x = 12;
            int y = 15;
            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());
            physics.clearBuffer(entity);
            Assert.AreEqual(0, (physics.getBuffer().Count));
        }
        [TestMethod]
        public void clearNullBuffer()
        {
            int x = 12;
            int y = 15;
            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, oMap.getID());

            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(1, 0), speed = 2 });
            Assert.AreEqual(1, (physics.getBuffer().Count));
            physics.clearBuffer(null);
            Assert.AreEqual(1, (physics.getBuffer().Count));
        }
        [TestMethod]
        public void removeGame()
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

            physics.removeFromGame(null);
            Assert.AreEqual(3, (physics.getBuffer().Count));
            physics.removeFromGame(entity);
            Assert.AreEqual(2, (physics.getBuffer().Count));
            physics.removeFromGame(entity2);
            Assert.AreEqual(1, (physics.getBuffer().Count));
            physics.removeFromGame(entity3);
            Assert.AreEqual(0, (physics.getBuffer().Count));
        }

        //moveObject(private) : check position after valid movement with wall and others
        //* checkCollision(private): add slimus-toxic/toxic-bubble/slimus-gem
        //* add slimus-wall/toxic-wall/bubble-wall
       


        [TestInitialize]
        public void ClassInitialize()
        {
            engine.Start();
        }

        [TestCleanup]
        public void ClassCleanup()
        {
            engine.Stop();
        }
    }
}
