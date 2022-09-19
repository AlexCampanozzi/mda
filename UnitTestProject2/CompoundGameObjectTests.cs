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

namespace ExplorusTests

{
    [TestClass]
    public class CompoundGameObjectTest
    {
        CompoundGameObject cp = new CompoundGameObject();
        Slimus player = new Slimus(new Point(96, 768));
        private Map map = Map.GetInstance();
        private GameMaster gameMaster = GameMaster.GetInstance();

        [TestMethod]
        public void Add()
        {
            cp.add(player, 0, 0);
            
            Assert.AreEqual(cp.getComponentGameObjetList().Count, 1);
        }

        [TestMethod]
        public void Remove()
        {
            cp.remove(player);
            Assert.AreEqual(cp.getComponentGameObjetList().Count, 0);
        }

        [TestMethod]
        public void ProcessInput()
        {
            cp.add(player, 0, 0);
            player.SetCurrentInput(Keys.Up);
            cp.processInput();
            Assert.AreEqual(0, player.SlimeDirX);
            Assert.AreEqual(-1, player.SlimeDirY);
        }

        [TestMethod]
        public void Update()
        {
            player.SetCurrentInput(Keys.Right);
            cp.processInput();
            cp.update(Keys.Right);
            cp.processInput();

            for (int i = 0; i < 10; i++)
            {
                cp.update(Keys.Right);
                cp.processInput();
            }
            Assert.AreEqual(player.GetPosition(), new Point(96, 768));
        }
    }
}
