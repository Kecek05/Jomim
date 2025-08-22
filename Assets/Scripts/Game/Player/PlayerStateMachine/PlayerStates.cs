using System;
using KeceK.General;
using KeceK.Input;
using KeceK.Utils.Components;
using UnityEngine;

namespace KeceK.Game
{
    public class PlayerWalkState : IState
    {
        private PlayerState _state = PlayerState.Walk;
        private Rigidbody2D _rigidbody2D;
        private PlayerStateMachine _playerStateMachine;
        private GroundCheck _groundCheck;
        
        public PlayerState State => _state;

        public PlayerWalkState(Rigidbody2D rigidbody2D, PlayerStateMachine playerStateMachine, GroundCheck groundCheck)
        {
            _rigidbody2D = rigidbody2D;
            _playerStateMachine = playerStateMachine;
            _groundCheck = groundCheck;
        }
        
        public void Enter()
        {

        }

        public void Execute()
        {
            if (_groundCheck.IsGrounded())
            {
                if (!MathK.IsVelocityAboveThreshold(_rigidbody2D.linearVelocityX))
                {
                    //Idle
                    _playerStateMachine.ChangeState(PlayerState.Idle);
                }
            }
            else
            {
                //Falling
                _playerStateMachine.ChangeState(PlayerState.Fall);
            }
        }

        public void Exit()
        {

        }
    }
    
    public class PlayerIdleState : IState
    {
        private PlayerState _state = PlayerState.Idle;
        private Rigidbody2D _rigidbody2D;
        private PlayerStateMachine _playerStateMachine;
        private GroundCheck _groundCheck;
        public PlayerState State => _state;

        public PlayerIdleState(Rigidbody2D rigidbody2D, PlayerStateMachine playerStateMachine, GroundCheck groundCheck)
        {
            _rigidbody2D = rigidbody2D;
            _playerStateMachine = playerStateMachine;
            _groundCheck = groundCheck;
        }
        
        public void Enter()
        {
            
        }

        public void Execute()
        {
            if (_groundCheck.IsGrounded())
            {
                if (MathK.IsVelocityAboveThreshold(_rigidbody2D.linearVelocityX))
                {
                    //Walking
                    _playerStateMachine.ChangeState(PlayerState.Walk);
                }
            }
            else
            {
                //Falling
                _playerStateMachine.ChangeState(PlayerState.Fall);
            }
        }

        public void Exit()
        {
            
        }
    }
    
    public class PlayerJumpState : IState
    {
        private PlayerState _state = PlayerState.Jump;
        private Rigidbody2D _rigidbody2D;
        private PlayerStateMachine _playerStateMachine;
        
        public PlayerState State => _state;

        public PlayerJumpState(Rigidbody2D rigidbody2D, PlayerStateMachine playerStateMachine)
        {
            _rigidbody2D = rigidbody2D;
            _playerStateMachine = playerStateMachine;
        }
        
        public void Enter()
        {

        }

        public void Execute()
        {
            if (_rigidbody2D.linearVelocityY < -MathK.VelocityThreshold)
            {
                //Falling
                _playerStateMachine.ChangeState(PlayerState.Fall);
            }
        }

        public void Exit()
        {

        }
    }
    
    public class PlayerFallState : IState
    {
        private PlayerState _state = PlayerState.Fall;
        private Rigidbody2D _rigidbody2D;
        private PlayerStateMachine _playerStateMachine;
        private GroundCheck _groundCheck;
        
        public PlayerState State => _state;

        public PlayerFallState(Rigidbody2D rigidbody2D, PlayerStateMachine playerStateMachine, GroundCheck groundCheck)
        {
            _rigidbody2D = rigidbody2D;
            _playerStateMachine = playerStateMachine;
            _groundCheck = groundCheck;
        }
        
        public void Enter()
        {

        }

        public void Execute()
        {
            if (_groundCheck.IsGrounded())
            {
                if (MathK.IsVelocityAboveThreshold(_rigidbody2D.linearVelocityX))
                {
                    //Walking
                    _playerStateMachine.ChangeState(PlayerState.Walk);
                }
                else
                {
                    //Idle
                    _playerStateMachine.ChangeState(PlayerState.Idle);
                }
            }
        }

        public void Exit()
        {

        }
    }
    
    public class PlayerDead : IState
    {
        public static event Action OnPlayerDead;
        
        private PlayerState _state = PlayerState.Dead;
        private PlayerRagdoll _playerRagdoll;
        private InputEnabler _inputEnabler;
        private FasterFallVelocity _fasterFallVelocity;
        
        public PlayerState State => _state;

        public PlayerDead(PlayerRagdoll playerRagdoll, InputEnabler inputEnabler, FasterFallVelocity fasterFallVelocity)
        {
            _playerRagdoll = playerRagdoll;
            _inputEnabler = inputEnabler;
            OnPlayerDead?.Invoke();
            _fasterFallVelocity = fasterFallVelocity;
        }
        
        public void Enter()
        {
            _fasterFallVelocity.SetIsEnabled(false);
            _inputEnabler.LockInput();
            _playerRagdoll.EnableRagdoll();
            OnPlayerDead?.Invoke();
        }

        public void Execute()
        {

        }

        public void Exit()
        {

        }
    }
}
