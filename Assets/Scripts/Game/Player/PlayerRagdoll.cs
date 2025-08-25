using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.U2D.IK;

namespace KeceK.Game
{
    public class PlayerRagdoll : MonoBehaviour
    {
        [Title("References")] [SerializeField] [Tooltip("Should be the GFX Object")]
        private GameObject ragdollRoot;
        [SerializeField] private List<Collider2D> _colliders;
        [SerializeField] private List<HingeJoint2D> _hingeJoints;
        [SerializeField] private List<Rigidbody2D> _rigidbodies;
        [SerializeField] private List<LimbSolver2D> _limbSolvers;
        [Space(10f)]
        [Title("Player References")]
        [SerializeField] private Animator _animator;
        [SerializeField] private IKManager2D _ikManager;
        [SerializeField] private Collider2D _playerCollider;
        [SerializeField] private Rigidbody2D _playerRigidbody;

        private void Awake()
        {
            DisableRagdoll();
        }


        [ContextMenu("Get Ragdoll References")]
        private void GetRagdollReferences()
        {
            if (ragdollRoot == null)
            {
                Debug.LogError("No Ragdoll root found!");
                return;
            }
            
            _colliders = new List<Collider2D>();
            _hingeJoints = new List<HingeJoint2D>();
            _rigidbodies = new List<Rigidbody2D>();
            _limbSolvers = new List<LimbSolver2D>();
            foreach (var collider in ragdollRoot.GetComponentsInChildren<Collider2D>())
            {
                _colliders.Add(collider);
            }
            foreach (var hingeJoint in ragdollRoot.GetComponentsInChildren<HingeJoint2D>())
            {
                _hingeJoints.Add(hingeJoint);
            }
            foreach (var rigidbody in ragdollRoot.GetComponentsInChildren<Rigidbody2D>())
            {
                _rigidbodies.Add(rigidbody);
            }
            foreach (var limbSolver in ragdollRoot.GetComponentsInChildren<LimbSolver2D>())
            {
                _limbSolvers.Add(limbSolver);
            }
        }
        
        [Button]
        public void EnableRagdoll()
        {
            ToggleRagdoll(true);
        }

        [Button]
        public void DisableRagdoll()
        {
            ToggleRagdoll(false);
        }
        
        private void ToggleRagdoll(bool isActive)
        {
            _animator.enabled = !isActive;
            _ikManager.weight = isActive ? 0f : 1f;
            _ikManager.enabled = !isActive;
            _limbSolvers.ForEach(obj =>
            {
                obj.weight = isActive ? 0f : 1f;
                obj.enabled = !isActive;
            });
            
            _playerRigidbody.linearVelocity = Vector2.zero;
            _rigidbodies.ForEach(obj => obj.linearVelocity = Vector2.zero);
            _playerRigidbody.bodyType = isActive ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
            _playerCollider.enabled = !isActive;
            
            _rigidbodies.ForEach(obj => obj.simulated = isActive);
            _colliders.ForEach(obj => obj.enabled = isActive);
            _hingeJoints.ForEach(obj => obj.enabled = isActive);
        }
    }
}
