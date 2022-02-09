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

    [Header("Ground Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float radiusChecker;
    [SerializeField] private bool showGroundChecker;

    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isInFloor;

    private Rigidbody _rb;
    private Vector3 _inputs;
    [SerializeField] private bool _freeze;
    private PlayerAnimation playerAnimation;
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

        _inputs = new Vector3(InputController.instance.GetReadValueFromInput().x, 0, InputController.instance.GetReadValueFromInput().y);

        if (_inputs != Vector3.zero)
        {
            isMoving = true;
            transform.forward = _inputs;
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
            InputController.instance.isAttacking = false;
            InputController.instance.canPress = false;
        }
    }

    private void FixedUpdate()
    {
        if (InputController.instance.isBlocking && playerAnimation.IfCurrentAnimationIsPlaying("Shield-Block") || playerAnimation.IfCurrentAnimationIsPlaying("Shield-Walk-Slow-Block"))
        {
            _rb.MovePosition(_rb.position + _inputs * walkSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _rb.MovePosition(_rb.position + _inputs * runSpeed * Time.fixedDeltaTime);
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
        //isMoving = false;
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
