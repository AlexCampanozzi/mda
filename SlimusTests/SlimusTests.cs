using Explorus;
using System;
using System.Drawing;
//using System.Windows.Forms;

namespace SlimusTests
{
    [TestClass]
    public class SlimusTests
    {
        [TestMethod]
        public void CreateSlimus()
        {
            Point initial = new(10, 15);
            Slimus player = new(initial);
            //Console.WriteLine(player.GetPosition());
            //Assert.AreEqual(initial, (player.GetPosition()));

        }

        [TestMethod]
        public void CheckPosition()
        {

        }
    }
}