using System;
using KeceK.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeceK.Game
{
    public class PlayerPauser : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] [Required]
        private InputReader _inputReader;

        private void Start()
        {
            _inputReader.OnPauseEvent += InputReaderOnOnPauseEvent;
        }

        private void OnDestroy()
        {
            _inputReader.OnPauseEvent -= InputReaderOnOnPauseEvent;
        }

        private void InputReaderOnOnPauseEvent(InputAction.CallbackContext callback)
        {
            if(callback.performed)
                PauseManager.TogglePauseState();
        }
    }
}
