using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private static InputController _inputController;
    public static InputController instance { get { return _inputController; } }
    private PlayerInputAction playerInputAction;
    private PlayerMovement playerMovement;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isBlocking;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isRolling;
    /*[HideInInspector]*/ public bool canPress;
    [HideInInspector] public bool isStrongAttacking;
    [HideInInspector] public bool isSwitchingWeapon;

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

        playerMovement = GetComponent<PlayerMovement>();

        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Jump.started += JumpIsPressed;
        playerInputAction.Player.Block.performed += BlockIsPressed;
        playerInputAction.Player.Block.canceled += BlockNotPressed;
        playerInputAction.Player.Attack.started += AttackIsPressed;
        playerInputAction.Player.Roll.started += RollIsPressed;
        playerInputAction.Player.StrongAttack.started += StrongAttackIsPressed;
        playerInputAction.Player.SwitchWeapon.started += SwitchRightWeapon;
    }

    private void Start()
    {
        canPress = true;
    }

    public void SwitchRightWeapon(InputAction.CallbackContext context)
    {
        if (!canPress)
        {
            return;
        }
        isSwitchingWeapon = true;
    }

    public void RollIsPressed(InputAction.CallbackContext context)
    {

        if (!canPress || isJumping)
        {
            return;
        }

        if (playerMovement.isMoving)
        {
            StartCoroutine(DelayRoll());
        }
        else
        {
            isRolling = true;
        }  
    }

    private IEnumerator DelayRoll()
    {
        yield return new WaitForSeconds(.08f);
        isRolling = true;
    }

    public void AttackIsPressed(InputAction.CallbackContext context)
    {

        if (!canPress || !playerMovement.GroundChecker())
        {
            return;
        }

        isAttacking = true;
    }

    public void StrongAttackIsPressed(InputAction.CallbackContext context)
    {
        if (!canPress || isBlocking)
        {
            return;
        }

        isStrongAttacking = true;
    }

    public Vector2 GetReadValueFromInput()
    {
        return playerInputAction.Player.Movement.ReadValue<Vector2>(); ;
    }

    public void JumpIsPressed(InputAction.CallbackContext context)
    {

        if (!canPress || isBlocking)
        {
            return;
        }

        if (playerMovement.isMoving)
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

    //private void BlockPressButton()
    //{
    //    canPress = false;
    //}

    //private void UnlockPressButton()
    //{
    //    canPress = true;
    //}
}
