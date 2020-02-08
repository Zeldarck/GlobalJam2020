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

    float m_targetRageValue = 0.0f;

    float m_time = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_slider = GetComponent<Slider>();
        EventManager.Instance.RegisterOnRageUpdate((o, number) => OnRageUpdated(number.m_number));
        m_slider.value = 0.0f;

    }


    void OnRageUpdated(float a_rageLevel)
    {
        m_slider.value = m_targetRageValue;
        if (m_slider.value < a_rageLevel)
        {
            GetComponent<UtilsAnimator>().Shake(m_timeMultiplierShaker * (a_rageLevel - m_slider.value), Mathf.Min(m_maxIntensity, m_intensityMultiplierShaker * ( a_rageLevel - m_slider.value)) );
        }
        m_targetRageValue = a_rageLevel;
    }

    private void Update()
    {
        m_slider.value += Time.deltaTime * (m_targetRageValue - m_slider.value ) * m_speedFill;
    }
}
