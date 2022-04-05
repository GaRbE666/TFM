using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigalacertusAttack : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private GigalacertusMovement gigalacertusMovement;
    [SerializeField] private GigalacertusAnimation gigalacertusAnimation;
    [SerializeField] private Transform frontAttackDetection;
    [SerializeField] private Transform target;

    [Header("Parameters")]
    [SerializeField] private float maxTimeToNextAttack;
    [SerializeField] private float minTimeToNextAttack;

    [Header("Debug Config")]
    [Tooltip("Select this option to make the enemy repeat indefinitely the mele attack of your choice.")]
    [SerializeField] private bool forceAttack;
    [Range(1, 5)]
    [Tooltip("Choose which mele attack you want to be repeated")]
    [SerializeField] private int doThisAttack;
    [SerializeField] private float frontAttackRadius;
    [SerializeField] private bool canDraw;
    [SerializeField] private Color frontAttackDetectionColor;

    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool _canAttack;
    public bool _detectedInFront;
    public bool _doFrontalAttack;
    private const int MIN_ATTACK = 1;
    private const int MAX_ATTACK = 2;
    #endregion 


    #region UNITY METHODS
    private void Start()
    {
        _canAttack = true;
    }

    private void Update()
    {
        if (!_canAttack)
        {
            return;
        }

        if (gigalacertusMovement.isMoving)
        {
            return;
        }

        BossAttack();
    }

    private void OnDrawGizmos()
    {
        if (canDraw)
        {
            Gizmos.color = frontAttackDetectionColor;
            Gizmos.DrawWireSphere(frontAttackDetection.position, frontAttackRadius);
        }
    }
    #endregion


    #region CUSTOM METHODS
    private void BossAttack()
    {
        
        if (CheckDistance(gigalacertusMovement.stoppingDistance)/* && !gigalacertusMovement.isMoving*/)
        {
            isAttacking = true;
            _canAttack = false;
            if (forceAttack)
            {
                gigalacertusAnimation.AttackAnim(doThisAttack);
            }
            else
            {
                DecideWhatAttackMake();
                SelectAttackToDo();
            }
        }
    }

    private void SelectAttackToDo()
    {
        if (_doFrontalAttack)
        {
            gigalacertusAnimation.AttackAnim(GenerateRandomFrontalAttack());
        }
    }

    private void CanAttackAgain() //Method called by AnimationEvent
    {
        isAttacking = false;
        StartCoroutine(WaitForNextAttack());
    }

    private IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(GenerateRandomTimeToNextTime());
        _canAttack = true;
    }

    private void DecideWhatAttackMake()
    {
        if (CheckIfPlayerIsInFront())
        {
            _doFrontalAttack = true;
        }
        else
        {
            _doFrontalAttack = false;
        }
    }

    private bool CheckIfPlayerIsInFront()
    {
        Collider[] colliders = Physics.OverlapSphere(frontAttackDetection.position, frontAttackRadius);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.layer == 9)
            {
                _detectedInFront = true;
            }
            else
            {
                _detectedInFront = false;
            }

        }

        return _detectedInFront;
    }

    private float GenerateRandomTimeToNextTime()
    {
        return Random.Range(minTimeToNextAttack, maxTimeToNextAttack);
    }

    private int GenerateRandomFrontalAttack()
    {
        return Random.Range(MIN_ATTACK, MAX_ATTACK);
    }

    private bool CheckDistance(float distance)
    {
        return Vector3.Distance(transform.position, target.position) <= distance;
    }
    #endregion
}
