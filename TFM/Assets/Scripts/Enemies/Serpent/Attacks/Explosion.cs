using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Explosion : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private SerpentHealth serpentHealth;
    [SerializeField] private SerpentAnimation serpentAnimation;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform startPointAttack;
    [SerializeField] private Transform endPointAttack;

    [Header("Parameters")]
    [SerializeField] private float timeToFaceTarget;
    [SerializeField] private float timeCloneMove;

    private bool _canRotateToTarget;
    private bool _canMoveExplosion;
    private GameObject explosionClone;
    #endregion

    #region UNITY METHODS
    private void FixedUpdate()
    {
        if (serpentHealth.isDead)
        {
            return;
        }

        if (_canMoveExplosion && explosionClone)
        {
            MoveExplosion(explosionClone);
        }

        if (!_canRotateToTarget)
        {
            return;
        }

        RotateToPlayer();

    }
    #endregion

    #region CUSTOM MTEHODS
    public void StartExplosionShoot() //Method called by AnimationEvent Cast03
    {
        _canRotateToTarget = true;
    }

    private void RotateToPlayer()
    {
        Debug.Log("RotoExplosion");
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    public void LaunchExplosionAttack() //Method called by AnimationEvent Cast03
    {
        Attack();
    }

    public void Attack()
    {
        _canRotateToTarget = false;
        explosionClone = Instantiate(explosion, startPointAttack.position, transform.rotation);
        StartCoroutine(TimeToCanMoveExplosion());
    }

    private IEnumerator TimeToCanMoveExplosion()
    {
        _canMoveExplosion = true;
        yield return new WaitForSeconds(timeCloneMove);
        _canMoveExplosion = false;
    }

    private void MoveExplosion(GameObject explosion)
    {
        explosion.transform.DOMove(endPointAttack.position, timeCloneMove);
    }
    #endregion
}
