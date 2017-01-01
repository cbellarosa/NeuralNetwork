using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net.Config;
using System.Collections.Generic;
using System;
using com.bellarosa.ia.neuronalnetwork.image;

namespace com.bellarosa.ia.neuronalnetwork.number.image.Tests
{
    [TestClass()]
    public class ImageDataTrimNeuronTests
    {
        #region Initializer / Cleanup
        [ClassInitialize]
        public static void classInitialize(TestContext testContext)
        {
            XmlConfigurator.Configure();
        }
        #endregion

        #region tests
        [TestMethod()]
        public void processTrimAroundTest()
        {
            ImageData testInputData = new ImageData(new byte[] {
                255, 255, 255, 255, 255,
                255, 255, 255, 255, 255,
                255, 255, 0, 0, 255,
                255, 0, 255, 255, 255,
                255, 255, 255, 255, 255}, 5, 5);
            ImageData referenceData = new ImageData(new byte[] {255,  0, 0, 0, 255, 255}, 3, 2);
            object neuronResult = new ImageDataTrimNeuron().process(new Dictionary<int, object> { { 1, testInputData } });
            Assert.AreEqual(referenceData, neuronResult, "Test Trim Around");
        }

        [TestMethod()]
        public void processNoTrimTest()
        {
            ImageData testInputData = new ImageData(new byte[] {
                0, 255, 255, 255, 0,
                255, 255, 255, 255, 255,
                255, 255, 0, 255, 255,
                255, 255, 255, 255, 255,
                0, 255, 255, 255, 0}, 5, 5);
            object neuronResult = new ImageDataTrimNeuron().process(new Dictionary<int, object> { { 1, testInputData } });
            Assert.AreEqual(testInputData, neuronResult, "Test Trim Around");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void processTestArgumentNull()
        {
            new ImageDataTrimNeuron().process(null);
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
            new ImageDataTrimNeuron().process(new Dictionary<int, object> { { 1, testInputData } });
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void processTestArgumentDataNull()
        {
            ImageData nullDataImage = new ImageData(null, 0, 0);
            new ImageDataTrimNeuron().process(new Dictionary<int, object> { { 1, nullDataImage } });
        }
        #endregion
    }
}