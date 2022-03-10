using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GargoyleMovement : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private GargoyleAnimation gargoyleAnimation;
    [SerializeField] private GargoyleHealth gargoyleHealth;

    [Header("Parameters")]
    [SerializeField] private float distanceToFollow;

    [Header("Debug Parameters")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Color reachableObjetive;
    [SerializeField] private Color nonReachableObjetive;

    private NavMeshAgent _navMeshAgent;
    [HideInInspector] public float stoppingDistance;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isFlying;
    #endregion

    #region UNITY METHODS
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        stoppingDistance = _navMeshAgent.stoppingDistance;
        isFlying = false;
    }

    void Update()
    {
        if (gargoyleHealth.isDead)
        {
            return;
        }

        if (CheckDistanceToPlayer() && !CheckDistanceToPlayer(_navMeshAgent.stoppingDistance))
        {
            isMoving = true;
            if (!isFlying)
            {
                gargoyleAnimation.StopFlyAnim();
            }
            gargoyleAnimation.WalkAnim();
            _navMeshAgent.SetDestination(target.position);
        }
        else
        {
            isMoving = false;
            _navMeshAgent.SetDestination(transform.position);
            gargoyleAnimation.StopWalkAnim();
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
    private bool CheckDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, target.position) < distanceToFollow;
    }

    private bool CheckDistanceToPlayer(float ditanceToCompare)
    {
        return Vector3.Distance(transform.position, target.position) <= ditanceToCompare;
    }
    #endregion
}
