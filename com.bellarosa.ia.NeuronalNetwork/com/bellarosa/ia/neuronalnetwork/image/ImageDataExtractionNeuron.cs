using com.bellarosa.ia.neuronalnetwork.image;
using System;
using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork.number.image
{
    /// <summary>
    /// Extract usefull byte of data from image raster and put to grayscale
    /// </summary>
    public class ImageDataExtractionNeuron : SimpleNeuron
    {
        #region Constants
        public const int NonSignificantByteNumber = 54;
        private const int NumberOfColors = 3;
        #endregion

        #region Public Methods
        public override object process(IDictionary<int, object> data)
        {
            if (data == null || data.Count == 0 || data[1] == null || !data[1].GetType().IsAssignableFrom(typeof(ImageData)))
            {
                throw new ArgumentException(data == null ? "Data is null" : data.ToString());
            }

            ImageData imageData = (ImageData)data[1];
            if (imageData.Data == null)
            {
                throw new ArgumentException(imageData.ToString());
            }

            byte[] byteTable = (byte[])imageData.Data;
            int numberOfPixel = (byteTable.Length - NonSignificantByteNumber) / NumberOfColors;
            byte[] extractedTable = new byte[numberOfPixel];
            int currentPixel = 0;
            for (int currentByte = NonSignificantByteNumber; currentByte < byteTable.Length; currentByte += NumberOfColors)
            {
                byte redColor = byteTable[currentByte];
                byte greenColor = byteTable[currentByte + 1];
                byte blueColor = byteTable[currentByte + 2];
                extractedTable[currentPixel++] = (byte)((float)redColor * 0.3 + (float)greenColor * 0.59 + (float)blueColor * 0.11);
            }
            return new ImageData(extractedTable, imageData.Width, imageData.Height);
        }
        #endregion
    }
}
