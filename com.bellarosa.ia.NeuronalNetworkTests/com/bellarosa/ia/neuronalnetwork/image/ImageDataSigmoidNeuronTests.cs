using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net.Config;
using System.Collections.Generic;
using System;

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
            byte[] testBytetable = new byte[] { 255, 203, 0, 2, 12, 0, 25, 0, 89, 242, 189, 255, 128, 0, 0, 0 };
            byte[] referenceBytetable = new byte[] { 255, 255, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0 };
            byte[] neuronResult = (byte[])new ImageDataSigmoidNeuron().process(new Dictionary<int, object> { { 1, testBytetable } });
            CollectionAssert.AreEqual(referenceBytetable, neuronResult, "Test floor");
        }

        [TestMethod()]
        public void processTestSame()
        {
            byte[] testBytetable = new byte[] { 255, 255, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0 };
            byte[] referenceBytetable = new byte[] { 255, 255, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0 };
            byte[] neuronResult = (byte[])new ImageDataSigmoidNeuron().process(new Dictionary<int, object> { { 1, testBytetable } });
            CollectionAssert.AreEqual(referenceBytetable, neuronResult, "Test same");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void processTestArgumentNull()
        {
            new ImageDataSigmoidNeuron().process(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void processTestArgumentDataNull()
        {
            byte[] nullByteArray = null;
            new ImageDataSigmoidNeuron().process(new Dictionary<int, object> { { 1, nullByteArray } });
        }
        #endregion
    }
}