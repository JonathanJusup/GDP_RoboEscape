using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
 
    
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controls;
    public static bool IsPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        controls.SetActive(false);
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
        controls.SetActive(false);
        Time.timeScale = 1.0f;
        IsPaused = false;
    }

    public void CloseControls()
    {
        pauseMenu.SetActive(true);
        controls.SetActive(false);
    }
    
    public void OpenControls()
    {
        pauseMenu.SetActive(false);
        controls.SetActive(true);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        IsPaused = false;
        Time.timeScale = 1.0f;
    }

    public void BackToMenu()
    {
        IsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");

        
    }


    public void QuitGame()
    {
        
        Application.Quit();
    }
    
    
    
}
