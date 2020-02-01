using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Timer m_gameTimer;

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
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
        m_gameTimer.StartTimer();
        LevelGenerator.Instance.GenerateLevel(7, 10);

    }



}