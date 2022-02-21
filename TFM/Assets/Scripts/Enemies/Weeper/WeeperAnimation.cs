using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeperAnimation : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private Animator weeperAnimator;

    private int walk = Animator.StringToHash("walk");
    private int death = Animator.StringToHash("death");
    private int hit = Animator.StringToHash("hit");
    private int dodge = Animator.StringToHash("dodge");
    private int attack1 = Animator.StringToHash("attack1");
    private int attack2 = Animator.StringToHash("attack2");
    private int attack3 = Animator.StringToHash("attack3");
    private int attack4 = Animator.StringToHash("attack4");
    private int canFinishAttack = Animator.StringToHash("canFinishAttack");
    #endregion

    #region CUSTOM METHODS
    public bool IfCurrentAnimationIsPlaying(string animationName)
    {
        return weeperAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    public void DodgeAnim()
    {
        weeperAnimator.SetTrigger(dodge);
    }

    public void FinishAttackAnim()
    {
        weeperAnimator.SetBool(canFinishAttack, true);
    }

    public void CantFinishAttackAnim()
    {
        weeperAnimator.SetBool(canFinishAttack, false);
    }

    public void AttackAnim(int numAttack)
    {
        switch (numAttack)
        {
            case 1:
                weeperAnimator.SetTrigger(attack1);
                break;
            case 2:
                weeperAnimator.SetTrigger(attack2);
                break;
            case 3:
                weeperAnimator.SetTrigger(attack3);
                break;
            case 4:
                weeperAnimator.SetTrigger(attack4);
                break;
        }
    }

    public void HitAnim()
    {
        weeperAnimator.SetTrigger(hit);
    }

    public void DeathAnim()
    {
        weeperAnimator.SetTrigger(death);
    }

    public void WalkAnim()
    {
        weeperAnimator.SetBool(walk, true);
    }

    public void StopWalkAnim()
    {
        weeperAnimator.SetBool(walk, false);
    }
    #endregion
}
