using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Projectile;
    [SerializeField]
    private float m_MoveSpeed = 1.0f;
    [SerializeField]
    private float m_Health = 100f;

    private static Vector2 m_MoveInput;
    private GameObject m_ProjectileGameObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_MoveInput != Vector2.zero)
        {
            Vector3 moveInput = new Vector3(m_MoveInput.x, m_MoveInput.y, 0f);
            transform.Translate(moveInput * m_MoveSpeed * Time.deltaTime, Space.World);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_MoveInput = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if ( context.performed )
        {
            m_ProjectileGameObject = Instantiate(m_Projectile);
            m_ProjectileGameObject.transform.position = transform.position;
        }
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
