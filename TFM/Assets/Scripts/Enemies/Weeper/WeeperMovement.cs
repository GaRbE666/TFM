using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeeperMovement : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private Transform player;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private bool canDraw;

    private NavMeshAgent _navMeshAgent;
    #endregion

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < distanceToAttack)
        {
            weeperAnimation.WalkAnim();
            _navMeshAgent.SetDestination(player.position);
        }
        else
        {
            _navMeshAgent.SetDestination(transform.position);
            weeperAnimation.StopWalkAnim();
        }
        
    }

    private void OnDrawGizmos()
    {
        if (canDraw)
        {
            if (Vector3.Distance(transform.position, player.position) > distanceToAttack)
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
