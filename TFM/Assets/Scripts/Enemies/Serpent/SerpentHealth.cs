using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentHealth : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private SerpentAttack serpentAttack;
    [SerializeField] private SerpentAnimation serpentAnimaton;
    [SerializeField] private BloodPrefabs bloodPrefab;
    [SerializeField] private CapsuleCollider[] serpentColliders;
    [SerializeField] private EnemyScore enemyScore;

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

    public void GenerateBlood(Transform hit) //Called by PlayerAttack
    {
        bloodPrefab.InstantiateBlood(hit);
    }

    private void CheckIfIAmDead()
    {
        if (currentHealth <= 0)
        {
            Dead();
        }
        else
        {
            serpentAttack.isAttacking = false;
            serpentAttack._canAttack = true;
            isGettingHurt = true;
            serpentAnimaton.HitAnim();
        }
    }

    public void CanGotHitAnimAgain() //Method called by AnimationEvent GotHit
    {
        isGettingHurt = false;
    }

    private void Dead()
    {
        DisableAllColliders();
        isDead = true;
        serpentAnimaton.DeadAnim();
        HUDController.instance.SetSoulsInCounter(enemyScore.GetScore());
    }

    private void DisableAllColliders()
    {
        foreach (CapsuleCollider collider in serpentColliders)
        {
            collider.enabled = false;
        }

    }
    #endregion
}
