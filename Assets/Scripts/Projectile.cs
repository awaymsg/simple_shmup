using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float m_ProjectileSpeed = 1.0f;
    [SerializeField]
    private float m_Lifetime = 1.0f;
    [SerializeField]
    private float m_Damage = 25f;
    [SerializeField]
    private bool m_IsEnemyProjectile = false;

    private float m_Lifetimer = 0f;
    private Collider2D m_Collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (m_IsEnemyProjectile ? -transform.up : transform.up) * Time.deltaTime * m_ProjectileSpeed;

        m_Lifetimer += Time.deltaTime;
        if (m_Lifetimer > m_Lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (m_IsEnemyProjectile)
        {
            PlayerController playerBaseClass = other.gameObject.GetComponent<PlayerController>();
            if (playerBaseClass != null)
            {
                playerBaseClass.TakeDamage(m_Damage);
                Destroy(gameObject);
            }
            return;
        }

        EnemyBase enemyBaseClass = other.gameObject.GetComponent<EnemyBase>();
        if (enemyBaseClass != null)
        {
            enemyBaseClass.TakeDamage(m_Damage);
            Destroy(gameObject);
        }
    }
}
