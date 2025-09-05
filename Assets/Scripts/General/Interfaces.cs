using System;
using System.Collections.Generic;
using UnityEngine;

namespace KeceK.General
{
    /// <summary>
    /// Used at the state machine of the player.
    /// </summary>
    public interface IState
    {
        public PlayerState State { get; } // The state enum that this state represents
        public void Enter(); // Code that runs when we first enter the state

        public void Execute(); // per-frame logic, include condition to transition to a new state

        public void Exit(); // Code that runs when we exit the state
    }
    
    /// <summary>
    /// Interface for components that can take damage and potentially die.
    /// </summary>
    public interface IDamageable
    {
        public bool IsDead { get; }
        public void TakeDamage(float damage);

        public void Die();
    }

    /// <summary>
    /// Interface for components that can be collected.
    /// </summary>
    public interface ICollectable
    {
        public void Collect();
    }

    /// <summary>
    /// Interface for components that can be identified based on the PlayerType.
    /// </summary>
    public interface IIdentifier
    {
        /// <summary>
        /// Called to trigger the identification of the component.
        /// </summary>
        public void TriggerIdentify(PlayerType playerType);
    }
    
    /// <summary>
    /// Interface for components that can activate or deactivate Activatables. Always need a ref of <see cref="IActivatable"/>.
    /// </summary>
    public interface IActivator
    {
        public List<IActivatable> Activables { get; }
        /// <summary>
        /// Try to activate the IActivatable object.
        /// </summary>
        public void TriggerActivate();
        /// <summary>
        /// Try to deactivate the IActivatable object.
        /// </summary>
        public void TriggerDeactivate();
        
        public event Action<bool> OnActivationStateChanged;
    }
    
    /// <summary>
    /// Interface for components that can be activated or deactivated.
    /// </summary>
    public interface IActivatable
    {
        public bool IsActive { get; }
        
        /// <summary>
        /// Call this to try to activate.
        /// </summary>
        /// <returns>If it was successfully</returns>
        public bool TryActivate();
        /// <summary>
        /// Call this to try to deactivate.
        /// </summary>
        /// <returns>If it was successfully</returns>
        public bool TryDeactivate();
        
        public event Action<bool> OnActivatableStateChanged;
    }

    /// <summary>
    /// Interface for components that can be touched by other GameObjects.
    /// </summary>
    public interface ITouchable
    {
        public void Touch(GameObject whoTouchedMe);
    }

    /// <summary>
    /// Interface for components that can be untouched by other GameObjects.
    /// </summary>
    public interface IUnTouchable
    {
        public void Untouch(GameObject whoUntouchedMe);
    }
}
