using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : Module
{
    [SerializeField]
    GameObject m_enableIndicator;

    float m_intensityTime = 0.0f;
    float m_intensityModif = -1;

    TriggerObservable m_trigger;

    Renderer m_renderer;

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
            m_enableIndicator.SetActive(false /*disabled*/);
            m_intensityModif = IsActived ? 1.75f : -2.5f;
        }
    }

    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
        Material mat = new Material(m_renderer.materials[1].shader);
        m_renderer.materials[1] = mat;

        IsActived = false;
        m_trigger = GetComponentInChildren<TriggerObservable>();
        m_trigger.Register((o, other) => TriggerEnter(o, other), null, null);


    }

    private void Update()
    {

        m_intensityTime += Time.deltaTime * m_intensityModif;

        m_intensityTime = Mathf.Clamp01(m_intensityTime);

        float intensityValue = Mathf.Lerp(0f, 2.5f, m_intensityTime);

        m_renderer.materials[1].SetFloat("_Intensity_Emissive", intensityValue);

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

            item.IsDead = true;

            Destroy(item.gameObject);

            //IsActived = false;

            //SoundManager.Instance.StartAudio(AUDIOCLIP_KEY.BONUS_USED, MIXER_GROUP_TYPE.SFX, false, false, AUDIOSOURCE_KEY.CREATE_KEY, 0, null ,0.5f);
            EventManager.Instance.InvokeOnCornerHitted(this);
        }
    }


}
