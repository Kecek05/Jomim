using System;
using KeceK.General;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeceK.Input
{
    public class InputReader : MonoBehaviour, PlayerControls.IPlayer1Actions, PlayerControls.IPlayer2Actions
    {
        public event Action<InputAction.CallbackContext> OnJumpEvent;
        public event Action<InputAction.CallbackContext> OnMove;
        
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
            OnMove?.Invoke(context);
        }

        void PlayerControls.IPlayer2Actions.OnMove(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context);
        }
        
        private void UpdateInput()
        {
            switch (_thisPlayerType)
            {
                case PlayerType.Player1:
                    _controls.Player1.Enable();
                    _controls.Player2.Disable();
                    break;
                case PlayerType.Player2:
                    _controls.Player1.Disable();
                    _controls.Player2.Enable();
                    break;
            }
        }
        
        public void SetPlayerType(PlayerType playerType)
        {
            _thisPlayerType = playerType;
            UpdateInput();
        }
    }
}
