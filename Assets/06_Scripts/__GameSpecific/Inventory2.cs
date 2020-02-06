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


    [SerializeField]
    Vector3 m_localOffsetMainItemVfx = new Vector3();

    [SerializeField]
    Vector3 m_localOffsetSecondItemVfx = new Vector3();


    [SerializeField]
    GameObject m_vfxAppearPrefab;


    ParticleSystem m_vfxAppearMainItem;
    ParticleSystem m_vfxAppearSecondItem;




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

        m_vfxAppearMainItem = Instantiate(m_vfxAppearPrefab, transform).GetComponent<ParticleSystem>();
        m_vfxAppearSecondItem = Instantiate(m_vfxAppearPrefab, transform).GetComponent<ParticleSystem>();
        m_vfxAppearMainItem.transform.localPosition = m_localOffsetMainItemVfx;
        m_vfxAppearSecondItem.transform.localPosition = m_localOffsetSecondItemVfx;

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

        Utils.TriggerWaitForSeconds(0.5f, GiveItem);
        Utils.TriggerWaitForSeconds(0.5f, GiveItem);
    }


    private void Update()
    {
        if (MainItem != null)
        {
            MainItem.transform.localPosition = m_localOffsetMainItem;
            MainItem.transform.localRotation = Quaternion.Euler(0, -155, 0);
            MainItem.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            CanThrow = true;

            if(m_vfxAppearMainItem.time > 0.1f && !MainItem.IsActiveMesh())
            {
                MainItem.DisplayMesh();
            }
        }
        if (SecondItem != null)
        {
            SecondItem.transform.localPosition = m_localOffsetSecondItem;
            SecondItem.transform.localRotation = Quaternion.Euler(0, 155, 0);
            SecondItem.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            if (m_vfxAppearSecondItem.time > 0.1f && !SecondItem.IsActiveMesh())
            {
                SecondItem.DisplayMesh();
            }
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
            CornerManager.Instance.TriggerNextItem();
            //GiveItem(GenerateRandomItem());
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
            m_vfxAppearMainItem.Play();
        }
        else if (SecondItem == null)
        {
            SecondItem = a_item;
            m_vfxAppearSecondItem.Play();
        }
        else
        {
            Destroy(a_item.gameObject);
        }
    }


    bool CanGiveItem()
    {
        return GameManager.Instance.GameTimer.IsTimerRunning() && (MainItem == null || SecondItem == null);
    }


    ThrowableItem GenerateRandomItem()
    {

        if (m_litemPrefab.Count == 0)
        {
            Debug.LogError("No item setup in Inventory");
            return null;
        }
        ThrowableItemType type = CornerManager.Instance.GetRandomItemType();
        ThrowableItem prefab = m_litemPrefab.Find(x => x.ItemType == type);


        if (prefab == null)
        {
            Debug.LogError("No item setup in Inventory for " + type);
            return null;
        }


        GameObject item = GameObjectManager.Instance.InstantiateObject(prefab.gameObject, Vector3.zero, Quaternion.identity, SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);

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
