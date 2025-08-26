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
        public event Action<bool> OnActivationStateChanged;

        [SerializeField] [Title("References")] [Tooltip("The activatable object that this pressure plate will activate or deactivate.")] [Required]
        [ValidateInput(nameof(HasIActivatable), "GameObject must have a component implementing IActivatable.")]
        private List<GameObject> _iActivatableObjects;
        
        [SerializeField] [Title("Settings")] [Tooltip("If true, the pressure plate will be active on start.")]
        private bool _isInitiallyActive;
        [SerializeField] [Tooltip("If true, the pressure plate will disable as soon as the colliding object gets out.")]
        private bool _needToKeepTouchingToKeepActive;
        
        private List<GameObject> _collidingObjects = new List<GameObject>();
        private List<IActivatable> _activables = new List<IActivatable>();
        
        public List<IActivatable> Activables => _activables;

        
        private void Awake()
        {
            UpdateIActivatableReference();
        }
        
        private void OnEnable()
        {
            if (_isInitiallyActive)
                TriggerActivate();
        }

        private bool HasIActivatable(List<GameObject> gameObjects)
        {
            return gameObjects != null && gameObjects.TrueForAll(obj => obj.GetComponent<IActivatable>() != null);
        }

        private void UpdateIActivatableReference()
        {
            _iActivatableObjects.ForEach(obj => _activables.Add(obj.GetComponent<IActivatable>()));
        }
        

        [Button(ButtonSizes.Large)] [HorizontalGroup("Trigger")] [HideInEditorMode]
        public void TriggerActivate()
        {
            _activables.ForEach(obj => obj.TryActivate());
            OnActivationStateChanged?.Invoke(true);
        }
        [Button(ButtonSizes.Large)] [HorizontalGroup("Trigger")] [HideInEditorMode]
        public void TriggerDeactivate()
        {
            _activables.ForEach(obj => obj.TryDeactivate());
            OnActivationStateChanged?.Invoke(false);
        }
        
        private void AddTouchingObject(GameObject gameObject)
        {
            if (!_collidingObjects.Contains(gameObject))
            {
                _collidingObjects.Add(gameObject);
            }
            if(_collidingObjects.Count == 1)
                TriggerActivate();
        }

        private void RemoveTouchingObject(GameObject gameObject)
        {
            if (_collidingObjects.Contains(gameObject))
            {
                _collidingObjects.Remove(gameObject);
            }
            
            if (_needToKeepTouchingToKeepActive && _collidingObjects.Count == 0)
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
