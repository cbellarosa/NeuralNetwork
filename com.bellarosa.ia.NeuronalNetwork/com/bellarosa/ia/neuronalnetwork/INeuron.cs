using System;
using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork
{
    /// <summary>
    /// Interface for all neurons
    /// </summary>
    public interface INeuron
    {
        #region Properties
        ICollection<INeuron> Synapses {get;}
        #endregion

        #region Public Methods
        void init();
        Object process(IDictionary<int, object> data);
        #endregion
    }
}
