using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class IdentifierSpriteRenderer : BaseIdentifier
    {
        [SerializeField] [FoldoutGroup("References")]
        private SpriteRenderer[] _spriteRenderersToChangeColorBasedOnPlayer;
        
        protected override void Identify(PlayerType playerType)
        {
            foreach (SpriteRenderer spriteRenderer in _spriteRenderersToChangeColorBasedOnPlayer)
            {
                spriteRenderer.color = playerType == PlayerType.Player1 
                    ? PlayersInfo.Player1Color 
                    : PlayersInfo.Player2Color;
            }
        }
    }
}
