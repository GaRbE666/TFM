using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentAnimation : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private Animator serpentAnimator;

    private int walk = Animator.StringToHash("walk");
    private int death = Animator.StringToHash("dead");
    private int hit = Animator.StringToHash("hit");
    private int attack1 = Animator.StringToHash("attack1");
    private int attack2 = Animator.StringToHash("attack2");
    private int attack3 = Animator.StringToHash("attack3");
    private int magicAttack1 = Animator.StringToHash("magicAttack1");
    private int magicAttack2 = Animator.StringToHash("magicAttack2");
    private int magicAttack3 = Animator.StringToHash("magicAttack3");
    #endregion

    #region CUSTOM METHODS
    public bool IfCurrentAnimationIsPlaying(string animationName)
    {
        return serpentAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    public void AttackAnim(int numAttack)
    {
        switch (numAttack)
        {
            case 1:
                serpentAnimator.SetTrigger(attack1);
                break;
            case 2:
                serpentAnimator.SetTrigger(attack2);
                break;
            case 3:
                serpentAnimator.SetTrigger(attack3);
                break;
            case 4:
                serpentAnimator.SetTrigger(magicAttack1);
                break;
            case 5:
                serpentAnimator.SetTrigger(magicAttack2);
                break;
            case 6:
                serpentAnimator.SetTrigger(magicAttack3);
                break;
        }
    }

    public void HitAnim()
    {
        serpentAnimator.SetTrigger(hit);
    }

    public void DeadAnim()
    {
        serpentAnimator.SetTrigger(death);
    }

    public void WalkAnim()
    {
        serpentAnimator.SetBool(walk, true);
    }

    public void StopWalkAnim()
    {
        serpentAnimator.SetBool(walk, false);
    }
    #endregion
}
