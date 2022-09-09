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
        public void SlimusDirGetSet()
        {
            player.SlimeDirX = 1;
            player.SlimeDirY = 2;
            Assert.AreEqual(player.SlimeDirX, 1);
            Assert.AreEqual(player.SlimeDirY, 2);
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
        public void GetSetSlimusImage()
        {
            player.SetCurrentInput(Keys.Down);
            player.processInput();
            Image imgDown = player.GetImage();
            player.SetCurrentInput(Keys.Up);
            player.processInput();
            Image imgUp = player.GetImage();
            player.SetCurrentInput(Keys.Left);
            player.processInput();
            Image imgLeft = player.GetImage();
            player.SetCurrentInput(Keys.Right);
            player.processInput();
            Image imgRight = player.GetImage();

            Assert.AreNotEqual(imgRight.ToString(), imgLeft.ToString());
            Assert.AreNotEqual(imgRight.ToString(), imgUp.ToString());
            Assert.AreNotEqual(imgRight.ToString(), imgDown.ToString());
            Assert.AreNotEqual(imgLeft.ToString(), imgUp.ToString());
            Assert.AreNotEqual(imgLeft.ToString(), imgDown.ToString());
            Assert.AreNotEqual(imgUp.ToString(), imgDown.ToString());
        }
    }
}
