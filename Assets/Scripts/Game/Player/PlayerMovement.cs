using System;
using System.Collections;
using DG.Tweening;
using KeceK.Input;
using KeceK.Plugins.EditableAssetAttribute;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeceK.Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("References")] [Required]
        private Transform _playerFeet;
        [CreateEditableAsset] [SerializeField] [FoldoutGroup("References")] [Required]
        private PlayerMovementSO _playerMovementSO;
        [SerializeField] [FoldoutGroup("References")] [Required]
        private InputReader _inputReader;
        [SerializeField] [FoldoutGroup("References")] [Required]
        private Rigidbody2D _rigidbody2D;
        [Space(10)]
        
        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("Layers that is possible to jump")] 
        private LayerMask _jumpableLayers;
        [SerializeField] [FoldoutGroup("Settings")] [Tooltip("Radius to check if the player is on ground")]
        private float _isGroundedRadius = 0.5f;
        [Space(10)]
        

        [SerializeField] [FoldoutGroup("Jump Settings")]
        private float jumpDuration = 0.2f;
        [SerializeField] [FoldoutGroup("Jump Settings")]
        private Ease jumpEase = Ease.OutQuad;
        [Space(10)]
        
        private Tween jumpTween;
        private Vector2 moveInput;
        private bool _jumpButtonHeld = false;
        private bool _canJump = true;
        
        //Coyote Time
        private float _coyoteTime = 0.15f;
        private float _coyoteTimeCounter;
        
        //Jump buffer
        private float _jumpBufferTime = 0.05f;
        private float _jumpBufferCounter;
        
        //Jump Cooldown
        private float _cooldownBetweenJumps = 0.2f;
        private WaitForSeconds _waitForJump;
        private Coroutine _jumpCoroutine;
        
        //Debugs
        public bool CanJump => _canJump;
        public float CoyoteTimeCounter => _coyoteTimeCounter;
        public float JumpBufferCounter => _jumpBufferCounter;
        public float CooldownBetweenJumps => _cooldownBetweenJumps;

        private void Awake()
        {
            _waitForJump = new WaitForSeconds(_cooldownBetweenJumps);
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

        private void InputReaderOnOnMoveEvent(InputAction.CallbackContext context)
        {
            if (context.performed)
                moveInput = context.ReadValue<Vector2>();
            else if (context.canceled)
                StopMove();
        }

        private void Update()
        {
            if (IsGrounded())
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
            DoMove(_playerMovementSO.speed);
            DoJump(_playerMovementSO.jumpForce);
        }
        
        /// <summary>
        /// Moves the object with the provided Speed
        /// </summary>
        private void DoMove(float speed)
        {
            Vector2 velocity = new Vector2(moveInput.x * speed, _rigidbody2D.linearVelocity.y);
            
            _rigidbody2D.AddForceX(velocity.x);
            _rigidbody2D.linearVelocityX = Mathf.Clamp(_rigidbody2D.linearVelocityX, -_playerMovementSO.maxSpeed, _playerMovementSO.maxSpeed);
            // Debug.Log(_rigidbody2D.linearVelocity);
        }

        private void DoJump(float jumpForce)
        {
            if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && _canJump)
            {
                _canJump = false;
                // _rigidbody2D.linearVelocityY = jumpForce;
                if(_jumpCoroutine != null)
                    StopCoroutine(_jumpCoroutine);
                _jumpCoroutine = StartCoroutine(JumpCooldown());
                
                // _rigidbody2D.AddForceY(jumpForce, ForceMode2D.Force);
                _jumpBufferCounter = 0f;
                _coyoteTimeCounter = 0f;
                
                // Cancel existing jump tweens
                jumpTween?.Kill();

                // Start new tween on velocity.y
                float startY = _rigidbody2D.linearVelocity.y;

                jumpTween = DOTween.To(
                        () => _rigidbody2D.linearVelocity.y,
                        y => _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, y),
                        jumpForce,
                        jumpDuration
                    )
                    .SetEase(jumpEase);
            }
        }
        
        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(
                _playerFeet.position,
                _isGroundedRadius,
                _jumpableLayers
            ) != null;
        }

        private IEnumerator JumpCooldown()
        {
            yield return _waitForJump;
            _canJump = true;
            _jumpCoroutine = null;
        }
        
        #if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (_playerFeet != null)
            {
                Gizmos.color = IsGrounded() ? Color.green : Color.red;
                Gizmos.DrawWireSphere(_playerFeet.position, _isGroundedRadius);
            }
        }
        #endif
        
        private void StopMove()
        {
            moveInput = Vector2.zero;
        }
    }
}
