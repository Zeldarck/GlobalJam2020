using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ItemTypeIcon
{
    [SerializeField]
    IconItem m_iconItem;
    [SerializeField]
    ThrowableItemType m_itemType;

    public IconItem IconItem { get => m_iconItem; set => m_iconItem = value; }
    public ThrowableItemType ItemType { get => m_itemType; set => m_itemType = value; }
}


public class Client : MonoBehaviour
{

    [SerializeField]
    List<ItemTypeIcon> m_itemTypeIconList = new List<ItemTypeIcon>();


    [SerializeField]
    ThrowableItemType m_wantedItem;

    [SerializeField]
    float m_waitingTime = 5.0f;


    [SerializeField]
    int m_baseScore = 10;

    [SerializeField]
    int m_minimumScore = 4;



    [SerializeField]
    float m_baseRage = 10;

    Timer m_timer;

    public ThrowableItemType WantedItem
    {
        get => m_wantedItem;
        set
        {
            m_wantedItem = value;
            ItemTypeIcon iconItemType = m_itemTypeIconList.Find(o => o.ItemType == m_wantedItem);

            if (iconItemType == null)
            {
                Debug.LogError("No item setup in Client for " + m_wantedItem);
                return;
            }

            iconItemType.IconItem.gameObject.SetActive(true);

        }
    }


    public void StartTimer()
    {
        m_timer = TimerFactory.Instance.GetTimer();
        m_timer.StartTimer(m_waitingTime, () => CompleteClient(false));
    }

    public void CompleteClient(bool a_isHappy)
    {
        if(this == null)
        {
            return;
        }
        EventManager.Instance.InvokeOnClientComplete(this, new ClientEventArgs(this));

        if(!a_isHappy)
        {
            EventManager.Instance.InvokeOnRageIncrease(this, new NumberEventArgs(m_baseRage));
        }
        else
        {
            EventManager.Instance.InvokeOnScoreIncrease(this, new IntEventArgs((int)Mathf.Max(m_baseScore * m_timer.GetTimeLeft()/m_waitingTime, 4)));
        }
    }

    private void OnDestroy()
    {
        if (m_timer != null)
        {
            Destroy(m_timer);
        }
    }

}
