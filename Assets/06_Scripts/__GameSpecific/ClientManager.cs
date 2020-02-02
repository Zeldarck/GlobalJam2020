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


    float m_currentClientIntervalTime;

    Timer m_timer;

    int m_currentTableId = 100000;

    bool m_allowSpawn = false;

    private void Start()
    {
        EventManager.Instance.RegisterOnClientComplete((o, client) => OnClientComplete(client.m_client));
        EventManager.Instance.RegisterOnStart(o => Init());
        EventManager.Instance.RegisterOnLoose((o) => m_allowSpawn = false);

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

    void OnClientComplete(Client a_client)
    {
        m_currentClientsList.Remove(a_client);
        Destroy(a_client.gameObject);

        if (m_currentClientsList.Count < m_minimumNbClient)
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
