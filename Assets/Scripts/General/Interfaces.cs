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
}
