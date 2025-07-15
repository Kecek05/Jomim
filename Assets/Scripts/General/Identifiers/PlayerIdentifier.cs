using System;
using UnityEngine;

namespace KeceK.General
{
    public class PlayerIdentifier : MonoBehaviour
    {
        /// <summary>
        /// Called when this player type is changed. Passing the New Player Type.
        /// </summary>
        public event Action<PlayerType> OnPlayerTypeChanged;
        
        [SerializeField] private PlayerType _playerType;

        public PlayerType ThisPlayerType => _playerType;
        
        public void SetPlayerType(PlayerType playerType)
        {
            _playerType = playerType;
            OnPlayerTypeChanged?.Invoke(_playerType);
        }
    }
}
