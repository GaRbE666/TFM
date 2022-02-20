using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region "FIELDS"
    [Header("Movements Settings")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float backDashDistance;
    [SerializeField] private float forwardDashDistance;
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Header("Ground Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float radiusChecker;
    [SerializeField] private bool showGroundChecker;

    [Header("References")]
    [SerializeField] private Transform cam;
    [SerializeField] private CinemachineFreeLook camFreeLook;

    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isInFloor;

    private Rigidbody _rb;
    private Vector3 _inputs;
    private bool _freeze;
    private PlayerAnimation playerAnimation;
    private float _targetRotation;
    private float _rotationVelocity;
    private Vector3 targetDirection;

    #endregion

    #region "UNITY EVENTS"
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {

        if (_freeze)
        {
            return;
        }

        _inputs = new Vector3(InputController.instance.GetReadValueFromInput().x, 0, InputController.instance.GetReadValueFromInput().y).normalized;

        if (_inputs != Vector3.zero)
        {
            isMoving = true;


            _targetRotation = Mathf.Atan2(_inputs.x, _inputs.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0f, rotation, 0f);

            targetDirection = Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward; 
        }
        else
        {
            isMoving = false;
        }

        if (InputController.instance.isJumping && GroundChecker())
        {
            Jump();
        }

        if (InputController.instance.isRolling && GroundChecker() && !isMoving)
        {
            DashBack();
        }

        if (InputController.instance.isRolling && GroundChecker() && isMoving)
        {
            DashForward();
        }

        if (GroundChecker())
        {
            playerAnimation.LandAnim(true);
            InputController.instance.isJumping = false;
            InputController.instance.isRolling = false;
        }
        else
        {
            playerAnimation.LandAnim(false);
            if (playerAnimation.IfCurrentAnimationIsPlaying("Armed-Jump"))
            {
                InputController.instance.canPress = false;
            }

        }
    }

    private void FixedUpdate()
    {
        if (_freeze || !isMoving)
        {
            return;
        }

        if (InputController.instance.isBlocking && playerAnimation.IfCurrentAnimationIsPlaying("Shield-Block") || playerAnimation.IfCurrentAnimationIsPlaying("Shield-Walk-Slow-Block"))
        {
            _rb.MovePosition(_rb.position + targetDirection  * walkSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _rb.MovePosition(_rb.position + targetDirection * runSpeed * Time.fixedDeltaTime);
        }
    }
    #endregion

    #region "METHODS"

    private void DashBack()
    {
        playerAnimation.RollBackTrigger();
        _rb.drag = 8f;
        Vector3 dashVelocity = Vector3.Scale(-transform.forward, backDashDistance * new Vector3(Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime, 0, Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1))/ -Time.deltaTime));
        _rb.AddForce(dashVelocity, ForceMode.VelocityChange);
        _rb.drag = 0f;
        InputController.instance.isRolling = false;
    }

    private void DashForward()
    {
        playerAnimation.RollForwardTrigger();
        _rb.drag = 8f;
        Vector3 dashVelocity = Vector3.Scale(transform.forward, forwardDashDistance * new Vector3(Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime, 0, Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime));
        _rb.AddForce(dashVelocity, ForceMode.VelocityChange);
        _rb.drag = 0f;
        InputController.instance.isRolling = false;
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y), ForceMode.VelocityChange);
        playerAnimation.JumpAnim();
        InputController.instance.isJumping = false;
    }

    private bool GroundChecker()
    {
        return Physics.CheckSphere(groundChecker.position, radiusChecker, groundLayer, QueryTriggerInteraction.Ignore);
    }

    private void FreezePlayer()
    {
        _freeze = true;
        InputController.instance.canPress = false;
        _inputs = Vector3.zero;
    }

    private void UnFreezePlayer()
    {
        _freeze = false;
        InputController.instance.canPress = true;
    }

    private void OnDrawGizmos()
    {
        if (showGroundChecker)
        {
            Gizmos.DrawWireSphere(groundChecker.position, radiusChecker);
        }
    }
    #endregion
}
