using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [Range(0, 1)]
    [SerializeField] private float damageReduction;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerBlock playerBlock;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private PlayerWeapon playerWeapon;

    [HideInInspector] public bool death;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        currentHealth = maxHealth;
        HUDController.instance.SetPlayerHealthMaxValueSlider(currentHealth);
    }

    #endregion

    #region CUSTOM METHODS
    public void TakeDamage(float damage)
    {
        if (playerAnimation.IfCurrentAnimationIsPlaying("Shield-Block") || playerAnimation.IfCurrentAnimationIsPlaying("Shield-Walk-Slow-Block"))
        {
            ReduceDamage(damage, damageReduction);
            HUDController.instance.ConsumeStamina(playerBlock.costOfStamina);
        }
        else
        {
            currentHealth -= damage;
        }
        HUDController.instance.SetPlayerHealthValue(currentHealth);
        CheckIfIAmDead();
    }

    private void ReduceDamage(float damage, float reduction)
    {
        currentHealth -= (damage * reduction);
    }

    private void CheckIfIAmDead()
    {
        if (currentHealth <= 0)
        {
            PlayerDead();
        }
        else
        {
            playerAnimation.HitAnim();
        }
    }

    private void PlayerDead()
    {
        playerMovement.enabled = false;
        death = true;
        playerAnimation.DeathAnim();
    }
    #endregion
}
