using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MirrorController : MonoBehaviour
{
    [SerializeField] private PressurePlate leftRotationTrigger;
    [SerializeField] private PressurePlate rightRotationTrigger;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float lowerBound;
    [SerializeField] private float upperBound;

    private Quaternion m_InitialRotation;
    private float m_CurrentRotation = 0.0f;

    private void Start()
    {
        m_InitialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        CalcMirrorRotation();
    }

    private void CalcMirrorRotation()
    {
        float rotationFactor = 0.0f;
        
        if (leftRotationTrigger && rightRotationTrigger)
        {
            if (leftRotationTrigger.isPressed && !rightRotationTrigger.isPressed)
            {
                //Rotate CounterClockwise
                Debug.Log("LEFT BUTTON PRESSED");
                rotationFactor = 1.0f;
            } 
            else if (!leftRotationTrigger.isPressed && rightRotationTrigger.isPressed)
            {
                //Rotate Clockwise
                Debug.Log("RIGHT BUTTON PRESSED");
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
        m_CurrentRotation = Mathf.Clamp(m_CurrentRotation, lowerBound, upperBound);
        transform.rotation = m_InitialRotation * Quaternion.Euler(0.0f, 0.0f, m_CurrentRotation);
    }
}
