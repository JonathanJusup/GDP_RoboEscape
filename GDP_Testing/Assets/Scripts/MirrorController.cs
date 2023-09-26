using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MirrorController : MonoBehaviour
{
    [SerializeField] private PressurePlate leftRotationTrigger;
    [SerializeField] private PressurePlate rightRotationTrigger;
    
    [SerializeField] private Transform translationCenter;
    [SerializeField] private Transform body;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float lowerRotationBound;
    [SerializeField] private float upperRotationBound;

    [SerializeField] private float lowerXBound = -0.5f;
    [SerializeField] private float upperXBound = 0.5f;
    
    private float m_OffsetLowerBound;
    private float m_OffsetUpperBound;

    private Quaternion m_InitialRotation;
    private float m_CurrentRotation = 0.0f;

    

    private void Start()
    {
        m_InitialRotation = body.rotation;
        m_OffsetLowerBound = lowerRotationBound - body.transform.eulerAngles.z;
        m_OffsetUpperBound = upperRotationBound - body.transform.eulerAngles.z;

        float initPositionX = transform.position.x;
        lowerXBound += initPositionX;
        upperXBound += initPositionX;
    }

    // Update is called once per frame
    void Update()
    {
        CalcMirrorRotation();

        float offsetX = Mathf.Lerp(lowerXBound, upperXBound,
            (m_CurrentRotation - lowerRotationBound) / (upperRotationBound - lowerRotationBound));

        translationCenter.position = new Vector3(offsetX, translationCenter.position.y, translationCenter.position.z);
    }

    private void CalcMirrorRotation()
    {
        float rotationFactor = 0.0f;
        
        if (leftRotationTrigger && rightRotationTrigger)
        {
            if (leftRotationTrigger.isPressed && !rightRotationTrigger.isPressed)
            {
                //Rotate CounterClockwise
                rotationFactor = 1.0f;
            } 
            else if (!leftRotationTrigger.isPressed && rightRotationTrigger.isPressed)
            {
                //Rotate Clockwise
                rotationFactor = -1.0f;
            }
        } 
        else if (leftRotationTrigger)
        {
            if (leftRotationTrigger.isPressed)
            {
                rotationFactor = 1.0f;
            }
        } 
        else if (rightRotationTrigger)
        {
            if (rightRotationTrigger.isPressed)
            {
                rotationFactor = -1.0f;
            }
        }
        
        
        m_CurrentRotation += rotationFactor * rotationSpeed * Time.deltaTime;
        m_CurrentRotation = Mathf.Clamp(m_CurrentRotation, m_OffsetLowerBound, m_OffsetUpperBound);
        body.rotation = m_InitialRotation * Quaternion.Euler(0.0f, 0.0f, m_CurrentRotation);
    }
}
