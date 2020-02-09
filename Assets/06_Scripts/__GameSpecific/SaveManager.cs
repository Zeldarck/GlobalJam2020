

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Highscore
{
    int m_score;
    string m_pseudo;

    public int Score { get => m_score; set => m_score = value; }
    public string Pseudo { get => m_pseudo; set => m_pseudo = value; }


    public Highscore(int a_score, string a_pseudo)
    {
        m_score = a_score;
        m_pseudo = a_pseudo;
    }
}

public class SaveManager
{
    const int m_highscoreNumber = 10;
    Highscore[] m_scores = new Highscore[m_highscoreNumber];

    public Highscore[] Scores { get => m_scores; set => m_scores = value; }

    public SaveManager()
    {
        for (int i = 0; i < m_highscoreNumber; ++i)
        {
            Scores[i] = new Highscore(0, "-");
        }
        Load();
    }

    void Load()
    {
        for (int i = 0; i < m_highscoreNumber; ++i)
        {
            Scores[i].Score = PlayerPrefs.GetInt("Score" + i);
            Scores[i].Pseudo = PlayerPrefs.GetString("Pseudo" + i);
        }
    }

    void Save()
    {
        for (int i = 0; i < m_highscoreNumber; ++i)
        {
            PlayerPrefs.SetInt("Score" + i, Scores[i].Score);
            PlayerPrefs.SetString("Pseudo" + i, Scores[i].Pseudo);
        }
        PlayerPrefs.Save();
    }

    public int Add(Highscore a_highscore)
    {
        int res = 0;
        for (int i = 0; i < m_highscoreNumber; ++i)
        {
            if (a_highscore.Score > Scores[i].Score)
            {
                res = i;
                for (int j = m_highscoreNumber - 1; j > i; --j)
                {
                    Scores[j] = Scores[j - 1];
                }
                Scores[i] = a_highscore;
                break;
            }
        }
        Save();

        return res;
    }

    public int IsHighScore(int a_score)
    {
        Load();
        for (int i = 0; i < m_highscoreNumber; ++i)
        {
            if (a_score > Scores[i].Score)
            {
                return i+1;
            }
        }

        return -1;
    }
}
