using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region "FIELDS"
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerAttack playerAttack;

    private int _move = Animator.StringToHash("move");
    private int _block = Animator.StringToHash("block");
    private int _jump = Animator.StringToHash("jump");
    private int _death = Animator.StringToHash("death");
    private int _attack = Animator.StringToHash("attack");
    private int _land = Animator.StringToHash("land");
    private int _rollBack = Animator.StringToHash("rollBack");
    private int _rollForward = Animator.StringToHash("rollForward");
    #endregion

    private void Update()
    {
        MoveAnim();
        BlockAnim();
        JumpAnim();
        DeathAnim();
        AttackAnim();
        LandAnim();
        RollBackAnim();
        RollForwardAnim();
    }

    public bool IfCurrentAnimationIsPlaying(string animationName)
    {
        return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    private void RollBackAnim()
    {
        if (playerMovement.isMoving)
        {
            playerAnimator.SetBool(_rollBack, false);
        }
        playerAnimator.SetBool(_rollBack, InputController.instance.isRolling);
    }

    private void RollForwardAnim()
    {
        if (!playerMovement.isMoving)
        {
            playerAnimator.SetBool(_rollForward, false);
        }        
        playerAnimator.SetBool(_rollForward, InputController.instance.isRolling);
    }

    private void BlockAnim()
    {
        playerAnimator.SetBool(_block, InputController.instance.isBlocking);
    }

    private void MoveAnim()
    {
        playerAnimator.SetBool(_move, playerMovement.isMoving);
    }

    private void JumpAnim()
    {
        playerAnimator.SetBool(_jump, InputController.instance.isJumping);
    }

    private void DeathAnim()
    {
        playerAnimator.SetBool(_death, playerHealth.death);
    }

    private void AttackAnim()
    {
        playerAnimator.SetBool(_attack, InputController.instance.isAttacking);

    }

    private void LandAnim()
    {
        playerAnimator.SetBool(_land, playerMovement.isInFloor);
    }

    private void FinishAttackAnim()
    {
        InputController.instance.isAttacking = false;
    }

    private void FinishJumpAnim()
    {
        InputController.instance.isJumping = false;
    }

    private void FinishRollAnim()
    {   
        InputController.instance.isRolling = false;
        InputController.instance.canRoll = true;
    }
}
