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
    [SerializeField] private float maxTimeToNextAttack;
    [SerializeField] private float minTimeToNextAttack;

    [Header("Debug Config")]
    [Tooltip("Select this option to make the enemy repeat indefinitely the mele attack of your choice.")]
    [SerializeField] private bool forceMeleAttack;
    [Range(1, 3)]
    [Tooltip("Choose which mele attack you want to be repeated")]
    [SerializeField] private int doThisMeleAttack;
    [Tooltip("Select this option to make the enemy repeat indefinitely the magic attack of your choice.")]
    [SerializeField] private bool forceMagicAttack;
    [Range(4, 6)]
    [Tooltip("Choose which magic attack you want to be repeated")]
    [SerializeField] private int doThisMagicAttack;
    [Tooltip("Draw the attack radius")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Color reachableObjetive;
    [SerializeField] private Color nonReachableObjetive;

    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool _canAttack;
    private const int MIN_ATTACK = 1;
    private const int MAX_ATTACK = 3;
    private const int MIN_MAGIC_ATTACK = 4;
    private const int MAX_MAGIC_ATTACK = 6;
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

        if (serpentMovement.meleDistance)
        {
            MeleAttack();
        }
        else
        {
            MagicAttack();
        }


    }

    private void OnDrawGizmos()
    {
        if (canDraw)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (serpentMovement.meleDistance)
            {
                if (distance <= serpentMovement.stoppingMeleDistance)
                {
                    Gizmos.color = reachableObjetive;
                }
                else
                {
                    Gizmos.color = nonReachableObjetive;
                }
                Gizmos.DrawWireSphere(transform.position, serpentMovement.stoppingMeleDistance);
            }
            else
            {
                if (distance <= serpentMovement.stoppingMagicDistance)
                {
                    Gizmos.color = reachableObjetive;
                }
                else
                {
                    Gizmos.color = nonReachableObjetive;
                }
                Gizmos.DrawWireSphere(transform.position, serpentMovement.stoppingMagicDistance);
            }


        }
    }
    #endregion

    #region CUSTOM METHODS
    private void MeleAttack()
    {
        if (CheckDistance(serpentMovement.stoppingMeleDistance)/* && !weeperHealth.isGettingHurt*/)
        {
            isAttacking = true;
            _canAttack = false;
            if (forceMeleAttack)
            {
                serpentAnimation.AttackAnim(doThisMeleAttack);
            }
            else
            {
                serpentAnimation.AttackAnim(GenerateRandomMeleAttack());
            }
        }
    }

    private void MagicAttack()
    {
        if (CheckDistance(serpentMovement.stoppingMagicDistance)/* && !weeperHealth.isGettingHurt*/)
        {
            isAttacking = true;
            _canAttack = false;
            if (forceMagicAttack)
            {
                serpentAnimation.AttackAnim(doThisMagicAttack);
            }
            else
            {
                serpentAnimation.AttackAnim(GenerateRandomMagicAttack());
            }
        }
    }

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

    private int GenerateRandomMeleAttack()
    {
        return Random.Range(MIN_ATTACK, MAX_ATTACK);
    }
    private int GenerateRandomMagicAttack()
    {
        return Random.Range(MIN_MAGIC_ATTACK, MAX_MAGIC_ATTACK);
    }

    private float GenerateRandomTimeToNextAttack()
    {
        return Random.Range(minTimeToNextAttack, maxTimeToNextAttack);
    }
    #endregion
}
