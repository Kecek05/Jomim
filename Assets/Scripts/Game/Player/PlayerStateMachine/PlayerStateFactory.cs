using System.Collections.Generic;
using KeceK.General;
using UnityEngine;

namespace KeceK.Game
{
    public class PlayerStateFactory
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly PlayerStateMachine _stateMachine;

        public PlayerStateFactory(Rigidbody2D rigidbody2D, PlayerStateMachine stateMachine)
        {
            _rigidbody2D = rigidbody2D;
            _stateMachine = stateMachine;
        }
        
        public Dictionary<PlayerState, IState> CreateStates()
        {
            return new Dictionary<PlayerState, IState>
            {
                { PlayerState.Idle, new PlayerIdleState(_rigidbody2D, _stateMachine) },
                { PlayerState.Walk, new PlayerWalkState(_rigidbody2D, _stateMachine) },
                { PlayerState.Jump, new PlayerJumpState(_rigidbody2D, _stateMachine) },
                { PlayerState.Fall, new PlayerFallState(_rigidbody2D, _stateMachine) },
            };
        }
    }
}
