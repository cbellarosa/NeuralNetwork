using com.bellarosa.ia.neuronalnetwork.image;
using com.bellarosa.ia.neuronalnetwork.number.image;
using System;
using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork.number
{
    /// <summary>
    /// Network to recognize simple digits
    /// </summary>
    public class NumberMatchNetwork : MultiLayerNetwork
    {
        #region Attributes
        private float minAcceptable = 0.95F;
        #endregion

        #region Properties
        public float MinAcceptable
        {
            get { return this.minAcceptable; }
            set { this.minAcceptable = value; }
        }
        #endregion

        #region Constructor
        public NumberMatchNetwork()
        {
            ImageDataExtractionNeuron imageDataExtractionNeuron = new ImageDataExtractionNeuron();
            this.roots.Add(imageDataExtractionNeuron);

            ImageDataSigmoidNeuron imageFilterNeuron = new ImageDataSigmoidNeuron();
            imageDataExtractionNeuron.Synapses.Add(imageFilterNeuron);
            for (int i = 0;i < 10;i++)
            {
                NumberMatchNeuron numberMatchNeuron = new NumberMatchNeuron(i);
                numberMatchNeuron.init();
                imageFilterNeuron.Synapses.Add(numberMatchNeuron);
            }
        }
        #endregion

        #region Public Methods
        public override object process(object data)
        {
            if (data == null && !data.GetType().IsAssignableFrom(typeof(ImageData)))
            {
                throw new ArgumentException();
            }

            ImageData imageData = (ImageData)data;
            float maxNeuronResult = 0;
            int candidateNeuron = 0;
            foreach (ImageDataExtractionNeuron imageDataExtractionNeuron in this.roots)
            {
                IDictionary<int, object> inputImage = new Dictionary<int, object> { { 1, imageData.Data } };
                byte[] imageDataExtractedResult = (byte[])imageDataExtractionNeuron.process(inputImage);
                foreach (ImageDataSigmoidNeuron imageDataSignoidNeuron in imageDataExtractionNeuron.Synapses)
                {
                    IDictionary<int, object> inputImageDataExtracted = new Dictionary<int, object> { { 1, imageDataExtractedResult } };
                    byte[] imageDataSignmoidResult = (byte[])imageDataSignoidNeuron.process(inputImageDataExtracted);
                    foreach (NumberMatchNeuron numberMatchNeuron in imageDataSignoidNeuron.Synapses)
                    {
                        IDictionary<int, object> inputImageDataSigmoid = new Dictionary<int, object> { { 1, imageDataSignmoidResult } };
                        float currentNeuronResult = (float)numberMatchNeuron.process(inputImageDataSigmoid);
                        if (maxNeuronResult < currentNeuronResult)
                        {
                            maxNeuronResult = currentNeuronResult;
                            candidateNeuron = numberMatchNeuron.Id;
                        }
                    }
                }
            }

            return (maxNeuronResult > this.minAcceptable) ? candidateNeuron : (int?)null;
        }
        #endregion
    }
}
