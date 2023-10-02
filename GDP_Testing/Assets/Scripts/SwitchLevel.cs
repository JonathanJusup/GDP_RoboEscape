using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the '1' key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Load or switch to level 1
            SceneManager.LoadScene(0);
        }
        
        // Check if the '2' key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Load or switch to level 2
            SceneManager.LoadScene(1);
        }
        
        // Check if the '3' key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Load or switch to level 3
            SceneManager.LoadScene(2);
        }
        // Check if the 'R' key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Load or switch to current base level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
