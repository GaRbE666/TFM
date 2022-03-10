using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleHealth : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private GargoyleAnimation gargoyleAnimation;
    [SerializeField] private GargoyleAttack gargoyleAttack;
    [SerializeField] private BloodPrefabs bloodPrefab;
    [SerializeField] private CapsuleCollider[] gargoyleColliders;

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

    public void CheckIfIAmDead()
    {
        if (currentHealth <= 0)
        {
            DisableAllColliders();
            isDead = true;
            gargoyleAnimation.DeadAnim();
        }
        else
        {
            gargoyleAttack.isAttacking = false;
            gargoyleAttack._canAttack = true;
            isGettingHurt = true;
            gargoyleAnimation.HitAnim();
        }
    }

    public void CanGotHitAnimAgain() //Method called by AnimationEvent Hit amd FlyHit
    {
        isGettingHurt = false;
    }

    private void DisableAllColliders()
    {
        foreach (CapsuleCollider collider in gargoyleColliders)
        {
            collider.enabled = false;
        }
    }
    #endregion
}
