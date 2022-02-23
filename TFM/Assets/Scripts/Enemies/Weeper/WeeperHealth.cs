using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeperHealth : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private WeeperAttack weeperAttack;
    [SerializeField] private WeeperAnimation weeperAnimaton;
    [SerializeField] private BloodPrefabs bloodPrefab;
    [SerializeField] private CapsuleCollider[] weeperColliders;

    [Header("Parameters")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isGettingHurt;
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

    public void GenerateBlood(Transform hit)
    {
        bloodPrefab.InstantiateBlood(hit);
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
            weeperAttack.isAttacking = false;
            weeperAttack._canAttack = true;
            isGettingHurt = true;
            weeperAnimaton.HitAnim();
        }
    }

    public void CanGotHitAnimAgain() //Method called by AnimationEvent GotHit
    {
        isGettingHurt = false;
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
