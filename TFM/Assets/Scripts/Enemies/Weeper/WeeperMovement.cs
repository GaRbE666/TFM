using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeeperMovement : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private WeeperAttack weeperAttack;
    [SerializeField] private WeeperHealth weeperHealth;

    [Header("Config")]
    [SerializeField] private float distanceToFollow;
    [SerializeField] private float distanceToDodge;
    [SerializeField] private float minTimeToDodgeAgain;
    [SerializeField] private float maxTimeToDodgeAgain;
    [Range(0, 1)]
    [SerializeField] private float percentageToDodge;

    [Header("Debug Config")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Color reachableObjetive;
    [SerializeField] private Color nonReachableObjetive;

    private NavMeshAgent _navMeshAgent;
    public Vector3 startPoint;
    private float _randomTimeToDodgeAgain;
    [SerializeField] private bool _canDodge;
    [HideInInspector] public float stoppingDistance;
    [HideInInspector] public bool isMoving;
    #endregion

    #region UNITY METHODS
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        startPoint = transform.position;
        stoppingDistance = _navMeshAgent.stoppingDistance;
        _canDodge = true;
    }

    void Update()
    {

        if (weeperHealth.isDead)
        {
            return;
        }

        if (weeperAttack.isAttacking)
        {
            return;
        }

        if (CheckDistanceToPlayer())
        {
            if (CheckMinDistanceToPlayer(stoppingDistance)) //Player is too near and stop walk
            {
                isMoving = false;
                _navMeshAgent.SetDestination(transform.position);
                weeperAnimation.StopWalkAnim();
            }
            else
            {
                isMoving = true;
                _navMeshAgent.SetDestination(target.position);
            }
        }
        else
        {
            _navMeshAgent.SetDestination(startPoint);
        }

        if (CheckDistanceToStartPoint(stoppingDistance)) //Reach the start point
        {
            isMoving = false;
            weeperAnimation.StopWalkAnim();
        }

        //if (CheckDistanceToPlayer(distanceToDodge) && _canDodge && !weeperHealth.isGettingHurt)
        //{
        //    StartCoroutine(DodgeCoroutine());
        //} 
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

    private bool CheckMinDistanceToPlayer(float ditanceToCompare)
    {
        return Vector3.Distance(transform.position, target.position) <= ditanceToCompare;
    }

    private bool CheckDistanceToStartPoint(float distanceToCompare)
    {
        return Vector3.Distance(transform.position, startPoint) <= distanceToCompare;
    }

    private IEnumerator DodgeCoroutine()
    {
        _canDodge = false;
        _randomTimeToDodgeAgain = Random.Range(minTimeToDodgeAgain, maxTimeToDodgeAgain);
        if (Random.value < percentageToDodge)
        {    
            RotateToPlayer();
            DodgeAnimation();
        }
        yield return new WaitForSeconds(_randomTimeToDodgeAgain);
        _canDodge = true;
    }

    private void RotateToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = rotation;
    }

    private void DodgeAnimation()
    {
        weeperAnimation.DodgeAnim();
    }
    #endregion
}
