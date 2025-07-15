using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class IdentifierGameObjectLayer : BaseIdentifier
    {
        [SerializeField] [FoldoutGroup("Layers")] [Required]
        protected GameObject[] _objectsToChangePlayerLayer;
        protected override void Identify(PlayerType playerType)
        {
            foreach (GameObject gameObject in _objectsToChangePlayerLayer)
            {
                gameObject.layer = playerType == PlayerType.Player1 
                    ? PlayersInfo.Player1LayerMaskIndex 
                    : PlayersInfo.Player2LayerMaskIndex;
            }
        }
    }
}
