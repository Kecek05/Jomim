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
        [Space(5)]
        
        [SerializeField] [FoldoutGroup("Settings")]
        private bool _isEnabled = true;

        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("Force that will be applied to the RB when starts falling")] [ShowIf(nameof(_isEnabled))]
        private float _fallForce = 300f;
        
        private void OnValidate()
        {
            if (_rigidbody2D == null)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
        }

        private void FixedUpdate()
        {
            if (_isEnabled)
            {
                if (_rigidbody2D.linearVelocityY < 0f)
                {
                    _rigidbody2D.AddForceY(-_fallForce);
                }
            }
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
