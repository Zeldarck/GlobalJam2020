

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveManager
{
    const int m_highscoreNumber = 10;
    int[] m_scores = new int[m_highscoreNumber];
    string[] m_pseudos = new string[m_highscoreNumber];

    public SaveManager()
    {
        for (int i = 0; i < m_highscoreNumber; ++i)
        {
            m_scores[i] = 0;
            m_pseudos[i] = "-";
        }
        Load();

    }

    void Load()
    {
        for (int i = 0; i < m_highscoreNumber; ++i)
        {
            m_scores[i] = PlayerPrefs.GetInt("Score" + i);
            m_pseudos[i] = PlayerPrefs.GetString("Score" + i);
        }
    }

    void Save()
    {
        for (int i = 0; i < m_highscoreNumber; ++i)
        {
            PlayerPrefs.SetInt("Score" + i, m_scores[i]);
            PlayerPrefs.SetString("Score" + i, m_pseudos[i]);
        }
    }

    public void Add(string a_pseudo, int a_score)
    {
        for (int i = 0; i < m_highscoreNumber; ++i)
        {
            if (a_score > m_scores[i])
            {
                for (int j = m_highscoreNumber - 1; j > i; --j)
                {
                    m_scores[j] = m_scores[j - 1];
                    m_pseudos[j] = m_pseudos[j - 1];
                }
                m_scores[i] = a_score;
                m_pseudos[i] = a_pseudo;
                break;
            }
        }
        Save();
    }

    public bool IsHighScore(int a_score)
    {
        for (int i = 0; i < m_highscoreNumber; ++i)
        {
            if (a_score > m_scores[i])
            {
                return true;
            }
        }

        return false;
    }
}
