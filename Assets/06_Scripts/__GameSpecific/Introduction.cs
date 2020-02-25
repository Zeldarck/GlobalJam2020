using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{

    [SerializeField]
    AudioSource m_mainTrack;

    Animator m_animator;

    bool m_doOnce;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_doOnce && Time.timeSinceLevelLoad > 2.75f )
        {
            m_animator.SetTrigger("Start");
            m_mainTrack.Play();
            m_doOnce = true;
        }
        if(m_doOnce && PlayerPrefs.HasKey("IntroPlayed") && Input.anyKeyDown)
        {
            FinishIntro();
        }
    }

    void FinishIntro()
    {
        PlayerPrefs.SetInt("IntroPlayed", 1);
        SceneManager.LoadScene("MainMenu");
    }

}
