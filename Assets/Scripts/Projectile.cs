using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float m_ProjectileSpeed = 1.0f;
    [SerializeField]
    private float m_Lifetime = 1.0f;

    private float m_Lifetimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * m_ProjectileSpeed;

        m_Lifetimer += Time.deltaTime;
        if (m_Lifetimer > m_Lifetime)
        {
            Destroy(gameObject);
        }
    }
}
