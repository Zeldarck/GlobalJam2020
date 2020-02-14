using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    Animator m_animator;


    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        EventManager.Instance.RegisterOnStart((o) => m_animator.SetTrigger("Start"));
       // EventManager.Instance.RegisterOnLoose((o) => m_backgroundImage.CrossFadeAlpha(1, m_durationFadingIn, false));
    }

    // Update is called once per frame
    void Update()
    {
       
    }


}
