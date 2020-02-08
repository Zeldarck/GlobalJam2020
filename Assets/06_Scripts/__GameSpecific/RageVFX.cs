using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageVFX : MonoBehaviour
{
    [SerializeField]
    GameObject m_vfxPrefab;

    [SerializeField]
    Text m_textMain;

    [SerializeField]
    Text m_textShadow;

    [SerializeField]
    Vector3 m_offset = new Vector3(0,1.65f,0);

    [SerializeField]
    float m_delay = 0.1f;

    Vector3 m_position;

    GameObject m_lastCreated;

    private void Start()
    {
        EventManager.Instance.RegisterOnRageIncrease((o, number, client) => { m_position = client.m_client.transform.position + m_offset; Utils.TriggerWaitForSeconds(m_delay, () => DisplayRage((int)number.m_number)); });
    }

    void DisplayRage(int a_rage)
    {
        string sign = Utils.SignWithZero(a_rage) > 0 ? "-" : "+";
        m_textMain.text = sign + Mathf.Abs(a_rage);
        m_textShadow.text = sign + Mathf.Abs(a_rage);

        if(m_lastCreated != null)
        {
            Destroy(m_lastCreated);
        }

        m_lastCreated = Instantiate(m_vfxPrefab, m_position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
