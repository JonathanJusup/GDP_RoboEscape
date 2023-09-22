using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Mirror : BaseToggleComponent
{
    private bool m_DoRotate;
    [SerializeField] private float rotationSpeed = 1.0f;
    public override void Toggle(bool state)
    {
        m_DoRotate = state;
    }

    private void Update()
    {
        if (m_DoRotate)
        {
            transform.Rotate(Vector3.forward, rotationSpeed);
        }
    }
}
