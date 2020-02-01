using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageMeterUI : MonoBehaviour
{
    Slider m_slider;

    // Start is called before the first frame update
    void Start()
    {
        m_slider = GetComponent<Slider>();
        EventManager.Instance.RegisterOnRageUpdate((o, number) => OnRageUpdated(number.m_number));
    }


    void OnRageUpdated(float a_rageLevel)
    {
        m_slider.value = a_rageLevel;
    }
}
