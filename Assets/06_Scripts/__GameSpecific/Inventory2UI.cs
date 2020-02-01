using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory2UI : MonoBehaviour
{
    Inventory2 m_inventory;

    [SerializeField]
    Text m_slotMainItem;

    [SerializeField]
    Text m_slotSecondItem;


    void Start()
    {
        m_inventory = Player.Instance.GetComponent<Inventory2>();
    }


    void Update()
    {
        if(m_inventory.MainItem != null)
        {
            m_slotMainItem.text = m_inventory.MainItem.ItemType.ToString();
        }
        else
        {
            m_slotMainItem.text = "";
        }

        if (m_inventory.SecondItem != null)
        {
            m_slotSecondItem.text = m_inventory.SecondItem.ItemType.ToString();
        }
        else
        {
            m_slotSecondItem.text = "";
        }
    }
}
