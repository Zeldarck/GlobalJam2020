using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Module
{

    Stack<Client> m_clientStack = new Stack<Client>();


    public void AddClient(Client a_client)
    {
        m_clientStack.Push(a_client);
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

            if (m_clientStack.Count == 0 || item.IsDead)
            {
                return;
            }

            Client client = m_clientStack.Pop();

            ThrowableItemType wantedType = client.WantedItem;

            if (item.ItemType == wantedType)
            {
                //client.happy();
            }
            else
            {
                //client.rage();
            }

            Destroy(item.gameObject);
            Destroy(client.gameObject);
            EventManager.Instance.InvokeOnGiveRandomItem(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
