using log4net;
using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork
{
    /// <summary>
    /// Base class for all simple neurons
    /// </summary>
    public abstract class SimpleNeuron : INeuron
    {
        #region Attributes
        protected readonly ICollection<INeuron> synapses = new LinkedList<INeuron>();
        protected readonly ILog log = LogManager.GetLogger(typeof(SimpleNeuron));
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
        public virtual void init() {}

        public abstract object process(IDictionary<int, object> data);
        #endregion
    }
}
