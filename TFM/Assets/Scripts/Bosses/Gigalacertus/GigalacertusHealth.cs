using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigalacertusHealth : MonoBehaviour
{
    #region FIELDS
    [Header("References")]
    [SerializeField] private GigalacertusAnimation gigalacertusAnimation;
    [SerializeField] private BloodPrefabs bloodPrefabs;
    [SerializeField] private Collider[] gigalocertusColliders;
    [SerializeField] private Collider[] tongueColliders;

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
        bloodPrefabs.InstantiateBlood(hit);
    }

    public void CheckIfIAmDead()
    {
        if (currentHealth <= 0)
        {
            Dead();
        }
        else
        {
            isGettingHurt = true;
            gigalacertusAnimation.TongueHitAnim();
        }
    }

    public void CanGotHitAnimAgain() //Method called by AnimationEvent
    {
        isGettingHurt = false;
    }

    private void Dead()
    {
        DisableAllColliders();
        isDead = true;
        gigalacertusAnimation.DeadAnim();
    }

    public void EnableTongueColliders() //Method called by AnimationEvent
    {
        foreach (Collider collider in tongueColliders)
        {
            collider.enabled = true;
        }
    }

    public void DisableTongueColliders() //method called by AnimationEvent
    {
        foreach (Collider collider in tongueColliders)
        {
            collider.enabled = false;
        }
    }

    private void DisableAllColliders()
    {
        foreach (Collider collider in gigalocertusColliders)
        {
            collider.enabled = false;
        }
    }
    #endregion
}
