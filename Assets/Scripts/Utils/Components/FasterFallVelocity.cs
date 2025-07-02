using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    /// <summary>
    /// Component added to an object to make it fall faster. Requires an Rigidbody2D in the same object.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class FasterFallVelocity : MonoBehaviour
    {
        [SerializeField] [Required] [FoldoutGroup("References")]
        private Rigidbody2D _rigidbody2D;
        [Space(5f)]
        
        [SerializeField] [FoldoutGroup("Settings")] [OnValueChanged(nameof(ResetHaveStoppingDistance))]
        private bool _isEnabled = true;
        [SerializeField] [FoldoutGroup("Settings")] [ShowIf(nameof(_isEnabled))]
        private bool _haveStoppingDistance;
        [Space(2f)]
        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("Force that will be applied to the RB when starts falling")] [ShowIf(nameof(_isEnabled))]
        private float _fallForce = 300f;
        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("Minimum force to start adding negative force to Y")] [ShowIf(nameof(_isEnabled))]
        private float velocityYThreshold = 0.1f;
        [Space(1f)]

        [SerializeField] [FoldoutGroup("Stopping Distance Settings")] [Tooltip("Transform to start the raycast")] 
        [ShowIf(nameof(_isEnabled))] [ShowIf(nameof(_haveStoppingDistance))]
        private Transform _stoppingDistanceStartTransform;
        
        [SerializeField] [FoldoutGroup("Stopping Distance Settings")] [Tooltip("Layers to stop adding negative Y velocity when getting close")] 
        [ShowIf(nameof(_isEnabled))] [ShowIf(nameof(_haveStoppingDistance))]
        private LayerMask _stoppingDistanceLayerMask;
        
        [SerializeField] [FoldoutGroup("Stopping Distance Settings")] [Tooltip("Layers to stop adding negative Y velocity when getting close")] 
        [ShowIf(nameof(_isEnabled))] [ShowIf(nameof(_haveStoppingDistance))]
        private float _rayLength = 2.5f;
        
        private RaycastHit2D _raycastHit; //cache
        
        private void OnValidate()
        {
            if (_rigidbody2D == null)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
        }
        private void ResetHaveStoppingDistance()
        {
            _haveStoppingDistance = false;
        }

        private void FixedUpdate()
        {
            if (_isEnabled)
            {
                if(ShouldStopAddingForce()) return;
                
                if (_rigidbody2D.linearVelocityY < velocityYThreshold)
                {
                    _rigidbody2D.AddForceY(-_fallForce);
                }
            }
        }

        /// <summary>
        /// Call this to check if the object is too close to the colliding layer.
        /// </summary>
        /// <returns>
        /// True: colliding with the provided layer
        /// False: Not colliding with the provided layer
        /// </returns>
        private bool ShouldStopAddingForce()
        {
            if (!_haveStoppingDistance) return false;
            
            _raycastHit = Physics2D.Raycast(_stoppingDistanceStartTransform.position, Vector2.down, _rayLength, _stoppingDistanceLayerMask);
            // Debug.DrawRay(
            //     _stoppingDistanceStartTransform.position,
            //     Vector2.down * _rayLength,
            //     Color.red
            // );   
            return _raycastHit.collider != null;
        }

        /// <summary>
        /// Call this to enable or disable this component
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetIsEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }
    }
}
