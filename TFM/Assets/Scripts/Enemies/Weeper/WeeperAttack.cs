using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeperAttack : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private WeeperMovement weeperMovement;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private WeeperHealth weeperHealth;
    [SerializeField] private Transform target;

    [Header("Attack Config")]
    [Tooltip("Value of damage done by the enemy")]
    [SerializeField] private float damage;
    [SerializeField] private float maxTimeToNextAttack;
    [SerializeField] private float minTimeToNextAttack;

    [Header("Debug Config")]
    [Tooltip("Select this option to make the enemy repeat indefinitely the attack of your choice.")]
    [SerializeField] private bool forceAttack;
    [Range(1, 4)] [Tooltip("Choose which attack you want to be repeated")]
    [SerializeField] private int doThisAttack;
    [Tooltip("Draw the attack radius")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Color reachableObjetive;
    [SerializeField] private Color nonReachableObjetive;

    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool _canAttack;
    private const int MIN_ATTACK = 1;
    private const int MAX_ATTACK = 4;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        _canAttack = true;
    }

    private void Update()
    {
        if (weeperHealth.isDead)
        {
            return;
        }

        if (!_canAttack)
        {
            return;
        }

        if (weeperMovement.isMoving)
        {
            return;
        }

        if (CheckDistance(weeperMovement.stoppingDistance) && !weeperHealth.isGettingHurt)
        {
            isAttacking = true;
            _canAttack = false;
            if (forceAttack)
            {
                weeperAnimation.AttackAnim(doThisAttack);
            }
            else
            {
                weeperAnimation.AttackAnim(GenerateRandomAttack());
            }
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
