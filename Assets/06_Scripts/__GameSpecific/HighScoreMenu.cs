using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreMenu : MonoBehaviour
{

    [SerializeField]
    Transform m_container;

    [SerializeField]
    HighscoreEntry m_highscoreEntryPrefab;




    private void OnEnable()
    {
        Utils.DestroyChildsImmediate(m_container);

        SaveManager saveManager = new SaveManager();

        int index = 1;
        foreach(Highscore highscore in saveManager.Scores)
        {
            HighscoreEntry entry = Instantiate(m_highscoreEntryPrefab.gameObject, m_container).GetComponent<HighscoreEntry>();
            entry.SetHighScore(highscore, index);
            ++index;
        }
    }

    public void SetColored(int a_index)
    {
        m_container.GetChild(a_index).GetComponent<HighscoreEntry>().SetColored();
    }


}
