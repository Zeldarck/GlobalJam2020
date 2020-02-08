using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreAdd : MonoBehaviour
{

    [SerializeField]
    Button m_validateButton;

    [SerializeField]
    InputField m_inputField;

    [SerializeField]
    Text m_scoreText;

    [SerializeField]
    Text m_rank;

    int m_score;
    SaveManager m_saveManager;

    Canvas m_canvas;
    // Start is called before the first frame update
    void Start()
    {

        m_saveManager = new SaveManager();
        m_canvas = GetComponent<Canvas>();
        m_validateButton.onClick.AddListener( OnValidatePseudo);
        EventManager.Instance.RegisterOnScoreUpdate((o, a_int) => m_score = a_int.m_int);
        EventManager.Instance.RegisterOnLoose((o) => CheckIfHighScore());
    }

    private void OnValidatePseudo()
    {
        m_saveManager.Add(new Highscore(m_score, m_inputField.text));
        m_canvas.enabled = false;
    }

    void CheckIfHighScore()
    {
        int index = m_saveManager.IsHighScore(m_score);
        if (index > 0)
        {
            m_canvas.enabled = true;
            m_scoreText.text = "Score :  " + m_score;
            m_rank.text = "Rank :  " + index;
            m_inputField.text = "";
        }

    }

}
