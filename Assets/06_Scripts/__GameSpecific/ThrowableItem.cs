using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThrowableItemType { TV, APPAREIL_PHOTO, FRITEUSE};

public class ThrowableItem : MonoBehaviour
{

    [SerializeField]
    ThrowableItemType m_itemType;

    public ThrowableItemType ItemType { get => m_itemType; set => m_itemType = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
