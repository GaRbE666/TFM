using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour, IAttack
{
    #region FIELDS
    [Header("Parameters")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask player;
    [SerializeField] private float timeToFaceTarget;
    [SerializeField] private float damage;
    [SerializeField] private float offsetZ;
    [SerializeField] private bool isRight;

    [Header("References")]
    [SerializeField] private Transform LeftStartPoint;
    [SerializeField] private Transform RightStartPoint;
    [SerializeField] private Transform target;
    [SerializeField] private SerpentAnimation serpentAnimation;
    [SerializeField] private SerpentHealth serpentHealth;

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

    private void OnDrawGizmos()
    {
        Vector3 offsetPosition;
        if (isRight)
        {
            offsetPosition = new Vector3(RightStartPoint.position.x, RightStartPoint.position.y, RightStartPoint.position.z + offsetZ);
        }
        else
        {
            offsetPosition = new Vector3(LeftStartPoint.position.x, LeftStartPoint.position.y, LeftStartPoint.position.z + offsetZ);
        }
        
        Gizmos.DrawWireSphere(offsetPosition, radius);
    }
    #endregion

    #region CUSTOM METHODS
    public void StartMeleAttack() //Method called by Animation Event in Attack 1, Attack 2, Attack 3
    {
        _canRotateToTarget = true;
    }

    public void LaunchMeleAttack() //Method called by Animation Event in Attack 1, Attack 2, Attack 3
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
        Debug.Log("RotoMele");
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    public void CheckIfHitWithPlayer() 
    {
        Vector3 offsetPosition;
        if (serpentAnimation.IfCurrentAnimationIsPlaying("attack2"))
        {
            isRight = false;
            offsetPosition = new Vector3(LeftStartPoint.position.x, LeftStartPoint.position.y, LeftStartPoint.position.z + offsetZ);
        }
        else
        {
            isRight = true;
            offsetPosition = new Vector3(RightStartPoint.position.x, RightStartPoint.position.y, RightStartPoint.position.z + offsetZ);
        }
        Collider[] collisions = Physics.OverlapSphere(offsetPosition, radius, player);

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
    #endregion
}
