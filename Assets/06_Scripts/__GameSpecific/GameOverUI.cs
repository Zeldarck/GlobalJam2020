using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    Canvas m_canvas;
    // Start is called before the first frame update
    void Start()
    {
        m_canvas = GetComponent<Canvas>();
        EventManager.Instance.RegisterOnLoose((o) => m_canvas.enabled = true);
        EventManager.Instance.RegisterOnStart((o) => m_canvas.enabled = false);
    }

}
