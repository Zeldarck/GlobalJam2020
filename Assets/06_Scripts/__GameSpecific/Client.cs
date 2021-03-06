﻿using System.Collections;
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
    float m_baseRage = 5;


    [SerializeField]
    float m_alertBeginValue = 4.0f;


    [SerializeField]
    GameObject m_happyVFXPrefab;

    [SerializeField]
    GameObject m_unhappyVFXPrefab;




    Timer m_timer;


    private void Start()
    {
        CreateTimer();
        m_rendererChild = m_materialChild.GetComponent<Renderer>();
        Material mat = new Material(m_rendererChild.material.shader);
        m_rendererChild.material = mat;


        if (!m_timer.IsTimerRunning())
        {
            m_rendererChild.material.DisableKeyword("_FIRSTCLIENT_ON");
        }

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
        Utils.TriggerNextFrame(SetAsFirstClient);
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
            GameObject vfx = Instantiate(m_unhappyVFXPrefab, transform.position + new Vector3(0,1.35f,0), transform.rotation);
            Utils.TriggerWaitForSeconds(3,() => Destroy(vfx));
            SoundManager.Instance.StartAudio(AUDIOCLIP_KEY.ENEMY_DIE, MIXER_GROUP_TYPE.SFX, false, false, AUDIOSOURCE_KEY.CREATE_KEY, 0, null, 0.55f);
            EventManager.Instance.InvokeOnRageIncrease(this, new NumberEventArgs(m_baseRage), new ClientEventArgs(this));
        }
        else
        {
            GameObject vfx = Instantiate(m_happyVFXPrefab, transform.position + new Vector3(0, 1.35f, 0), transform.rotation);
            Utils.TriggerWaitForSeconds(3, () => Destroy(vfx));
            SoundManager.Instance.StartAudio(AUDIOCLIP_KEY.WIN, MIXER_GROUP_TYPE.SFX, false, false, AUDIOSOURCE_KEY.CREATE_KEY, 0, null, 0.55f);
            EventManager.Instance.InvokeOnScoreIncrease(this, new IntEventArgs(m_baseScore * GameManager.Instance.ScoreMultiplier), new ClientEventArgs(this));
            EventManager.Instance.InvokeOnRageIncrease(this, new NumberEventArgs(m_baseRage/-3.0f), new ClientEventArgs(this));
        }
    }

    void SetAsFirstClient()
    {
        m_rendererChild.material.EnableKeyword("_FIRSTCLIENT_ON");
    }

    private void OnDestroy()
    {
        if (m_timer != null)
        {
            Destroy(m_timer);
        }
    }

}
