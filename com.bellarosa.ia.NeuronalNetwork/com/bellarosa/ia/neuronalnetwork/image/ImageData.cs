using log4net;
using System;
using System.Drawing;

namespace com.bellarosa.ia.neuronalnetwork.image
{
    public class ImageData : IData
    {
        #region Attributes
        private byte[] data = new byte[35];
        private static readonly ILog log = LogManager.GetLogger(typeof(ImageData));
        #endregion

        #region Constructor
        public ImageData(byte[] data)
        {
            this.data = data;
        }

        public ImageData(String fileName)
        {
            try
            {
                Image image = Image.FromFile(fileName);
                ImageConverter imageConverter = new ImageConverter();
                this.data = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
            }
            catch (System.IO.IOException e)
            {
                log.Error(String.Format("Unable to load resources {0:s}", fileName), e);
                throw e;
            }
        }
        #endregion

        #region Public Methods
        public object Data
        {
            get { return this.data; }
        }
        #endregion
    }
}
