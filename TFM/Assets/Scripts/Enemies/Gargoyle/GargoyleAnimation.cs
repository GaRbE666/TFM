using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleAnimation : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private Animator gargoyleAnimator;

    private int _walk = Animator.StringToHash("walk");
    private int _hit = Animator.StringToHash("hit");
    private int _dead = Animator.StringToHash("dead");
    private int _fly = Animator.StringToHash("fly");
    private int _attack1 = Animator.StringToHash("attack1");
    private int _attack2 = Animator.StringToHash("attack2");
    private int _magicAttack = Animator.StringToHash("magicAttack");
    private int _stopAttack = Animator.StringToHash("stopAttack");
    private int _flyAttack1 = Animator.StringToHash("flyAttack1");
    private int _flyAttack2 = Animator.StringToHash("flyAttack2");
    #endregion

    #region CUSTOM METHODS
    public bool IfCurrentAnimationIsPlaying(string animationName)
    {
        return gargoyleAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    public void FlyAttack1Anim()
    {
        gargoyleAnimator.SetTrigger(_flyAttack1);
    }

    public void FlyAttack2Anim()
    {
        gargoyleAnimator.SetTrigger(_flyAttack2);
    }

    public void WalkAnim()
    {
        gargoyleAnimator.SetBool(_walk, true);
    }

    public void StopWalkAnim()
    {
        gargoyleAnimator.SetBool(_walk, false);
    }

    public void HitAnim()
    {
        gargoyleAnimator.SetTrigger(_hit);
    }

    public void DeadAnim()
    {
        gargoyleAnimator.SetTrigger(_dead);
    }

    public void FlyAnim()
    {
        gargoyleAnimator.SetBool(_fly, true);
    }

    public void StopFlyAnim()
    {
        gargoyleAnimator.SetBool(_fly, false);
    }

    public void StopAttack()
    {
        gargoyleAnimator.SetBool(_stopAttack, true);
    }

    public void ResetStopAttack()
    {
        gargoyleAnimator.SetBool(_stopAttack, false);
    }

    public void AttackAnim(int numAttack)
    {
        switch (numAttack)
        {
            case 1:
                gargoyleAnimator.SetTrigger(_attack1);
                break;
            case 2:
                gargoyleAnimator.SetTrigger(_attack2);
                break;
            case 3:
                gargoyleAnimator.SetTrigger(_magicAttack);
                break;
            case 4:
                gargoyleAnimator.SetTrigger(_flyAttack1);
                break;
            case 5:
                gargoyleAnimator.SetTrigger(_flyAttack2);
                break;
        }
    }
    #endregion
}
