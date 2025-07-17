using System;
using KeceK.General;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace KeceK.Game
{
    public class PressurePlate : MonoBehaviour, IActivator
    {
        [SerializeField] [FoldoutGroup("References")] [Tooltip("The activatable object that this pressure plate will activate or deactivate.")] [Required]
        [ValidateInput(nameof(HasIActivatable), "GameObject must have a component implementing IActivatable.")]
        private GameObject _iActivatableObject;
        
        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("If true, the pressure plate will be active on start.")]
        private bool _isInitiallyActive;
        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("If true, the pressure plate will disable as soon as the colliding object gets out.")]
        private bool _needToKeepTouchingToKeepActive;
        
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerActivate();
        }

        private void HandleCollision()
        {
            
        }
    }
}
