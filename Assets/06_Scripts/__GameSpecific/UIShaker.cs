using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShaker : MonoBehaviour
{

    RectTransform m_rectransform;

    bool m_isShaking;
    bool m_isBubble;

    Timer m_timerShake;
    Timer m_timerBubble;

    Vector3 m_originalPos;
    Vector3 m_originalScale;
    Quaternion m_originalRot;

    float m_intensityShake;
    float m_intensityBubble;

    float m_bubbleBounciness = 5;
    float m_percentTimeBubbleScaleUp = 5;


    // Start is called before the first frame update
    void Start()
    {
        m_rectransform = GetComponent<RectTransform>();
        m_timerShake = TimerFactory.Instance.GetTimer();
        m_timerBubble = TimerFactory.Instance.GetTimer();
    }

    public void Shake(float a_time = 0.5f, float a_intensity = 0.5f)
    {
        m_timerShake.StartTimer(a_time, () => { m_isShaking = false; m_rectransform.localPosition = m_originalPos; });
        m_isShaking = true;
        m_originalPos = m_rectransform.localPosition;
        m_intensityShake = a_intensity;
    }

    public void Bubble(float a_time = 0.5f, float a_intensity = 0.5f, float a_bounciness = 5, float a_percentTimeScaleUp = 0.14f)
    {
        m_timerBubble.StartTimer(a_time, () => { m_isBubble = false;  m_rectransform.localScale = m_originalScale; });
        m_isBubble = true;
        m_originalScale = m_rectransform.localScale;
        m_intensityBubble = a_intensity;
        m_percentTimeBubbleScaleUp = a_percentTimeScaleUp;
        m_bubbleBounciness = a_bounciness;
    }


    // Update is called once per frame
    void Update()
    {
        if (m_isShaking)
        {
            float intensity = Mathf.Lerp(m_intensityShake, 0.0f, m_timerShake.GetCurrentTime() / m_timerShake.GetLength());
            m_rectransform.localPosition = m_originalPos + new Vector3(Utils.RandomFloat(-intensity, intensity), Utils.RandomFloat(-intensity, intensity), Utils.RandomFloat(-intensity, intensity));
        }

        if (m_isBubble)
        {
            float percentTime = m_timerBubble.GetLength() * m_percentTimeBubbleScaleUp;

            if (m_timerBubble.GetCurrentTime() < percentTime)
            {
                m_rectransform.localScale = Vector3.Lerp(m_originalScale, m_originalScale + new Vector3(m_intensityBubble, m_intensityBubble, m_intensityBubble), m_timerBubble.GetCurrentTime() / (percentTime));
            }
            else
            {
                m_rectransform.localScale = Utils.SpringDamper(m_originalScale + new Vector3(m_intensityBubble, m_intensityBubble, m_intensityBubble), m_originalScale
                    , (m_timerBubble.GetCurrentTime() - (percentTime)) / (m_timerBubble.GetLength() - (percentTime)), m_bubbleBounciness);
            }
        }
    }

}
