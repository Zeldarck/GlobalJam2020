using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFPS : CameraStrategy
{
    float m_verticalSpeed;
    float m_horizontalSpeed;
    Vector3 m_anchorPoint;
    bool m_centerOnTarget = true;
    bool m_rotateTarget = false;



    public CameraFPS(float a_verticalSpeed = 3.5f, float a_horizontalSpeed = 5.0f, bool a_rotateTarget = false)
    {
        InitValues(a_verticalSpeed, a_horizontalSpeed, a_rotateTarget);
    }

    public CameraFPS(Vector3 a_anchorPoint, float a_verticalSpeed = 3.5f, float a_horizontalSpeed = 5.0f, bool a_rotateTarget = false)
    {
        InitValues(a_verticalSpeed, a_horizontalSpeed, a_rotateTarget);
        m_anchorPoint = a_anchorPoint;
        m_centerOnTarget = false;
    }



    private void InitValues(float a_verticalSpeed, float a_horizontalSpeed, bool a_rotateTarget)
    {
        m_verticalSpeed = a_verticalSpeed;
        m_rotateTarget = a_rotateTarget;
        m_horizontalSpeed = a_horizontalSpeed;
    }


    public override void Update(Transform a_transform)
    {
        if (m_target && Cursor.lockState == CursorLockMode.Locked)
        {
            float h = m_horizontalSpeed * Input.GetAxis("Mouse X");
            float v = m_verticalSpeed * Input.GetAxis("Mouse Y")* -1.0f;

            a_transform.Rotate(v, h, 0);
            a_transform.localRotation = Quaternion.Euler(a_transform.localRotation.eulerAngles.x, a_transform.localRotation.eulerAngles.y, 0);

            if (m_rotateTarget)
            {
                m_target.transform.localRotation = a_transform.localRotation;
            }
        }

    }

    public override void SetTarget(GameObject a_target)
    {
        base.SetTarget(a_target);
    }

    public override void Initialize(Transform a_transform)
    {
        base.Initialize(a_transform);
        
        if (m_centerOnTarget)
        {
            m_anchorPoint = m_target.transform.position;
        }
        a_transform.position = m_anchorPoint;
        a_transform.localRotation = new Quaternion();// m_target.transform.forward;

        Camera.main.cullingMask = (Camera.main.cullingMask ^ LayerMask.GetMask("Player"));
    }


    public override void Release()
    {
        base.Release();
        Camera.main.cullingMask = (Camera.main.cullingMask ^ LayerMask.GetMask("Player"));
    }

}

