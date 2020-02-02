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
        EventManager.Instance.RegisterOnSetCornerOrder((o, item, order) => SetItemOrder(item.m_itemType, order.m_int));
        EventManager.Instance.RegisterOnLoose((o) => Clean());
    }

    void Clean()
    {
        m_currentItemIcon = new List<IconItem>();
        Utils.DestroyChilds(m_container.transform);
    }

    void SetItemOrder(ThrowableItemType a_item, int a_order)
    {
        if (!GameManager.Instance.GameTimer.IsTimerRunning())
        {
            return;
        }

        IconItem iconItem = m_currentItemIcon.Find(x => x.ItemType == a_item);

        if (iconItem == null)
        {

            IconItem iconItemPrefab = m_itemIconPrefab.Find(x => x.ItemType == a_item);

            if(iconItemPrefab == null)
            {
                Debug.LogError("No iconItem set up in Corner Ui for " + a_item);
                return;
            }
            iconItem = Instantiate(iconItemPrefab, m_container.transform);
            m_currentItemIcon.Add(iconItem);
        }

        iconItem.transform.SetSiblingIndex(a_order);
    }

}
