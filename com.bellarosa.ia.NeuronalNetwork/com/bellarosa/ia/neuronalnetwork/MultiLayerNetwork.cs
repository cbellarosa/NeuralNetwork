using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork
{
    public abstract class MultiLayerNetwork : INetwork
    {
        #region Attributes
        protected readonly ICollection<INeuron> roots = new LinkedList<INeuron>();
        #endregion

        #region Properties
        public ICollection<INeuron> Roots
        {
            get { return roots; }
        }
        #endregion

        #region Public Methods
        public abstract object process(IData data);
        #endregion
    }
}
