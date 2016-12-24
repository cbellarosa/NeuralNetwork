using com.bellarosa.ia.neuronalnetwork.image;
using com.bellarosa.ia.neuronalnetwork.number.image;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace com.bellarosa.ia.neuronalnetwork.number
{
    public class NumberMatchNeuron : INeuron
    {
        #region Private Attributes
        private int id;
        private int imageWidth = 0;
        private int imageHeight = 0;
        private byte[] byteTable;
        private readonly ILog log = LogManager.GetLogger(typeof(NumberMatchNeuron));
        private const int numberOfColors = 3;
        private readonly ICollection<INeuron> synapses = new LinkedList<INeuron>();
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

        public ICollection<INeuron> Synapses
        {
            get
            {
                return this.synapses;
            }
        }
        #endregion

        #region Public Methods
        public void init()
        {
            string fileName = String.Format(@".\resources\{0:d}.bmp", this.id);
            try
            {
                Image image = Image.FromFile(fileName);
                this.imageWidth = image.Width;
                this.imageHeight = image.Height;
                ImageConverter imageConverter = new ImageConverter();
                this.byteTable = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
            }
            catch (IOException e)
            {
                log.Error(String.Format("Unable to load resources {0:s}", fileName), e);
            }
        }

        public object process(IData[] data) 
        {
            if (data == null || data.Length != 1 || !data[0].GetType().IsAssignableFrom(typeof(ImageData)) || data[0].Data == null)
            {
                throw new ArgumentException();
            }

            byte[] comparingByteTable = (byte[])data[0].Data;
            int byteLength = this.byteTable.Length;
            int numberOfSamePixels = 0;
            int currentByte = ImageFilterNeuron.NonSignificantByteNumber;
            while (currentByte < byteLength)
            {
                if (this.byteTable[currentByte] == comparingByteTable[currentByte]
                    && this.byteTable[currentByte + 1] == comparingByteTable[currentByte + 1]
                    && this.byteTable[currentByte + 2] == comparingByteTable[currentByte + 2])
                {
                    numberOfSamePixels++;
                }
                currentByte += numberOfColors;

                bool isEndOfRow = (currentByte - ImageFilterNeuron.NonSignificantByteNumber) % ( numberOfColors * this.imageWidth) == 0;
                if (isEndOfRow)
                {
                    currentByte++;
                }
            }
            return (float)numberOfSamePixels / ((float)this.imageHeight * (float)this.imageWidth);
        }
        #endregion
    }
}
