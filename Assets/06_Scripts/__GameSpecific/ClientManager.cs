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


    bool m_allowSpawn = false;

    RandomCycle m_clientRandomCycle;

    bool resetRandomTable = true;
    RandomCycle m_tableRandomCycle;



    private void Start()
    {
        EventManager.Instance.RegisterOnClientComplete((o, client) => OnClientComplete(client.m_client));
        EventManager.Instance.RegisterOnStart(o => Init());
        EventManager.Instance.RegisterOnLoose((o) => m_allowSpawn = false);
        EventManager.Instance.RegisterOnIncreaseDifficulty((o, number) => IncreaseDifficulty(number.m_int));
        m_clientRandomCycle = new RandomCycle(m_clientPrefabList.Count,4);

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

        resetRandomTable = true;

        m_timer.StartTimer(Utils.RandomFloat(m_currentClientIntervalTime - 0.25f, m_currentClientIntervalTime + 0.25f) + 1.5f, GenerateNewClient);

    }

    void IncreaseDifficulty(int a_difficultyLevel)
    {
        float time = Mathf.SmoothStep(0.0f, 1.0f, a_difficultyLevel / m_maxLevel);
        m_currentClientIntervalTime = Mathf.Lerp(m_clientIntervalTime, m_minClientIntervalTime, time);
        ++m_currentMinimumNbClient;
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

        if (!m_allowSpawn)
        {
            return;
        }

        m_timer.StartTimer(Utils.RandomFloat(m_currentClientIntervalTime - 0.25f, m_currentClientIntervalTime + 0.25f), GenerateNewClient);
        Table table = GetTable();


        int id = m_clientRandomCycle.GetNextRandom();

        Client client = GameObjectManager.Instance.InstantiateObject(m_clientPrefabList[id].gameObject, Vector3.zero, Quaternion.identity, SPAWN_CONTAINER_TYPE.DESTRUCTIBLE).GetComponent<Client>();

        client.WantedItem = CornerManager.Instance.GetRandomItemType();

        m_currentClientsList.Add(client);
        table.AddClient(client);

    }


    Table GetTable()
    {
        if (resetRandomTable)
        {
            m_tableRandomCycle = new RandomCycle(LevelGenerator.Instance.Tables.Count, 10);
            resetRandomTable = false;
        }

        if (LevelGenerator.Instance.Tables.Count == 0)
        {
            Debug.LogError("No tables generated");
            return null;
        }

        return LevelGenerator.Instance.Tables[m_tableRandomCycle.GetNextRandom()];
    }


}
