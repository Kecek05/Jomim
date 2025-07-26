using KeceK.General;
using KeceK.Utils.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class PlayerManager : MonoBehaviour
    {
        
        [SerializeField] [FoldoutGroup("References")] [Required]
        private PlayerMovement _playerMovement;
        [SerializeField] [FoldoutGroup("References")] [Required]
        private PlayerAnimator _playerAnimator;
        [SerializeField] [FoldoutGroup("References")] [Required]
        private Rigidbody2D _rigidbody2D;
        [SerializeField] [FoldoutGroup("References")] [Required]
        private GroundCheck _groundCheck;
        
        private PlayerStateMachine _playerStateMachine;

        //Debug
        public PlayerStateMachine PlayerStateMachine => _playerStateMachine;
        
        private void Start()
        {
            _playerStateMachine = new PlayerStateMachine(_rigidbody2D, _groundCheck);
            
            _playerStateMachine.OnStateChanged += PlayerStateMachineOnOnStateChanged;
            _playerStateMachine.Initialize(PlayerState.Idle);
            
            _playerMovement.OnJump += PlayerMovementOnOnJump;
        }

        private void OnDestroy()
        {
            _playerStateMachine.OnStateChanged -= PlayerStateMachineOnOnStateChanged;
            _playerMovement.OnJump -= PlayerMovementOnOnJump;
        }

        private void Update()
        {
            _playerStateMachine.ExecuteState();
        }
        
        private void PlayerMovementOnOnJump()
        {
            _playerStateMachine.ChangeState(PlayerState.Jump);
        }

        private void PlayerStateMachineOnOnStateChanged(PlayerState newState)
        {
            _playerAnimator.PlayerStateMachineOnOnStateChanged(newState);
        }
    }
}
