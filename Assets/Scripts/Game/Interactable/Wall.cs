using System;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class Wall : MonoBehaviour, IActivatable
    {
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
                //TODO Add TryActivate Logic with Mover
                return true;
            }
            return false;
        }
        
        [Button(ButtonSizes.Large, DrawResult = false)] [HorizontalGroup("Try")] [HideInEditorMode]
        public bool TryDeactivate()
        {
            if (!IsActive) return false;

            _isActive = false;
            Debug.Log("Deactivate Wall");
            
            //TODO Add TryDeactivate Logic with Mover
            
            return true;
        }
    }
}
