using KeceK.General;
using LayerLab.ArtMaker;
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
        private const string ANIMATION_SPEED = "speed";
        
        [SerializeField] [FoldoutGroup("References")] [Required]
        private Animator _animator;
        [SerializeField] [FoldoutGroup("References")] [Required]
        private Rigidbody2D _rigidbody2D;
        [SerializeField] [FoldoutGroup("References")] [Required]
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private PartsManager _partsManager;
        [SerializeField] private Transform _modelTransform;
        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("Minimum speed value to flip the sprite")]
        private float flipSpriteThreshold = 1f;

        private Animations _currentAnimation;

        private readonly int[] animations =
        {
            Animator.StringToHash("Idle"),
            Animator.StringToHash("Fall"),
            Animator.StringToHash("Jump"),
            Animator.StringToHash("Walk"),
        };

        // private readonly string[] animations =
        // {
        //     "Idle",
        //     "Fall",
        //     "Jump",
        //     "Run",
        // };

        private void Update()
        {
            // _animator.SetFloat(ANIMATION_SPEED, _rigidbody2D.linearVelocity.magnitude * 2f);

            if (_rigidbody2D.linearVelocityX > flipSpriteThreshold)
                _modelTransform.localScale = new Vector3(-1f, _modelTransform.localScale.y, _modelTransform.localScale.z);
            else if (_rigidbody2D.linearVelocityX < -flipSpriteThreshold)
                _modelTransform.localScale = new Vector3(1f, _modelTransform.localScale.y,
                    _modelTransform.localScale.z);
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
                    PlayAnimation(Animations.Run);
                    break;
            }
        }

        private void PlayAnimation(Animations animation)
        {
            if(animation == _currentAnimation) return;

            _currentAnimation = animation;
            
            // _partsManager.PlayAnimation(animations[(int)_currentAnimation]);
            _animator.CrossFade(animations[(int)_currentAnimation], 0.1f);
        }
    }
}
