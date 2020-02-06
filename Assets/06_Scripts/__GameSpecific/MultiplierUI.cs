using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierUI : MonoBehaviour
{
    Text m_multiplierText;

    UIAnimator m_UIAnimator;

    Slider m_slider;


    [SerializeField]
    float m_bubbleTime = 2.0f;

    [SerializeField]
    float m_bubbleIntensity = 0.75f;

    [SerializeField]
    float m_bubbleBounciness = 30;

    [SerializeField]
    float m_percentTimeBubbleScaleUp = 0.7f;

    [SerializeField]
    int m_maxFontSize = 50;

    [SerializeField]
    int m_stepSize = 4;

    int m_originalSize;

    int m_multiplier = 0;

    [SerializeField]
    float m_initialScaleUpTime = 0.5f;

    [SerializeField]
    float m_initialScaleUpBounciness = 35;


    [SerializeField]
    float m_scaleDownTime = 0.4f;


    [SerializeField]
    float m_intensityShake = 15f;

    [SerializeField]
    float m_percentageShake = 0.5f;

    [SerializeField]
    Color m_fullColor;


    [SerializeField]
    Color m_emptyColor;



    bool m_onceShake = true;

    private void Start()
    {
        m_multiplierText = GetComponentInChildren<Text>();
        m_UIAnimator = GetComponent<UIAnimator>();
        m_slider = GetComponentInChildren<Slider>();
        EventManager.Instance.RegisterOnUpdateMultiplier((o, number) => OnUpdateMultiplier(number.m_int));
        m_originalSize = m_multiplierText.fontSize;
    }

    void OnUpdateMultiplier(int a_multiplier)
    {
        m_multiplierText.text = "x" + a_multiplier;

        if (a_multiplier < m_multiplier)
        {
            m_UIAnimator.Bubble(m_bubbleTime, 1, m_bubbleBounciness, m_percentTimeBubbleScaleUp);
        }
        else if (a_multiplier == 2)
        {
            m_UIAnimator.SpringDamperScaleUp(m_initialScaleUpTime, 1, m_initialScaleUpBounciness);
        }
        else if (a_multiplier == 1)
        {
            m_UIAnimator.LinearScaleDown(m_scaleDownTime, 0);
        }
        else
        {
            m_UIAnimator.Bubble(m_bubbleTime, m_bubbleIntensity, m_bubbleBounciness, m_percentTimeBubbleScaleUp);
        }

        m_multiplierText.fontSize = Mathf.Min(m_maxFontSize, m_originalSize + a_multiplier * m_stepSize);

        m_UIAnimator.StopShaking();
    }

    private void Update()
    {
        m_slider.value = 1 - GameManager.Instance.MultiplerTimer.GetPercent();
        m_slider.fillRect.GetComponent<Image>().color = Color.Lerp(m_fullColor,m_emptyColor,GameManager.Instance.MultiplerTimer.GetPercent());
        if (m_slider.value >= m_percentageShake)
        {
            m_onceShake = true;
        }
        else if (m_onceShake)
        {
            m_UIAnimator.Shake(GameManager.Instance.MultiplerTimer.GetTimeLeft(), m_intensityShake, true);
            m_onceShake = false;
        }    

    }

}
