using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KeceK.General;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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
        
        [SerializeField] [Required] [Title("References")] [Tooltip("List of sprite renderers that will have the shine effect. All simultaneously.")]
        private SpriteRenderer[] _spriteRenderers;
        
        [SerializeField] [Title("Settings")]
        private bool _shouldLoop = true;
        [SerializeField] [ShowIf(nameof(_shouldLoop))]
        private Ease _shineEase = Ease.InOutSine;
        [SerializeField] [ShowIf(nameof(_shouldLoop))]
        [MinMaxSlider(0, 30)]
        private Vector2 _shineDuration = new Vector2();
        [SerializeField] [ShowIf(nameof(_shouldLoop))] [Tooltip("Delay between shine loops")]
        [MinMaxSlider(0, 10)] [InfoBox("This value should be changed out of play mode")]
        private Vector2 _shineLoopDelay = new Vector2();
        
        [Space(5f)]
        [SerializeField] [ShowIf(nameof(_shouldLoop))]
        private bool _startShineOnStart = true;
        [SerializeField] [ShowIf(nameof(_startShineOnStart))]
        [MinMaxSlider(0, 10)] [InfoBox("This value should be changed out of play mode")]
        private Vector2 _startDelay = new Vector2();
        
        private float _shineLocationValue = 0f;
        private bool _loopFinished = true;
        private Coroutine _shineLoopCoroutine;
        private Tween _shineTween;
        
        private List<Material> _materials = new();
        private WaitForSeconds _shineLoopWait => new WaitForSeconds(MathK.GetRandomFloatByRange(_shineLoopDelay));

        private void Awake()
        {
            _spriteRenderers.ForEach(spriteRenderer => _materials.Add(spriteRenderer.material));
        }

        private void OnEnable()
        {
            if(_startShineOnStart)
                Invoke(nameof(StartShineLoop), MathK.GetRandomFloatByRange(_startDelay));
        }

        private void OnDisable()
        {
            StopShineLoop();
        }

        /// <summary>
        /// Do the shine animation once.
        /// </summary>
        [Button] [HideInEditorMode]
        private void DoShine()
        {
            _loopFinished = false;
            _shineTween?.Kill(false);
            _shineLocationValue = 0f;
            _shineTween = DOTween.To(
                () => _shineLocationValue, 
                x => _shineLocationValue = x, 
                1f, MathK.GetRandomFloatByRange(_shineDuration))
                .SetEase(_shineEase)
                .OnUpdate(() =>
                {
                    _materials.ForEach(material => material.SetFloat(SHINE_LOCATION_PROPERTY, _shineLocationValue));
                }).OnComplete(() =>
                {
                    ResetShineValue();
                    _loopFinished = true;
                });
        }
        
        /// <summary>
        /// Call this to start the shine loop. If the loop is already running, it will restart it.
        /// </summary>
        public void StartShineLoop()
        {
            _shouldLoop = true;
            CancelShineCoroutine();
            _loopFinished = true;
            _shineLoopCoroutine = StartCoroutine(ShineLoop());
            OnShineLoopChanged?.Invoke(true);
        }

        /// <summary>
        /// Call this to stop the shine loop. This will also reset the shine value to 0.
        /// </summary>
        public void StopShineLoop()
        {
            _shouldLoop = false;
            CancelShineCoroutine();
            ResetShineValue();
            OnShineLoopChanged?.Invoke(false);
        }

        /// <summary>
        /// Called by <see cref="StartShineLoop"/> and <see cref="StopShineLoop"/> to cancel the current shine coroutine and reset the shine value.
        /// </summary>
        private void CancelShineCoroutine()
        {
            if (_shineLoopCoroutine != null)
                StopCoroutine(_shineLoopCoroutine);
            
            _shineLoopCoroutine = null;
            _shineTween?.Kill(false);
            ResetShineValue();
        }

        /// <summary>
        /// Reset all materials shine value to 0 and the _shineLocationValue value to 0.
        /// </summary>
        private void ResetShineValue()
        {
            _materials.ForEach(material => material.SetFloat(SHINE_LOCATION_PROPERTY, 0f));
            _shineLocationValue = 0f;
        }

        /// <summary>
        /// Do the shine loop while _shouldLoop is true.
        /// </summary>
        /// <returns></returns>
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
