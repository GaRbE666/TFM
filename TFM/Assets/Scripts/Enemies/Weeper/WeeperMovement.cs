using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeeperMovement : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private WeeperAttack weeperAttack;

    [Header("Config")]
    [SerializeField] private float distanceToFollow;

    [Header("Debug Config")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Color reachableObjetive;
    [SerializeField] private Color nonReachableObjetive;

    private NavMeshAgent _navMeshAgent;
    [HideInInspector] public float stoppingDistance;
    [HideInInspector] public bool isMoving;
    #endregion

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        stoppingDistance = _navMeshAgent.stoppingDistance;
    }

    void Update()
    {
        if (CheckDistance() && !CheckDistance(_navMeshAgent.stoppingDistance) && !weeperAttack.isAttacking)
        {
            isMoving = true;
            weeperAnimation.WalkAnim();
            _navMeshAgent.SetDestination(player.position);
        }
        else
        {
            isMoving = false;
            _navMeshAgent.SetDestination(transform.position);
            weeperAnimation.StopWalkAnim();
        }
        
    }

    private bool CheckDistance()
    {
        return Vector3.Distance(transform.position, player.position) < distanceToFollow;
    }

    private bool CheckDistance(float ditanceToCompare)
    {
        return Vector3.Distance(transform.position, player.position) <= ditanceToCompare;
    }

    private void OnDrawGizmos()
    {
        if (canDraw)
        {
            float distance = Vector3.Distance(transform.position, player.position);
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
}
