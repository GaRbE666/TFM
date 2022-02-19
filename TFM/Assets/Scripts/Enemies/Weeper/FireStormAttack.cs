using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStormAttack : MonoBehaviour, IAttack
{
    #region FIELDS
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private GameObject misileObject;
    [SerializeField] private Transform target;
    [Tooltip("Tiempo que la animación se queda atacando hasta que finaliza el ataque")]
    [SerializeField] private float timeLoopAttack;
    [SerializeField] private float timeToFaceTarget;
    
    private bool _prepareAttack;
    private bool _isLooping;
    #endregion

    private void Update()
    {
        if (weeperAnimation.IfCurrentAnimationIsPlaying("Cast02start"))
        {
            _prepareAttack = true;
        }
        else
        {
            _prepareAttack = false;
        }
    }

    public void Attack()
    {
        Instantiate(misileObject, target.position, misileObject.transform.rotation);
    }

    private void FixedUpdate()
    {
        if (!_prepareAttack)
        {
            return;
        }

        RotateToPlayer();
    }

    private void RotateToPlayer()
    {
        var rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * timeToFaceTarget);
    }

    public void TimeToCanFinishAttack() //Method called from AnimationEvent Cast02Loop
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
}
