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

    // Start is called before the first frame update
    void Start()
    {
        m_slider = GetComponent<Slider>();
        EventManager.Instance.RegisterOnRageUpdate((o, number) => OnRageUpdated(number.m_number));
    }


    void OnRageUpdated(float a_rageLevel)
    {
        if (m_slider.value < a_rageLevel)
        {
            GetComponent<UIShaker>().Shake(m_timeMultiplierShaker * (a_rageLevel - m_slider.value), m_intensityMultiplierShaker * ( a_rageLevel - m_slider.value) );
        }
        m_slider.value = a_rageLevel;
    }
}
