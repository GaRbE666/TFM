using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask player;
    [SerializeField] private float damage;

    private bool canDamage;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        canDamage = true;
    }

    private void FixedUpdate()
    {
        if (!particle.isPlaying)
        {
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, player);

        foreach (Collider collision in colliders)
        {
            if (collision.GetComponent<PlayerHealth>() && canDamage)
            {
                StartCoroutine(CanDamageAgain());
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                if (playerHealth.death)
                {
                    return;
                }
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    #endregion

    private IEnumerator CanDamageAgain()
    {
        canDamage = false;
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }
}
