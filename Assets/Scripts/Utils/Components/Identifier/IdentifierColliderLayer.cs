using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class IdentifierColliderLayer : BaseIdentifier
    {
        [SerializeField] [FoldoutGroup("References")]
        private Collider2D[] _collidersToChangeCollisionLayer;

        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("If true, the layer will be inverted, meaning Player1 will not touch Player2 Layer, will touch only Player1 Layer and vice versa.")]
        private bool _thisPlayerTypeCanCollideWithThisObject;
        
        protected override void Identify(PlayerType playerType)
        {
            foreach (Collider2D collider2D in _collidersToChangeCollisionLayer)
            {
                int layerToExclude = -1;
                if (_thisPlayerTypeCanCollideWithThisObject)
                {
                    layerToExclude = playerType == PlayerType.Player1 
                        ? PlayersInfo.Player2LayerMaskIndex 
                        : PlayersInfo.Player1LayerMaskIndex;
                }
                else
                {
                    layerToExclude = playerType == PlayerType.Player1 
                        ? PlayersInfo.Player1LayerMaskIndex 
                        : PlayersInfo.Player2LayerMaskIndex;
                }
                
                //Remove the layer index from the bitmask
                collider2D.excludeLayers = 1 << layerToExclude;
            }
        }
    }
}
