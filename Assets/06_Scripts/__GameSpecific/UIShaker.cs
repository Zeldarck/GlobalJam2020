using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShaker : MonoBehaviour
{

    RectTransform m_rectransform;

    bool m_isShaking;

    Timer m_timerShake;

    Vector3 m_originalPos;

    float m_intensity;

    // Start is called before the first frame update
    void Start()
    {
        m_rectransform = GetComponent<RectTransform>();
        m_timerShake = TimerFactory.Instance.GetTimer();
    }

    public void Shake(float a_time = 0.5f, float a_intensity = 0.5f)
    {
        m_timerShake.StartTimer(a_time, () => { m_isShaking = false; m_rectransform.localPosition = m_originalPos; });
        m_isShaking = true;
        m_originalPos = m_rectransform.localPosition;
        m_intensity = a_intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isShaking)
        {
            float intensity = Mathf.Lerp(m_intensity, 0.0f, m_timerShake.GetCurrentTime() / m_timerShake.GetLength());
            m_rectransform.localPosition = m_originalPos + new Vector3(Utils.RandomFloat(-intensity, intensity), Utils.RandomFloat(-intensity, intensity), Utils.RandomFloat(-intensity, intensity));
        }
    }
}
