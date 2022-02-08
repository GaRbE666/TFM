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
    #endregion

    #region "UNITY EVENTS"
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (InputController.instance.isAttacking)
        {
            _inputs = Vector3.zero;
            return;
        }

        if (_freeze)
        {
            return;
        }

        GroundChecker();

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

        if (InputController.instance.isRolling && GroundChecker())
        {
            DashBack();
        }

        if (InputController.instance.isRolling && GroundChecker() && isMoving)
        {
            DashForward();
        }

        if (GroundChecker())
        {
            isInFloor = true;
            InputController.instance.isJumping = false;
            InputController.instance.isRolling = false;
        }
        else
        {
            isInFloor = false;
        }
    }

    private void FixedUpdate()
    {
        if (InputController.instance.isBlocking)
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
        _rb.drag = 8f;
        Vector3 dashVelocity = Vector3.Scale(-transform.forward, backDashDistance * new Vector3(Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime, 0, Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1))/ -Time.deltaTime));
        _rb.AddForce(dashVelocity, ForceMode.VelocityChange);
        _rb.drag = 0f;
    }

    private void DashForward()
    {
        _rb.drag = 8f;
        Vector3 dashVelocity = Vector3.Scale(transform.forward, forwardDashDistance * new Vector3(Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime, 0, Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime));
        _rb.AddForce(dashVelocity, ForceMode.VelocityChange);
        _rb.drag = 0f;
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y), ForceMode.VelocityChange);
    }

    private bool GroundChecker()
    {
        return Physics.CheckSphere(groundChecker.position, radiusChecker, groundLayer);
    }

    private void FreezePlayer()
    {
        _freeze = true;
        //_inputs = Vector3.zero;
    }

    private void UnFreezePlayer()
    {
        _freeze = false;
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
