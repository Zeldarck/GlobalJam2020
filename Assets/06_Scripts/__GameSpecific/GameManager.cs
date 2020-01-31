using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        LevelGenerator.Instance.GenerateLevel(7,10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
