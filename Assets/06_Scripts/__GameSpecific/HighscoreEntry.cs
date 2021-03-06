﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreEntry : MonoBehaviour
{
    [SerializeField]
    Text m_pseudo;

    [SerializeField]
    Text m_score;

    [SerializeField]
    Text m_index;

    [SerializeField]
    Color m_colored;



    public void SetHighScore(Highscore a_highscore,int index)
    {
        m_index.text = index + "."; 
        m_pseudo.text = a_highscore.Pseudo;
        m_score.text = a_highscore.Score + "";
    }

    public void SetColored()
    {
        m_index.color = m_colored;
        m_pseudo.color = m_colored;
        m_score.color = m_colored;
    }
}
