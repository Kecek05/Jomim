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
        [SerializeField] private List<Collider2D> _ragdollColliders;
        [SerializeField] private List<HingeJoint2D> _ragdollHingeJoints;
        [SerializeField] private List<Rigidbody2D> _ragdollRigidbodies;
        [SerializeField] private List<LimbSolver2D> _ragdollLimbSolvers;
        [SerializeField] private Rigidbody2D _ragdollSpineRigidbody;
        [Space(10f)]
        [Title("Player References")]
        [SerializeField] private Animator _animator;
        [SerializeField] private IKManager2D _ikManager;
        [SerializeField] private Collider2D _playerCollider;
        [SerializeField] private Rigidbody2D _playerRigidbody;

        private readonly float _forceMultiplierToMainSpine = 10f;
        
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
            
            _ragdollColliders = new List<Collider2D>();
            _ragdollHingeJoints = new List<HingeJoint2D>();
            _ragdollRigidbodies = new List<Rigidbody2D>();
            _ragdollLimbSolvers = new List<LimbSolver2D>();
            foreach (var collider in ragdollRoot.GetComponentsInChildren<Collider2D>())
            {
                _ragdollColliders.Add(collider);
            }
            foreach (var hingeJoint in ragdollRoot.GetComponentsInChildren<HingeJoint2D>())
            {
                _ragdollHingeJoints.Add(hingeJoint);
            }
            foreach (var rigidbody in ragdollRoot.GetComponentsInChildren<Rigidbody2D>())
            {
                _ragdollRigidbodies.Add(rigidbody);
            }
            foreach (var limbSolver in ragdollRoot.GetComponentsInChildren<LimbSolver2D>())
            {
                _ragdollLimbSolvers.Add(limbSolver);
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
            Vector2 playerLinearVelocity = _playerRigidbody.linearVelocity;
            TogglePlayerObjectsAndRagdollLimbSolvers(isActive);
            ToggleRagdollObjects(isActive);
            
            if(isActive)
                _ragdollSpineRigidbody.AddForce(playerLinearVelocity * _forceMultiplierToMainSpine, ForceMode2D.Impulse);
        }

        
        private void TogglePlayerObjectsAndRagdollLimbSolvers(bool isActive)
        {
            _animator.enabled = !isActive;
            _ikManager.weight = isActive ? 0f : 1f;
            _ikManager.enabled = !isActive;
            _ragdollLimbSolvers.ForEach(obj =>
            {
                obj.weight = isActive ? 0f : 1f;
                obj.enabled = !isActive;
            });
            
            _playerRigidbody.linearVelocity = Vector2.zero;
            _playerRigidbody.bodyType = isActive ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
            _playerCollider.enabled = !isActive;
        }

        private void ToggleRagdollObjects(bool isActive)
        {
            _ragdollRigidbodies.ForEach(obj => obj.linearVelocity = Vector2.zero);
            _ragdollRigidbodies.ForEach(obj => obj.simulated = isActive);
            _ragdollColliders.ForEach(obj => obj.enabled = isActive);
            _ragdollHingeJoints.ForEach(obj => obj.enabled = isActive);
        }
    }
}
