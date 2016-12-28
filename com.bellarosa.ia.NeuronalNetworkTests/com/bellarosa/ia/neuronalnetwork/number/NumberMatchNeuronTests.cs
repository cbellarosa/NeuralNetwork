using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net.Config;
using com.bellarosa.ia.neuronalnetwork.image;
using System;

namespace com.bellarosa.ia.neuronalnetwork.number.Tests
{
    [TestClass()]
    public class NumberMatchNeuronTests
    {
        #region Initializer / Cleanup
        [ClassInitialize]
        public static void classInitialize(TestContext testContext)
        {
            XmlConfigurator.Configure();
        }
        #endregion

        #region Tests
        [TestMethod()]
        public void processTestRecognize()
        {
            ImageData testImage = new ImageData(@".\resources\1.bmp");
            NumberMatchNeuron numberMatchNeuron = new NumberMatchNeuron(1);
            numberMatchNeuron.init();
            float neuronResult = (float)numberMatchNeuron.process(new ImageData[] { testImage });
            Assert.AreEqual(1, neuronResult, "Test recognize");
        }

        [TestMethod()]
        public void processTestRecognizeSimilar()
        {
            ImageData testImage = new ImageData(@".\resources\1_like.bmp");
            NumberMatchNeuron numberMatchNeuron = new NumberMatchNeuron(1);
            numberMatchNeuron.init();
            float neuronResult = (float)numberMatchNeuron.process(new ImageData[] { testImage });
            Assert.AreEqual(0.832908154f, neuronResult, "Test recognize");
        }

        [TestMethod()]
        public void processTestNotRecognized()
        {
            ImageData testImage = new ImageData(@".\resources\6.bmp");
            NumberMatchNeuron numberMatchNeuron = new NumberMatchNeuron(2);
            numberMatchNeuron.init();
            float neuronResult = (float)numberMatchNeuron.process(new ImageData[] { testImage });
            Assert.AreEqual(0.8596939f, neuronResult, "Test not recognized");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void processTestArgumentNull()
        {
            NumberMatchNeuron numberMatchNeuron = new NumberMatchNeuron(2);
            numberMatchNeuron.init();
            numberMatchNeuron.process(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void processTestArgumentDataNull()
        {
            byte[] nullByteArray = null;
            ImageData testImage = new ImageData(nullByteArray);
            NumberMatchNeuron numberMatchNeuron = new NumberMatchNeuron(2);
            numberMatchNeuron.init();
            numberMatchNeuron.process(new ImageData[] { testImage });
        }
        #endregion
    }
}