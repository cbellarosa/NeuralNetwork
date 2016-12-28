namespace com.bellarosa.ia.neuronalnetwork
{
    /// <summary>
    /// Interface for all neuron networks
    /// </summary>
    public interface INetwork
    {
        #region Public Methods
        object process(object data);
        #endregion
    }
}
