using log4net;
using System;
using System.Drawing;
using System.Linq;

namespace com.bellarosa.ia.neuronalnetwork.image
{
    /// <summary>
    /// Allow to load and manage Image data
    /// </summary>
    public class ImageData
    {
        #region Attributes
        private byte[] data = new byte[35];
        private static readonly ILog log = LogManager.GetLogger(typeof(ImageData));
        private readonly int width;
        private readonly int height;
        #endregion

        #region Properties
        public int Width
        {
            get { return this.width; }
        }

        public int Height
        {
            get { return this.height; }
        }

        public byte[] Data
        {
            get { return this.data; }
        }
        #endregion

        #region Constructor
        public ImageData(String fileName)
        {
            try
            {
                Image image = Image.FromFile(fileName);
                this.width = image.Width;
                this.height = image.Height;
                ImageConverter imageConverter = new ImageConverter();
                this.data = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
            }
            catch (System.IO.IOException e)
            {
                log.Error(String.Format("Unable to load resources {0:s}", fileName), e);
                throw e;
            }
        }

        public ImageData(byte[] byteArray, int width, int height)
        {
            if (byteArray == null)
            {
                throw new ArgumentException("Byte array is null");
            }

            if (byteArray.Length != width * height)
            {
                throw new ArgumentException(String.Format("Invalid size '{0:d}' expected '{1:d}'", width * height, byteArray.Length));
            }

            this.width = width;
            this.height = height;
            this.data = byteArray;
        }
        #endregion

        #region Public Methods
        public override bool Equals(object obj)
        {
            if (obj == null || !obj.GetType().IsAssignableFrom(typeof(ImageData)))
            {
                return false;
            }

            ImageData objAsImageData = (ImageData)obj;
            if (width != objAsImageData.width || height != objAsImageData.height)
            {
                return false;
            }

            if (data == null )
            {
                return objAsImageData.Data == null;
            }
            else if (objAsImageData.Data == null)
            {
                return false;
            }

            return data.SequenceEqual<byte>(objAsImageData.Data);
        }

        public override int GetHashCode()
        {
            int dataHashCode = data == null ? 0 : data.GetHashCode();
            return 24 +  width + Height + dataHashCode;
        }
        #endregion
    }
}
