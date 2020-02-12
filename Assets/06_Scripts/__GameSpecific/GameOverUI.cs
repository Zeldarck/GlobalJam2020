using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{

    Canvas m_canvas;

    Animator m_animator;


    [SerializeField]
    HighScoreMenu m_highscoreMenu;


    [SerializeField]
    Button m_highscoreButton;


    [SerializeField]
    Mask m_maskChildren;


    bool m_trackHighScoreMenu;
    bool m_highScoreMenuEnabled;
    int m_highscorecolored;

    // Start is called before the first frame update
    void Start()
    {
        m_canvas = GetComponent<Canvas>();
        m_animator = GetComponent<Animator>();
        EventManager.Instance.RegisterOnLoose((o) => { m_canvas.enabled = true; m_animator.SetTrigger("GameOver"); });
        EventManager.Instance.RegisterOnStart((o) => { m_canvas.enabled = false; m_animator.SetTrigger("ReStart"); });
        m_highscoreButton.onClick.AddListener(() => DisplayHighscore());
    }


    public void DisplayHighscore(int a_indexColored = -1)
    {
        m_animator.SetTrigger("GoToHighScore");
        m_highScoreMenuEnabled = true;
        m_maskChildren.enabled = true;
        m_highscorecolored = a_indexColored;
    }


    private void Update()
    {
        if (m_trackHighScoreMenu && m_highscoreMenu.gameObject.activeSelf == false)
        {
            m_animator.SetTrigger("GoToGameOver");
            m_trackHighScoreMenu = false;
            m_highScoreMenuEnabled = false;
            m_maskChildren.enabled = false;
        }
    }

    void EnableHighScore()
    {
        if (m_highScoreMenuEnabled)
        {
            m_trackHighScoreMenu = true;
            m_highscoreMenu.gameObject.SetActive(true);
            if (m_highscorecolored >= 0)
            {
                Utils.TriggerNextFrame(() => m_highscoreMenu.SetColored(m_highscorecolored));
            }
        }
    }

}
