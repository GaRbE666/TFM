using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigalacertusMovement : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private GigalacertusAnimation gigalacertusAnimation;
    [SerializeField] private GigalacertusAttack gigalacertusAttack;
    [SerializeField] private GigalacertusHealth gigalacertusHealth;

    [Header("Parameters")]
    [SerializeField] private float distanceToFollow;
    public float stoppingDistance;
    [SerializeField] private float speed;
    [SerializeField] private float timeToFaceTarget;
    [SerializeField] private float timeToFollowPlayerAgain;

    [Header("Debug Parameters")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Color reachableObjetive;
    [SerializeField] private Color nonReachableObjetive;

    [HideInInspector] public bool isMoving;
    private bool canFollow;
    #endregion

    #region UNITY METHODS
    private void Update()
    {
        if (gigalacertusHealth.isDead)
        {
            return;
        }

        if (gigalacertusAttack.isAttacking)
        {
            return;
        }

        if (CheckDistanceToPlayer() && !CheckStoppingDistance() && canFollow)
        {
            isMoving = true;
            Move();
        }
        else
        {
            isMoving = false;
            DontMove();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            GetComponent<BoxCollider>().enabled = false;
            gigalacertusAnimation.PlayerDetectedAnim();
        }
    }
    private void OnDrawGizmos()
    {
        if (canDraw)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance > distanceToFollow)
            {
                Gizmos.color = nonReachableObjetive;
            }
            else
            {
                Gizmos.color = reachableObjetive;
            }
            Gizmos.DrawWireSphere(transform.position, distance);
        }
    }
    #endregion

    #region CUSTOM METHODS
    private void Move()
    {
        gigalacertusAnimation.StartWalkAnim();
        RotateToPlayer();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void DontMove()
    {
        gigalacertusAnimation.StopWalkAnim();
        StartCoroutine(WaitToFollowPlayerAgain());
    }

    private IEnumerator WaitToFollowPlayerAgain()
    {
        canFollow = false;
        yield return new WaitForSeconds(timeToFollowPlayerAgain);
        canFollow = true;
    }

    private void RotateToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    private bool CheckStoppingDistance()
    {
        return Vector3.Distance(transform.position, target.position) < stoppingDistance;
    }

    private bool CheckDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, target.position) < distanceToFollow;
    }
    #endregion
}
