using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigalacertusAnimation : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private Animator bossAnimator;

    private int _dead = Animator.StringToHash("dead");
    private int _playerDetected = Animator.StringToHash("playerDetected");
    private int _walk = Animator.StringToHash("walk");
    private int _electroMagneticAttack = Animator.StringToHash("electroMagneticAttack");
    private int _hornsLightingAttack = Animator.StringToHash("hornsLightingAttack");
    private int _spittersAttack = Animator.StringToHash("spittersAttack");
    private int _tongueAttack = Animator.StringToHash("tongueAttack");
    private int _tongueHit = Animator.StringToHash("tongueHit");
    private int _ramAttack = Animator.StringToHash("ramAttack");
    #endregion

    #region CUSTOM METHODS
    public bool IfCurrentAnimationIsPlaying(string animationName)
    {
        return bossAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    public void StartWalkAnim()
    {
        bossAnimator.SetBool(_walk, true);
    }

    public void StopWalkAnim()
    {
        bossAnimator.SetBool(_walk, false);
    }

    public void DeadAnim()
    {
        bossAnimator.SetTrigger(_dead);
    }

    public void PlayerDetectedAnim()
    {
        bossAnimator.SetTrigger(_playerDetected);
    }

    public void TongueHitAnim()
    {
        bossAnimator.SetTrigger(_tongueHit);
    }

    public void AttackAnim(int attack)
    {
        switch (attack)
        {
            case 1:
                bossAnimator.SetTrigger(_ramAttack);
                break;
            case 2:
                bossAnimator.SetTrigger(_hornsLightingAttack);
                break;
            case 3:
                bossAnimator.SetTrigger(_electroMagneticAttack);
                break;
            case 4:
                bossAnimator.SetTrigger(_tongueAttack);
                break;
            case 5:
                bossAnimator.SetTrigger(_spittersAttack);
                break;
        }
    }
    #endregion
}
