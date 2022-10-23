using Explorus.Model;
using Explorus.Controller;
using Explorus.Threads;
using Explorus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ExplorusTests

{
    [TestClass]
    public class renderTests
    {
        GameEngine engine = new GameEngine();
        public RenderThread render = RenderThread.GetInstance();

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
        // render thread
        [TestMethod]
        public void renderThreadtest()
        {
            Assert.AreNotEqual(null, render);
        }
    }
}
