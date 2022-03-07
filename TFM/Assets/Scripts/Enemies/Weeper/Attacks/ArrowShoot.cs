using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour, IAttack
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private WeeperHealth weeperHealth;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject arrowEmission;
    [SerializeField] private Transform startPointAttack;

    [Header("Parameters")]
    [SerializeField] private float timeToFaceTarget;

    private bool _canRotateToTarget;
    #endregion

    #region UNITY METHODS
    private void FixedUpdate()
    {
        if (weeperHealth.isDead)
        {
            return;
        }

        if (!_canRotateToTarget)
        {
            return;
        }

        RotateToPlayer();
    }
    #endregion

    #region CUSTOM MTEHODS
    public void StartArrowShoot() //Method called by AnimationEvent Cast03
    {
        _canRotateToTarget = true;
    }

    private void RotateToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    public void LaunchArrowShootAttack() //Method called by AnimationEvent Cast03
    {
        Attack();
    }

    public void Attack()
    {
        _canRotateToTarget = false;
        Instantiate(arrowEmission, startPointAttack.position, transform.rotation);
    }
    #endregion
}
