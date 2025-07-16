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
        
        private IActivatable _activable;

        public IActivatable Activable => _activable;

        private void Awake()
        {
            UpdateIActivatableReference();
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
    }
}
