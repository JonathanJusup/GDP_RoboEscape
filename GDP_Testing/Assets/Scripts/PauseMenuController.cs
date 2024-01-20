using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
 
    
    [SerializeField] private GameObject pauseMenu;
    public static bool IsPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        IsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    
    
    
    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        IsPaused = true;
    }
    
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        IsPaused = false;
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            Debug.Log("IST NICHT NULL");
        }
        Destroy(audioManager);
    }


    public void QuitGame()
    {
        
        Application.Quit();
    }
    
}
