using System.Collections.Generic;
using KeceK.General;
using KeceK.Input;
using KeceK.Utils.Components;
using UnityEngine;

namespace KeceK.Game
{
    public class PlayerStateFactory
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly PlayerStateMachine _stateMachine;
        private readonly GroundCheck _groundCheck;
        private readonly PlayerRagdoll _playerRagdoll;
        private readonly InputEnabler _inputEnabler;
        private readonly FasterFallVelocity _fasterFallVelocity;
        private readonly ShaderAnimator _shaderAnimator;
        
        public PlayerStateFactory(Rigidbody2D rigidbody2D, PlayerStateMachine stateMachine, GroundCheck groundCheck, PlayerRagdoll playerRagdoll, InputEnabler inputEnabler, FasterFallVelocity fasterFallVelocity, ShaderAnimator shaderAnimator)
        {
            _rigidbody2D = rigidbody2D;
            _stateMachine = stateMachine;
            _groundCheck = groundCheck;
            _playerRagdoll = playerRagdoll;
            _inputEnabler = inputEnabler;
            _fasterFallVelocity = fasterFallVelocity;
            _shaderAnimator = shaderAnimator;
        }
        
        public Dictionary<PlayerState, IState> CreateStates()
        {
            return new Dictionary<PlayerState, IState>
            {
                { PlayerState.Idle, new PlayerIdleState(_rigidbody2D, _stateMachine, _groundCheck) },
                { PlayerState.Walk, new PlayerWalkState(_rigidbody2D, _stateMachine, _groundCheck) },
                { PlayerState.Jump, new PlayerJumpState(_rigidbody2D, _stateMachine) },
                { PlayerState.Fall, new PlayerFallState(_rigidbody2D, _stateMachine, _groundCheck) },
                { PlayerState.Dead, new PlayerDeadState(_playerRagdoll, _inputEnabler, _fasterFallVelocity, _shaderAnimator) }
            };
        }
    }
}
