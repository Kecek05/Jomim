using KeceK.General;
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
        Walk,
        None, //At the bottom!
    }
    
    public class PlayerAnimator : MonoBehaviour
    {
        private const string ANIMATION_SPEED = "speed";
        
        [SerializeField] [FoldoutGroup("References")] [Required]
        private Animator _animator;
        [SerializeField] [FoldoutGroup("References")] [Required]
        private Rigidbody2D _rigidbody2D;
        [SerializeField] [FoldoutGroup("References")] [Required]
        private SpriteRenderer _spriteRenderer;

        private Animations _currentAnimation;

        private readonly int[] animations =
        {
            Animator.StringToHash("Idle"),
            Animator.StringToHash("Fall"),
            Animator.StringToHash("Jump"),
            Animator.StringToHash("Walk"),
        };

        private void Update()
        {
            _animator.SetFloat(ANIMATION_SPEED, _rigidbody2D.linearVelocity.magnitude * 2f);

            if (_rigidbody2D.linearVelocity.magnitude > 0f)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipX = true;
        }
        
        public void PlayerStateMachineOnOnStateChanged(PlayerState newState)
        {
            switch (newState)
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
                    PlayAnimation(Animations.Walk);
                    break;
            }
        }

        private void PlayAnimation(Animations animation)
        {
            if(animation == _currentAnimation) return;

            _currentAnimation = animation;
            
            _animator.CrossFade(animations[(int)_currentAnimation], 0f);
        }
    }
}
