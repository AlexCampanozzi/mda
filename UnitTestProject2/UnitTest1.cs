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
        private Slimus player = new Slimus(new Point(10, 15));

        [TestMethod]
        public void CreateSlimusPosition()
        {
            Assert.AreEqual(new Point(10, 15), (player.GetPosition()));
        }

        [TestMethod]
        public void MoveSlimus()
        {
            player.SetCurrentInput(Keys.Left);
        }
    }
}
