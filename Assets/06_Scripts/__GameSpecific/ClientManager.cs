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

    int m_currentTableId = 0;

    private void Start()
    {
        EventManager.Instance.RegisterOnClientComplete((o, client) => OnClientComplete(client.m_client));
        Init();
    }

    private void Init()
    {
        m_currentClientIntervalTime = m_clientIntervalTime;

        if(m_timer != null)
        {
            m_timer.Stop();
        }
        else
        {
            m_timer = TimerFactory.Instance.GetTimer();
        }
        m_currentTableId = 0;
        Utils.ShuffleList(LevelGenerator.Instance.Tables);

        m_timer.StartTimer(Utils.RandomFloat(m_currentClientIntervalTime - 0.25f, m_currentClientIntervalTime + 0.25f), GenerateNewClient);

    }

    void OnClientComplete(Client a_client)
    {
        m_currentClientsList.Remove(a_client);
        if(m_currentClientsList.Count < m_minimumNbClient)
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

        m_timer.StartTimer(Utils.RandomFloat(m_currentClientIntervalTime - 0.25f, m_currentClientIntervalTime + 0.25f), GenerateNewClient);
        Table table = GetTable();


        int id = Utils.RandomInt(0, m_clientPrefabList.Count);

        Client client = GameObjectManager.Instance.InstantiateObject(m_clientPrefabList[id].gameObject, Vector3.zero, Quaternion.identity, SPAWN_CONTAINER_TYPE.DESTRUCTIBLE).GetComponent<Client>();

        table.AddClient(client);
        m_currentClientsList.Add(client);
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
