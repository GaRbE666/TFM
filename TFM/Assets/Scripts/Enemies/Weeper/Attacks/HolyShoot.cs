using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyShoot : MonoBehaviour, IAttack
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private WeeperHealth weeperHealth;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject holyShoot;
    [SerializeField] private Transform startPointAttack;

    [Header("Parameters")]
    [SerializeField] private float timeToFaceTarget;

    private bool _canRotateToTarget;
    #endregion

    #region UNITY METHODS
    private void FixedUpdate()
    {
        if (!_canRotateToTarget)
        {
            return;
        }

        if (weeperHealth.isDead)
        {
            return;
        }

        RotateToPlayer();
    }
    #endregion

    #region CUSTOM METHODS
    public void StartHolyShoot() //Methos called by AnimationEvent Cast01
    {
        _canRotateToTarget = true;
    }

    private void RotateToPlayer()
    {
        Debug.Log("Rotate HOLYSHOOT");
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    public void LaunchHolyShootAttack() //Method called by AnimationEvent Cast01
    {
        Attack();
    }

    public void Attack()
    {
        _canRotateToTarget = false;
        Instantiate(holyShoot, startPointAttack.position, transform.rotation);
    }
    #endregion
}
