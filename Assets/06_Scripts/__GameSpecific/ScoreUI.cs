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
    }

    // Update is called once per frame
    void Update()
    {
     //   m_scoreText.text = ;
    }
}
