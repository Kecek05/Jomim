using System;
using KeceK.General;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeceK.Input
{
    public class InputReader : MonoBehaviour, PlayerControls.IPlayer1Actions, PlayerControls.IPlayer2Actions
    {
        public event Action<InputAction.CallbackContext> OnJumpEvent;
        public event Action<InputAction.CallbackContext> OnMoveEvent;
        public event Action<InputAction.CallbackContext> OnPauseEvent;
        
        private PlayerControls _controls;
        private PlayerType _thisPlayerType = PlayerType.None;
        private void Awake()
        {
            if (_controls == null)
            {
                _controls = new PlayerControls();
                _controls.Player1.SetCallbacks(this);
                _controls.Player2.SetCallbacks(this);
            }
        }
        
        public void OnJump(InputAction.CallbackContext context)
        {
            OnJumpEvent?.Invoke(context);
        }
        
        void PlayerControls.IPlayer1Actions.OnMove(InputAction.CallbackContext context)
        {
            OnMoveEvent?.Invoke(context);
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            OnPauseEvent?.Invoke(context);
        }

        void PlayerControls.IPlayer2Actions.OnMove(InputAction.CallbackContext context)
        {
            OnMoveEvent?.Invoke(context);
        }
        
        public PlayerControls GetPlayerControls() => _controls;
    }
}
