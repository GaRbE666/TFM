using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFlyAttack : MonoBehaviour
{
    #region FIELDS
    [Header("Parameters")]
    [SerializeField] private float radiusFlyAttack1;
    [SerializeField] private float radiusFlyAttack2;
    [SerializeField] private LayerMask player;
    [SerializeField] private float timeToFaceTarget;
    [SerializeField] private float damage;

    [Header("References")]
    [SerializeField] private Transform flyAttack1Position;
    [SerializeField] private Transform flyAttack2Position;
    [SerializeField] private Transform target;
    [SerializeField] private GargoyleAnimation gargoyleAnimation;
    [SerializeField] private GargoyleHealth gargoyleHealth;

    private bool _canRotateToTarget;
    private float _radiusFlyAttack1Aux;
    private float _radiusFlyAttack2Aux;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        _radiusFlyAttack1Aux = radiusFlyAttack1;
        _radiusFlyAttack2Aux = radiusFlyAttack2;
    }

    private void FixedUpdate()
    {
        if (gargoyleHealth.isDead)
        {
            return;
        }

        if (!_canRotateToTarget)
        {
            return;
        }

        RotateToPlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(flyAttack1Position.position, radiusFlyAttack1);
        Gizmos.DrawWireSphere(flyAttack2Position.position, radiusFlyAttack2);
    }
    #endregion

    #region CUSTOM METHODS
    public void StartFlyAttack() //Method called by Animation Event in Attack 1, Attack 2
    {
        _canRotateToTarget = true;
    }

    public void LaunchFlyAttack() //Method called by Animation Event in Attack 1, Attack 2
    {
        Attack();
    }

    public void Attack()
    {
        _canRotateToTarget = false;
        CheckIfHitWithPlayer();
    }

    private void RotateToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    public void CheckIfHitWithPlayer()
    {
        if (gargoyleAnimation.IfCurrentAnimationIsPlaying("FlyAttack01"))
        {
            Collider[] collisions = Physics.OverlapSphere(flyAttack1Position.position, _radiusFlyAttack1Aux, player);

            foreach (Collider collision in collisions)
            {
                if (collision.GetComponent<PlayerHealth>())
                {
                    _radiusFlyAttack1Aux = 0;
                    PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                    if (playerHealth.death)
                    {
                        return;
                    }
                    playerHealth.TakeDamage(damage);
                    
                }
            }
        }
        else
        {
            Collider[] collisions = Physics.OverlapSphere(flyAttack2Position.position, _radiusFlyAttack2Aux, player);

            foreach (Collider collision in collisions)
            {
                if (collision.GetComponent<PlayerHealth>())
                {
                    _radiusFlyAttack2Aux = 0;
                    PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                    if (playerHealth.death)
                    {
                        return;
                    }
                    playerHealth.TakeDamage(damage);
                    
                }
            }
        }
        _radiusFlyAttack1Aux = radiusFlyAttack1;
        _radiusFlyAttack2Aux = radiusFlyAttack2;
    }
    #endregion
}
