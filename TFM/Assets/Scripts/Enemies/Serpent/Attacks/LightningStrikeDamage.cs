using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeDamage : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Vector3 bounds;
    [SerializeField] private LayerMask player;
    [SerializeField] private float damage;
    [SerializeField] private float Offset;
    #endregion

    #region UNITY METHODS
    private void FixedUpdate()
    {
        if (!particle.isPlaying)
        {
            bounds = Vector3.zero;
            return;
        }
        Vector3 offsetPosition = new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z);
        Collider[] colliders = Physics.OverlapBox(offsetPosition, bounds, transform.rotation);

        foreach (Collider collision in colliders)
        {
            if (collision.GetComponent<PlayerHealth>())
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                if (playerHealth.death)
                {
                    return;
                }
                playerHealth.TakeDamage(damage);
                bounds = Vector3.zero;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z);
        Gizmos.DrawWireCube(newPosition, bounds);
    }
    #endregion
}
