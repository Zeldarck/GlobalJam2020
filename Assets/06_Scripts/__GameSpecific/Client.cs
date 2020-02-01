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

    public ThrowableItemType WantedItem { get => m_wantedItem; set => m_wantedItem = value; }

    public void StartTimer()
    {
        Utils.TriggerWaitForSeconds(m_waitingTime, () => CompleteClient(false));
    }

    public void CompleteClient(bool a_isHappy)
    {
        if(this == null)
        {
            return;
        }
        EventManager.Instance.InvokeOnClientComplete(this, new ClientEventArgs(this));
    }

}
