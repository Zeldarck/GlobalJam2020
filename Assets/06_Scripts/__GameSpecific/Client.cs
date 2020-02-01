using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Client : MonoBehaviour
{

    [SerializeField]
    List<IconItem> m_iconItemList = new List<IconItem>();

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
            IconItem iconItem = m_iconItemList.Find(o => o.ItemType == m_wantedItem);

            if (iconItem == null)
            {
                Debug.LogError("No item setup in Client for " + m_wantedItem);
                return;
            }

            iconItem.gameObject.SetActive(true);

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
