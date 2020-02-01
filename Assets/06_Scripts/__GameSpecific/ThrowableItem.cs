using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThrowableItemType { TV, APPAREIL_PHOTO, FRITEUSE, TETE_DE_MORT, BURRITOS, THEIRE, NAMICA_SWITCH, CACTUS, NB_ITEM_TYPE};

public class ThrowableItem : MonoBehaviour
{

    [SerializeField]
    ThrowableItemType m_itemType;

    bool m_isDead = false;

    public ThrowableItemType ItemType { get => m_itemType; set => m_itemType = value; }
    public bool IsDead { get => m_isDead; set => m_isDead = value; }

    // Probablement switch sur un système se basant sur la velocité
    public void Fired()
    {
        Utils.TriggerWaitForSeconds(0.67f, () => Dead());
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
