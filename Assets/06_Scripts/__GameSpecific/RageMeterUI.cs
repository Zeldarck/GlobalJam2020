using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageMeterUI : MonoBehaviour
{
    Slider m_slider;

    [SerializeField]
    float m_timeMultiplierShaker = 0.05f;
    [SerializeField]
    float m_intensityMultiplierShaker = 1.5f;

    [SerializeField]
    float m_maxIntensity = 30;


    [SerializeField]
    float m_speedFill = 1.25f;


    [SerializeField]
    float m_boucinessFill = 35;
    [SerializeField]
    float m_intensityFill = 0.25f;
    [SerializeField]
    float m_timeMultiplierFill = 0.25f;


    float m_targetRageValue = 0.0f;

    float m_time = 0;

    [SerializeField]
    Color m_rageColor;

    [SerializeField]
    Color m_normalColor;

    // Start is called before the first frame update
    void Start()
    {
        m_slider = GetComponent<Slider>();
        EventManager.Instance.RegisterOnRageUpdate((o, number) => OnRageUpdated(number.m_number));
        EventManager.Instance.RegisterOnStart((o) => m_targetRageValue = 100.0f);
    }


    void OnRageUpdated(float a_rageLevel)
    {
        m_slider.value =  m_targetRageValue;
        m_targetRageValue = 100.0f - a_rageLevel;
        if (m_slider.value > m_targetRageValue)
        {
            GetComponent<UtilsAnimator>().Shake(m_timeMultiplierShaker * (m_slider.value - m_targetRageValue), Mathf.Min(m_maxIntensity, m_intensityMultiplierShaker * (m_slider.value - m_targetRageValue)) );
            m_slider.fillRect.GetComponent<Image>().color = m_rageColor;
        }
        else
        {
            GetComponent<UtilsAnimator>().Bubble(Mathf.Abs(m_slider.value - m_targetRageValue) * m_timeMultiplierShaker, m_intensityFill, m_boucinessFill, 0.14f);
            m_slider.fillRect.GetComponent<Image>().color = m_normalColor;
        }

    }

    private void Update()
    {
        m_slider.value -= Time.deltaTime * (m_slider.value - m_targetRageValue) * m_speedFill;

        if(Utils.Equals(m_slider.value, m_targetRageValue, 0.85f))
        {
            m_slider.fillRect.GetComponent<Image>().color = m_normalColor;
        }
    }
}
