using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : Module
{
    [SerializeField]
    GameObject m_enableIndicator;

    TriggerObservable m_trigger;

    bool m_isActived;

    public bool IsActived
    {
        get
        {
           return m_isActived;
        }
        set
        {
            m_isActived = value;
            m_enableIndicator.SetActive(m_isActived);
        }
    }

    private void Awake()
    {
        IsActived = false;
        m_trigger = GetComponentInChildren<TriggerObservable>();
        m_trigger.Register((o, other) => TriggerEnter(o, other), null, null);
    }

    private void TriggerEnter(TriggerObservable me, Collider other)
    {

        if (other.tag == "Item")
        {
            ThrowableItem item = other.GetComponent<ThrowableItem>();

            if (item.IsDead)
            {
                return;
            }

            item.IsDead = true;

            Destroy(item.gameObject);

            IsActived = false;

            EventManager.Instance.InvokeOnCornerHitted(this);
        }
    }


}
