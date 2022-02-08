using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region "FIELDS"
    [Header("Movement options")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float dashDistance;
    [SerializeField] private Vector3 drag; 

    [Header("Camera options")]
    [SerializeField] private Transform cam;
    [SerializeField] private float turnSmoothTime = 0.1f;

    [Header("Ground options")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCkeck;
    [SerializeField] private float groundRadius = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool drawSphereChecker;

    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isWalking;
    [HideInInspector] public bool isLanding;

    private CharacterController _controller;
    private float _turnSmoothVelocity;
    private Vector3 _velocity;
    private Vector2 _inputVector;
    private Vector3 _move;
    private Vector3 _moveDir;
    private bool _freeze;
    private bool _rolling = true;
    #endregion

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
        if (InputController.instance.isAttacking)
        {
            return;
        }

        if (_freeze)
        {
            return;
        }

        CollectData();

        #region "JUMP CODE"
        Jump();
        _velocity.y += gravity * Time.deltaTime; //We apply gravity
        _controller.Move(_velocity * Time.deltaTime);
        #endregion

        #region "MOVE CODE"
        //Check if the vector is moving
        if (_move.magnitude >= 0.1f)
        {
            CalculatePlayerRotation();
            if (InputController.instance.isBlocking)
            {
                _controller.Move(_moveDir.normalized * walkSpeed * Time.deltaTime);
            }
            else
            {
                _controller.Move(_moveDir.normalized * speed * Time.deltaTime);
            }
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        #endregion

        ResetGravityVelocity();
    }

    #region "METHODS"
    private void Jump()
    {
        if (InputController.instance.isJumping && CheckIfIsGrounded())
        {
            isLanding = false;
            _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    private void CalculatePlayerRotation()
    {
        float targetAngle = Mathf.Atan2(_move.x, _move.z) * Mathf.Rad2Deg + cam.eulerAngles.y; //In this variable we know what angle we should rotate our character depending on which vector we are in.
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime); //Smoothing the rotation
        transform.rotation = Quaternion.Euler(0f, angle, 0f); //We apply the rotation to our character
        _moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; //We say go forward depending on where you are looking.

    }

    private void CollectData()
    {
        //Collect data to fill Vectors
        _inputVector = InputController.instance.GetReadValueFromInput();
        _move = new Vector3(_inputVector.x, 0, _inputVector.y);
    }

    private void ResetGravityVelocity()
    {
        if (CheckIfIsGrounded() && _velocity.y < 0)
        {
            InputController.instance.isJumping = false;
            isLanding = true;
            _velocity.y = -2f;
        }
    }

    private bool CheckIfIsGrounded()
    {
        return Physics.CheckSphere(groundCkeck.position, groundRadius, groundMask);
    }

    public void FreezePlayer()
    {
        _freeze = true;
    }

    public void UnFreezePlayer()
    {
        _freeze = false;
    }

    private void OnDrawGizmos()
    {
        if (drawSphereChecker)
        {
            Gizmos.DrawWireSphere(groundCkeck.position, groundRadius);
        }
    }
    #endregion
}


