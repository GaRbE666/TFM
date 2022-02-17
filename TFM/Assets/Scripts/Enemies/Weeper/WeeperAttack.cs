using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeperAttack : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private WeeperMovement weeperMovement;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject attack1;
    [SerializeField] private GameObject attack2;
    [SerializeField] private Transform startPointAttack1;

    [Header("Attack Config")]
    [Tooltip("Valor del daño que hace el enemigo")]
    [SerializeField] private float damage;
    [SerializeField] private float maxTimeToNextAttack;
    [SerializeField] private float minTimeToNextAttack;
    [Tooltip("Tiempo que la animación se queda atacando hasta que finaliza el ataque")]
    [SerializeField] private float timeLoopAttack;

    private bool _canAttack;
    private const int MIN_ATTACK = 1;
    private const int MAX_ATTACK = 4;
    private bool _isLoopin;
    #endregion

    private void Start()
    {
        _canAttack = true;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= weeperMovement.stoppingDistance && _canAttack && !weeperMovement.isMoving)
        {
            _canAttack = false;
            weeperAnimation.AttackAnim(1/*GenerateRandomAttack()*/);
        }
    }

    public void TimeToCanFinishAttack()
    {
        if (_isLoopin)
        {
            return;
        }
        StartCoroutine(TimeToStopAttack());
    }

    private IEnumerator TimeToStopAttack()
    {
        _isLoopin = true;
        weeperAnimation.CantFinishAttackAnim();
        LaunchAttack2();
        yield return new WaitForSeconds(timeLoopAttack);
        weeperAnimation.FinishAttackAnim();
        _isLoopin = false;

    }

    public void CanAttackAgain()
    {
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

    public void LaunchAttack1()
    {
        Debug.Log("ataque 1");
        Instantiate(attack1, startPointAttack1.position, transform.rotation);
    }

    private void LaunchAttack2()
    {
        Instantiate(attack2, target.position, Quaternion.identity);
    }
}
