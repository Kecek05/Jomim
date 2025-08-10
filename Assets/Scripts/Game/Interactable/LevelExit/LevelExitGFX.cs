using System.Collections.Generic;
using DG.Tweening;
using KeceK.Utils;
using KeceK.Utils.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class LevelExitGFX : MonoBehaviour
    {
        [Title("References")] 
        [SerializeField] private ShaderAnimatorTrigger _shaderAnimatorTrigger;
        [SerializeField] [Tooltip("List of sprite that will change the color based on the exit state.")]
        private List<SpriteRenderer> _spriteRenderers;


        [Title("Color Settings")]
        [SerializeField]
        private Color _disabledColor = Color.gray;
        [SerializeField]
        private Color _enabledColor = Color.white;
        [SerializeField]
        private Ease _colorChangeEase = Ease.Linear;
        [SerializeField]
        private float _colorChangeDuration = 0.5f;
        
        private Tween _colorChangeTween;
        
        private void Start()
        {
            //Always start with the exit disabled
            ChangeColor(false, false);
            ChangeShine(false);
            GameManager.OnCanExit += GameManagerOnOnCanExit;
        }

        private void OnDestroy()
        {
            GameManager.OnCanExit -= GameManagerOnOnCanExit;
        }

        private void GameManagerOnOnCanExit()
        {
            ChangeColor(true, true);
            ChangeShine(true);
        }

        /// <summary>
        /// Call this to change the color of the exit based on whether it is enabled or not.
        /// </summary>
        /// <param name="enabled"> If should display the enabled or disabled color</param>
        /// <param name="tween"> If should tween the changing of colors</param>
        private void ChangeColor(bool enabled, bool tween)
        {
            if (tween)
            {
                _colorChangeTween?.Kill();
            
                _colorChangeTween = DOVirtual.Color(enabled ? _disabledColor : _enabledColor, enabled ? _enabledColor : _disabledColor, _colorChangeDuration, (
                    value =>
                    {
                        _spriteRenderers.ForEach(spriteRenderer => spriteRenderer.color = value);
                    }));

            } else
                _spriteRenderers.ForEach(spriteRenderer => spriteRenderer.color = enabled ? _enabledColor : _disabledColor);
        }

        /// <summary>
        /// Call this to change the shine state of the exit. This should be enabled when the exit is active and ready to be used.
        /// </summary>
        /// <param name="enabled"> If true, will enable the Shine Components</param>
        private void ChangeShine(bool enabled)
        {
            if(enabled)
                _shaderAnimatorTrigger.StartAnimation(UtilsK.ShaderProperty._ShineLocation);
            else 
                _shaderAnimatorTrigger.StopAnimation(UtilsK.ShaderProperty._ShineLocation);
        }

    }
}