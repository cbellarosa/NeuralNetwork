using System;
using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork
{
    public interface INeuron
    {
        #region Properties
        ICollection<INeuron> Synapses {get;}
        #endregion

        #region Public Methods
        void init();
        Object process(IData[] data);
        #endregion
    }
}
