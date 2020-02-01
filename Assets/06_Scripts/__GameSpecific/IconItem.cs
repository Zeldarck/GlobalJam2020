using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconItem : MonoBehaviour
{

    [SerializeField]
    ThrowableItemType m_itemType;
    public ThrowableItemType ItemType { get => m_itemType; set => m_itemType = value; }

}
