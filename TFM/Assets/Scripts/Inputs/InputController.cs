using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private static InputController _inputController;
    public static InputController instance { get { return _inputController; } }
    private PlayerInputAction playerInputAction;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isBlocking;
    [HideInInspector] public bool isAttacking;

    private void Awake()
    {
        if (_inputController != null && _inputController != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _inputController = this;
        }

        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Jump.started += JumpIsPressed;
        playerInputAction.Player.Block.performed += BlockIsPressed;
        playerInputAction.Player.Block.canceled += BlockNotPressed;
        playerInputAction.Player.Attack.started += AttackIsPressed;
    }

    public void AttackIsPressed(InputAction.CallbackContext context)
    {
        if (isBlocking || isJumping)
        {
            return;
        }
        isAttacking = true;
    }

    public Vector2 GetReadValueFromInput()
    {
        return playerInputAction.Player.Movement.ReadValue<Vector2>(); ;
    }

    public void JumpIsPressed(InputAction.CallbackContext context)
    {
        if (isBlocking)
        {
            return;
        }
        isJumping = true;
    }

    public void BlockIsPressed(InputAction.CallbackContext context)
    {
        isBlocking = true;
    }

    public void BlockNotPressed(InputAction.CallbackContext context)
    {
        isBlocking = false;
    }
}
