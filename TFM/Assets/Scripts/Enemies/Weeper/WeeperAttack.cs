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
    [Tooltip("Valor del daño que hace el enemigo")]
    [SerializeField] private float damage;
    [SerializeField] private float maxTimeToNextAttack;
    [SerializeField] private float minTimeToNextAttack;

    [Header("Debug Config")]
    [Tooltip("Selecciona esta opción para que el enemigo repita indefinidamente el ataque que eligas")]
    [SerializeField] private bool forceAttack;
    [Range(1, 4)] [Tooltip("Elige que ataque quieres que se repita")]
    [SerializeField] private int doThisAttack;
    [Tooltip("Dibuja el radio de ataque")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Color reachableObjetive;
    [SerializeField] private Color nonReachableObjetive;

    [HideInInspector] public bool isAttacking;
    private bool _canAttack;
    private const int MIN_ATTACK = 1;
    private const int MAX_ATTACK = 4;
    #endregion

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

        if (CheckDistance())
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

    #region CUSTOM METHODS
    private bool CheckDistance()
    {
        return Vector3.Distance(transform.position, target.position) <= weeperMovement.stoppingDistance;
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

    #region DEBUG METHODS
    private void OnDrawGizmos()
    {
        if (canDraw)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= weeperMovement.stoppingDistance)
            {
                Gizmos.color = reachableObjetive;
            }
            else
            {
                Gizmos.color = nonReachableObjetive;
            }
            Gizmos.DrawWireSphere(transform.position, weeperMovement.stoppingDistance);
        }
    }
    #endregion
}
