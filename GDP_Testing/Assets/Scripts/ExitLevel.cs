using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    private Door doorScript;
    private void OnTriggerEnter(Collider other)
    {
        doorScript = GameObject.Find("Door").GetComponent<Door>();
        if (other.gameObject.CompareTag("Player") && doorScript.GetIsOpen())
        {
            SceneManager.LoadScene(1);
        }
    }
}
