using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HackMenuAnim : MonoBehaviour
{

    [SerializeField]
    TutorialUI m_tutorial;
    bool begin;

    void FinishMain()
    {
        m_tutorial.StartTurorial();
        begin = true;
    }

    private void Update()
    {
        if (begin && m_tutorial.GetComponent<Canvas>().enabled == false)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
