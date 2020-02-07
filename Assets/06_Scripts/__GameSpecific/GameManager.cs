using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Timer m_gameTimer;
    Timer m_multiplerTimer;

    float m_rageLevel = 0;

    int m_score = 0;

    int m_scoreMultiplier = 0;

    int m_difficulty = 1;

    [SerializeField]
    float m_timeDifficulty = 13.5f;

    [SerializeField]
    float m_timeMultiplier = 4.5f;

    [SerializeField]
    float m_percentMultiplier = 0.05f;



    public Timer GameTimer { get => m_gameTimer; set => m_gameTimer = value; }
    public Timer MultiplerTimer { get => m_multiplerTimer; set => m_multiplerTimer = value; }


    public int ScoreMultiplier
    {
        get
        {
            return m_scoreMultiplier;
        }
        set
        {
            if(m_scoreMultiplier != value)
            {
                EventManager.Instance.InvokeOnUpdateMultiplier(this, new IntEventArgs(value));
            }
            m_scoreMultiplier = value;
        }
    }


    void Start()
    {
        CameraManager.Instance.Target = Player.Instance.gameObject;
        CameraManager.Instance.CurrentStrategy = new CameraFPS(3.5f, 5.0f, true);



        GameTimer = TimerFactory.Instance.GetTimer();
        MultiplerTimer = TimerFactory.Instance.GetTimer();

        Utils.TriggerWaitForSeconds(0.5f, () => EventManager.Instance.InvokeOnStart(this) );

        EventManager.Instance.RegisterOnStart((o) => StartGame());
        EventManager.Instance.RegisterOnLoose((o) => GameOver());

        EventManager.Instance.RegisterOnRageIncrease((o, number) => IncreaseRage(number.m_number));
        EventManager.Instance.RegisterOnScoreIncrease((o, number) => IncreaseScore((int)number.m_int));

    }


    void Update()
    {

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.lockState = (CursorLockMode)(((int)(Cursor.lockState + 1)) % 2);
        }
#endif


        if (m_gameTimer.GetCurrentTime() / m_timeDifficulty >= m_difficulty)
        {
            ++m_difficulty;
 
  
            EventManager.Instance.InvokeOnIncreaseDifficulty(this, new IntEventArgs(m_difficulty));

        }



    }


    void StartGame()
    {
        GameTimer.StartTimer();

        //Cursor Set Up
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ScoreMultiplier = 1;
        m_difficulty = 0;
        LevelGenerator.Instance.GenerateLevel(8, 11);
        RailManager.Instance.GenerateRail(8, 11);
        ResetRage();
        ResetScore();
    }

    void IncreaseRage(float a_number)
    {
        m_rageLevel += a_number;
        m_rageLevel = Mathf.Clamp(m_rageLevel, 0, 100);
        EventManager.Instance.InvokeOnRageUpdate(this, new NumberEventArgs(m_rageLevel));

        if (m_rageLevel >= 100.0f && IsGameRunning())
        {
            EventManager.Instance.InvokeOnLoose(this);
        }
    }



    void ResetRage()
    {
        m_rageLevel = 0;
        EventManager.Instance.InvokeOnRageUpdate(this, new NumberEventArgs(m_rageLevel));
    }


    void IncreaseScore(int a_number)
    {
        ScoreMultiplier++;
        StartMultiplierTimer();

        a_number *= m_scoreMultiplier;
        m_score += a_number;
        SendEventScore();
    }

    void DecreaseMultiplier()
    {
        if(ScoreMultiplier > 1)
        {
            ScoreMultiplier--;
            StartMultiplierTimer();
        }
    }

    void StartMultiplierTimer()
    {
        MultiplerTimer.StartTimer(Mathf.Max(m_timeMultiplier - m_timeMultiplier * m_percentMultiplier * m_scoreMultiplier, 0.2f), () => { DecreaseMultiplier(); });
    }


    void ResetScore()
    {
        m_score = 0;
        m_scoreMultiplier = 1;
        SendEventScore();
    }



    void SendEventScore()
    {
        EventManager.Instance.InvokeOnScoreUpdate(this, new IntEventArgs(m_score));
    }

    void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SoundManager.Instance.StartAudio(AUDIOCLIP_KEY.LOOSE, MIXER_GROUP_TYPE.SFX, false, false, AUDIOSOURCE_KEY.CREATE_KEY, 0,null,1,false);

        GameTimer.Pause();
    }

    public bool IsGameRunning()
    {
        return m_gameTimer.IsTimerRunning();
    }

}