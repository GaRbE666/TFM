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

    [Header("Parameters")]
    [SerializeField] private float distanceToFollow;

    [Header("Debug parameters")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Color reachableObjetive;
    [SerializeField] private Color nonReachableObjetive;

    private NavMeshAgent _navMeshAgent;
    /*[HideInInspector]*/ public float stoppingDistance;
    /*[HideInInspector]*/ public bool isMoving;
    #endregion

    #region UNITY METHODS
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        stoppingDistance = _navMeshAgent.stoppingDistance;
    }

    void Update()
    {
        if (serpentHealth.isDead)
        {
            return;
        }

        if (CheckDistanceToPlayer() && !CheckDistanceToPlayer(_navMeshAgent.stoppingDistance))
        {
            isMoving = true;
            serpentAnimation.WalkAnim();
            _navMeshAgent.SetDestination(target.position);
        }
        else
        {
            isMoving = false;
            _navMeshAgent.SetDestination(transform.position);
            serpentAnimation.StopWalkAnim();
        }
    }

    private void OnDrawGizmos()
    {
        if (canDraw)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance > distanceToFollow)
            {
                Debug.Log("entro");
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

    //private void RotateToPlayer()
    //{
    //    Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
    //    transform.rotation = rotation;
    //}
    #endregion
}
