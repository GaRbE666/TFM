using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStorm : MonoBehaviour, IAttack
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private WeeperHealth weeperHealth;
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private GameObject misileObject;
    [SerializeField] private Transform target;

    [Header("Parameters")]
    [Tooltip("Tiempo que la animación se queda atacando hasta que finaliza el ataque")]
    [SerializeField] private float timeLoopAttack;
    [SerializeField] private float timeToFaceTarget;
    
    private bool _prepareAttack;
    private bool _isLooping;
    #endregion

    #region UNITY METHODS
    private void Update()
    {
        if (weeperHealth.isDead)
        {
            return;
        }

        if (weeperAnimation.IfCurrentAnimationIsPlaying("Cast02start"))
        {
            _prepareAttack = true;
        }
        else
        {
            _prepareAttack = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_prepareAttack)
        {
            return;
        }

        RotateToPlayer();
    }
    #endregion

    #region CUSTOM METHODS
    public void Attack()
    {
        Instantiate(misileObject, target.position, misileObject.transform.rotation);
    }

    private void RotateToPlayer()
    {
        Debug.Log("Rotate FIRESTORM");
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    public void TimeToCanFinishFireStormAttack() //Method called by AnimationEvent Cast02Loop
    {
        if (_isLooping)
        {
            return;
        }
        StartCoroutine(TimeToStopAttack());
    }

    private IEnumerator TimeToStopAttack()
    {
        _isLooping = true;
        weeperAnimation.CantFinishAttackAnim();
        Attack();
        yield return new WaitForSeconds(timeLoopAttack);
        weeperAnimation.FinishAttackAnim();
        _isLooping = false;
    }
    #endregion
}
