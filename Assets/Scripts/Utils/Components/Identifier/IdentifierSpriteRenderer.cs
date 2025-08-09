using System;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class IdentifierSpriteRenderer : BaseIdentifier
    {
        [Serializable]
        private struct AdvancedIdentifierData 
        {
            public SpriteRenderer[] AdvancedSpriteRenderersToChangeBasedOnPlayer;
            public SpriteIdentifierDataSO SpriteIdentifierData;
        }
        
        [SerializeField] [Title("References")]
        private bool _simpleIdentifier = true;
        [SerializeField] [ShowIf(nameof(_simpleIdentifier))]
        private SpriteIdentifierDataSO _spriteIdentifierData;
        [SerializeField] [ShowIf(nameof(_simpleIdentifier))]
        private SpriteRenderer[] _spriteRenderersToChangeBasedOnPlayer;
        [SerializeField] [HideIf(nameof(_simpleIdentifier))]
        private AdvancedIdentifierData[] _advancedIdentifierDatas;
        
        protected override void Identify(PlayerType playerType)
        {
            if (_simpleIdentifier)
                SimpleIdentifier(playerType);
            else
                AdvancedIdentifier(playerType);
        }
        
        /// <summary>
        /// Change the sprite of the sprite renderers based on the player type.
        /// </summary>
        /// <param name="playerType"></param>
        private void SimpleIdentifier(PlayerType playerType)
        {
            foreach (SpriteRenderer spriteRenderer in _spriteRenderersToChangeBasedOnPlayer)
            {
                spriteRenderer.sprite = playerType == PlayerType.Player1 
                    ? _spriteIdentifierData.player1Sprite 
                    : _spriteIdentifierData.player2Sprite;
            }
        }

        /// <summary>
        /// Change each sprite list based on each SpriteIdentifierData.
        /// </summary>
        /// <param name="playerType"></param>
        private void AdvancedIdentifier(PlayerType playerType)
        {
            foreach (AdvancedIdentifierData advancedIdentifierData in _advancedIdentifierDatas)
            {
                foreach (SpriteRenderer spriteRenderer in advancedIdentifierData.AdvancedSpriteRenderersToChangeBasedOnPlayer)
                {
                    spriteRenderer.sprite = playerType == PlayerType.Player1 
                        ? advancedIdentifierData.SpriteIdentifierData.player1Sprite 
                        : advancedIdentifierData.SpriteIdentifierData.player2Sprite;
                }
            }
        }
    }
}
