using System;
using System.Collections;
using DG.Tweening;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class ShineAnimator : MonoBehaviour
    {
        /// <summary>
        /// Called when the shine loop state changes. True = Starts, False = Stops.
        /// </summary>
        public event Action<bool> OnShineLoopChanged; 
        
        private readonly string SHINE_LOCATION_PROPERTY = "_ShineLocation";
        
        [SerializeField] [Required] [FoldoutGroup("References")]
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField] [FoldoutGroup("Settings")]
        private bool _shouldLoop = true;
        [SerializeField] [FoldoutGroup("Settings")] [ShowIf(nameof(_shouldLoop))]
        private Ease _shineEase = Ease.InOutSine;
        [SerializeField] [FoldoutGroup("Settings")] [ShowIf(nameof(_shouldLoop))]
        [MinMaxSlider(0, 30)]
        private Vector2 _shineDuration = new Vector2();
        [SerializeField] [FoldoutGroup("Settings")] [ShowIf(nameof(_shouldLoop))] [Tooltip("Delay between shine loops")]
        [MinMaxSlider(0, 10)] [InfoBox("This value should be changed out of play mode")]
        private Vector2 _shineLoopDelay = new Vector2();
        
        [Space(5f)]
        [SerializeField] [FoldoutGroup("Settings")] [ShowIf(nameof(_shouldLoop))]
        private bool _startShineOnStart = true;
        [SerializeField] [FoldoutGroup("Settings")] [ShowIf(nameof(_startShineOnStart))]
        [MinMaxSlider(0, 10)] [InfoBox("This value should be changed out of play mode")]
        private Vector2 _startDelay = new Vector2();
        
        private float _shineLocationValue = 0f;
        private bool _loopFinished = true;
        private Coroutine _shineLoopCoroutine;
        private Tween _shineTween;
        
        private Material _material => _spriteRenderer.material;
        private WaitForSeconds _shineLoopWait => new WaitForSeconds(MathK.GetRandomFloatByRange(_shineLoopDelay));
        
        private void OnEnable()
        {
            if(_startShineOnStart)
                Invoke(nameof(StartShineLoop), MathK.GetRandomFloatByRange(_startDelay));
        }

        private void OnDisable()
        {
            StopShineLoop();
        }

        private void DoShine()
        {
            _loopFinished = false;
            _shineTween?.Kill(false);
            _shineTween = DOTween.To(
                () => _material.GetFloat(SHINE_LOCATION_PROPERTY), 
                x => _material.SetFloat(SHINE_LOCATION_PROPERTY, x), 
                1f, MathK.GetRandomFloatByRange(_shineDuration))
                .SetEase(_shineEase).OnComplete(() =>
                {
                    _material.SetFloat(SHINE_LOCATION_PROPERTY, 0);
                    _loopFinished = true;
                });
        }
        
        public void StartShineLoop()
        {
            _shouldLoop = true;
            CancelShineCoroutine();
            _loopFinished = true;
            _shineLoopCoroutine = StartCoroutine(ShineLoop());
            OnShineLoopChanged?.Invoke(true);
        }

        public void StopShineLoop()
        {
            _shouldLoop = false;
            CancelShineCoroutine();
            _material.SetFloat(SHINE_LOCATION_PROPERTY, 0);
            OnShineLoopChanged?.Invoke(false);
        }

        private void CancelShineCoroutine()
        {
            if (_shineLoopCoroutine != null)
                StopCoroutine(_shineLoopCoroutine);
            
            _shineLoopCoroutine = null;
            _shineTween?.Kill(false);
            _material.SetFloat(SHINE_LOCATION_PROPERTY, 0);
        }

        private IEnumerator ShineLoop()
        {
            while (_shouldLoop)
            {
                if (_loopFinished)
                {
                    yield return _shineLoopWait;
                    DoShine();
                }
                else
                    yield return null;
                
            }
        }
    }
}
