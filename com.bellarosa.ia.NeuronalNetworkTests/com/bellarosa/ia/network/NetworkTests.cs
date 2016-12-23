using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace com.bellarosa.ia.network.Tests
{
    [TestClass()]
    public class NetworkTests
    {
        #region Private Attributes
        private static Network testNetwork;
        #endregion

        #region Initializer / Cleanup
        [AssemblyInitialize]
        public static void assemblyInitialize(TestContext testContext)
        {
            XmlConfigurator.Configure();
            testNetwork = new Network();
        }
        #endregion

        #region Tests
        [TestMethod()]
        public void processTestRecognize1()
        {
            ImageData testImage = new ImageData(@".\resources\1.bmp");
            Assert.AreEqual(1, testNetwork.process(testImage), "Test recognize 1");
        }

        [TestMethod()]
        public void processTestRecognize5()
        {
            ImageData testImage = new ImageData(@".\resources\5.bmp");
            Assert.AreEqual(5, testNetwork.process(testImage), "Test recognize 5");
        }

        [TestMethod()]
        public void processTestRecognize9()
        {
            ImageData testImage = new ImageData(@".\resources\9.bmp");
            Assert.AreEqual(9, testNetwork.process(testImage), "Test recognize 9");
        }

        [TestMethod()]
        public void processTestWrong()
        {
            ImageData testImage = new ImageData(@".\resources\wrong.bmp");
            Assert.IsNull(testNetwork.process(testImage), "Test recognize wrong");
        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void processTestInvalid()
        {
            ImageData testImage = new ImageData(@".\resources\wrong1.bmp");
            testNetwork.process(testImage);
        }
        #endregion
    }
}