using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeFireDamage : MonoBehaviour
{
    #region FIELDS
    [Header("Parameters")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask player;
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float offsetZ;
    [Header("References")]
    [SerializeField] private GameObject hit;

    private bool _canDamage;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        _canDamage = true;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        Vector3 offsetVector = new Vector3(transform.position.x, transform.position.y, transform.position.z + offsetZ);
        Collider[] colliders = Physics.OverlapSphere(offsetVector, radius, player);

        foreach (Collider collision in colliders)
        {
            if (collision.GetComponent<PlayerHealth>() && _canDamage)
            {
                _canDamage = false;
                Instantiate(hit, collision.transform.position + Vector3.up, transform.rotation);
                Debug.Log("Entro");
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                if (playerHealth.death)
                {
                    return;
                }
                playerHealth.TakeDamage(damage);
                radius = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 offsetVector = new Vector3(transform.position.x, transform.position.y, transform.position.z + offsetZ);
        Gizmos.DrawWireSphere(offsetVector, radius);
    }
    #endregion
}
