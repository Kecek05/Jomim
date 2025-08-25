using System.Collections;
using KeceK.General;
using KeceK.Utils;
using KeceK.Utils.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    //Same order as int[] animations
    public enum Animations
    {
        Idle,
        Fall,
        Jump,
        Run,
        None, //At the bottom!
    }
    
    public class PlayerAnimator : MonoBehaviour
    {
        private const float ANIMATION_NORMALIZED_TRANSITION_DURATION = 0.1f;
        
        [SerializeField] [Title("References")] [Required]
        private Animator _animator;
        [SerializeField] [Required]
        private Rigidbody2D _rigidbody2D;
        [SerializeField] [Required] 
        private Transform _modelTransform;
        [SerializeField] [Required] 
        private ShaderAnimatorTrigger _shaderAnimatorTrigger;
        
        [SerializeField] [Title("Settings")] 
        [Tooltip("Minimum speed value to flip the sprite")]
        private float _flipSpriteThreshold = 1f;
        [SerializeField]
        private float _delayToTriggerChangingLevelAnimation = 0.5f;

        private Animations _currentAnimation;
        private PlayerState _currentPlayerState;

        private readonly int[] animations =
        {
            Animator.StringToHash("Idle"),
            Animator.StringToHash("Fall"),
            Animator.StringToHash("Jump"),
            Animator.StringToHash("Walk"),
        };

        private void OnEnable()
        {
            GameManager.OnChangingLevel += GameManagerOnOnChangingLevel;
        }

        private void OnDisable()
        {
            GameManager.OnChangingLevel -= GameManagerOnOnChangingLevel;
        }
        
        private void Update()
        {
            RotatePlayerGFX();
        }
        
        public void PlayerStateMachineOnOnStateChanged(PlayerState newState)
        {
            _currentPlayerState = newState;
            switch (_currentPlayerState)
            {
                case PlayerState.Idle:
                    PlayAnimation(Animations.Idle);
                    break;
                case PlayerState.Fall:
                    PlayAnimation(Animations.Fall);
                    break;
                case PlayerState.Jump:
                    PlayAnimation(Animations.Jump);
                    break;
                case PlayerState.Walk:
                    PlayAnimation(Animations.Run);
                    break;
            }
        }

        private void RotatePlayerGFX()
        {
            if (_rigidbody2D.linearVelocityX > _flipSpriteThreshold)
                _modelTransform.localScale = new Vector3(-1f, _modelTransform.localScale.y, _modelTransform.localScale.z);
            else if (_rigidbody2D.linearVelocityX < -_flipSpriteThreshold)
                _modelTransform.localScale = new Vector3(1f, _modelTransform.localScale.y,
                    _modelTransform.localScale.z);
        }

        private void PlayAnimation(Animations animation)
        {
            if(animation == _currentAnimation) return;

            _currentAnimation = animation;
            
            _animator.CrossFade(animations[(int)_currentAnimation], ANIMATION_NORMALIZED_TRANSITION_DURATION);
        }
        
        private void GameManagerOnOnChangingLevel()
        {
            StartCoroutine(DelayedChangingLevelAnimation());
        }

        private IEnumerator DelayedChangingLevelAnimation()
        {
            yield return new WaitForSeconds(_delayToTriggerChangingLevelAnimation);
            _shaderAnimatorTrigger.StartAnimation(ShaderProperty._FadeAmount);
            _shaderAnimatorTrigger.StartAnimation(ShaderProperty._HologramBlend);
        }
    }
}
