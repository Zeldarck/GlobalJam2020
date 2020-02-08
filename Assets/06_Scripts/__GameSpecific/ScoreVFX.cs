using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreVFX : MonoBehaviour
{
    [SerializeField]
    GameObject m_vfxPrefab;

    [SerializeField]
    Text m_textMain;

    [SerializeField]
    Text m_textShadow;

    [SerializeField]
    Vector3 m_offset = new Vector3(0,1.55f,0);


    private void Start()
    {
        EventManager.Instance.RegisterOnScoreIncrease((o, number, client) => DisplayScore((int)number.m_int, client.m_client));
    }

    void DisplayScore(int a_score, Client a_client)
    {
        m_textMain.text = "+ " + a_score;
        m_textShadow.text = "+ " + a_score;
        Instantiate(m_vfxPrefab, a_client.transform.position + m_offset, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
