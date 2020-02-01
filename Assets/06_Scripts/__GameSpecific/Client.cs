using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{

    [SerializeField]
    ThrowableItemType m_wantedItem;

    [SerializeField]
    float m_waitingTime = 5.0f;


    [SerializeField]
    int m_baseScore = 10;

    [SerializeField]
    float m_baseRage = 10;

    Timer m_timer;

    public ThrowableItemType WantedItem { get => m_wantedItem; set => m_wantedItem = value; }

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
    }

    private void OnDestroy()
    {
        if (m_timer != null)
        {
            Destroy(m_timer);
        }
    }

}
