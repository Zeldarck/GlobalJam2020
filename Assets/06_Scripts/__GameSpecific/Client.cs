using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Client : MonoBehaviour
{

    [SerializeField]
    List<IconItem> m_iconItemList = new List<IconItem>();

    [SerializeField]
    GameObject m_materialChild;

    Renderer m_rendererChild;

    ThrowableItemType m_wantedItem;

    [SerializeField]
    float m_waitingTime = 15.0f;


    [SerializeField]
    int m_baseScore = 25;

    [SerializeField]
    int m_minimumScore = 4;



    [SerializeField]
    float m_baseRage = 5;


    [SerializeField]
    float m_alertBeginValue = 4.0f;


    Timer m_timer;

    private void Start()
    {
        CreateTimer();
        m_rendererChild = m_materialChild.GetComponent<Renderer>();
        Material mat = new Material(m_rendererChild.material.shader);
        m_rendererChild.material = mat;

        SetRage(0);
    }

    void SetRage(float a_value)
    {
        m_rendererChild.material.SetFloat("_GoToRage", a_value);
    }

    void Update()
    {
        if (m_timer.IsTimerRunning())
        {
            float left = m_timer.GetTimeLeft();
            if(left < m_alertBeginValue)
            {
                SetRage((m_alertBeginValue - left) / m_alertBeginValue);
            }
        }
    }

    public ThrowableItemType WantedItem
    {
        get => m_wantedItem;
        set
        {
            m_wantedItem = value;
            IconItem iconItem = m_iconItemList.Find(o => o.ItemType == m_wantedItem);

            if (iconItem == null)
            {
                Debug.LogError("No item setup in Client for " + m_wantedItem);
                return;
            }

            iconItem.gameObject.SetActive(true);

        }
    }

    void CreateTimer()
    {
        if(m_timer == null)
        {
            m_timer = TimerFactory.Instance.GetTimer();
        }
    }

    public void StartTimer()
    {
        CreateTimer();
        m_timer.StartTimer(m_waitingTime, () => CompleteClient(false));
    }

    public void CompleteClient(bool a_isHappy)
    {
        if(this == null)
        {
            return;
        }
        EventManager.Instance.InvokeOnClientComplete(this, new ClientEventArgs(this));

        if(!a_isHappy)
        {
            EventManager.Instance.InvokeOnRageIncrease(this, new NumberEventArgs(m_baseRage));
        }
        else
        {
            EventManager.Instance.InvokeOnScoreIncrease(this, new IntEventArgs((int)Mathf.Max(m_baseScore * m_timer.GetTimeLeft()/m_waitingTime, m_minimumScore)));
        }
    }

    private void OnDestroy()
    {
        if (m_timer != null)
        {
            Destroy(m_timer);
        }
    }

}
