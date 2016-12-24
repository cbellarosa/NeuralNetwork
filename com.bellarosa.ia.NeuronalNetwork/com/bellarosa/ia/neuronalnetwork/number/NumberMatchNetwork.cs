using com.bellarosa.ia.neuronalnetwork.image;
using com.bellarosa.ia.neuronalnetwork.number.image;
using System;
using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork.number
{
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
            ImageFilterNeuron imageFilterNeuron = new ImageFilterNeuron();
            this.roots.Add(imageFilterNeuron);
            for (int i = 0;i < 10;i++)
            {
                NumberMatchNeuron neuron = new NumberMatchNeuron(i);
                neuron.init();
                imageFilterNeuron.Synapses.Add(neuron);
            }
        }
        #endregion

        #region Public Methods
        public override object process(IData data)
        {
            if (data == null && !data.GetType().IsAssignableFrom(typeof(ImageData)))
            {
                throw new ArgumentException();
            }

            float maxNeuronResult = 0;
            int candidateNeuron = 0;
            foreach (ImageFilterNeuron imageFilterNeuron in roots)
            {
                byte[] imageFilteredResult = (byte[])imageFilterNeuron.process(new IData[] { data });
                foreach (NumberMatchNeuron numberMatchNeuron in imageFilterNeuron.Synapses)
                {
                    float currentNeuronResult = (float)numberMatchNeuron.process(new IData[] { new ImageData(imageFilteredResult) });
                    if (maxNeuronResult < currentNeuronResult)
                    {
                        maxNeuronResult = currentNeuronResult;
                        candidateNeuron = numberMatchNeuron.Id;
                    }
                }
            }

            return (maxNeuronResult > this.minAcceptable) ? candidateNeuron : (int?)null;
        }
        #endregion
    }
}
