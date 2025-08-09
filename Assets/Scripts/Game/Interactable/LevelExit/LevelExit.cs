using System;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class LevelExit : MonoBehaviour, ITouchable, IUnTouchable
    {
        /// <summary>
        /// Called when the level exit is touched. Passes true if the level exit is touched, false if it is untouching.
        /// </summary>
        public event Action<bool> OnLevelExitTouched; 
        
        [SerializeField] [FoldoutGroup("Settings")]
        [Tooltip("If true, the pressure plate will disable as soon as the colliding object gets out.")]
        private bool _needToKeepTouchingToKeepActive = true;
        private bool _isTouching;

        
        public void Touch(GameObject whoTouchedMe)
        {
            _isTouching = true;
            OnLevelExitTouched?.Invoke(_isTouching);
        }

        public void Untouch(GameObject whoUntouchedMe)
        {
            _isTouching = false;
            OnLevelExitTouched?.Invoke(_isTouching);
        }
    }
}
