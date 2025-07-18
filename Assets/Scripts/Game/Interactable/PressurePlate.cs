using System;
using System.Collections.Generic;
using KeceK.General;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace KeceK.Game
{
    public class PressurePlate : MonoBehaviour, IActivator, ITouchable, IUnTouchable
    {
        [SerializeField] [FoldoutGroup("References")] [Tooltip("The activatable object that this pressure plate will activate or deactivate.")] [Required]
        [ValidateInput(nameof(HasIActivatable), "GameObject must have a component implementing IActivatable.")]
        private GameObject _iActivatableObject;
        
        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("If true, the pressure plate will be active on start.")]
        private bool _isInitiallyActive;
        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("If true, the pressure plate will disable as soon as the colliding object gets out.")]
        private bool _needToKeepTouchingToKeepActive;
        
        private List<GameObject> _collidingObjects = new List<GameObject>();
        private IActivatable _activable;

        public IActivatable Activable => _activable;

        private void Awake()
        {
            UpdateIActivatableReference();
        }
        
        private void OnEnable()
        {
            if (_isInitiallyActive)
                TriggerActivate();
        }

        private bool HasIActivatable(GameObject gameObject)
        {
            return gameObject != null && gameObject.GetComponent<IActivatable>() != null;
        }

        private void UpdateIActivatableReference()
        {
            _activable = _iActivatableObject.GetComponent<IActivatable>();
        }
        [Button(ButtonSizes.Large)] [HorizontalGroup("Trigger")] [HideInEditorMode]
        public void TriggerActivate()
        {
            _activable.TryActivate();
        }
        [Button(ButtonSizes.Large)] [HorizontalGroup("Trigger")] [HideInEditorMode]
        public void TriggerDeactivate()
        {
            _activable.TryDeactivate();
        }

        private void AddTouchingObject(GameObject gameObject)
        {
            if (!_collidingObjects.Contains(gameObject))
            {
                _collidingObjects.Add(gameObject);
            }
            
            if (!_activable.IsActive)
            {
                TriggerActivate();
            }
        }

        private void RemoveTouchingObject(GameObject gameObject)
        {
            if (_collidingObjects.Contains(gameObject))
            {
                _collidingObjects.Remove(gameObject);
            }
            
            if (_needToKeepTouchingToKeepActive && _collidingObjects.Count == 0 && _activable.IsActive)
            {
                TriggerDeactivate();
            }
        }

        public void Touch(GameObject whoTouchedMe)
        {
            AddTouchingObject(whoTouchedMe);
        }

        public void Untouch(GameObject whoUntouchedMe)
        {
            RemoveTouchingObject(whoUntouchedMe);
        }
    }
}
