using System;
using UnityEngine;

namespace KeceK.Game
{
    public static class PauseManager
    {

        public static event Action<bool> OnPauseStateChanged;
        
        private static bool _isPaused;
        private static bool _canChangePauseState = true;
        
        public static bool IsPaused => _isPaused;
        public static bool CanChangePauseState => _canChangePauseState;

        private static void ChangePauseState(bool pause)
        {
            if(!_canChangePauseState) return;
            
            if (_isPaused == pause) return;
            
            Time.timeScale = pause ? 0f : 1f;
            _isPaused = pause;
            OnPauseStateChanged?.Invoke(_isPaused);
        }
        
        public static void TogglePauseState()
        {
            ChangePauseState(!_isPaused);
        }

        public static void PauseGame()
        {
            ChangePauseState(true);
        }
        
        public static void UnPauseGame()
        {
            ChangePauseState(false);
        }
        
        public static void SetCanChangePauseState(bool canChange)
        {
            _canChangePauseState = canChange;
        }
    }
}
