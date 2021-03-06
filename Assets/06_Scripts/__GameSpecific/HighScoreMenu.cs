﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour
{

    [SerializeField]
    Transform m_container;

    [SerializeField]
    HighscoreEntry m_highscoreEntryPrefab;


    [SerializeField]
    Button m_resetButton;


    private void OnEnable()
    {
        Utils.DestroyChilds(m_container);


        m_resetButton.onClick.RemoveAllListeners();
        m_resetButton.onClick.AddListener(ResetScores);
        GenerateChilds();

    }

    public void SetColored(int a_index)
    {
        m_container.GetChild(a_index).GetComponent<HighscoreEntry>().SetColored();
    }

    void GenerateChilds()
    {
        SaveManager saveManager = new SaveManager();

        int index = 1;
        foreach (Highscore highscore in saveManager.Scores)
        {
            HighscoreEntry entry = Instantiate(m_highscoreEntryPrefab.gameObject, m_container).GetComponent<HighscoreEntry>();
            entry.SetHighScore(highscore, index);
            ++index;
        }
    }

    private void ResetScores()
    {
        Utils.DestroyChilds(m_container);
        SaveManager saveManager = new SaveManager();
        saveManager.ResetHighscore();
        GenerateChilds();
    }


}
