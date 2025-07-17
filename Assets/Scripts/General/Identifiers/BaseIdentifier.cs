using UnityEngine;

namespace KeceK.General
{
    public abstract class BaseIdentifier : MonoBehaviour, IIdentifier
    {
        public void TriggerIdentify(PlayerType playerType)
        {
            Identify(playerType);
        }

        /// <summary>
        /// The actual implementation of the identification logic.
        /// </summary>
        protected abstract void Identify(PlayerType playerType);
    }
}
