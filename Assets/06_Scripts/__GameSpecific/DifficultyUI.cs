using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyUI : MonoBehaviour
{
    Text m_difficultyText;

    UIShaker m_shaker;


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

    private void Start()
    {
        m_difficultyText = GetComponentInChildren<Text>();
        m_shaker = GetComponent<UIShaker>();
        EventManager.Instance.RegisterOnIncreaseDifficulty((o, number) => OnDifficultyUpdated(number.m_int));
        m_originalSize = m_difficultyText.fontSize;
    }

    void OnDifficultyUpdated(float a_difficulty)
    {
        m_difficultyText.text = "x" + a_difficulty;
        m_shaker.Bubble(m_bubbleTime, m_bubbleIntensity, m_bubbleBounciness, m_percentTimeBubbleScaleUp);
        m_difficultyText.fontSize = Mathf.Min(m_maxFontSize, m_difficultyText.fontSize + m_stepSize);
    }

}
