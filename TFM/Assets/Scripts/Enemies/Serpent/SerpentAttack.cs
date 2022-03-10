using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentAttack : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private SerpentMovement serpentMovement;
    [SerializeField] private SerpentAnimation serpentAnimation;
    [SerializeField] private Transform target;

    [Header("Attack Config")]
    [Tooltip("Value of damage done by the enemy")]
    [SerializeField] private float maxTimeToNextAttack;
    [SerializeField] private float minTimeToNextAttack;

    [Header("Debug Config")]
    [Tooltip("Select this option to make the enemy repeat indefinitely the attack of your choice.")]
    [SerializeField] private bool forceAttack;
    [Range(1, 6)]
    [Tooltip("Choose which attack you want to be repeated")]
    [SerializeField] private int doThisAttack;
    [Tooltip("Draw the attack radius")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Color reachableObjetive;
    [SerializeField] private Color nonReachableObjetive;

    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool _canAttack;
    private const int MIN_ATTACK = 1;
    private const int MAX_ATTACK = 6;
    #endregion

    #region UNITY METHODS
    void Start()
    {
        _canAttack = true;
    }

    void Update()
    {
        if (!_canAttack)
        {
            return;
        }

        if (serpentMovement.isMoving)
        {
            return;
        }

        if (CheckDistance(serpentMovement.stoppingDistance)/* && !weeperHealth.isGettingHurt*/)
        {
            isAttacking = true;
            _canAttack = false;
            if (forceAttack)
            {
                serpentAnimation.AttackAnim(doThisAttack);
            }
            else
            {
                serpentAnimation.AttackAnim(GenerateRandomAttack());
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (canDraw)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= serpentMovement.stoppingDistance)
            {
                Gizmos.color = reachableObjetive;
            }
            else
            {
                Gizmos.color = nonReachableObjetive;
            }
            Gizmos.DrawWireSphere(transform.position, serpentMovement.stoppingDistance);
        }
    }
    #endregion

    #region CUSTOM METHODS
    private bool CheckDistance(float distance)
    {
        return Vector3.Distance(transform.position, target.position) <= distance;
    }

    public void CanAttackAgain() //Method called from AnimationEvent
    {
        isAttacking = false;
        StartCoroutine(WaitForNextAttack());
    }

    private IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(GenerateRandomTimeToNextAttack());
        _canAttack = true;
    }

    private int GenerateRandomAttack()
    {
        return Random.Range(MIN_ATTACK, MAX_ATTACK);
    }

    private float GenerateRandomTimeToNextAttack()
    {
        return Random.Range(minTimeToNextAttack, maxTimeToNextAttack);
    }
    #endregion
}
