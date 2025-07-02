using KeceK.General;
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
                if (Mathf.Abs(_rigidbody2D.linearVelocityX) == 0f)
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
                if (Mathf.Abs(_rigidbody2D.linearVelocityX) > 0f)
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
            if (_rigidbody2D.linearVelocityY < 0f)
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
                if (Mathf.Abs(_rigidbody2D.linearVelocityX) > 0f)
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
}
