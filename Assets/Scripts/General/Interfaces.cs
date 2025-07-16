using System;
using UnityEngine;

namespace KeceK.General
{
    public interface IState
    {
        public PlayerState State { get; } // The state enum that this state represents
        public void Enter(); // Code that runs when we first enter the state

        public void Execute(); // per-frame logic, include condition to transition to a new state

        public void Exit(); // Code that runs when we exit the state
    }
    
    public interface IDamageable
    {
        public bool IsDead { get; }
        public void TakeDamage(float damage);

        public void Die();
    }

    public interface ICollectable
    {
        public void Collect();
    }

    public interface IIdentifier
    {
        /// <summary>
        /// Called to trigger the identification of the component.
        /// </summary>
        public void TriggerIdentify(PlayerType playerType);
    }

    public interface IActivator
    {
        public IActivatable Activable { get; }
        /// <summary>
        /// Try to activate the IActivatable object.
        /// </summary>
        public void TriggerActivate();
        /// <summary>
        /// Try to deactivate the IActivatable object.
        /// </summary>
        public void TriggerDeactivate();
    }
    
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
    }
}
