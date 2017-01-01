using com.bellarosa.ia.neuronalnetwork.image;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace com.bellarosa.ia.neuronalnetwork.number.Tests
{
    [TestClass()]
    public class NumberMatchNetworkTests
    {
        #region Private Attributes
        private static NumberMatchNetwork testNetwork;
        #endregion

        #region Initializer / Cleanup
        [ClassInitialize]
        public static void classInitialize(TestContext testContext)
        {
            XmlConfigurator.Configure();
            testNetwork = new NumberMatchNetwork();
        }
        #endregion

        #region Tests
        [TestMethod()]
        public void processTestRecognize1()
        {
            ImageData testImage = new ImageData(@".\resources\1_like.bmp");
            Assert.AreEqual(1, testNetwork.process(testImage), "Test recognize 1");
        }

        [TestMethod()]
        public void processTestRecognize5()
        {
            ImageData testImage = new ImageData(@".\resources\5.bmp");
            Assert.AreEqual(5, testNetwork.process(testImage), "Test recognize 5");
        }


        [TestMethod()]
        public void processTestRecognize8()
        {
            ImageData testImage = new ImageData(@".\resources\8.bmp");
            Assert.AreEqual(8, testNetwork.process(testImage), "Test recognize 8");
        }

        [TestMethod()]
        public void processTestRecognize9()
        {
            ImageData testImage = new ImageData(@".\resources\9_like.bmp");
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