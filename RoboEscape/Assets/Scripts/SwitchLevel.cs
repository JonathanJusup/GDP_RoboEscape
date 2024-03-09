using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuController.IsPaused)
        {
            
        
            // Check if the '1' key is pressed
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // Load or switch to level 1
                SceneManager.LoadScene(1);
            }
        
            // Check if the '2' key is pressed
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // Load or switch to level 2
                SceneManager.LoadScene(2);
            }
            
            // Check if the '3' key is pressed
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                // Load or switch to level 3
                SceneManager.LoadScene(3);
            }
            
            // Check if the '0' key is pressed
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                // Load or switch to sandbox level
                SceneManager.LoadScene(4);
            }
        
            // Check if the 'R' key is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Restart level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        
        
        
    }
}