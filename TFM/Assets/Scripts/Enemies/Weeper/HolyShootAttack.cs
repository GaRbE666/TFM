using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyShootAttack : MonoBehaviour, IAttack
{
    #region FIELDS
    [SerializeField] private WeeperAnimation weeperAnimation;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject holyShoot;
    [SerializeField] private Transform startPointAttack;
    [SerializeField] private float timeToFaceTarget;

    private bool _isShooting;
    #endregion

    private void Update()
    {
        if (weeperAnimation.IfCurrentAnimationIsPlaying("Cast01"))
        {
            _isShooting = true;
        }
        else
        {
            _isShooting = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_isShooting)
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

    public void Attack()
    {
        Instantiate(holyShoot, startPointAttack.position, transform.rotation);
    }
}
