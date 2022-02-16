using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeeperMovement : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private Transform player;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private float distanceTofollow;
    [SerializeField] private bool canDraw;

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
        if (CheckDistance() && !CheckDistance(_navMeshAgent.stoppingDistance))
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
        return Vector3.Distance(transform.position, player.position) < distanceTofollow;
    }

    private bool CheckDistance(float ditanceToCompare)
    {
        return Vector3.Distance(transform.position, player.position) <= ditanceToCompare;
    }

    private void OnDrawGizmos()
    {
        if (canDraw)
        {
            if (Vector3.Distance(transform.position, player.position) > distanceTofollow)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, player.position);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, player.position);
            }   
        }
    }
}
