using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumenCrash : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private WeeperHealth weeperHealth;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private GameObject lumenCrash;
    [SerializeField] private Transform target;

    [Header("Parameters")]
    [Tooltip("Time that the animation stays attacking until the attack is finished")]
    [SerializeField] private float timeLoopAttack;
    [SerializeField] private float timeToFaceTarget;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask player;
    [SerializeField] private float damage;
    [Tooltip("Waiting time to check if the player is inside the radius and damage it")]
    [SerializeField] private float waitParticleTime;

    private bool _prepareAttack;
    private bool _isLooping;
    private GameObject particleClone;
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
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    public void Attack()
    {
        particleClone = Instantiate(lumenCrash, target.position, lumenCrash.transform.rotation);
        StartCoroutine(WaitToCheck());
    }

    private IEnumerator WaitToCheck()
    {
        yield return new WaitForSeconds(waitParticleTime);
        CheckIfPlayerIsInAttackRange();
    }

    private void CheckIfPlayerIsInAttackRange()
    {
        Collider[] colliders = Physics.OverlapSphere(particleClone.transform.position, radius, player);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<PlayerHealth>())
            {
                PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(damage);
            }
        }
    }

    public void TimeToCanFinishLumenCrashAttack() //Method called from AnimationEvent Cast04Loop
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

    private void OnDrawGizmos()
    {
        if (particleClone)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(particleClone.transform.position, radius);
        }
    }
    #endregion
}
