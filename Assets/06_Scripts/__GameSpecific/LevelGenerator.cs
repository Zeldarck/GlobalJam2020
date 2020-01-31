﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : Singleton<LevelGenerator>
{
    [SerializeField]
    List<Corner> m_lcornerPrefab = new List<Corner>();

    [SerializeField]
    List<Table> m_ltablePrefab = new List<Table>();

    [SerializeField]
    float m_sizeProp = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateLevel(int length, int width)
    {

        if(m_lcornerPrefab.Count == 0)
        {
            Debug.LogError("No corner setup in LevelGenerator");
            return;
        }

        if (m_ltablePrefab.Count == 0)
        {
            Debug.LogError("No table setup in LevelGenerator");
            return;
        }


        float posX = m_sizeProp * (((float)width) / 2.0f);
        float posZ = m_sizeProp * (((float)length) / 2.0f);


        //Generate Corners
        GameObjectManager.Instance.InstantiateObject(GetRandomCorner().gameObject, new Vector3(-posX, 0.0f, -posZ), Quaternion.identity);
        GameObjectManager.Instance.InstantiateObject(GetRandomCorner().gameObject, new Vector3(-posX, 0.0f, posZ), Quaternion.Euler(0,90 , 0));
        GameObjectManager.Instance.InstantiateObject(GetRandomCorner().gameObject, new Vector3(posX, 0.0f, -posZ), Quaternion.Euler(0,-90, 0));
        GameObjectManager.Instance.InstantiateObject(GetRandomCorner().gameObject, new Vector3(posX, 0.0f, posZ), Quaternion.Euler(0,180, 0));

        for(int i = 1; i < width; ++i)
        {
            GameObjectManager.Instance.InstantiateObject(GetRandomTable().gameObject, new Vector3(-posX + m_sizeProp * i, 0.0f, posZ), Quaternion.Euler(0, 180, 0));
        }

        for (int i = 1; i < width; ++i)
        {
            GameObjectManager.Instance.InstantiateObject(GetRandomTable().gameObject, new Vector3(-posX + m_sizeProp * i, 0.0f, -posZ), Quaternion.identity);
        }

        for (int i = 1; i < length; ++i)
        {
            GameObjectManager.Instance.InstantiateObject(GetRandomTable().gameObject, new Vector3(-posX, 0.0f, -posZ + m_sizeProp * i), Quaternion.Euler(0, 90, 0));
        }

        for (int i = 1; i < length; ++i)
        {
            GameObjectManager.Instance.InstantiateObject(GetRandomTable().gameObject, new Vector3(posX, 0.0f, -posZ + m_sizeProp * i), Quaternion.Euler(0, -90, 0));
        }


    }

    Corner GetRandomCorner()
    {
        if (m_lcornerPrefab.Count == 0)
        {
            Debug.LogError("No corner setup in LevelGenerator");
            return null;
        }

        int id = Utils.RandomInt(0, m_lcornerPrefab.Count);

        return m_lcornerPrefab[id];
    }

    Table GetRandomTable()
    {
        if (m_ltablePrefab.Count == 0)
        {
            Debug.LogError("No table setup in LevelGenerator");
            return null;
        }

        int id = Utils.RandomInt(0, m_ltablePrefab.Count);

        return m_ltablePrefab[id];
    }


}