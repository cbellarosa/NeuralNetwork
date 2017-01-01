using com.bellarosa.ia.neuronalnetwork.image;
using System;
using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork.number.image
{
    /// <summary>
    /// Trim useless data from image raster
    /// </summary>
    public class ImageDataTrimNeuron : SimpleNeuron
    {
        #region Constants
        private const int EmptyDataColor = 255;
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

            int pixelIndex = 0;
            while (byteTable[pixelIndex] == ImageDataTrimNeuron.EmptyDataColor && pixelIndex < byteTableLength) { pixelIndex++; }
            int minRowEmpty = pixelIndex / imageWidth;
            int minColumnEmpty = pixelIndex % imageWidth;

            pixelIndex = byteTableLength - 1;
            while (byteTable[pixelIndex] == ImageDataTrimNeuron.EmptyDataColor && pixelIndex >= 0) { pixelIndex--; }
            int maxRowEmpty = pixelIndex / imageWidth;
            int maxColumnEmpty = pixelIndex % imageWidth;

            for (int row = minRowEmpty; row <= maxRowEmpty; row++)
            {
                pixelIndex = row * imageWidth;
                int currentIndex = 0;
                while (byteTable[pixelIndex + currentIndex] == ImageDataTrimNeuron.EmptyDataColor && currentIndex < minColumnEmpty) { currentIndex++; }
                if (currentIndex < minColumnEmpty)
                {
                    minColumnEmpty = currentIndex;
                }

                currentIndex = imageWidth - 1;
                while (byteTable[pixelIndex + currentIndex] == ImageDataTrimNeuron.EmptyDataColor && currentIndex > maxColumnEmpty) { currentIndex--; }
                if (currentIndex > maxColumnEmpty)
                {
                    maxColumnEmpty = currentIndex;
                }
            }

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
    }
}
 