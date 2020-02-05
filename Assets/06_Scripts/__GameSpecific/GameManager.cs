using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Timer m_gameTimer;

    float m_rageLevel = 0;

    int m_score = 0;

  //  int m_scoreMultiplier = 1;

    int m_difficulty = 1;

    [SerializeField]
    float m_timeMultiplier = 15.0f;

  //  [SerializeField]
   // float m_timeOffsetDifficulty = 5.0f;


    public Timer GameTimer { get => m_gameTimer; set => m_gameTimer = value; }

    void Start()
    {
        CameraManager.Instance.Target = Player.Instance.gameObject;
        CameraManager.Instance.CurrentStrategy = new CameraFPS(3.5f, 5.0f, true);



        GameTimer = TimerFactory.Instance.GetTimer();

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


     /*   if (m_gameTimer.GetCurrentTime() / m_timeMultiplier >= m_scoreMultiplier)
        {
            ++m_scoreMultiplier;
            SendEventScore();
        }*/

        if (m_gameTimer.GetCurrentTime() / m_timeMultiplier >= m_difficulty)
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

        a_number *= m_difficulty;
        m_score += a_number;
        SendEventScore();
    }

    void ResetScore()
    {
        m_score = 0;
        m_difficulty = 0;
        SendEventScore();
    }

   /* void ResetScoreMultiplier()
    {
        m_scoreMultiplier = 1;
        SendEventScore();
    }*/


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