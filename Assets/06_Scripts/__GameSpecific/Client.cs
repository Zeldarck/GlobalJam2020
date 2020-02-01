using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{

    [SerializeField]
    ThrowableItemType m_wantedItem;

    public ThrowableItemType WantedItem { get => m_wantedItem; set => m_wantedItem = value; }


    public void CompleteClient(bool a_isHappy)
    {
        EventManager.Instance.InvokeOnClientComplete(this, new ClientEventArgs(this));
    }

}
