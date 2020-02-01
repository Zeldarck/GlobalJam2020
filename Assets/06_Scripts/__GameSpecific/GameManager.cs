using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        LevelGenerator.Instance.GenerateLevel(7, 10);
        CameraManager.Instance.Target = Player.Instance.gameObject;
        CameraManager.Instance.CurrentStrategy = new CameraFPS(3.5f, 5.0f, true);


        //Cursor Set Up
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.lockState = (CursorLockMode)(((int)(Cursor.lockState + 1)) % 2);
        }
    }

}