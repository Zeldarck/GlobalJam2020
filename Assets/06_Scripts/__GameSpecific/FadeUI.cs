using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{

    Image m_backgroundImage;


    [SerializeField]
    float m_durationFading = 3f;

    // Start is called before the first frame update
    void Start()
    {
        m_backgroundImage = GetComponent<Image>();
        EventManager.Instance.RegisterOnStart((o) => m_backgroundImage.CrossFadeAlpha(0, m_durationFading, false));
    }

    // Update is called once per frame
    void Update()
    {
       
    }


}
