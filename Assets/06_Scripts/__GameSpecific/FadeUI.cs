using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{

    Image m_backgroundImage;


    [SerializeField]
    float m_durationFadingOut = 3f;

    [SerializeField]
    float m_durationFadingIn = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        m_backgroundImage = GetComponent<Image>();
        m_backgroundImage.color = Color.black;
        EventManager.Instance.RegisterOnStart((o) => m_backgroundImage.CrossFadeAlpha(0, m_durationFadingOut, false));
        EventManager.Instance.RegisterOnLoose((o) => m_backgroundImage.CrossFadeAlpha(1, m_durationFadingIn, false));
    }

    // Update is called once per frame
    void Update()
    {
       
    }


}
