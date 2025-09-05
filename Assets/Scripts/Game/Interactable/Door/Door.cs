using System;
using KeceK.General;
using KeceK.Utils.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class Door : MonoBehaviour, IActivatable
    {
        public event Action<bool> OnActivatableStateChanged;
        
        [SerializeField] [FoldoutGroup("References")]
        private Mover _mover;
        
        [SerializeField] [FoldoutGroup("Settings")] [ReadOnly]
        private bool _isActive;
        [SerializeField] [FoldoutGroup("Settings")]
        private bool _isInitiallyActive;
        
        private int _activationsCount;
        
        public bool IsActive => _isActive;

        private void OnEnable()
        {
            if (_isInitiallyActive)
                TryActivate();
        }
        
        [Button(ButtonSizes.Large, DrawResult = false)] [HorizontalGroup("Try")] [HideInEditorMode]
        public bool TryActivate()
        {
            _activationsCount++;
            if (!IsActive)
            {
                _isActive = true;
                _mover.Move();
                OnActivatableStateChanged?.Invoke(_isActive);
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Tries to deactivate the door. If there are multiple activators, it will only deactivate when all activators have deactivated.
        /// </summary>
        /// <returns>If it was successfully deactivated</returns>
        [Button(ButtonSizes.Large, DrawResult = false)] [HorizontalGroup("Try")] [HideInEditorMode]
        public bool TryDeactivate()
        {
            if (!IsActive) return false;
            
            _activationsCount--;
            if (_activationsCount > 0)
            {
                return false;
            }

            _isActive = false;
            _mover.Move();
            OnActivatableStateChanged?.Invoke(_isActive);
            return true;
        }

    }
}
