using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SerpentMovement : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private SerpentAnimation serpentAnimation;
    [SerializeField] private SerpentHealth serpentHealth;
    [SerializeField] private SerpentAttack serpentAttack;

    [Header("Parameters")]
    [SerializeField] private float distanceToFollow;
    public bool meleDistance;
    public float stoppingMagicDistance;
    public float stoppingMeleDistance;

    [Header("Debug parameters")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Color reachableObjetive;
    [SerializeField] private Color nonReachableObjetive;

    private NavMeshAgent _navMeshAgent;
    public Vector3 startPoint;
    //[HideInInspector] public float stoppingDistance;
    [HideInInspector] public bool isMoving;
    #endregion

    #region UNITY METHODS
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        startPoint = transform.position;
        if (meleDistance)
        {
            _navMeshAgent.stoppingDistance = stoppingMeleDistance;
        }
        else
        {
            _navMeshAgent.stoppingDistance = stoppingMagicDistance;
        }
        
    }

    void Update()
    {
        if (serpentHealth.isDead)
        {
            return;
        }

        if (serpentAttack.isAttacking)
        {
            return;
        }

        MoveEnemy();

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
    private void MoveEnemy()
    {
        if (CheckDistanceToPlayer())
        {
            if (CheckMinDistanceToPlayer(_navMeshAgent.stoppingDistance)) //Player is too near and stop walk
            {
                isMoving = false;
                _navMeshAgent.SetDestination(transform.position);
                serpentAnimation.StopWalkAnim();
            }
            else
            {
                isMoving = true;
                serpentAnimation.WalkAnim();
                _navMeshAgent.SetDestination(target.position);
            }
        }
        else
        {
            _navMeshAgent.SetDestination(startPoint);
        }

        if (CheckDistanceToStartPoint(_navMeshAgent.stoppingDistance)) //Reach the start point
        {
            isMoving = false;
            serpentAnimation.StopWalkAnim();
        }
    }

    private bool CheckDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, target.position) < distanceToFollow;
    }

    private bool CheckDistanceToStartPoint(float distanceToCompare)
    {
        return Vector3.Distance(transform.position, startPoint) <= distanceToCompare;
    }

    private bool CheckMinDistanceToPlayer(float ditanceToCompare)
    {
        return Vector3.Distance(transform.position, target.position) <= ditanceToCompare;
    }
    #endregion
}
