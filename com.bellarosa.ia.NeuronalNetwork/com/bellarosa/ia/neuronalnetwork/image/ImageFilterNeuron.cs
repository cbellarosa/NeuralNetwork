using com.bellarosa.ia.neuronalnetwork.image;
using log4net;
using System;
using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork.number.image
{
    public class ImageFilterNeuron : INeuron
    {
        #region Constants
        public const int NonSignificantByteNumber = 53;
        #endregion

        #region Attributes
        private readonly ICollection<INeuron> synapses = new LinkedList<INeuron>();
        private readonly ILog log = LogManager.GetLogger(typeof(NumberMatchNeuron));
        #endregion

        #region Properties
        public ICollection<INeuron> Synapses
        {
            get
            {
                return this.synapses;
            }
        }
        #endregion

        #region Public Methods
        public void init() {}

        public object process(IData[] data)
        {
            if (data == null || data.Length != 1 || !data[0].GetType().IsAssignableFrom(typeof(ImageData)) || data[0].Data == null)
            {
                throw new ArgumentException();
            }

            byte[] byteTable = (byte[])data[0].Data;
            for (int currentByte = NonSignificantByteNumber; currentByte < byteTable.Length; currentByte++)
            {
                byteTable[currentByte] = byteTable[currentByte] < (byte)128 ? (byte)0 : (byte)255;
            }
            return byteTable;
        }
        #endregion
    }
}
