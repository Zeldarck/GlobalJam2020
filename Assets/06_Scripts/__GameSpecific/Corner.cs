using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : Module
{

    float m_intensityTime = 0.0f;
    float m_intensityModif = -1;

    TriggerObservable m_trigger;

    Renderer m_renderer;

    bool m_isActived;

    Timer m_timerBlink;

    [SerializeField]
    float m_timeBlinking;


    public bool IsActived
    {
        get
        {
           return m_isActived;
        }
        set
        {
            m_isActived = value;
        }
    }

    private void Awake()
    {
        m_timerBlink = TimerFactory.Instance.GetTimer();

        m_renderer = GetComponent<Renderer>();
        Material mat = new Material(m_renderer.materials[1].shader);
        m_renderer.materials[1] = mat;

        IsActived = false;
        m_trigger = GetComponentInChildren<TriggerObservable>();
        m_trigger.Register((o, other) => TriggerEnter(o, other), null, null);
    }


    private void Update()
    {
    }

    private void TriggerEnter(TriggerObservable me, Collider other)
    {

        if (IsActived && other.tag == "Item")
        {
            ThrowableItem item = other.GetComponent<ThrowableItem>();

            if (item.IsDead)
            {
                return;
            }

            SoundManager.Instance.StartAudio(AUDIOCLIP_KEY.BONUS_USED, MIXER_GROUP_TYPE.SFX, false, false, AUDIOSOURCE_KEY.BUTTON_MENU, 0, null, 0.5f);


            item.IsDead = true;

            Destroy(item.gameObject);
            //IsActived = false;


            m_renderer.materials[1].EnableKeyword("_ISFLICKERING_ON");

            m_timerBlink.StartTimer(m_timeBlinking, () => {
                m_renderer.materials[1].DisableKeyword("_ISFLICKERING_ON");
            });

            EventManager.Instance.InvokeOnCornerHitted(this);
        }
    }


}
