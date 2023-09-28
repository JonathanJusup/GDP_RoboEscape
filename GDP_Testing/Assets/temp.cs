using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    private Door door;
    private void Start()
    {
       door = GameObject.Find("Door").GetComponent<Door>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("DOOR SHOULD OPEN");
            door.Open();
        }
    }
    
}
