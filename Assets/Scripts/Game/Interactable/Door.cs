using System;
using KeceK.General;
using KeceK.Utils.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class Door : MonoBehaviour, IActivatable
    {
        [SerializeField] [FoldoutGroup("References")]
        private Mover _mover;
        
        [SerializeField] [FoldoutGroup("Settings")] [ReadOnly]
        private bool _isActive;
        [SerializeField] [FoldoutGroup("Settings")]
        private bool _isInitiallyActive;
        
        public bool IsActive => _isActive;

        private void OnEnable()
        {
            if (_isInitiallyActive)
                TryActivate();
        }
        
        [Button(ButtonSizes.Large, DrawResult = false)] [HorizontalGroup("Try")] [HideInEditorMode]
        public bool TryActivate()
        {
            if (!IsActive)
            {
                _isActive = true;
                _mover.Move();
                return true;
            }
            return false;
        }
        
        [Button(ButtonSizes.Large, DrawResult = false)] [HorizontalGroup("Try")] [HideInEditorMode]
        public bool TryDeactivate()
        {
            if (!IsActive) return false;

            _isActive = false;
            _mover.Move();
            return true;
        }
    }
}
