using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailManager : Singleton<RailManager>
{
    [SerializeField]
    float m_distanceRail = 1.0f;

    float m_timeInRow = 0.0f;

    List<Vector3> m_corners = new List<Vector3>();

    int m_indexCorner = 0;

    public void GenerateRail(int length, int width)
    {

        float posX = LevelGenerator.Instance.SizeProp * (((float)width) / 2.0f);
        float posZ = LevelGenerator.Instance.SizeProp * (((float)length) / 2.0f);

        posX -= (LevelGenerator.Instance.SizeProp / 2.0f) + m_distanceRail;
        posZ -= (LevelGenerator.Instance.SizeProp / 2.0f) + m_distanceRail;


        m_corners.Add(new Vector3(posX, Player.Instance.Height, posZ));
        m_corners.Add(new Vector3(-posX, Player.Instance.Height, posZ));
        m_corners.Add(new Vector3(posX, Player.Instance.Height, -posZ));
        m_corners.Add(new Vector3(-posX, Player.Instance.Height, -posZ));



        m_indexCorner = 0;

        Player.Instance.transform.SetPositionAndRotation(m_corners[m_indexCorner], Quaternion.LookRotation(m_corners[m_indexCorner+1] - m_corners[m_indexCorner], Vector3.up));
        m_timeInRow = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if(m_corners.Count == 0 || !GameManager.Instance.IsGameRunning())
        {
            return;
        }

        int nextCorner = (m_indexCorner + 1) % m_corners.Count;

        m_timeInRow += Time.deltaTime;
        float time = m_timeInRow / ((m_corners[nextCorner] - m_corners[m_indexCorner]).magnitude/Player.Instance.Speed);

        Player.Instance.transform.position = Vector3.Lerp(m_corners[m_indexCorner], m_corners[nextCorner], time);

        if(time >= 1)
        {
            m_indexCorner = nextCorner;
            m_timeInRow = 0.0f;
        }
    }
}
