using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private static InputController _inputController;
    public static InputController instance { get { return _inputController; } }
    private PlayerInputAction playerInputAction;
    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isBlocking;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isRolling;
    [HideInInspector] public bool canRoll;

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

        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();

        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Jump.started += JumpIsPressed;
        playerInputAction.Player.Block.performed += BlockIsPressed;
        playerInputAction.Player.Block.canceled += BlockNotPressed;
        playerInputAction.Player.Attack.started += AttackIsPressed;
        playerInputAction.Player.Roll.started += RollIsPressed;
    }

    private void Start()
    {
        canRoll = true;
    }

    public void RollIsPressed(InputAction.CallbackContext context)
    {
        if (isJumping || isAttacking || !canRoll)
        {
            return;
        }

        if (playerMovement.isMoving)
        {
            StartCoroutine(DelayRoll());
        }
        else
        {
            canRoll = false;
            isRolling = true;
        }  
    }

    private IEnumerator DelayRoll()
    {
        yield return new WaitForSeconds(.08f);
        canRoll = false;
        isRolling = true;
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
        if (isBlocking || isRolling || isAttacking)
        {
            return;
        }

        if (playerMovement.isMoving || playerAnimation.IfCurrentAnimationIsPlaying("Shield-Idle"))
        {
            StartCoroutine(DelayJump());
        }
        else
        {
            isJumping = true;
        }
        
    }

    private IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(.08f);
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
