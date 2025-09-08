using KeceK.Game;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace KeceK.Utils.Debug
{
    #if UNITY_EDITOR
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI[] _texts;
        [Space(10)] 
        
        
        [SerializeField] [Required]
        private Rigidbody2D _rigidbody2D;

        [SerializeField] [Required] 
        private PlayerMovement _playerMovement;
        [SerializeField] [Required] 
        private PlayerManager _playerManager;

        private void Update()
        {
            _texts[0].text = $"LinearX Vel: {_rigidbody2D.linearVelocityX}";
            _texts[1].text = $"LinearY Vel: {_rigidbody2D.linearVelocityY}";
            _texts[2].text = $"Can Jump: {_playerMovement.CanJump}";
            _texts[3].text = $"Coyote: {_playerMovement.CoyoteTimeCounter}";
            _texts[4].text = $"Jump Buffer: {_playerMovement.JumpBufferCounter}";
            _texts[5].text = $"Jump Cooldown: {_playerMovement.CooldownBetweenJumps}";
            _texts[6].text = $"State: {_playerManager?.PlayerStateMachine?.CurrentState.ToString() ?? "Null"}";
        }
    }
    #endif
}
