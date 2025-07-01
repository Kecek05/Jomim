using System;
using KeceK.Input;
using KeceK.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeceK.Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _playerFeet;
        [SerializeField] private PlayerMovementSO _playerMovementSO;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private DebugCubesSO _debugCubesSO;
        [Space(5)]
        [Header("Settings")]
        
        [SerializeField] [Tooltip("Layers that is possible to jump")]
        private LayerMask _jumpableLayers;

        [SerializeField] [Tooltip("Distance to check if the player is on ground")]
        private float _groundDistance = 0.2f;

        private Vector2 moveInput;
        private bool _jumpButtonHeld = false;
        private bool _canJump = false;
        
        //Coyote Time
        private float _coyoteTime = 0.2f;
        private float _coyoteTimeCounter;
        
        //Jump buffer
        private float _jumpBufferTime = 0.2f;
        private float _jumpBufferCounter;
        
        private void OnEnable()
        {
            _inputReader.OnMove += InputReaderOnOnMove;
            _inputReader.OnJumpEvent += InputReaderOnOnJumpEvent;
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

        private void OnDisable()
        {
            _inputReader.OnMove -= InputReaderOnOnMove;
        }

        private void InputReaderOnOnMove(InputAction.CallbackContext context)
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
                _canJump = true;
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
        }

        private void DoJump(float jumpForce)
        {
            if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && _canJump)
            {
                _canJump = false;
                // _rigidbody2D.AddForceY(jumpForce, ForceMode2D.Force);
                _rigidbody2D.linearVelocityY = jumpForce;
                _jumpBufferCounter = 0f;
                DebugCubeSpawner.SpawnDebugCube(_debugCubesSO.DebugCubePrefabs[0],transform.position, Quaternion.identity, 1f);
            }
        }
        
        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                _playerFeet.position,
                Vector2.down,
                _groundDistance,
                _jumpableLayers
            );
            Debug.DrawRay(_playerFeet.position, Vector2.down * _groundDistance, hit ? Color.green : Color.red);
            // Debug.Log($"Is Grounded: {hit.collider != null}");
            return hit.collider != null;
        }

        private void StopMove()
        {
            moveInput = Vector2.zero;
        }
    }
}
