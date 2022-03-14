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
    [SerializeField] private GargoyleAttack gargoyleAttack;

    [Header("Parameters")]
    [SerializeField] private float distanceToFollow;
    [SerializeField] private float percentageToFly;
    [SerializeField] private float timeToCheckIfICanFly;

    [Header("Debug Parameters")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Color reachableObjetive;
    [SerializeField] private Color nonReachableObjetive;

    private NavMeshAgent _navMeshAgent;
    private Transform startPoint;
    public bool _canCheckIfICanFly;
    [HideInInspector] public float stoppingDistance;
    [HideInInspector] public bool isMoving;
    /*[HideInInspector]*/ public bool isFlying;
    #endregion

    #region UNITY METHODS
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        startPoint = transform;
        stoppingDistance = _navMeshAgent.stoppingDistance;
        isFlying = false;
        _canCheckIfICanFly = true;
    }

    void Update()
    {
        if (gargoyleHealth.isDead)
        {
            return;
        }

        if (gargoyleAttack.isAttacking)
        {
            return;
        }

        if (CheckDistanceToPlayer() && !CheckMinDistanceToPlayer(_navMeshAgent.stoppingDistance) && !isMoving)
        {
            CheckIfICanFly();
        }

        if (CheckDistanceToPlayer())
        {
            ChangeAnimationMovementState(); //Le decimos si puede volar o no
            if (CheckMinDistanceToPlayer(_navMeshAgent.stoppingDistance))
            {
                isMoving = false;
                _navMeshAgent.SetDestination(transform.position);
                gargoyleAnimation.StopWalkAnim();
            }
            else
            {
                isMoving = true;

                _navMeshAgent.SetDestination(target.position);
            }

        }
        else
        {
            
            isFlying = false;
            gargoyleAnimation.StopFlyAnim();
            _navMeshAgent.SetDestination(startPoint.position);
            gargoyleAnimation.WalkAnim();
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

    private void ChangeAnimationMovementState()
    {
        if (!isFlying)
        {
            gargoyleAnimation.StopFlyAnim();
            gargoyleAnimation.WalkAnim();
        }
        else
        {
            gargoyleAnimation.FlyAnim();
        }
    }

    public void CheckIfICanFly()
    {
        float percentageRandom = Random.value;
        Debug.Log(percentageRandom);
        if (percentageRandom < percentageToFly)
        {
            isFlying = true;
        }
        else
        {
            isFlying = false;
        }
    }

    private bool CheckDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, target.position) < distanceToFollow;
    }

    private bool CheckMinDistanceToPlayer(float ditanceToCompare)
    {
        return Vector3.Distance(transform.position, target.position) <= ditanceToCompare;
    }
    #endregion
}
