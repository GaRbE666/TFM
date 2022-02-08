using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Movements Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float dashDistance;

    [Header("Ground Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float radiusChecker;
    [SerializeField] private bool showGroundChecker;

    private Rigidbody _rb;
    private Vector3 _inputs;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundChecker();

        _inputs = new Vector3(InputController.instance.GetReadValueFromInput().x, 0, InputController.instance.GetReadValueFromInput().y);

        if (_inputs != Vector3.zero)
        {
            transform.forward = _inputs;
        }

        if (InputController.instance.isJumping && GroundChecker())
        {
            Jump();
        }

        if (InputController.instance.isRollingBack && GroundChecker())
        {
            Debug.Log("Hago dash");
            Dash();
        }

        if (GroundChecker())
        {
            InputController.instance.isJumping = false;
            InputController.instance.isRollingBack = false;
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _inputs * speed * Time.fixedDeltaTime);
    }

    private void Dash()
    {
        _rb.drag = 8f;
        Vector3 dashVelocity = Vector3.Scale(-transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime), 0, Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1))/ -Time.deltaTime));
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

    private void OnDrawGizmos()
    {
        if (showGroundChecker)
        {
            Gizmos.DrawWireSphere(groundChecker.position, radiusChecker);
        }
    }

}
