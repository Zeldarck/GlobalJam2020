using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerManager : MonoBehaviour
{

    Queue<ThrowableItemType> m_cornerQueue = new Queue<ThrowableItemType>();


    void Start()
    {
        EventManager.Instance.RegisterOnCornerHitted((o) => CornerHitted());
        EventManager.Instance.RegisterOnStart(o => Init());
        EventManager.Instance.RegisterOnLoose((o) => Clean());

        for(int i = 0; i < (int)ThrowableItemType.NB_ITEM_TYPE; ++i)
        {
            m_cornerQueue.Enqueue((ThrowableItemType)i);
        }
          
        Utils.ShuffleQueue(m_cornerQueue);

        Utils.TriggerNextFrame(SendPosition);
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


    void CornerHitted()
    {
        ThrowableItemType itemType =  m_cornerQueue.Dequeue();
        m_cornerQueue.Enqueue(itemType);
        EventManager.Instance.InvokeOnSetCornerOrder(this, new ItemEventArgs(itemType), new IntEventArgs(m_cornerQueue.Count - 1));
        EventManager.Instance.InvokeOnGiveItem(this, new ItemEventArgs(itemType));
        ActivateACorner();
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
        ActivateACorner();
    }

    void Clean()
    {
        foreach(Corner corner in LevelGenerator.Instance.Corners)
        {
            corner.IsActived = false;
        }
    }
}
