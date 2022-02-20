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
    private int _death = Animator.StringToHash("death");
    private int _attack = Animator.StringToHash("attack");
    private int _land = Animator.StringToHash("land");
    private int _jump = Animator.StringToHash("jump");
    private int _rollBack = Animator.StringToHash("rollBack");
    private int _rollForward = Animator.StringToHash("rollForward");
    private int _strongAttack = Animator.StringToHash("strongAttack");
    private int _switchWeapon = Animator.StringToHash("switchWeapon");
    #endregion

    private void Update()
    {
        MoveAnim();
        DeathAnim();
    }

    public bool IfCurrentAnimationIsPlaying(string animationName)
    {
        return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    public void SwitchWeaponAnim()
    {
        playerAnimator.SetTrigger(_switchWeapon);
    }

    public void StrongAttackAnim()
    {
        playerAnimator.SetTrigger(_strongAttack);
    }

    public void RollBackTrigger()
    {
        playerAnimator.SetTrigger(_rollBack);
    }

    public void RollForwardTrigger()
    {
        playerAnimator.SetTrigger(_rollForward);
    }

    public void BlockAnim(bool isBlocking)
    {
        playerAnimator.SetBool(_block, isBlocking);
    }

    private void MoveAnim()
    {
        playerAnimator.SetBool(_move, playerMovement.isMoving);
    }

    public void JumpAnim()
    {
        playerAnimator.SetTrigger(_jump);
    }

    private void DeathAnim()
    {
        playerAnimator.SetBool(_death, playerHealth.death);
    }

    public void AttackAnim()
    {
        playerAnimator.SetTrigger(_attack);
    }

    public void LandAnim(bool isInFloor)
    {
        playerAnimator.SetBool(_land, isInFloor);
    }

    //private void FinishAttackAnim()
    //{
    //    InputController.instance.isAttacking = false;
    //}

    //private void FinishJumpAnim()
    //{
    //    InputController.instance.isJumping = false;
    //}

    //private void FinishRollAnim()
    //{   
    //    InputController.instance.isRolling = false;
    //}
}
