using com.bellarosa.ia.neuronalnetwork.image;
using System;
using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork.number.image
{
    /// <summary>
    /// Readjust gray scale to black and white
    /// </summary>
    public class ImageDataSigmoidNeuron : SimpleNeuron
    {
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
            for (int currentByte = 0; currentByte < byteTable.Length; currentByte++)
            {
                byteTable[currentByte] = byteTable[currentByte] < (byte)128 ? (byte)0 : (byte)255;
            }
            return new ImageData(byteTable, imageData.Width, imageData.Height);
        }
        #endregion
    }
}
