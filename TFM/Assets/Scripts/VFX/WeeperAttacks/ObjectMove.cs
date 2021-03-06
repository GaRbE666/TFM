using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public float time;
    float m_time;
    float m_time2;
    public float MoveSpeed = 10;
    public bool AbleHit;
    public float HitDelay;
    public GameObject m_hitObject;
    GameObject m_makedObject;
    public float MaxLength;
    public float DestroyTime2;
    float m_scalefactor;
    [SerializeField] private float damage;

    private void Start()
    {
        m_scalefactor = transform.parent.localScale.x;
        m_time = Time.time;
        m_time2 = Time.time;
    }

    void LateUpdate()
    {
        if (Time.time > m_time + time)
            Destroy(gameObject);

        transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed * m_scalefactor);
        if(AbleHit)
        { 
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, MaxLength))
            {
                if (Time.time > m_time2 + HitDelay)
                {
                    m_time2 = Time.time;
                    HitObj(hit);
                }
            }
        }
    }

    void HitObj(RaycastHit hit)
    {
        Debug.Log("Entro");
        m_makedObject = Instantiate(m_hitObject, hit.point, Quaternion.LookRotation(hit.normal)).gameObject;
        PlayerHealth playerHealth = hit.transform.GetComponent<PlayerHealth>();
        if (!playerHealth.death)
        {
            playerHealth.TakeDamage(damage);
        }
        Destroy(m_makedObject, DestroyTime2);
    }

}
