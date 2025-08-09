using System;
using System.Collections.Generic;
using DG.Tweening;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    /// <summary>
    /// Use this component to animate any shader property over time or trigger.
    /// </summary>
    public class ShaderAnimator : MonoBehaviour
    {
        public class AnimationEventData
        {
            public int AnimationID;
            public bool IsLooping;
        }
        
        [Serializable]
        private class ShaderAnimationData
        {
            /// <summary>
            /// Called when the animation loop state changes. True = Starts, False = Stops.
            /// Pass the AnimationID too.
            /// </summary>
            public event Action<AnimationEventData> OnAnimationLoopChanged;
            
            [FoldoutGroup("$PropertyName")] [Tooltip("Name of the shader property to animate. This should match the name in the shader.")]
            public string PropertyName;
            [FoldoutGroup("$PropertyName")] [Tooltip("ID of the animation. Used to identify the animation in the list. This SHOULD be unique for each animation.")]
            public int AnimationID;
            [FoldoutGroup("$PropertyName")]
            public Ease Ease = Ease.Linear;
            [Space(10f)]
            
            [FoldoutGroup("$PropertyName")]
            [Required] [Tooltip("List of sprite renderers that will have this animation effect. All simultaneously.")]
            public List<SpriteRenderer> _spriteRenderers = new();
            [Space(10f)]
            
            //Duration
            [FoldoutGroup("$PropertyName")]
            public bool IsRandomDuration;
            [HideIf(nameof(IsRandomDuration))] [FoldoutGroup("$PropertyName")]
            public float Duration;
            [MinMaxSlider(0,30)] [ShowIf(nameof(IsRandomDuration))] [FoldoutGroup("$PropertyName")]
            public Vector2 RandomDuration = new Vector2(1f, 5f);
            [Space(5f)]
            
            //Start Value
            [FoldoutGroup("$PropertyName")]
            public bool IsRandomStartValue;
            [HideIf(nameof(IsRandomStartValue))] [FoldoutGroup("$PropertyName")]
            public float StartValue;
            [MinMaxSlider(0, 1)] [ShowIf(nameof(IsRandomStartValue))] [FoldoutGroup("$PropertyName")]
            public Vector2 RandomStartValue = new Vector2(0f, 0.5f);
            [Space(5f)]
            
            //End Value
            [FoldoutGroup("$PropertyName")]
            public bool IsRandomEndValue;
            [HideIf(nameof(IsRandomEndValue))] [FoldoutGroup("$PropertyName")]
            public float EndValue;
            [MinMaxSlider(1, 0)] [ShowIf(nameof(IsRandomEndValue))] [FoldoutGroup("$PropertyName")]
            public Vector2 RandomEndValue = new Vector2(1f, 0.7f);
            [Space(10f)]
            [FoldoutGroup("$PropertyName")]
            public bool ShouldLoop = false;
            
            [FoldoutGroup("$PropertyName")] [OnValueChanged(nameof(StartAnimationOnStartValidate))]
            public bool StartAnimationOnStart = true;
            [FoldoutGroup("$PropertyName")] [ShowIf(nameof(StartAnimationOnStart))]
            public bool IsRandomStartAnimationOnStart;
            [ShowIf(nameof(IsRandomStartAnimationOnStart))]
            [MinMaxSlider(0, 10)] [InfoBox("This value should be changed out of play mode")]
            public Vector2 RandomStartDelay = new Vector2();
            [HideIf(nameof(IsRandomStartAnimationOnStart))] [ShowIf(nameof(StartAnimationOnStart))]
            public float StartDelay = 0f;
            
            private float _shaderPropertyValue = 0f;
            private bool _loopFinished = true;
            private Tween _animationTween;
            
            private List<Material> _materials = new();
            private WaitForSeconds _animationLoopWait => new WaitForSeconds(StartAnimationOnStart ? MathK.GetRandomFloatByRange(RandomStartDelay) : StartDelay);

            private void StartAnimationOnStartValidate()
            {
                if (!StartAnimationOnStart)
                    IsRandomStartAnimationOnStart = false;
            }
            
            public void InitializeMaterials()
            {
                _spriteRenderers = new();
                _spriteRenderers.ForEach(spriteRenderer => _materials.Add(spriteRenderer.material));
            }
            
            
        }
        
        [SerializeField] [Title("Settings")]
        private List<ShaderAnimationData> _shaderAnimations = new();
        
    }
}
