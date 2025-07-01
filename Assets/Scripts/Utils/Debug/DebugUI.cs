using KeceK.Game;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace KeceK.Utils.Debug
{
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
            _texts[0].text = $"Linear Vel: {_rigidbody2D.linearVelocity}";
            _texts[1].text = $"Can Jump: {_playerMovement.CanJump}";
            _texts[2].text = $"Coyote: {_playerMovement.CoyoteTimeCounter}";
            _texts[3].text = $"Jump Buffer: {_playerMovement.JumpBufferCounter}";
            _texts[4].text = $"Jump Cooldown: {_playerMovement.CooldownBetweenJumps}";
            _texts[5].text = $"State: {_playerManager?.PlayerStateMachine?.CurrentState.ToString() ?? "Null"}";
        }
    }
}
