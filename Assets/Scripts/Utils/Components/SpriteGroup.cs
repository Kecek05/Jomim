using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    /// <summary>
    /// This component is used to group multiple sprites together. To change their properties, such as alpha.
    /// </summary>
    public class SpriteGroup : MonoBehaviour
    {
        [SerializeField] [Title("References")]
        private List<SpriteRenderer> _spriteRenderers;
        
        [SerializeField] [Title("Settings")]
        [Range(0, 1)] [OnValueChanged(nameof(ChangeAlphaOnValidate))]
        [Tooltip("Alpha value to set for all sprite renderers in this group.")]
        private float _alpha = 1f;
        
        private float _previousAlpha = -1f;

        private void ChangeAlphaOnValidate()
        {
            ChangeAlphaFromAllSpriteRenderers(_alpha);
        }
        
        private void LateUpdate()
        {
            if(_previousAlpha == _alpha) return;
            
            ChangeAlphaFromAllSpriteRenderers(_alpha);
        }

        public void ChangeAlphaFromAllSpriteRenderers(float alpha)
        {
            if (_spriteRenderers == null)
            {
                Debug.LogError($"SpriteGroup: SpriteRenderers have not been initialized. Please Get all Sprites first. {gameObject.name}");
                return;
            }
            _spriteRenderers.ForEach(spriteRenderer => spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha));
            _previousAlpha = alpha;
        }

        [ContextMenu("Get Sprite Renderers from Children")]
        private void GetSpriteRenderers()
        {
            _spriteRenderers = new List<SpriteRenderer>();
            foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                if (!_spriteRenderers.Contains(spriteRenderer))
                {
                    _spriteRenderers.Add(spriteRenderer);
                }
            }
        }
    }
}
