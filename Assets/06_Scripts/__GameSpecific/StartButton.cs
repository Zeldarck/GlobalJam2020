using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    UtilsAnimator m_animator;

    [SerializeField]
    float m_waitingTime = 4.35f;


    [SerializeField]
    Animator m_mainAnimator;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => StartGame());
        m_animator = GetComponent<UtilsAnimator>();
        TimerFactory.Instance.GetTimer().StartTimer(m_waitingTime, () => m_animator.SpringDamperScaleUp(0.85f, 1, 18));

    }


    void StartGame()
    {
        m_mainAnimator.SetTrigger("GoToTutorial");
    }



}
