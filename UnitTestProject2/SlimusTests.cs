using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Explorus;
using System.Drawing;
using System.Windows.Forms;


namespace ExplorusTests

{
    [TestClass]
    public class SlimusTests
    {
        private Slimus player = new Slimus(new Point(96, 96));
        private Map map = Map.GetInstance();

        [TestMethod]
        public void CreateSlimusPosition()
        {
            Assert.AreEqual(new Point(96, 96), (player.GetPosition()));
        }

        [TestMethod]
        public void MoveSlimusNowhere()
        {
            player.processInput();
            Assert.AreEqual(0, player.SlimeDirX);
            Assert.AreEqual(0, player.SlimeDirY);
        }

        [TestMethod]
        public void MoveSlimusLeft()
        {
            player.SetCurrentInput(Keys.Left);
            player.processInput();
            Assert.AreEqual(-1, player.SlimeDirX);
            Assert.AreEqual(0, player.SlimeDirY);
        }

        [TestMethod]
        public void MoveSlimusRight()
        {
            player.SetCurrentInput(Keys.Right);
            player.processInput();
            Assert.AreEqual(1, player.SlimeDirX);
            Assert.AreEqual(0, player.SlimeDirY);
        }

        [TestMethod]
        public void MoveSlimusUp()
        {
            player.SetCurrentInput(Keys.Up);
            player.processInput();
            Assert.AreEqual(0, player.SlimeDirX);
            Assert.AreEqual(-1, player.SlimeDirY);
        }

        [TestMethod]
        public void MoveSlimusDown()
        {
            player.SetCurrentInput(Keys.Down);
            player.processInput();
            Assert.AreEqual(0, player.SlimeDirX);
            Assert.AreEqual(1, player.SlimeDirY);
        }

        [TestMethod]
        public void GridPositionChange()
        {
            for (int i = 0; i < 96; i++)
            {
                player.SetCurrentInput(Keys.Right);
                player.processInput();
                player.update();
            }
            Assert.AreEqual(new Point(2, 1), player.GetGridPosition());
        }

        [TestMethod]
        public void PositionChange()
        {
            for (int i = 0; i < 96*3; i++)
            {
                player.SetCurrentInput(Keys.Right);
                player.processInput();
                player.update();
            }
            Assert.AreEqual(new Point(384, 96), player.GetPosition());
        }

        [TestMethod]
        public void WallCollision()
        {
            player.SetCurrentInput(Keys.Left);
            player.processInput();
            player.update();
            
            Assert.AreEqual(new Point(96, 96), player.GetPosition()); //Position stays the same since there is an obstacle
        }

    }
}
