using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerItemUI : MonoBehaviour
{

    [SerializeField]
    List<IconItem> m_itemIconPrefab = new List<IconItem>();

    [SerializeField]
    GameObject m_container;

    List<IconItem> m_currentItemIcon = new List<IconItem>();

    void Start()
    {
        foreach(IconItem prefab in m_itemIconPrefab)
        {
            m_currentItemIcon.Add(Instantiate(prefab, m_container.transform));
        }

        EventManager.Instance.RegisterOnSetCornerOrder((o, item, order) => SetItemOrder(item.m_itemType, order.m_int));
    }

    void SetItemOrder(ThrowableItemType a_item, int a_order)
    {


        IconItem iconItem = m_currentItemIcon.Find(x => x.ItemType == a_item);

        if (iconItem == null)
        {
            Debug.LogError("No iconItem in Corner Ui for " + a_item);
            return;
        }

        iconItem.transform.SetSiblingIndex(a_order);
    }

}
