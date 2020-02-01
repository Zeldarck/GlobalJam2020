using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{

    [SerializeField]
    float m_fireForce = 1.0f;

    [SerializeField]
    float m_fireAngle = 35.0f;

    Inventory2 m_inventory;
    // Start is called before the first frame update
    void Start()
    {
        m_inventory = GetComponent<Inventory2>();
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
        if (!m_inventory.CanThrow)
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
    }
}
