using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{

    [SerializeField]
    Button m_closeButton;


    //should be pack up in a sub class which handle a slider and his text and ainputfiel to manually enter the value
    [SerializeField]
    Slider m_verticalSensibility;
    [SerializeField]
    Text m_verticalSensibilityText;


    [SerializeField]
    Slider m_horizontalSensibility;
    [SerializeField]
    Text m_horizontalSensibilityText;

    [SerializeField]
    Button m_resetSensiButton;



    [SerializeField]
    float m_defaultVertSensi = 3.5f;

    [SerializeField]
    float m_defaultHorzSensi = 5.0f;


    [SerializeField]
    Button m_tutorialButton;

    [SerializeField]
    TutorialUI m_tutirialUI;


    Canvas m_canvas;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("sensiVert"))
        {
            PlayerPrefs.SetFloat("sensiVert", m_defaultVertSensi);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("sensiHorz"))
        {
            PlayerPrefs.SetFloat("sensiHorz", m_defaultHorzSensi);
            PlayerPrefs.Save();
        }



        m_canvas = GetComponent<Canvas>();
        m_closeButton.onClick.AddListener(() => Time.timeScale = 1);
        m_resetSensiButton.onClick.AddListener(() => m_verticalSensibility.value = m_defaultVertSensi);
        m_resetSensiButton.onClick.AddListener(() => m_horizontalSensibility.value = m_defaultHorzSensi);
        m_tutorialButton.onClick.AddListener(() => m_tutirialUI.StartTurorial());
        Utils.TriggerNextFrame(InitSensi);
    }

    void InitSensi()
    {
        m_verticalSensibility.onValueChanged.AddListener((value) => { CameraManager.Instance.CurrentStrategy.VerticalSpeed = value; PlayerPrefs.SetFloat("sensiVert", value); });
        m_horizontalSensibility.onValueChanged.AddListener((value) => { CameraManager.Instance.CurrentStrategy.HorizontalSpeed = value; PlayerPrefs.SetFloat("sensiHorz", value); });


        m_verticalSensibility.value = PlayerPrefs.GetFloat("sensiVert");
        m_horizontalSensibility.value = PlayerPrefs.GetFloat("sensiHorz");

    }

    

    // Update is called once per frame
    void Update()
    {
        m_canvas.enabled = Time.timeScale == 0;
        if (m_canvas.enabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(GameManager.Instance.IsGameRunning()
#if UNITY_EDITOR
         && !GameManager.Instance.DebugFreeze
#endif
         )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            m_tutirialUI.FinishTutorialExit();
        }

        m_verticalSensibilityText.text = "Vertical:  " + (m_verticalSensibility.value - m_verticalSensibility.value % 0.01f);
        m_horizontalSensibilityText.text = "Horizontal:  " + (m_horizontalSensibility.value - m_horizontalSensibility.value % 0.01f);
    }

}
