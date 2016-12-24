using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.bellarosa.ia.neuronalnetwork.image;
using log4net.Config;
using System.Linq;

namespace com.bellarosa.ia.neuronalnetwork.number.image.Tests
{
    [TestClass()]
    public class ImageFilterNeuronTests
    {
        #region Initializer / Cleanup
        [ClassInitialize]
        public static void classInitialize(TestContext testContext)
        {
            XmlConfigurator.Configure();
        }
        #endregion

        [TestMethod()]
        public void processTestFloor()
        {
            ImageData referenceImage = new ImageData(@".\resources\1.bmp");
            ImageData testImage = new ImageData(@".\resources\1_like.bmp");
            byte[] neuronResult = (byte[])new ImageFilterNeuron().process(new ImageData[] { testImage });
            CollectionAssert.AreEqual((byte[])referenceImage.Data, neuronResult, "Test floor");
        }

        [TestMethod()]
        public void processTestSame()
        {
            ImageData referenceImage = new ImageData(@".\resources\1.bmp");
            ImageData testImage = new ImageData(@".\resources\1.bmp");
            byte[] neuronResult = (byte[])new ImageFilterNeuron().process(new ImageData[] { testImage });
            CollectionAssert.AreEqual((byte[])referenceImage.Data, neuronResult, "Test same");
        }
    }
}