using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : Module
{
    [SerializeField]
    GameObject m_enableIndicator;

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

}
