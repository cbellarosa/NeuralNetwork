using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net.Config;
using System;
using System.Collections.Generic;
using com.bellarosa.ia.neuronalnetwork.image;

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
            ImageData testImageData = new ImageData(new byte[] {
                0,0,0, 0,0,0,0,0, 0,0,0,0,0, 0,
                0,0,0, 0,0,0,0,0, 0,0,0,0,0, 0,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,0,0,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,0,0,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255,
                255,255,255, 255,255,255,0,0, 255,255,255,255,255, 255}, 14, 18);
            NumberMatchNeuron numberMatchNeuron = new NumberMatchNeuron(1);
            numberMatchNeuron.init();
            float neuronResult = (float)numberMatchNeuron.process(new Dictionary<int, object> { { 1, testImageData } });
            Assert.AreEqual(1, neuronResult, "Test recognize");
        }

        [TestMethod()]
        public void processTestNotRecognized()
        {
            ImageData testImageData = new ImageData(new byte[] {
                255,255,0, 0,0,0,0,0, 0,0,0,0,255, 255,
                255,255,0, 0,0,0,0,0, 0,0,0,0,255, 255,
                0,0,255, 255,255,255,255,255, 255,255,255,255,0, 0,
                0,0,255, 255,255,255,255,255, 255,255,255,255,0, 0,
                0,0,255, 255,255,255,255,255, 255,255,255,255,0, 0,
                0,0,255, 255,255,255,255,255, 255,255,255,255,0, 0,
                0,0,255, 255,255,255,255,255, 255,255,255,255,0, 0,
                0,0,255, 255,255,255,255,255, 255,255,255,255,0, 0,
                0,0,0, 0,0,0,0,0, 0,0,0,0,255, 255,
                0,0,0, 0,0,0,0,0, 0,0,0,0,255, 255,
                0,0,255, 255,255,255,255,255, 255,255,255,255,255, 255,
                0,0,255, 255,255,255,255,255, 255,255,255,255,255, 255,
                0,0,255, 255,255,255,255,255, 255,255,255,255,255, 255,
                0,0,255, 255,255,255,255,255, 255,255,255,255,0, 0,
                0,0,255, 255,255,255,255,255, 255,255,255,255,0, 0,
                0,0,255, 255,255,255,255,255, 255,255,255,255,0, 0,
                255,255,0, 0,0,0,0,0, 0,0,0,0,255, 255,
                255,255,0, 0,0,0,0,0, 0,0,0,0,255, 255}, 14, 18);
            NumberMatchNeuron numberMatchNeuron = new NumberMatchNeuron(8);
            numberMatchNeuron.init();
            float neuronResult = (float)numberMatchNeuron.process(new Dictionary<int, object> { { 1, testImageData } });
            Assert.AreEqual(0.960317433f, neuronResult, "Test not recognized");
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
        public void processTestArgumentInvalidSize()
        {
            ImageData testInputData = new ImageData(new byte[] {
                0, 255, 255, 255, 0,
                255, 255, 255, 255, 255,
                255, 255, 0, 255, 255,
                255, 255, 255, 255, 255,
                0, 255, 255, 255, 0}, 10, 20);
            NumberMatchNeuron numberMatchNeuron = new NumberMatchNeuron(2);
            numberMatchNeuron.init();
            numberMatchNeuron.process(new Dictionary<int, object> { { 1, testInputData } });
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void processTestArgumentDataNull()
        {
            ImageData nullDataImage = new ImageData(null, 0, 0);
            NumberMatchNeuron numberMatchNeuron = new NumberMatchNeuron(2);
            numberMatchNeuron.init();
            numberMatchNeuron.process(new Dictionary<int, object> { { 1, nullDataImage } });
        }
        #endregion
    }
}