using System;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class Coin : MonoBehaviour, ICollectable
    {
        /// <summary>
        /// Called when a coin is collected. Param: the player who collected the coin. (1 for Player 1, 2 for Player 2)
        /// </summary>
        public static event Action<int> OnCoinCollected;
        
        [SerializeField] [Title("References")]
        private GameObject _particleSystemPrefab;
        [SerializeField] [Required]
        private PlayerIdentifier _playerIdentifier;
        public void Collect()
        {
            Instantiate(_particleSystemPrefab, transform.position, Quaternion.identity);
            OnCoinCollected?.Invoke(_playerIdentifier.ThisPlayerType == PlayerType.Player1 ? 1 : 2);
            Destroy(gameObject);
        }
    }
}
