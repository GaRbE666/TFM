using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeFire : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private SerpentHealth serpentHealth;
    [SerializeField] private SerpentAnimation serpentAnimation;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject chargeFire;
    [SerializeField] private Transform startPointAttack;

    [Header("Parameters")]
    [SerializeField] private float timeToFaceTarget;
    [SerializeField] private float offsetRotationY;

    private bool _canRotateToTarget;
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
    public void StartChargeFire() //Methos called by AnimationEvent Cast01
    {
        _canRotateToTarget = true;
    }

    private void RotateToPlayer()
    {
        Debug.Log("RotoFire");
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    public void LaunchChargeFireAttack() //Method called by AnimationEvent Cast01
    {
        Attack();
    }

    public void Attack()
    {
        _canRotateToTarget = false;
        Instantiate(chargeFire, startPointAttack.position, startPointAttack.rotation);
    }
    #endregion
}
