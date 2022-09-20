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


namespace ExplorusTests

{
    [TestClass]
    public class ThreadTests
    {
        GameEngine engine = new GameEngine();
        public PhysicsThread physics = PhysicsThread.GetInstance();

        [TestMethod]
        public void EmptyThread ()
        {
            Assert.AreEqual(0, (physics.getBuffer().Count));
        }

        [TestMethod]
        public void AddedObjectThread()
        {
            engine.Start();
            ImageLoader loader = new ImageLoader();
            
            int x = 12;
            int y = 15;

            physics.addMove(new PlayMovement() { obj = new Slimus(new Point(x * 96, y * 96), loader, Map.Instance.getID()), dir = new Direction(1,0), speed = 2 });
            Assert.AreEqual(1, (physics.getBuffer().Count));
            engine.Stop();
        }

        [TestMethod]
        public void RemoveObjectThread()
        {
            engine.Start();
            ImageLoader loader = new ImageLoader();

            int x = 12;
            int y = 15;

            RigidBody entity = new Slimus(new Point(x * 96, y * 96), loader, Map.Instance.getID());

            physics.addMove(new PlayMovement() { obj = entity, dir = new Direction(1, 0), speed = 2 });
            physics.removeFromGame(entity);
            Assert.AreEqual(0, (physics.getBuffer().Count));
            engine.Stop();
        }



    }
}
