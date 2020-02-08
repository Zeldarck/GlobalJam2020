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

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Main"));
        m_animator = GetComponent<UtilsAnimator>();
        TimerFactory.Instance.GetTimer().StartTimer(m_waitingTime, () => m_animator.SpringDamperScaleUp(0.85f, 1, 18));

    }

}
