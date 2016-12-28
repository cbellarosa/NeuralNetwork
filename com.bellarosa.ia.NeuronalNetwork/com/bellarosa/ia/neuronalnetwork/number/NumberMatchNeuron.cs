using com.bellarosa.ia.neuronalnetwork.image;
using com.bellarosa.ia.neuronalnetwork.number.image;
using System;
using System.Collections.Generic;
using System.IO;

namespace com.bellarosa.ia.neuronalnetwork.number
{
    /// <summary>
    /// Match a single digit
    /// </summary>
    public class NumberMatchNeuron : SimpleNeuron
    {
        #region Attributes
        private int id;
        private byte[] byteTable;
        #endregion

        #region Constructor
        public NumberMatchNeuron(int id)
        {
            this.id = id;
        }
        #endregion

        #region Properties
        public int Id
        {
            get { return this.id; }
        }
        #endregion

        #region Public Methods
        public override void init()
        {
            string fileName = String.Format(@".\resources\{0:d}.bmp", this.id);
            try
            {
                ImageData imageData = new ImageData(fileName);
                IDictionary<int, object> inputImage = new Dictionary<int, object> { {1, imageData.Data} };
                byte[] extractedBytes = (byte[])new ImageDataExtractionNeuron().process(inputImage);
                IDictionary<int, object> extractedData = new Dictionary<int, object> { {1, extractedBytes } };
                this.byteTable = (byte[])new ImageDataSigmoidNeuron().process(extractedData);
            }
            catch (IOException e)
            {
                String errorMessage = String.Format("Unable to load resources {0:s}", fileName);
                log.Error(errorMessage, e);
                throw new ArgumentException(errorMessage);
            }
        }

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

            byte[] comparingByteTable = (byte[])data[1];
            int byteLength = this.byteTable.Length;
            int numberOfSamePixels = 0;
            for (int currentByte = 0;currentByte < byteLength;currentByte++)
            {
                if (this.byteTable[currentByte] == comparingByteTable[currentByte])
                {
                    numberOfSamePixels++;
                }

            }
            return (float)numberOfSamePixels / (float)byteLength;
        }
        #endregion
    }
}
