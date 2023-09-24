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

    // Update is called once per frame
    void Update()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float rotationFactor = 0.0f;

        if (leftRotationTrigger && rightRotationTrigger)
        {
            if (leftRotationTrigger.isPressed && !rightRotationTrigger.isPressed)
            {
                //Rotate CounterClockwise
                Debug.Log("LEFT BUTTON PRESSED");
                rotationFactor = -1.0f;
            } 
            else if (!leftRotationTrigger.isPressed && rightRotationTrigger.isPressed)
            {
                //Rotate Clockwise
                Debug.Log("RIGHT BUTTON PRESSED");
                rotationFactor = 1.0f;
            }
            else
            {
                Debug.Log("NO BUTTON PRESSED");
            }
        }

        float currentRotationSpeed = rotationSpeed * rotationFactor;
        float newRotation = currentRotation.z + currentRotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newRotation);
    }
}
