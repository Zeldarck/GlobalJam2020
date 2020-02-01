using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThrowableItemType { TV, APPAREIL_PHOTO, FRITEUSE};

public class ThrowableItem : MonoBehaviour
{

    [SerializeField]
    ThrowableItemType m_itemType;

    bool m_isDead = false;

    public ThrowableItemType ItemType { get => m_itemType; set => m_itemType = value; }
    public bool IsDead { get => m_isDead; set => m_isDead = value; }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Probablement switch sur un système se basant sur la velocité
    public void Fired()
    {
        Utils.TriggerWaitForSeconds(0.75f, () => Dead());
    }

    void Dead()
    {
        if(this != null)
        {
            Debug.Log("ThrowableItem " + this + " is dead");
            IsDead = true;
            EventManager.Instance.InvokeOnGiveItem(this, new ItemEventArgs(m_itemType));
        }
    }
}
