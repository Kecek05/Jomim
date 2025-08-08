using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Input
{
    public class InputEnabler : MonoBehaviour
    {
        [SerializeField] [Required] [FoldoutGroup("References")]
        private PlayerIdentifier _playerIdentifier;
        [SerializeField] [Required] [FoldoutGroup("References")]
        private InputReader _inputReader;
        
        private PlayerControls _playerControls => _inputReader.GetPlayerControls();
        
        private void Start()
        {
            UpdateInput();
        }
        
        private void UpdateInput()
        {
            switch (_playerIdentifier.ThisPlayerType)
            {
                case PlayerType.Player1:
                    _playerControls.Player1.Enable();
                    _playerControls.Player2.Disable();
                    break;
                case PlayerType.Player2:
                    _playerControls.Player1.Disable();
                    _playerControls.Player2.Enable();
                    break;
            }
        }

        /// <summary>
        /// Call This to lock the input of the player.
        /// </summary>
        public void LockInput()
        {
            _playerControls.Player1.Disable();
            _playerControls.Player2.Disable();
        }
        
        /// <summary>
        /// Call this to unlock the input of the player.
        /// </summary>
        public void UnlockInput()
        {
            if (_playerIdentifier.ThisPlayerType == PlayerType.None)
            {
                Debug.LogWarning("InputReader: Player type is not set. Please set the player type before unlocking input.");
                return;
            }
            
            UpdateInput();
        }
    }
}
