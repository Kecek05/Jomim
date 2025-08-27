using KeceK.General;
using KeceK.Input;
using KeceK.Utils.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class PlayerManager : MonoBehaviour
    {
        
        [SerializeField] [Title("References")] [Required]
        private PlayerMovement _playerMovement;
        [SerializeField] [Required]
        private PlayerAnimator _playerAnimator;
        [SerializeField] [Required]
        private Rigidbody2D _rigidbody2D;
        [SerializeField] [Required]
        private GroundCheck _groundCheck;
        [SerializeField] [Required]
        private InputEnabler _inputEnabler;
        [SerializeField] [Required]
        private PlayerRagdoll _playerRagdoll;
        [SerializeField] [Required]
        private PlayerHealth _playerHealth;
        [SerializeField] [Required]
        private FasterFallVelocity _fasterFallVelocity;
        [SerializeField] [Required] 
        private ShaderAnimator _shaderAnimator;
        
        private PlayerStateMachine _playerStateMachine;

        //Debug
        public PlayerStateMachine PlayerStateMachine => _playerStateMachine;
        
        private void Start()
        {
            _playerStateMachine = new PlayerStateMachine(_rigidbody2D, _groundCheck, _playerRagdoll, _inputEnabler, _fasterFallVelocity, _shaderAnimator);
            
            _playerStateMachine.OnStateChanged += PlayerStateMachineOnOnStateChanged;
            _playerStateMachine.Initialize(PlayerState.Idle);
            
            _playerMovement.OnJump += PlayerMovementOnOnJump;
            GameManager.OnChangingLevel += GameManagerOnOnChangingLevel;
            
            _playerHealth.OnDeath += PlayerHealthOnOnDeath;
        }
        
        private void OnDestroy()
        {
            _playerStateMachine.OnStateChanged -= PlayerStateMachineOnOnStateChanged;
            _playerMovement.OnJump -= PlayerMovementOnOnJump;
            GameManager.OnChangingLevel -= GameManagerOnOnChangingLevel;
            _playerHealth.OnDeath -= PlayerHealthOnOnDeath;
        }

        private void Update()
        {
            _playerStateMachine.ExecuteState();
        }

        private void PlayerHealthOnOnDeath()
        {
            _playerStateMachine.ChangeState(PlayerState.Dead);
        }

        private void GameManagerOnOnChangingLevel()
        {
            _inputEnabler.LockInput();
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
