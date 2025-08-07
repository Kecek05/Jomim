using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class IdentifierSpriteRenderer : BaseIdentifier
    {
        [SerializeField] [FoldoutGroup("References")] [Required]
        private SpriteIdentifierDataSO _spriteIdentifierData;
        [SerializeField] [FoldoutGroup("References")]
        private SpriteRenderer[] _spriteRenderersToChangeBasedOnPlayer;
        
        protected override void Identify(PlayerType playerType)
        {
            foreach (SpriteRenderer spriteRenderer in _spriteRenderersToChangeBasedOnPlayer)
            {
                spriteRenderer.sprite = playerType == PlayerType.Player1 
                    ? _spriteIdentifierData.player1Sprite 
                    : _spriteIdentifierData.player2Sprite;
            }
        }
    }
}
