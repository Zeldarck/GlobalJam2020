using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : Singleton<ClientManager>
{
    [SerializeField]
    List<Client> m_clientPrefabList = new List<Client>();

    List<Client> m_currentClientsList = new List<Client>();

    [SerializeField]
    float m_clientIntervalTime = 3.5f;

    [SerializeField]
    int m_minimumNbClient = 3;


    [SerializeField]
    float m_maxLevel = 35.0f;

    [SerializeField]
    float m_minClientIntervalTime = 0.75f;


    int m_currentMinimumNbClient = 3;


    float m_currentClientIntervalTime;

    Timer m_timer;

    int m_currentTableId = 100000;

    bool m_allowSpawn = false;

    private void Start()
    {
        EventManager.Instance.RegisterOnClientComplete((o, client) => OnClientComplete(client.m_client));
        EventManager.Instance.RegisterOnStart(o => Init());
        EventManager.Instance.RegisterOnLoose((o) => m_allowSpawn = false);
        EventManager.Instance.RegisterOnIncreaseDifficulty((o, number) => IncreaseDifficulty(number.m_int));

    }


    void Clean()
    {
        foreach (Client client in m_currentClientsList)
        {
            DestroyImmediate(client.gameObject);
        }
    }

    private void Init()
    {

        Clean();

        m_allowSpawn = true;

        m_currentClientIntervalTime = m_clientIntervalTime;
        m_currentMinimumNbClient = m_minimumNbClient;

        m_currentClientsList = new List<Client>();

        if (m_timer != null)
        {
            m_timer.Stop();
        }
        else
        {
            m_timer = TimerFactory.Instance.GetTimer();
        }

        Utils.ShuffleList(LevelGenerator.Instance.Tables);

        m_timer.StartTimer(Utils.RandomFloat(m_currentClientIntervalTime - 0.25f, m_currentClientIntervalTime + 0.25f), GenerateNewClient);

    }

    void IncreaseDifficulty(int a_difficultyLevel)
    {
        float time = Mathf.SmoothStep(0.0f, 1.0f, a_difficultyLevel / m_maxLevel);
        m_currentClientIntervalTime = Mathf.Lerp(m_clientIntervalTime, m_minClientIntervalTime, time);
        ++m_currentMinimumNbClient;
        Debug.Log(m_currentClientIntervalTime);
    }

    void OnClientComplete(Client a_client)
    {
        m_currentClientsList.Remove(a_client);
        Destroy(a_client.gameObject);

        if (m_currentClientsList.Count < m_currentMinimumNbClient)
        {
            GenerateNewClient();
        }
    }

    void GenerateNewClient()
    {

        if (m_clientPrefabList.Count == 0)
        {
            Debug.LogError("No client set up in Client Manager");
            return;
        }

        if (!m_allowSpawn)
        {
            return;
        }

        m_timer.StartTimer(Utils.RandomFloat(m_currentClientIntervalTime - 0.25f, m_currentClientIntervalTime + 0.25f), GenerateNewClient);
        Table table = GetTable();


        int id = Utils.RandomInt(0, m_clientPrefabList.Count);

        Client client = GameObjectManager.Instance.InstantiateObject(m_clientPrefabList[id].gameObject, Vector3.zero, Quaternion.identity, SPAWN_CONTAINER_TYPE.DESTRUCTIBLE).GetComponent<Client>();

        client.WantedItem = CornerManager.Instance.GetRandomItemType();

        m_currentClientsList.Add(client);
        table.AddClient(client);

    }


    Table GetTable()
    {
        if(m_currentTableId >= LevelGenerator.Instance.Tables.Count || m_currentTableId >= 5)
        {
            Utils.ShuffleList(LevelGenerator.Instance.Tables);
            m_currentTableId = 0;
        }

        if (LevelGenerator.Instance.Tables.Count == 0)
        {
            Debug.LogError("No tables generated");
            return null;
        }

        Table res = LevelGenerator.Instance.Tables[m_currentTableId];
        m_currentTableId++;

        return res;
    }


}
