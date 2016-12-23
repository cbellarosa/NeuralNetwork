using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace com.bellarosa.ia
{
    public class Neuron
    {
        #region Private Attributes
        private int id;
        private int imageWidth = 0;
        private int imageHeight = 0;
        private readonly ICollection<Neuron> synapses = new LinkedList<Neuron>();
        private byte[] byteTable;
        private readonly ILog log = LogManager.GetLogger(typeof(Neuron));
        private const int nonSignificantByteNumber = 54;
        private const int numberOfColors = 3;
        #endregion

        #region Constructor
        public Neuron(int id)
        {
            this.id = id;
        }
        #endregion

        #region Properties
        public int Id
        {
            get { return this.id; }
        }

        public ICollection<Neuron> Synapses
        {
            get { return this.synapses; }
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

        public float process(IData data, double activation) 
        {
            int byteLength = this.byteTable.Length;
            int numberOfSamePixels = 0;
            byte[] comparingByteTable = data.Data;
            int currentByte = nonSignificantByteNumber;
            while (currentByte < byteLength)
            {
                if (this.byteTable[currentByte] == comparingByteTable[currentByte]
                    && this.byteTable[currentByte + 1] == comparingByteTable[currentByte + 1]
                    && this.byteTable[currentByte + 2] == comparingByteTable[currentByte + 2])
                {
                    numberOfSamePixels++;
                }
                currentByte += numberOfColors;

                bool isEndOfRow = (currentByte - nonSignificantByteNumber) % ( numberOfColors * this.imageWidth) == 0;
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
