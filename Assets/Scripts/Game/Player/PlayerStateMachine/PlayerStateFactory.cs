using System.Collections.Generic;
using KeceK.General;
using KeceK.Utils.Components;
using UnityEngine;

namespace KeceK.Game
{
    public class PlayerStateFactory
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly PlayerStateMachine _stateMachine;
        private readonly GroundCheck _groundCheck;
        
        public PlayerStateFactory(Rigidbody2D rigidbody2D, PlayerStateMachine stateMachine, GroundCheck groundCheck)
        {
            _rigidbody2D = rigidbody2D;
            _stateMachine = stateMachine;
            _groundCheck = groundCheck;
        }
        
        public Dictionary<PlayerState, IState> CreateStates()
        {
            return new Dictionary<PlayerState, IState>
            {
                { PlayerState.Idle, new PlayerIdleState(_rigidbody2D, _stateMachine, _groundCheck) },
                { PlayerState.Walk, new PlayerWalkState(_rigidbody2D, _stateMachine, _groundCheck) },
                { PlayerState.Jump, new PlayerJumpState(_rigidbody2D, _stateMachine) },
                { PlayerState.Fall, new PlayerFallState(_rigidbody2D, _stateMachine, _groundCheck) },
            };
        }
    }
}
