using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region FIELDS
    //[SerializeField] private Weapon swordScriptable;
    [SerializeField] private bool canHurt;
    [SerializeField] private PlayerWeapon weapons;
    [SerializeField] private PlayerAnimation _playerAnimation;
    #endregion

    #region UNITY METHODS
    //private void Awake()
    //{
    //    _playerAnimation = transform.parent.gameObject.GetComponent<PlayerAnimation>();
    //}

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
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && canHurt)
        {
            Debug.Log(other.transform.root);
            WeeperHealth weeperHealth = other.transform.root.GetComponent<WeeperHealth>();
            weeperHealth.TakeDamage(CalculateDamage());
            weeperHealth.GenerateBlood(other.transform);
        }
    }
    #endregion

    #region CUSTOM METHODS

    private float CalculateDamage()
    {
        float totalDamage;
        totalDamage = weapons.activeWeapon.damage;
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

    public void PlayerCanHurt()
    {
        canHurt = true;
    }

    public void PlayerCanNotHurt()
    {
        canHurt = false;
    }
    #endregion
}
