using com.bellarosa.ia.neuronalnetwork.image;
using System;
using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork.number.image
{
    /// <summary>
    /// Trim useless data from image raster
    /// </summary>
    public class ImageDataCroppingNeuron : SimpleNeuron
    {
        #region Constants
        private const int EmptyDataColor = 255;
        private const float maxDelta = 6.9F;
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
            int imageWidth = imageData.Width;
            int imageHeight = imageData.Height;
            int byteTableLength = byteTable.Length;
            if (byteTableLength != imageWidth * imageHeight)
            {
                String errorMessage = String.Format("Invalid size for {0:s}", byteTable);
                log.Error(errorMessage);
                throw new ArgumentException();
            }

            byte[] nbNonEmptyPixelsByColumn = new byte[imageWidth];
            byte[] nbNonEmptyPixelsByRow = new byte[imageHeight];
            int pixelIndex = 0;
            for (int row = 0; row < imageHeight; row++)
            {
                for (int column = 0; column < imageWidth; column++)
                {
                    byte isNonEmptyPixel = (byteTable[pixelIndex++] == ImageDataCroppingNeuron.EmptyDataColor) ? (byte)0 : (byte)1;
                    nbNonEmptyPixelsByColumn[column] += isNonEmptyPixel;
                    nbNonEmptyPixelsByRow[row] += isNonEmptyPixel;
                }
            }

            int minRowEmpty = this.getMinimumNonEmpty(nbNonEmptyPixelsByRow);
            int maxRowEmpty = this.getMaximumNonEmpty(nbNonEmptyPixelsByRow);
            int minColumnEmpty = this.getMinimumNonEmpty(nbNonEmptyPixelsByColumn);
            int maxColumnEmpty = this.getMaximumNonEmpty(nbNonEmptyPixelsByColumn);

            byte[] trimedByteTable = new byte[(maxColumnEmpty - minColumnEmpty + 1) * (maxRowEmpty - minRowEmpty + 1)];
            pixelIndex = 0;
            for (int row = minRowEmpty; row <= maxRowEmpty; row++)
            {
                for (int column = minColumnEmpty; column <= maxColumnEmpty; column++)
                {
                    trimedByteTable[pixelIndex++] = byteTable[row * imageWidth + column];
                }
            }

            return new ImageData(trimedByteTable, maxColumnEmpty - minColumnEmpty + 1, maxRowEmpty - minRowEmpty + 1);
        }
        #endregion

        #region Private Methods
        private int getMinimumNonEmpty(byte[] nbNonEmptyPixelsArray)
        {
            for (int index = 0; index < nbNonEmptyPixelsArray.Length - 1; index++)
            {
                if (nbNonEmptyPixelsArray[index] > 0 && nbNonEmptyPixelsArray[index + 1] / nbNonEmptyPixelsArray[index] < maxDelta)
                {
                    return index;
                }
            }
            return 0;
        }

        private int getMaximumNonEmpty(byte[] nbNonEmptyPixelsArray)
        {
            for (int index = nbNonEmptyPixelsArray.Length - 1; index > 0; index--)
            {
                if (nbNonEmptyPixelsArray[index] > 0 && nbNonEmptyPixelsArray[index - 1] / nbNonEmptyPixelsArray[index] < maxDelta)
                {
                    return index;
                }
            }
            return nbNonEmptyPixelsArray.Length - 1;
        }
        #endregion
    }
}
 