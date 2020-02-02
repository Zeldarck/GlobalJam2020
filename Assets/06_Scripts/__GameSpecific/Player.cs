using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{

    [SerializeField]
    float m_height = 1.55f;

    [SerializeField]
    float m_speed = 1.5f;



    [SerializeField]
    float m_fireForce = 1.0f;

    [SerializeField]
    float m_fireAngle = 35.0f;

    Inventory2 m_inventory;

    bool m_enableFire = false;

    public float Height { get => m_height; set => m_height = value; }
    public float Speed { get => m_speed; set => m_speed = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_inventory = GetComponent<Inventory2>();
        EventManager.Instance.RegisterOnStart((o) => m_enableFire = true);
        EventManager.Instance.RegisterOnLoose((o) => m_enableFire = false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        if (Input.GetMouseButtonDown(1))
        {
            m_inventory.ExchangeItem();
        }

    }

    private void Fire()
    {
        if (!m_inventory.CanThrow || !m_enableFire)
        {
            return;
        }

        ThrowableItem item =  m_inventory.RemoveMainItem();

        if(item == null)
        {
            return;
        }


        Rigidbody rigidbody = item.GetComponent<Rigidbody>();
        Vector3 direction = transform.forward * m_fireForce;
        direction = Quaternion.Euler(m_fireAngle * transform.right) * direction;

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(direction, ForceMode.Impulse);
        rigidbody.AddRelativeTorque(Utils.RandomFloat(0, 2), Utils.RandomFloat(0, 2), Utils.RandomFloat(0, 2));

        item.Fired();
        m_inventory.ExchangeItem();
    }
}
