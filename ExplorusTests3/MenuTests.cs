using System;
using System.Windows.Forms;
using Explorus;
using Explorus.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExplorusTests3
{
    [TestClass]
    public class MenuTests
    {
        public IGameEngine ge;


        [TestInitialize()]
        public void StartGame()
        {
            ge = new IGameEngine();

            ge.Start();

        }

        [TestCleanup()]
        public void StopGame()
        {
            ge.Stop();
        }

        //faire rouler les tests un à la fois
            
        [TestMethod]
        public void StartGameOption()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.None);
            Assert.AreEqual(Option.Start, engine.CurrentOption);
        }

        [TestMethod]
        public void UpKeyStartGameOption()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Up);
            Assert.AreEqual(Option.Start, engine.CurrentOption);
        }

        [TestMethod]
        public void AudioOption()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            Assert.AreEqual(Option.Audio, engine.CurrentOption);
        }

        [TestMethod]
        public void LevelOption()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Down);
            Assert.AreEqual(Option.Level, engine.CurrentOption);
        }

        [TestMethod]
        public void ExitOption()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Down);
            Assert.AreEqual(Option.Exit, engine.CurrentOption);
        }

        [TestMethod]
        public void DownKeyExitOption()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Down);
            Assert.AreEqual(Option.Exit, engine.CurrentOption);
        }

        [TestMethod]
        public void ChooseNothingOption()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.None);
            Assert.AreEqual("Start", engine.GetState().Name());
        }

        [TestMethod]
        public void ChooseStartGameOption()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Space);
            Assert.AreEqual("Play", engine.GetState().Name());
        }

        [TestMethod]
        public void ChooseAudioOption()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Space);
            Assert.AreEqual("Audio", engine.GetState().Name());
        }

        [TestMethod]
        public void ChooseLevelOption()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Space);
            Assert.AreEqual("Level", engine.GetState().Name());
        }

        [TestMethod]
        public void ChooseExitOption()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Space);
            Assert.AreEqual("Stop", engine.GetState().Name());
        }

        [TestMethod]
        public void MusicVolumeUp()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Space);
            engine.processInput(Keys.Right);
            Assert.AreEqual(51, engine.AudioState.MusicVolume);
        }

        [TestMethod]
        public void MusicVolumeDown()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Space);
            engine.processInput(Keys.Left);
            Assert.AreEqual(49, engine.AudioState.MusicVolume);
        }
        [TestMethod]
        public void SoundVolumeUp()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Space);
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Right);
            Assert.AreEqual(51, engine.AudioState.SoundVolume);
        }

        [TestMethod]
        public void SoundVolumeDown()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Space);
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Left);
            Assert.AreEqual(49, engine.AudioState.SoundVolume);
        }

        [TestMethod]
        public void MusicVolumeMute()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Space);
            engine.processInput(Keys.M);
            Assert.AreEqual(0, engine.AudioState.MusicVolume);
        }

        [TestMethod]
        public void MusicVolumeUnMute()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Space);
            engine.processInput(Keys.Right);
            engine.processInput(Keys.Right);
            engine.processInput(Keys.M);
            Assert.AreEqual(0, engine.AudioState.MusicVolume);
            engine.processInput(Keys.M);
            Assert.AreEqual(52, engine.AudioState.MusicVolume);

        }

        [TestMethod]
        public void LeaveMusicMenu()
        {
            GameEngine engine = GameEngine.GetInstance();
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Space);
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Down);
            engine.processInput(Keys.Space);
            Assert.AreEqual("Start", engine.GetState().Name());
        }
    }

}
