using System;
using System.Collections.Generic;
using KeceK.General;
using KeceK.Input;
using KeceK.Utils.Components;
using UnityEngine;

namespace KeceK.Game
{
    
    public class PlayerStateMachine
    {
        public event Action<PlayerState> OnStateChanged;
        
        private IState _currentState;
        private PlayerState _currentPlayerState;
        
        private readonly Dictionary<PlayerState, IState> _stateMap;

        private PlayerWalkState _playerWalkState;
        private PlayerIdleState _playerIdleState;
        private PlayerJumpState _playerJumpState;
        private PlayerFallState _playerFallState;
        private PlayerDeadState _playerDeadState;
        
        //Publics
        public PlayerState CurrentState => _currentPlayerState;
        
        public PlayerStateMachine(Rigidbody2D rigidbody2D, GroundCheck groundCheck, PlayerRagdoll playerRagdoll, InputEnabler inputEnabler, FasterFallVelocity fasterFallVelocity, ShaderAnimator shaderAnimator)
        {
            var stateFactory = new PlayerStateFactory(rigidbody2D, this, groundCheck, playerRagdoll, inputEnabler, fasterFallVelocity, shaderAnimator);
            _stateMap = stateFactory.CreateStates();
        }
        
        /// <summary>
        /// Call this to Initialize the State Machine
        /// </summary>
        /// <param name="newState"> Pass the initial state</param>
        public void Initialize(PlayerState newState)
        {
            IState startingState = GetIStateFromPlayerState(newState);
        
            _currentState = startingState;
            _currentState.Enter();

            OnStateChanged?.Invoke(_currentState.State);
        }
        
        /// <summary>
        /// Call this to change the state of the player
        /// </summary>
        /// <param name="nextState"> state to go to. Pass the PlayerState</param>
        public void ChangeState(PlayerState nextState)
        {
            _currentState.Exit();

            _currentPlayerState = nextState;
            _currentState = GetIStateFromPlayerState(nextState);
            _currentState.Enter();

            OnStateChanged?.Invoke(_currentState.State);
        }
        
        /// <summary>
        /// Called to execute the state machine, every frame.
        /// </summary>
        public void ExecuteState()
        {
            _currentState.Execute();
        }
        
        /// <summary>
        /// Get the IState from the PlayerState enum.
        /// </summary>
        /// <param name="playerState"></param>
        /// <returns>Returns the IState, Null if not found</returns>
        private IState GetIStateFromPlayerState(PlayerState playerState)
        {
            if (_stateMap.TryGetValue(playerState, out var state))
            {
                return state;
            }
            else
            {
                Debug.LogWarning($"No state found for PlayerState: {playerState}");
                return null;
            }
        }
    }
}
