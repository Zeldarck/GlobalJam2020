using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory2 : MonoBehaviour
{

    [SerializeField]
    List<ThrowableItem> m_litemPrefab = new List<ThrowableItem>();


    [SerializeField]
    Vector3 m_localOffsetMainItem = new Vector3();

    [SerializeField]
    Vector3 m_localOffsetSecondItem = new Vector3();

    ThrowableItem m_mainItem;
    ThrowableItem m_secondItem;

    bool m_canThrow = false;

    public ThrowableItem MainItem
    {
        get => m_mainItem;
        private set
        {
            m_mainItem = value;
            CanThrow = false;
        }
    }

    public ThrowableItem SecondItem { get => m_secondItem; private set => m_secondItem = value; }
    public bool CanThrow { get => m_canThrow; set => m_canThrow = value; }

    private void Start()
    {
        EventManager.Instance.RegisterOnGiveRandomItem((o) => GiveItem());
        EventManager.Instance.RegisterOnGiveItem((o, itemEvent) => GiveItem(itemEvent.m_itemType));
        EventManager.Instance.RegisterOnStart(o => Init());
    }

    private void Init()
    {
        if (MainItem != null)
        {

            Destroy(MainItem.gameObject);
        }
        if (SecondItem != null)
        {

            Destroy(SecondItem.gameObject);
        }
        MainItem = null;
        SecondItem = null;
        GiveItem();
        GiveItem();
    }


    private void Update()
    {
        if (MainItem != null)
        {
            MainItem.transform.localPosition = m_localOffsetMainItem;
            MainItem.transform.localRotation = Quaternion.Euler(0, -155, 0);
            MainItem.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            CanThrow = true;
        }
        if (SecondItem != null)
        {
            SecondItem.transform.localPosition = m_localOffsetSecondItem;
            SecondItem.transform.localRotation = Quaternion.Euler(0, 155, 0);
            SecondItem.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }

    public ThrowableItem RemoveMainItem()
    {
        ThrowableItem res = null;

        if (MainItem != null)
        {
            //center the item
            MainItem.transform.localPosition = new Vector3(0, m_localOffsetMainItem.y, m_localOffsetMainItem.z);
            MainItem.transform.SetParent(null);
            res = MainItem;
            MainItem = null;
        }

        return res;
    }

    public void ExchangeItem()
    {
        ThrowableItem temp = SecondItem;
        SecondItem = MainItem;
        MainItem = temp;
    }

    public void GiveItem()
    {
        if (CanGiveItem())
        {
            GiveItem(GenerateRandomItem());
        }
    }


    public void GiveItem(ThrowableItemType a_itemType)
    {
        if (CanGiveItem())
        {
            GiveItem(GenerateItem(a_itemType));
        }
    }


    public void GiveItem(ThrowableItem a_item)
    {
        if (MainItem == null)
        {
            MainItem = a_item;
        }
        else if (SecondItem == null)
        {
            SecondItem = a_item;
        }
        else
        {
            Destroy(a_item.gameObject);
        }
    }


    bool CanGiveItem()
    {
        return MainItem == null || SecondItem == null;
    }


    ThrowableItem GenerateRandomItem()
    {
        if (m_litemPrefab.Count == 0)
        {
            Debug.LogError("No item setup in Inventory");
            return null;
        }

        int id = Utils.RandomInt(0, m_litemPrefab.Count);

        GameObject item = GameObjectManager.Instance.InstantiateObject(m_litemPrefab[id].gameObject, Vector3.zero, Quaternion.identity, SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);

        item.transform.SetParent(this.transform, false);

        return item.GetComponent<ThrowableItem>();
    }


    ThrowableItem GenerateItem(ThrowableItemType a_itemType)
    {
        ThrowableItem itemPrefab = m_litemPrefab.Find(o => o.ItemType == a_itemType);

        if (itemPrefab == null)
        {
            Debug.LogError("No item setup as " + a_itemType + " in Inventory2");
            return null;
        }


        GameObject item = GameObjectManager.Instance.InstantiateObject(itemPrefab.gameObject, Vector3.zero, Quaternion.identity, SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);

        item.transform.SetParent(this.transform, false);

        return item.GetComponent<ThrowableItem>();
    }


}
