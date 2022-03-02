using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDance : MonoBehaviour, IAttack
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private WeeperHealth weeperHealth;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private GameObject swordAttack;
    [SerializeField] private Transform target;

    [Header("Parameters")]
    [Tooltip("Time that the animation stays attacking until the attack is finished")]
    [SerializeField] private float timeLoopAttack;
    [SerializeField] private float timeToFaceTarget;

    private bool _prepareAttack;
    private bool _isLooping;
    #endregion

    #region UNITY METHODS
    private void Update()
    {
        if (weeperHealth.isDead)
        {
            return;
        }

        if (weeperAnimation.IfCurrentAnimationIsPlaying("Cast04start"))
        {
            _prepareAttack = true;
        }
        else
        {
            _prepareAttack = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_prepareAttack)
        {
            return;
        }

        RotateToPlayer();
    }
    #endregion

    #region CUSTOM METHODS
    private void RotateToPlayer()
    {
        Debug.Log("Rotate SWORDDANCE");
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    public void Attack()
    {
        Instantiate(swordAttack, target.position, swordAttack.transform.rotation);
    }

    public void TimeToCanFinishSwordDanceAttack() //Method called from AnimationEvent Cast04Loop
    {
        if (_isLooping)
        {
            return;
        }
        StartCoroutine(TimeToStopAttack());
    }

    private IEnumerator TimeToStopAttack()
    {
        _isLooping = true;
        weeperAnimation.CantFinishAttackAnim();
        Attack();
        yield return new WaitForSeconds(timeLoopAttack);
        weeperAnimation.FinishAttackAnim();
        _isLooping = false;
    }
    #endregion
}
