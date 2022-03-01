using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStormDamage : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private float radius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float damage;
    #endregion

    #region UNITY METHODS
    private void OnParticleCollision(GameObject other)
    {
        Collider[] colliders = Physics.OverlapSphere(startPoint.position, radius, ground);

        foreach (Collider collision in colliders)
        {
            Debug.Log(collision.transform.position);
            if (collision.GetComponent<PlayerHealth>())
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                if (playerHealth.death)
                {
                    return;
                }
                playerHealth.TakeDamage(damage);
            }
        }
    }
    #endregion
}
