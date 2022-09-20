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
using Explorus.Controller;
/*
namespace ExplorusTests
    
{
    [TestClass]
    public class SlimusTests
    {
        private Slimus player = new Slimus(new Point(480, 768));
        private Map map = Map.GetInstance();
        private GameMaster gameMaster = GameMaster.GetInstance();

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
            player.SetCurrentInput(Keys.None);
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
        public void UpdateSlimus()
        {
            player.SetCurrentInput(Keys.Right);
            player.processInput();
            player.update();

            for (int i = 0; i < 10; i++)
            {
                player.update();
            }
            Assert.AreEqual(player.GetPosition(), new Point(106, 768));
        }
    }
}*/
