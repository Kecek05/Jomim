using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("Layers that is possible to jump")] 
        private LayerMask _jumpableLayers;
        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("Radius to check if the player is on ground")]
        private float _isGroundedRadius = 0.5f;
        [SerializeField] [FoldoutGroup("References")] [Required]
        private Transform _checkTransform;
        
        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(
                _checkTransform.position,
                _isGroundedRadius,
                _jumpableLayers
            ) != null;
        }
        
#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (_checkTransform != null)
            {
                Gizmos.color = IsGrounded() ? Color.green : Color.red;
                Gizmos.DrawWireSphere(_checkTransform.position, _isGroundedRadius);
            }
        }
#endif
    }
}