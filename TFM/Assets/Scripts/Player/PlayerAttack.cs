using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private PlayerWeapon weapons;
    [SerializeField] private PlayerAnimation _playerAnimation;

    [Header("Config")]
    [SerializeField] private bool canHurt;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float radius;

    [Header("Debuggin")]
    [SerializeField] private bool canDraw;
    [SerializeField] private Transform weaponActive;

    #endregion

    #region UNITY METHODS
    private void Update()
    {
        if (InputController.instance.isAttacking && InputController.instance.canPress)
        {
            Attack();
        }

        if (InputController.instance.isStrongAttacking && InputController.instance.canPress)
        {
            StrongAttack();
        }

        //if (canHurt)
        //{
        //    CheckForEnemy();
        //}
    }

    private void OnDrawGizmos()
    {
        if (canDraw)
        {
            Gizmos.DrawWireSphere(weaponActive.position, radius);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == 8 && canHurt)
    //    {
    //        Debug.Log(other.transform.root);
    //        WeeperHealth weeperHealth = other.transform.root.GetComponent<WeeperHealth>();
    //        weeperHealth.TakeDamage(CalculateDamage());
    //        weeperHealth.GenerateBlood(other.transform);
    //    }
    //}
    #endregion

    #region CUSTOM METHODS

    private float CalculateDamage()
    {
        float totalDamage;
        totalDamage = weapons.scriptableActiveWeapon.damage;
        return totalDamage;
    }

    private void StrongAttack()
    {
        _playerAnimation.StrongAttackAnim();
        InputController.instance.isStrongAttacking = false;
    }

    private void Attack()
    {
        _playerAnimation.AttackAnim();
        InputController.instance.isAttacking = false;
    }

    public void PlayerCanHurt() //Method Call by AnimationEvent
    {
        canHurt = true;
    }

    public void CheckForEnemy()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(weapons.activeWeapon.transform.GetChild(0).position, weapons.scriptableActiveWeapon.radiusHit, enemyLayer);
        foreach (Collider enemyCollider in enemyColliders)
        {
            WeeperHealth weeperHealth = enemyCollider.transform.root.GetComponent<WeeperHealth>();
            weeperHealth.TakeDamage(CalculateDamage());
            weeperHealth.GenerateBlood(enemyCollider.gameObject.transform);
        }
    }

    public void PlayerCanNotHurt() //Method call by AnimationEvent
    {
        canHurt = false;
    }
    #endregion
}
