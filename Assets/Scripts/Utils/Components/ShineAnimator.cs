using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class ShineAnimator : MonoBehaviour
    {
        private readonly string SHINE_LOCATION_PROPERTY = "_ShineLocation";
        
        [SerializeField] [Required] [FoldoutGroup("References")]
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField] [FoldoutGroup("Settings")]
        private Ease _shineEase = Ease.InOutSine;
        [SerializeField] [FoldoutGroup("Settings")]
        private float _shineDuration = 1f;
        [SerializeField] [FoldoutGroup("Settings")]
        private float _startDelay = 0f;
    
        private float _shineLocationValue = 0f;
        
        private Material _material => _spriteRenderer.material;

        private void Start()
        {
            Invoke(nameof(ShineLoop), _startDelay);
        }

        private void ShineLoop()
        {
            DOTween.To(
                () => _material.GetFloat(SHINE_LOCATION_PROPERTY), 
                x => _material.SetFloat(SHINE_LOCATION_PROPERTY, x), 
                1f, _shineDuration)
                .SetEase(_shineEase).OnComplete(() =>
                {
                    _material.SetFloat(SHINE_LOCATION_PROPERTY, 0);
                    ShineLoop();
                });
        }
    }
}
