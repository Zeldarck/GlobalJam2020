using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public ThrowableItem MainItem { get => m_mainItem; private set => m_mainItem = value; }
    public ThrowableItem SecondItem { get => m_secondItem; private set => m_secondItem = value; }

    private void Start()
    {
        GiveItem(GenerateRandomItem());
        GiveItem(GenerateRandomItem());
    }


    private void Update()
    {
        if (MainItem != null)
        {
            MainItem.transform.localPosition = m_localOffsetMainItem;
        }
        if (SecondItem != null)
        {
            SecondItem.transform.localPosition = m_localOffsetSecondItem;
        }
    }

    public ThrowableItem RemoveMainItem()
    {

        if (MainItem != null)
        {
            MainItem.transform.SetParent(null);
        }


        ThrowableItem res = MainItem;
        MainItem = null;

        return res;
    }

    void ExchangeItem()
    {
        ThrowableItem temp = SecondItem;
        SecondItem = MainItem;
        MainItem = temp;
    }

    void GiveItem(ThrowableItem a_item)
    {
        if (MainItem == null)
        {
            MainItem = a_item;
        }
        else if (SecondItem == null)
        {
            SecondItem = a_item;
        }
    }


    ThrowableItem GenerateRandomItem()
    {
        if (m_litemPrefab.Count == 0)
        {
            Debug.LogError("No item setup in Inventory");
            return null;
        }

        int id = Utils.RandomInt(0, m_litemPrefab.Count);

        GameObject item = GameObjectManager.Instance.InstantiateObject(m_litemPrefab[id].gameObject, new Vector3(0,0,0), Quaternion.identity, SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);

        item.transform.SetParent(this.transform, false);

        return item.GetComponent<ThrowableItem>();
    }

}
