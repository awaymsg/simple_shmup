using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private float m_Health = 100f;
    [SerializeField]
    private Vector3[] m_Path;
    [SerializeField]
    private float m_MoveSpeed = 1.0f;
    [SerializeField]
    private GameObject m_ProjectilePrefab;
    [SerializeField]
    private float m_TickSpeed = 0.1f;

    private int currentTarget = 0;
    private float m_TickTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (m_Path != null && m_Path.Length > 0)
        {
            currentTarget = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Path != null && m_Path.Length > 0 && MoveTo(m_Path[currentTarget]))
        {
            currentTarget++;

            if (currentTarget >= m_Path.Length)
            {
                currentTarget = 0;
            }
        }

        m_TickTimer += Time.deltaTime;
        
        if (m_TickTimer > m_TickSpeed)
        {
            if (UnityEngine.Random.Range(0, 50) < 1)
            {
                GameObject projectileGameObject = Instantiate(m_ProjectilePrefab);
                projectileGameObject.transform.position = transform.position;
            }

            m_TickTimer = 0f;
        }
    }

    private bool MoveTo(Vector3 location)
    {
        transform.position = Vector3.MoveTowards(transform.position, location, m_MoveSpeed * Time.deltaTime);

        if ((transform.position - location).sqrMagnitude < 0.005f)
        {
            return true;
        }

        return false;
    }

    public void TakeDamage(float dmg)
    {
        m_Health -= dmg;
        
        if (m_Health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
