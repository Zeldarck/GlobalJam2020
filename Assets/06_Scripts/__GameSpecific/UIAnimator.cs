using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{

    RectTransform m_rectransform;

    //shaking
    bool m_isShaking;
    bool m_isShakingInversed;
    Timer m_timerShake;
    float m_intensityShake;


    //bubble
    bool m_isBubble;
    Timer m_timerBubble;
    float m_intensityBubble;
    float m_bubbleBounciness;
    float m_percentTimeBubbleScaleUp;


    //scaleUp
    bool m_isScaleUp;
    Timer m_timerScaleUp;
    float m_finalSizeScaleUp;
    float m_scaleUpBounciness;


    //scaleUp
    bool m_isScaleDown;
    Timer m_timerScaleDown;
    float m_finalSizeScaleDown;


    Vector3 m_originalPos;
    Vector3 m_originalScale;
    Quaternion m_originalRot;



    // Start is called before the first frame update
    void Start()
    {
        m_rectransform = GetComponent<RectTransform>();
        m_timerShake = TimerFactory.Instance.GetTimer();
        m_timerBubble = TimerFactory.Instance.GetTimer();
        m_timerScaleUp = TimerFactory.Instance.GetTimer();
        m_timerScaleDown = TimerFactory.Instance.GetTimer();
    }

    public void StopShaking()
    {
        if (m_isShaking)
        {
            m_rectransform.localPosition = m_originalPos;
            m_timerShake.Stop();
            m_isShaking = false;
        }

    }

    public void Shake(float a_time = 0.5f, float a_intensity = 0.5f, bool a_IsInversed = false)
    {
        EndCurrentModifPos();
        m_timerShake.StartTimer(a_time, () => { m_isShaking = false; m_rectransform.localPosition = m_originalPos; });
        m_isShaking = true;
        m_isShakingInversed = a_IsInversed;
        m_originalPos = m_rectransform.localPosition;
        m_intensityShake = a_intensity;
    }

    public void Bubble(float a_time = 0.5f, float a_intensity = 0.5f, float a_bounciness = 5, float a_percentTimeScaleUp = 0.14f)
    {
        EndCurrentModifScale();
        m_timerBubble.StartTimer(a_time, () => { m_isBubble = false;  m_rectransform.localScale = m_originalScale; });
        m_isBubble = true;
        m_originalScale = m_rectransform.localScale;
        m_intensityBubble = a_intensity;
        m_percentTimeBubbleScaleUp = a_percentTimeScaleUp;
        m_bubbleBounciness = a_bounciness;
    }


    public void SpringDamperScaleUp(float a_time = 0.5f, float a_finalSize = 0.5f, float a_bounciness = 5)
    {
        EndCurrentModifScale();
        m_timerScaleUp.StartTimer(a_time);
        m_isScaleUp = true;
        m_finalSizeScaleUp = a_finalSize;
        m_scaleUpBounciness = a_bounciness;
    }


    public void LinearScaleDown(float a_time = 0.5f, float a_finalSize = 0.0f)
    {
        EndCurrentModifScale();
        m_timerScaleDown.StartTimer(a_time);
        m_originalScale = m_rectransform.localScale;
        m_isScaleDown = true;
        m_finalSizeScaleDown = a_finalSize;
    }


    public void EndCurrentModifScale()
    {
        if(m_isBubble)
        {
            m_rectransform.localScale = m_originalScale;
        }

        if (m_isScaleDown)
        {
            m_rectransform.localScale = new Vector3(m_finalSizeScaleDown, m_finalSizeScaleDown, m_finalSizeScaleDown);
        }

        if(m_isScaleUp)
        {
            m_rectransform.localScale = new Vector3(m_finalSizeScaleUp, m_finalSizeScaleUp, m_finalSizeScaleUp);
        }
    }

    public void EndCurrentModifPos()
    {
        if (m_isShaking)
        {
            m_rectransform.localPosition = m_originalPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isShaking)
        {
            float intensity = 0.0f; 
            if (m_isShakingInversed)
            {
                intensity=Mathf.Lerp(0.0f, m_intensityShake, m_timerShake.GetCurrentTime() / m_timerShake.GetLength());
            }
            else
            {
                intensity=Mathf.Lerp(m_intensityShake, 0.0f, m_timerShake.GetCurrentTime() / m_timerShake.GetLength());
            }
            m_rectransform.localPosition = m_originalPos + new Vector3(Utils.RandomFloat(-intensity, intensity), Utils.RandomFloat(-intensity, intensity), Utils.RandomFloat(-intensity, intensity));
        }

        //TODO => transform bubble in linear scaleup and springdamper scaledown
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


        if (m_isScaleUp)
        {
            m_rectransform.localScale = Utils.SpringDamper(Vector3.zero, new Vector3(m_finalSizeScaleUp, m_finalSizeScaleUp, m_finalSizeScaleUp)
                  , m_timerScaleUp.GetCurrentTime() / m_timerScaleUp.GetLength(), m_scaleUpBounciness);

            if (m_timerScaleUp.GetCurrentTime() / m_timerScaleUp.GetLength() >= 1)
            {
                m_rectransform.localScale = new Vector3(m_finalSizeScaleUp, m_finalSizeScaleUp, m_finalSizeScaleUp);
                m_isScaleUp = false;
            }
        }


        if (m_isScaleDown)
        {
            m_rectransform.localScale = Vector3.Lerp(m_originalScale, new Vector3(m_finalSizeScaleDown, m_finalSizeScaleDown, m_finalSizeScaleDown), m_timerScaleDown.GetCurrentTime() / m_timerScaleDown.GetLength());
            if(m_timerScaleDown.GetCurrentTime() / m_timerScaleDown.GetLength() >= 1)
            {
                m_rectransform.localScale = new Vector3(m_finalSizeScaleDown, m_finalSizeScaleDown, m_finalSizeScaleDown);
                m_isScaleDown = false;
            }
        }

    }

}
