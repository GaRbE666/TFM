using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeperHealth : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private WeeperAnimation weeperAnimaton;
    [SerializeField] private CapsuleCollider[] weeperColliders;

    [Header("Parameters")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [HideInInspector] public bool isDead;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
    #endregion

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
            DisableAllColliders();
            isDead = true;
            weeperAnimaton.DeathAnim();
        }
        else
        {
            weeperAnimaton.HitAnim();
        }
    }

    private void DisableAllColliders()
    {
        foreach (CapsuleCollider collider in weeperColliders)
        {
            collider.enabled = false;
        }
    }
    #endregion
}
