using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMeleAttack : MonoBehaviour
{
    #region FIELDS
    [Header("Parameters")]
    [SerializeField] private float radiusAttack1;
    [SerializeField] private float radiusAttack2;
    [SerializeField] private LayerMask player;
    [SerializeField] private float timeToFaceTarget;
    [SerializeField] private float damage;

    [Header("References")]
    [SerializeField] private Transform attack1Position;
    [SerializeField] private Transform attack2LeftPosition;
    [SerializeField] private Transform attack2RightPosition;
    [SerializeField] private Transform target;
    [SerializeField] private GargoyleAnimation gargoyleAnimation;
    [SerializeField] private GargoyleHealth gargoyleHealth;

    private bool _canRotateToTarget;
    #endregion


    #region UNITY METHODS
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
        Gizmos.DrawWireSphere(attack1Position.position, radiusAttack1);
        Gizmos.DrawWireSphere(attack2LeftPosition.position, radiusAttack2);
        Gizmos.DrawWireSphere(attack2RightPosition.position, radiusAttack2);
    }
    #endregion

    #region CUSTOM METHODS
    public void StartMeleAttack() //Method called by Animation Event in Attack 1, Attack 2
    {
        _canRotateToTarget = true;
    }

    public void LaunchMeleAttack() //Method called by Animation Event in Attack 1, Attack 2
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
        if (gargoyleAnimation.IfCurrentAnimationIsPlaying("Attack02"))
        {
            Collider[] collisionsLeft = Physics.OverlapSphere(attack2LeftPosition.position, radiusAttack2, player);
            Collider[] collisionsRight = Physics.OverlapSphere(attack2RightPosition.position, radiusAttack2, player);

            foreach (Collider collision in collisionsLeft)
            {
                if (collision.GetComponent<PlayerHealth>())
                {
                    PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                    if (playerHealth.death)
                    {
                        return;
                    }
                    playerHealth.TakeDamage(damage);

                }
            }

            foreach (Collider collision in collisionsRight)
            {
                if (collision.GetComponent<PlayerHealth>())
                {
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
            Collider[] collisions = Physics.OverlapSphere(attack1Position.position, radiusAttack1, player);

            foreach (Collider collision in collisions)
            {
                if (collision.GetComponent<PlayerHealth>())
                {
                    PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                    if (playerHealth.death)
                    {
                        return;
                    }
                    playerHealth.TakeDamage(damage);

                }
            }
        }
    }
    #endregion
}
