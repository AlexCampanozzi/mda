using GEI797Labo.Controllers;
using GEI797Labo.Models;
using GEI797Labo.Models.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Windows.Forms;

namespace GEI797Labo_Test
{
    [TestClass]
    public class UnitTest_Controller
    {
        private const int JOIN_TIMER = 100;     //ms
        private const int PROCESS_DELAY = 20;   //ms

        private GameController oController;
        private IGameModel oModel;
        private Thread oGameLoopThread;


        [TestInitialize]
        public void TestInit()
        {
            if (oGameLoopThread != null && oGameLoopThread.IsAlive)
            {
                StopThread();
            }

            //*****************************************************************************
            // UNIT TEST REQUIREMENT TO SKIP THE WINDOWS FORM SHOW
            //    - SEE the method IsWindowLess in the Controller
            // *****************************************************************************
            oModel = new GameModel();
            oController = new GameController(oModel);
            Assert.IsNotNull(oController);            
            oController.IsWindowLess = true;

            // IMPORTANT! Must start the GameLoop in a thread own by
            //            the Unit Test class to gain access
            oGameLoopThread = new Thread(oController.GameLoop);
            oGameLoopThread.Start();

            Assert.IsTrue(oGameLoopThread.IsAlive);
            Assert.IsFalse(oController.IsGameStopped);            
        }

        [TestCleanup]
        public void TestCleanup()
        {
            StopThread();
            Assert.IsTrue(oController.IsDisposed);
        }

        private void StopThread()
        {
            if (oGameLoopThread.IsAlive)
            {
                oController.StopGame();
                oGameLoopThread.Join(JOIN_TIMER);

                Assert.IsTrue(oController.IsGameStopped);

                if (oGameLoopThread.IsAlive)
                    oGameLoopThread.Abort();
            }
            Assert.IsFalse(oGameLoopThread.IsAlive);
        }

        //*****************************************************************************
        //                       UNIT TEST the Controller
        // *****************************************************************************
        [TestMethod]
        public void TestSpriteMoveUp()
        {
            Shape sprite = oModel.Shapes.GetItem(0);
            Assert.IsNotNull(sprite);

            float actualPosY = sprite.Position_Y;

            // Simulate a KEY_DOWN event
            oController.User_KeyDown(new KeyEventArgs(Keys.Up));

            // Give a little delay to process the key event in the GameLoop
            Thread.Sleep(PROCESS_DELAY);   

            Assert.IsTrue(actualPosY > sprite.Position_Y);
        }

        // Tests TODO :
        //   1. test all directions movement
        //   2. test a collision detection with a rectangle

    }
}

