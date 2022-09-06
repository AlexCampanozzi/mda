using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Explorus;
using System.Drawing;
using System.Windows.Forms;


namespace ExplorusTests

{
    [TestClass]
    public class GameEngineTests
    {
        GameEngine engine = new GameEngine();
        [TestMethod]
        public void GamePause()
        {
            if(engine.GetCurrentGameState() == Explorus.GameState.Paused)
            {
                engine = null;
            }
            Assert.AreEqual(Explorus.GameState.Paused, engine.GetCurrentGameState());
        }

    }
}
