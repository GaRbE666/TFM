using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;

    private int _move = Animator.StringToHash("move");
    private int _block = Animator.StringToHash("block");
    private int _jump = Animator.StringToHash("jump");
    private int _death = Animator.StringToHash("death");
    private int _attack = Animator.StringToHash("attack");
    private int _land = Animator.StringToHash("land");

    private void Update()
    {
        MoveAnim();
        BlockAnim();
        JumpAnim();
        DeathAnim();
        AttackAnim();
        LandAnim();
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
        playerAnimator.SetBool(_land, playerMovement.isLanding);
    }

    private void FinishAttackAnim()
    {
        InputController.instance.isAttacking = false;
    }

    private void FinishJumpAnim()
    {
        InputController.instance.isJumping = false;
    }
}
