using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeperHealth : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private WeeperAnimation weeperAnimaton;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    /*[HideInInspector]*/ public bool isDead;
    #endregion

    private void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    #region CUSTOM METHODS
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckIfIAmDead();
    }

    private void CheckIfIAmDead()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            weeperAnimaton.DeathAnim();
        }
        else
        {
            weeperAnimaton.HitAnim();
        }
    }
    #endregion
}
