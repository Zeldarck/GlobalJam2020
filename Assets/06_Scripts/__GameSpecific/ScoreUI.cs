using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    Text m_scoreText;

    private void Start()
    {
        m_scoreText = GetComponent<Text>();
        EventManager.Instance.RegisterOnScoreUpdate((o, number) => OnScoreUpdated(number.m_int));
    }

    void OnScoreUpdated(float a_score)
    {
        m_scoreText.text = a_score + "";
    }

}
