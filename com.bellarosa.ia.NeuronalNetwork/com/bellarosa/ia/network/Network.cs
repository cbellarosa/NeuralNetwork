using System.Collections.Generic;

namespace com.bellarosa.ia
{
    public class Network
    {
        #region Attributes
        private readonly ICollection<Neuron> roots = new LinkedList<Neuron>();
        private float minAcceptable = 0.95F;
        #endregion

        #region Properties
        public ICollection<Neuron> Roots
        {
                get { return roots; }
        }

        public float MinAcceptable
        {
            get { return this.minAcceptable; }
            set { this.minAcceptable = value; }
        }
        #endregion

        #region Constructor
        public Network()
        {

            for (int i = 0;i < 10;i++)
            {
                Neuron neuron = new Neuron(i);
                neuron.init();
                this.roots.Add(neuron);
            }
        }
        #endregion

        #region Public Methods
        public int? process(IData data)
        {
            float maxNeuronResult = 0;
            int candidateNeuron = 0;
            foreach (Neuron neuron in roots)
            {
                float currentNeuronResult = neuron.process(data, 0);
                if (maxNeuronResult < currentNeuronResult)
                {
                    maxNeuronResult = currentNeuronResult;
                    candidateNeuron = neuron.Id;
                }
            }

            return (maxNeuronResult > this.minAcceptable) ? candidateNeuron : (int?)null;
        }
        #endregion
    }
}
