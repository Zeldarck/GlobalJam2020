using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Module
{

    Queue<Client> m_clientQueue = new Queue<Client>();


    public void AddClient(Client a_client)
    {
        m_clientQueue.Enqueue(a_client);

        a_client.transform.SetParent(this.transform, false);

        if(m_clientQueue.Count == 1)
        {
            a_client.StartTimer();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Collide(collision.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        Collide(other.gameObject);
    }

    void Collide(GameObject a_gameObject)
    {
        if (a_gameObject.tag == "Item")
        {
            ThrowableItem item = a_gameObject.GetComponent<ThrowableItem>();

            if (m_clientQueue.Count == 0 || item.IsDead)
            {
                return;
            }

            item.IsDead = true;

            Client client = m_clientQueue.Peek();

            ThrowableItemType wantedType = client.WantedItem;

            client.CompleteClient(item.ItemType == wantedType);

            Destroy(item.gameObject);
            EventManager.Instance.InvokeOnGiveRandomItem(this);
        }
    }

    void CheckClient(Client a_client)
    {
        if (m_clientQueue.Count > 0 && m_clientQueue.Peek() == a_client)
        {
            m_clientQueue.Dequeue();

            if(m_clientQueue.Count > 0)
            {
                m_clientQueue.Peek().StartTimer();
            }
        }
    }

    private void Start()
    {
        EventManager.Instance.RegisterOnClientComplete((o, client) => CheckClient(client.m_client));
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach(Client client in m_clientQueue)
        {
            client.transform.localPosition = new Vector3(0, 0, i-1);
            --i;
        }
    }
}
