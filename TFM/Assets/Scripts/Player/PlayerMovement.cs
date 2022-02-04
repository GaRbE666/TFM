using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputAction playerInputAction;
    private CharacterController controller;
    [SerializeField] private Transform cam;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Vector3 velocity;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCkeck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool drawSphereChecker;

    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Jump.started += Jump;
        Debug.Log(Mathf.Sqrt(20 * -2 * gravity));

    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCkeck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector2 inputVector = playerInputAction.Player.Movement.ReadValue<Vector2>();
        float speed = 5f;
        Vector3 move = new Vector3(inputVector.x, 0, inputVector.y);

        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        //rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        if (drawSphereChecker)
        {
            Gizmos.DrawWireSphere(groundCkeck.position, groundDistance);
        }
    }
}


