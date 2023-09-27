using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{

    public float conveyorSpeed = 1.0f;
    

    private void OnCollisionStay(Collision other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.right * conveyorSpeed, ForceMode.Acceleration);
        }
    }
}
