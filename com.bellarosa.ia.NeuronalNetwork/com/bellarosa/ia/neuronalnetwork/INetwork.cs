using System.Collections.Generic;

namespace com.bellarosa.ia.neuronalnetwork
{
    public interface INetwork
    {
        #region Public Methods
        object process(IData data);
        #endregion
    }
}
