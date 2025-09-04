using System;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;
using KeceK.Utils.ScriptableObjects;

namespace KeceK.Game
{
    public class Coin : MonoBehaviour, ICollectable
    {
        /// <summary>
        /// Called when a coin is collected. Param: the player who collected the coin. (1 for Player 1, 2 for Player 2)
        /// </summary>
        public static event Action<int> OnCoinCollected;

        public event Action OnThisCoinCollected;
        
        [SerializeField] [Title("References")]
        private GameObjectIdentifierDataSO _coinParticle;
        [SerializeField] [Required]
        private PlayerIdentifier _playerIdentifier;

        [SerializeField] [Required] private GameObject _coinGFX;
        
        public void Collect()
        {
            Instantiate(_playerIdentifier.ThisPlayerType == PlayerType.Player1 ? _coinParticle.player1GameObject : _coinParticle.player2GameObject,
                transform.position, Quaternion.identity);
            OnCoinCollected?.Invoke(_playerIdentifier.ThisPlayerType == PlayerType.Player1 ? 1 : 2);
            _coinGFX.SetActive(false);
            OnThisCoinCollected?.Invoke();
            Destroy(gameObject, 1f);
        }
    }
}
