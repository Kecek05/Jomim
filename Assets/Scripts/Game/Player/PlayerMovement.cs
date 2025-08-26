using System;
using System.Collections;
using DG.Tweening;
using KeceK.Input;
using KeceK.Utils.Components;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeceK.Game
{
    public class PlayerMovement : MonoBehaviour
    {
        public event Action OnJump;
        

        [SerializeField] [Title("References")] [Required]
        private PlayerMovementSO _playerMovementSO;
        [SerializeField] [Required]
        private InputReader _inputReader;
        [SerializeField] [Required]
        private Rigidbody2D _rigidbody2D;
        [SerializeField] [Required]
        private GroundCheck _groundCheck;
        [Space(10)]
        
        //Stop Moving 
        [Title("Settings")]
        [SerializeField]
        private float _stopMovingDuration = 0.2f;
        [SerializeField]
        private Ease _stopMovingEase = Ease.OutQuad;
        private Tween _stopMovingTween;
        

        [SerializeField] [FoldoutGroup("Jump Settings")]
        private float jumpDuration = 0.1f;
        [SerializeField] [FoldoutGroup("Jump Settings")]
        private Ease jumpEase = Ease.OutQuad;
        private Tween _jumpTween;
        private Vector2 _moveInput;
        private bool _jumpButtonHeld;
        private bool _canJump = true;
        
        
        //Coyote Time
        private float _coyoteTime = 0.08f;
        private float _coyoteTimeCounter;
        
        //Jump buffer
        private float _jumpBufferTime = 0.07f;
        private float _jumpBufferCounter;
        
        //Jump Cooldown
        private float _cooldownBetweenJumps = 0.5f;
        private WaitForSeconds _waitForSecondsJumpCooldownCoroutine;
        private Coroutine _jumpCooldownCoroutine;
        
        
        //Debugs
        public bool CanJump => _canJump;
        public float CoyoteTimeCounter => _coyoteTimeCounter;
        public float JumpBufferCounter => _jumpBufferCounter;
        public float CooldownBetweenJumps => _cooldownBetweenJumps;

        private void Awake()
        {
            _waitForSecondsJumpCooldownCoroutine = new WaitForSeconds(_cooldownBetweenJumps);
        }

        private void OnEnable()
        {
            _inputReader.OnMoveEvent += InputReaderOnOnMoveEvent;
            _inputReader.OnJumpEvent += InputReaderOnOnJumpEvent;
        }
        private void OnDisable()
        {
            _inputReader.OnMoveEvent -= InputReaderOnOnMoveEvent;
            _inputReader.OnJumpEvent -= InputReaderOnOnJumpEvent;
        }
        
        private void InputReaderOnOnMoveEvent(InputAction.CallbackContext context)
        {
            if (context.performed)
                _moveInput = context.ReadValue<Vector2>();
            else if (context.canceled)
                StopMove();
        }

        private void InputReaderOnOnJumpEvent(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _jumpButtonHeld = true;
            }
            else if (context.canceled)
            {
                _jumpButtonHeld = false;
                _coyoteTimeCounter = 0f;
            }
        }

        private void Update()
        {
            if (_groundCheck.IsGrounded())
            {
                _coyoteTimeCounter = _coyoteTime;
                // _canJump = true;
            }
            else if(_coyoteTimeCounter > 0f)
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }

            if (_jumpButtonHeld)
            {
                _jumpBufferCounter = _jumpBufferTime;
            }
            else if (_jumpBufferCounter > 0f)
            {
                _jumpBufferCounter -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            if (_rigidbody2D.bodyType != RigidbodyType2D.Dynamic)
            {
                _stopMovingTween?.Kill();
                _jumpTween?.Kill();
                return;
            }
            
            DoMove(_playerMovementSO.speed);
            DoJump(_playerMovementSO.jumpHigh);
        }
        
        /// <summary>
        /// Moves the object with the provided Speed
        /// </summary>
        private void DoMove(float speed)
        {
            if (_moveInput.x == 0)
            {
                if (!_stopMovingTween.IsActive())
                {
                    _stopMovingTween = DOTween.To(
                        () => _rigidbody2D.linearVelocityX,
                        x => _rigidbody2D.linearVelocityX = x,
                        0f,
                        _stopMovingDuration
                    ).SetEase(_stopMovingEase);
                }
                return;
            }
            
            //Move
            KillStopTween();
            
            float velocityX = _moveInput.x * speed;
            
            _rigidbody2D.AddForceX(velocityX);
            _rigidbody2D.linearVelocityX = Mathf.Clamp(_rigidbody2D.linearVelocityX, -_playerMovementSO.maxSpeed, _playerMovementSO.maxSpeed);
        }

        /// <summary>
        /// Jump the player if possible.
        /// </summary>
        /// <param name="jumpHigh"> the target Y velocity that the player will be when jumped</param>
        private void DoJump(float jumpHigh)
        {
            if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && _canJump)
            {
                OnJump?.Invoke();
                _canJump = false;
                TriggerJumpCooldown();
                _jumpBufferCounter = 0f;
                _coyoteTimeCounter = 0f;

                _jumpTween?.Kill();
                
                _jumpTween = DOTween.To(
                        () => _rigidbody2D.linearVelocity.y,
                        y => _rigidbody2D.linearVelocityY = y,
                        jumpHigh,
                        jumpDuration
                    )
                    .SetEase(jumpEase);
            }
        }

        /// <summary>
        /// Call this to stop <see cref="_jumpCooldownCoroutine"/> if needed and start <see cref="JumpCooldown"/>
        /// </summary>
        private void TriggerJumpCooldown()
        {
            if(_jumpCooldownCoroutine != null)
                StopCoroutine(_jumpCooldownCoroutine);
            _jumpCooldownCoroutine = StartCoroutine(JumpCooldown());
        }

        /// <summary>
        /// Called to kill the <see cref="_stopMovingTween"/> if not null or is Active
        /// </summary>
        private void KillStopTween()
        {
            if (_stopMovingTween != null && _stopMovingTween.IsActive())
                _stopMovingTween.Kill();
        }

        private IEnumerator JumpCooldown()
        {
            yield return _waitForSecondsJumpCooldownCoroutine;
            _canJump = true;
            _jumpCooldownCoroutine = null;
        }
        
        private void StopMove()
        {
            _moveInput = Vector2.zero;
        }
    }
}
