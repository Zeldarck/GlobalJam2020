using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{

    Animator m_animator;

    [SerializeField]
    Button m_close;

    Canvas m_canvas;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_canvas = GetComponent<Canvas>();
        m_close.onClick.AddListener(() => m_animator.SetTrigger("Exit"));
    }


    public void StartTurorial()
    {
        m_canvas.enabled = true;
        m_animator.SetTrigger("Start");
    }

   public void FinishTutorialExit()
    {
        m_canvas.enabled = false;
    }
}
