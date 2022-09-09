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
using Explorus.Model;

namespace ExplorusTests

{
    [TestClass]
    public class SlimusTests
    {
        private Slimus player = new Slimus(new Point(480, 768));
        private Map map = Map.GetInstance();

        [TestMethod]
        public void CreateSlimusPosition()
        {
            Assert.AreEqual(new Point(480, 768), (player.GetPosition()));
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

    }
}
