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
            if (data == null)
            {
                throw new ArgumentException();
            }

            if (data.Count == 0 || data[1] == null)
            {
                throw new ArgumentException(data.ToString());
            }

            byte[] byteTable = (byte[])data[1];
            for (int currentByte = 0; currentByte < byteTable.Length; currentByte++)
            {
                byteTable[currentByte] = byteTable[currentByte] < (byte)128 ? (byte)0 : (byte)255;
            }
            return byteTable;
        }
        #endregion
    }
}
