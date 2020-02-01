using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{

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
            m_inventory.RemoveMainItem();
        }
    }
}
