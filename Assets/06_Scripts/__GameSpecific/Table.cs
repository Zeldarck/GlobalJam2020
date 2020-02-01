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

            Client client = m_clientQueue.Dequeue();

            ThrowableItemType wantedType = client.WantedItem;

            client.CompleteClient(item.ItemType == wantedType);

            Destroy(item.gameObject);
            Destroy(client.gameObject);
            EventManager.Instance.InvokeOnGiveRandomItem(this);
        }
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
