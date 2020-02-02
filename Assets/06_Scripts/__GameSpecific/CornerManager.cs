using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerManager : Singleton<CornerManager>
{

    Queue<ThrowableItemType> m_cornerQueue = new Queue<ThrowableItemType>();


    List<ThrowableItemType> m_availableItems = new List<ThrowableItemType>();
    List<ThrowableItemType> m_poolItems = new List<ThrowableItemType>();

    [SerializeField]
    int m_minimumObjectNumber = 3;

    int m_objectNumber = 3;

    void Start()
    {
        EventManager.Instance.RegisterOnCornerHitted((o) => TriggerNextItem());
        EventManager.Instance.RegisterOnStart(o => Init());
        EventManager.Instance.RegisterOnLoose((o) => Clean());
        EventManager.Instance.RegisterOnIncreaseDifficulty((o, number) => IncreaseDifficulty(number.m_int));

    }

    void IncreaseDifficulty(int m_int)
    {
        if (m_objectNumber < m_poolItems.Count)
        {
            m_availableItems.Add(m_poolItems[m_objectNumber]);
            m_cornerQueue.Enqueue(m_poolItems[m_objectNumber]);
            EventManager.Instance.InvokeOnSetCornerOrder(this, new ItemEventArgs(m_poolItems[m_objectNumber]), new IntEventArgs(m_objectNumber));
            ++m_objectNumber;
        }
    }

    void SendPosition()
    {
        int i = 0;
        foreach(ThrowableItemType itemType in m_cornerQueue)
        {
            EventManager.Instance.InvokeOnSetCornerOrder(this, new ItemEventArgs(itemType), new IntEventArgs(i));
            ++i;
        }
    }


    public void TriggerNextItem()
    {
        ThrowableItemType itemType =  m_cornerQueue.Dequeue();
        m_cornerQueue.Enqueue(itemType);
        EventManager.Instance.InvokeOnSetCornerOrder(this, new ItemEventArgs(itemType), new IntEventArgs(m_cornerQueue.Count - 1));
        EventManager.Instance.InvokeOnGiveItem(this, new ItemEventArgs(itemType));
       // ActivateACorner();
    }

    void ActivateACorner()
    {
        if (LevelGenerator.Instance.Corners.Count == 0)
        {
            Debug.LogError("No corners generated");
            return;
        }

        int id = Utils.RandomInt(0, LevelGenerator.Instance.Corners.Count);

        Corner nextOne = LevelGenerator.Instance.Corners[id];

        nextOne.IsActived = true;
    }

    void Init()
    {
        m_objectNumber = m_minimumObjectNumber;

        for (int i = 0; i < (int)ThrowableItemType.NB_ITEM_TYPE; ++i)
        {
            m_poolItems.Add((ThrowableItemType)i);
        }

        Utils.ShuffleList(m_poolItems);

        for (int i = 0 ; i < m_objectNumber; ++i)
        {
            m_availableItems.Add(m_poolItems[i]);
        }


        for (int i = 0; i < m_availableItems.Count; ++i)
        {
            m_cornerQueue.Enqueue(m_availableItems[i]);
        }

        Utils.ShuffleQueue(m_cornerQueue);

        Utils.TriggerNextFrame(SendPosition);

        foreach (Corner corner in LevelGenerator.Instance.Corners)
        {
            corner.IsActived = true;
        }


        //ActivateACorner();
    }

    public ThrowableItemType GetRandomItemType()
    {
        if (m_availableItems.Count == 0)
        {
            Debug.LogError("No available item in CornerManager");
            return 0;
        }

        int id = Utils.RandomInt(0, m_availableItems.Count);

        return m_availableItems[id];
    }


    void Clean()
    {
        foreach(Corner corner in LevelGenerator.Instance.Corners)
        {
            corner.IsActived = false;
        }

       m_availableItems = new List<ThrowableItemType>();
       m_poolItems = new List<ThrowableItemType>();
       m_cornerQueue = new Queue<ThrowableItemType>();

    }
}
