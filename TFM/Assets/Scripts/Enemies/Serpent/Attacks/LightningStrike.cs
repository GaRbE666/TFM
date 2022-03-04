using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour, IAttack
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private SerpentHealth serpentHealth;
    [SerializeField] private SerpentAnimation serpentAnimation;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject lightingStrike;
    [SerializeField] private Transform startPointAttack;

    [Header("Parameters")]
    [SerializeField] private float timeToFaceTarget;

    public bool _canRotateToTarget;
    #endregion

    #region UNITY METHODS
    private void FixedUpdate()
    {
        if (serpentHealth.isDead)
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

    #region CUSTOM METHODS
    public void StartLightingStrike() //Method called by AnimationEvent Cast01
    {
        _canRotateToTarget = true;
    }

    private void RotateToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    public void LaunchLightingStrikeAttack() //Method called by AnimationEvent Cast01
    {
        Attack();
    }

    public void Attack()
    {
        _canRotateToTarget = false;
        Instantiate(lightingStrike, startPointAttack.position, transform.rotation);
    }
    #endregion
}
