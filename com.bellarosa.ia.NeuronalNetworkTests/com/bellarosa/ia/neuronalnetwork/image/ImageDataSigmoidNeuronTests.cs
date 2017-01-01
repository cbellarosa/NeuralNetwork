using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net.Config;
using System.Collections.Generic;
using System;
using com.bellarosa.ia.neuronalnetwork.image;

namespace com.bellarosa.ia.neuronalnetwork.number.image.Tests
{
    [TestClass()]
    public class ImageDataSigmoidNeuronTests
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
        public void processTestFloor()
        {
            ImageData testImageData = new ImageData(new byte[] { 255, 203, 0, 2, 12, 0, 25, 0, 89, 242, 189, 255, 128, 0, 0, 0 }, 4, 4);
            ImageData referenceImageData = new ImageData(new byte[] { 255, 255, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0 }, 4, 4);
            object neuronResult = new ImageDataSigmoidNeuron().process(new Dictionary<int, object> { { 1, testImageData } });
            Assert.AreEqual(referenceImageData, neuronResult, "Test floor");
        }

        [TestMethod()]
        public void processTestSame()
        {
            ImageData testImageData = new ImageData(new byte[] { 255, 255, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0 }, 4, 4);
            object neuronResult = new ImageDataSigmoidNeuron().process(new Dictionary<int, object> { { 1, testImageData } });
            Assert.AreEqual(testImageData, neuronResult, "Test same");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void processTestArgumentNull()
        {
            new ImageDataSigmoidNeuron().process(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void processTestArgumentInvalidSize()
        {
            ImageData testInputData = new ImageData(new byte[] {
                0, 255, 255, 255, 0,
                255, 255, 255, 255, 255,
                255, 255, 0, 255, 255,
                255, 255, 255, 255, 255,
                0, 255, 255, 255, 0}, 10, 20);
            new ImageDataSigmoidNeuron().process(new Dictionary<int, object> { { 1, testInputData } });
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void processTestArgumentDataNull()
        {
            ImageData nullDataImage = new ImageData(null, 0, 0);
            new ImageDataSigmoidNeuron().process(new Dictionary<int, object> { { 1, nullDataImage } });
        }
        #endregion
    }
}