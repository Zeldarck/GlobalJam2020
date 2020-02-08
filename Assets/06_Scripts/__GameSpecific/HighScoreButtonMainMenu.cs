using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreButtonMainMenu : MonoBehaviour
{
    UtilsAnimator m_animator;

    [SerializeField]
    float m_waitingTime = 4.35f;

    void Start()
    {
        m_animator = GetComponent<UtilsAnimator>();
        TimerFactory.Instance.GetTimer().StartTimer(m_waitingTime, () => m_animator.SpringDamperScaleUp(0.85f, 1, 18));

    }

}
