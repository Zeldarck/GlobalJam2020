using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconItem : MonoBehaviour
{

    [SerializeField]
    ThrowableItemType m_itemType;
    public ThrowableItemType ItemType { get => m_itemType; set => m_itemType = value; }

    [SerializeField]
    Animator m_animatorFx;

    int m_wantedIndex;


    public void ModifySiblingIndex(int a_wantedIndex)
    {
        m_wantedIndex = a_wantedIndex;
        m_animatorFx.SetTrigger("Disapear");
       // Debug.Log("Begin Modify Index : " + ItemType + "   id : " + m_wantedIndex);
    }

    void EndFX()
    {
        transform.SetSiblingIndex(m_wantedIndex);
       // Debug.Log("End Modify Index : " + ItemType + "   id : "  + m_wantedIndex);

    }

}
