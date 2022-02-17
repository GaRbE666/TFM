using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeperAnimation : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private Animator animator;

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

    #region METHODS

    public void DodgeAnim()
    {
        animator.SetTrigger(dodge);
    }

    public void FinishAttackAnim()
    {
        animator.SetBool(canFinishAttack, true);
    }

    public void CantFinishAttackAnim()
    {
        animator.SetBool(canFinishAttack, false);
    }

    public void AttackAnim(int numAttack)
    {
        switch (numAttack)
        {
            case 1:
                animator.SetTrigger(attack1);
                break;
            case 2:
                animator.SetTrigger(attack2);
                break;
            case 3:
                animator.SetTrigger(attack3);
                break;
            case 4:
                animator.SetTrigger(attack4);
                break;
        }
    }

    public void HitAnim()
    {
        animator.SetTrigger(hit);
    }

    public void DeathAnim()
    {
        animator.SetTrigger(death);
    }

    public void WalkAnim()
    {
        animator.SetBool(walk, true);
    }

    public void StopWalkAnim()
    {
        animator.SetBool(walk, false);
    }
    #endregion
}
