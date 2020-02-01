using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    Text m_timertext;

    private void Start()
    {
        m_timertext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        System.TimeSpan time = System.TimeSpan.FromSeconds(GameManager.Instance.GameTimer.GetCurrentTime());

        m_timertext.text = string.Format("{0:00}:{1:00}:{2:00}", time.TotalMinutes, time.Seconds, time.Milliseconds/100);
    }
}
