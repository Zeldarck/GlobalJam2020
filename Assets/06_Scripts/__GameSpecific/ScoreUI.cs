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
        EventManager.Instance.RegisterOnScoreUpdate((o, number, number2) => OnScoreUpdated(number.m_int, number2.m_int));
    }

    void OnScoreUpdated(float a_score, float a_scoreMultiplier)
    {
        m_scoreText.text = "x" + a_scoreMultiplier + " || " + a_score;
    }

}
