using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraStrategy
{
    protected GameObject m_target;
    private float verticalSpeed;
    private float horizontalSpeed;

    public float VerticalSpeed { get => verticalSpeed; set => verticalSpeed = value; }
    public float HorizontalSpeed { get => horizontalSpeed; set => horizontalSpeed = value; }

    public abstract void Update(Transform a_transform);

    public virtual void SetTarget(GameObject a_target)
    {
        m_target = a_target;
    }

    public virtual void Initialize(Transform a_transform)
    {

    }

    public virtual void Release()
    { 

    }

}
