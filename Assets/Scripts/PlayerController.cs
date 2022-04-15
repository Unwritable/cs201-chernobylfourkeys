// Code written by Jamie Adaway, using Tutorial by Comp-3 Interactive
// Tutorial Link: https://www.youtube.com/playlist?list=PLfhbBaEcybmgidDH3RX_qzFM0mIxWJa21

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    private bool _isSprinting => _canSprint && Input.GetKey(_sprintKey);
    private bool _shouldJump => Input.GetKeyDown(_jumpKey) && _characterController.isGrounded;
    private bool _shouldCrouch => Input.GetKeyDown(_crouchKey) && !_duringCrouchAnim && _characterController.isGrounded;

    [Header("Controller Options")]
    [SerializeField] private bool _canSprint = true;
    [SerializeField] private bool _canJump = true;
    [SerializeField] private bool _canCrouch = true;

    [Header("Controls")]
    [SerializeField] private KeyCode _sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _crouchKey = KeyCode.LeftControl;

    [Header("Movement Properties"), Tooltip("Properties controlling Player Movement settings.")]
    [SerializeField] private float _walkSpeed = 3.0f;
    [SerializeField] private float _sprintSpeed = 6.0f;
    [SerializeField] private float _crouchSpeed = 1.5f;
    [SerializeField] private float _gravity = 30.0f;
    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _crouchingHeight = 0.5f;
    [SerializeField] private float _standingHeight = 2f;
    [SerializeField] private float _timeToCrouch = 0.25f;
    [SerializeField] private Vector3 _crouchingCentre = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 _standingCentre = Vector3.zero;
    private bool _isCrouching;
    private bool _duringCrouchAnim;

    [Header("View Properties"), Tooltip("Properties controlling Camera View/Look settings.")]
    [SerializeField, Range(1, 10)] private float _viewSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float _viewSpeedY = 2.0f;
    [SerializeField, Range(0, 180)] private float _upperViewLimit = 80.0f;
    [SerializeField, Range(0, 180)] private float _lowerViewLimit = 80.0f;

    private Camera _playerCamera;
    private CharacterController _characterController;

    private Vector3 _moveDirection;
    private Vector2 _currentInput;

    private float _rotationX = 0.0f;


    void Awake()
    {
        _playerCamera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // NOTE: If utilising something physics-based, try to keep it to a FixedUpdate() instead of Update()
    void Update()
    {
        if(CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if (_canJump)
                HandleJump();

            if(_canCrouch)
                HandleCrouch();
          

            ApplyFinalMovement();
        }
    }

    private void HandleMovementInput()
    {
        _currentInput = new Vector2((_isCrouching ? _crouchSpeed : _isSprinting ? _sprintSpeed : _walkSpeed) * Input.GetAxis("Vertical"), (_isCrouching ? _crouchSpeed : _isSprinting ? _sprintSpeed : _walkSpeed) * Input.GetAxis("Horizontal"));
        float moveDirectionY = _moveDirection.y;
        _moveDirection = (transform.TransformDirection(Vector3.forward) * _currentInput.x) + (transform.TransformDirection(Vector3.right) * _currentInput.y);
        _moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        // Mouse view Up/Down
        _rotationX -= Input.GetAxis("Mouse Y") * _viewSpeedY;
        _rotationX = Mathf.Clamp(_rotationX, -_upperViewLimit, _lowerViewLimit);
        _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);

        // Mouse view Left/Right
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _viewSpeedX, 0);
    }

    private void HandleJump()
    {
        if(_shouldJump)
            _moveDirection.y = _jumpForce;
    }

    private void HandleCrouch()
    {
        if (_shouldCrouch)
            StartCoroutine(CrouchStand());
    }

    private void ApplyFinalMovement()
    {
        if(!_characterController.isGrounded)
            _moveDirection.y -= _gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private IEnumerator CrouchStand()
    {
        if (_isCrouching && Physics.Raycast(_playerCamera.transform.position, Vector3.up, 1f))
            yield break;


        _duringCrouchAnim = true;

        float _timeElapsed = 0;
        // Get both the current height and centre point of the character controller to lerp from
        float _currentHeight = _characterController.height;
        Vector3 currentCentre = _characterController.center;
        // Get the target height and centre point of the character controller to lerp towards
        float _targetHeight = _isCrouching ? _standingHeight : _crouchingHeight;
        Vector3 _targetCentre = _isCrouching ? _standingCentre : _crouchingCentre;

        while(_timeElapsed < _timeToCrouch)
        {
            _characterController.height = Mathf.Lerp(_currentHeight, _targetHeight, (_timeElapsed / _timeToCrouch));
            _characterController.center = Vector3.Lerp(currentCentre, _targetCentre, (_timeElapsed / _timeToCrouch));
            _timeElapsed += Time.deltaTime;

            yield return null;
        }

        _characterController.height = _targetHeight;
        _characterController.center = _targetCentre;
        _isCrouching = !_isCrouching;

        _duringCrouchAnim = false;
    }
}
