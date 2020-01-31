using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory2 : MonoBehaviour
{

    ThrowableItem m_mainItem;
    ThrowableItem m_secondItem;

    void ExchangeItem()
    {
        ThrowableItem temp = m_secondItem;
        m_secondItem = m_mainItem;
        m_mainItem = temp;
    }

    void GiveItem(ThrowableItem a_item)
    {
        m_mainItem = a_item;
    }
}
