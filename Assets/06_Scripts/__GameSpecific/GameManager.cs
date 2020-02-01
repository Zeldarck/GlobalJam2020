using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Timer m_gameTimer;

    float m_rageLevel = 0;

    public float RageLevel { get => m_rageLevel; set => m_rageLevel = value; }


    void Start()
    {
        CameraManager.Instance.Target = Player.Instance.gameObject;
        CameraManager.Instance.CurrentStrategy = new CameraFPS(3.5f, 5.0f, true);


        //Cursor Set Up
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        m_gameTimer = TimerFactory.Instance.GetTimer();

        Utils.TriggerWaitForSeconds(0.5f, () => EventManager.Instance.InvokeOnStart(this) );

        EventManager.Instance.RegisterOnStart((o) => StartGame());
        EventManager.Instance.RegisterOnRageIncrease((o, number) => IncreaseRage(number.m_number));

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.lockState = (CursorLockMode)(((int)(Cursor.lockState + 1)) % 2);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            EventManager.Instance.InvokeOnStart(this);     
        }

    }


    void StartGame()
    {
        m_rageLevel = 0;
        m_gameTimer.StartTimer();
        LevelGenerator.Instance.GenerateLevel(7, 10);
        ResetRage();
    }

    void IncreaseRage(float a_number)
    {
        m_rageLevel += a_number;
        EventManager.Instance.InvokeOnRageUpdate(this, new NumberEventArgs(m_rageLevel));

        if (m_rageLevel >= 100.0f)
        {
            EventManager.Instance.InvokeOnLoose(this);
            //ToDelete
            Utils.TriggerNextFrame(() => EventManager.Instance.InvokeOnStart(this));
        }
    }

    void ResetRage()
    {
        m_rageLevel = 0;
        EventManager.Instance.InvokeOnRageUpdate(this, new NumberEventArgs(m_rageLevel));
    }

}