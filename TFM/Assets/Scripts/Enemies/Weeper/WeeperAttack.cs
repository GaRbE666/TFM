using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeperAttack : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private WeeperMovement weeperMovement;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private Transform target;
    [SerializeField] private float damage;
    [SerializeField] private float maxTimeToNextAttack;
    [SerializeField] private float minTimeToNextAttack;

    private bool canAttack;
    private const int MIN_ATTACK = 1;
    private const int MAX_ATTACK = 4;
    #endregion

    private void Start()
    {
        canAttack = true;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= weeperMovement.stoppingDistance && canAttack && !weeperMovement.isMoving)
        {
            canAttack = false;
            weeperAnimation.AttackAnim(GenerateRandomAttack());
        }
    }

    public void CanAttackAgain()
    {
        StartCoroutine(WaitForNextAttack());
    }

    private IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(GenerateRandomTimeToNextAttack());
        canAttack = true;
    }

    private int GenerateRandomAttack()
    {
        return Random.Range(MIN_ATTACK, MAX_ATTACK);
    }

    private float GenerateRandomTimeToNextAttack()
    {
        return Random.Range(minTimeToNextAttack, maxTimeToNextAttack);
    }
}
