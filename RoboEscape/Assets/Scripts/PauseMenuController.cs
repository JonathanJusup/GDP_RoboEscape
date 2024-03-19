using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class for the functionality of the pause menu.
 *
 * @author Florian Kern (cgt104661)
 */
public class PauseMenuController : MonoBehaviour
{
 
    /** The menu window */
    [SerializeField] private GameObject pauseMenu;
    
    /** Window for the controls */
    [SerializeField] private GameObject controls;
    
    /** Flag that indicates if the game is currently paused or not */
    public static bool IsPaused;
    
    
    
    private static PauseMenuController _instance;

    
    
    
    
    
    
    
    // Public property to access the singleton instance
    public static PauseMenuController Instance
    {
        get { return _instance; }
    }
    
    
    /**
     * Method is called before the first frame update.
     * Deactivates the windows for the pause menu and the controls and
     * sets the object to not be destroyed on load so it can be called from following scenes.
     */
    void Start()
    {
        pauseMenu.SetActive(false);
        controls.SetActive(false);
        IsPaused = false;
        // If the instance doesn't already exist, set it to this object
        if (_instance == null)
        {
            _instance = this;
            // Make the object persistent across scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this object
            Destroy(gameObject);
        }

    }

    /**
     * Method is called once per frame.
     * Checks if the escape key has been pressed to pause or resume the game.
     */
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
    
    
    /**
     * Opens the pause menu and freezes the scene.
     */
    private void PauseGame()
    {
        // Opening pause menu
        pauseMenu.SetActive(true);
        // Setting timescale to zero to freeze the scene
        Time.timeScale = 0.0f;
        IsPaused = true;
    }
    
    /**
     * Resumes the game.
     * Deactivates the pause menu and sets the timescale back to 1.
     */
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        controls.SetActive(false);
        Time.timeScale = 1.0f;
        IsPaused = false;
    }

    /**
     * Closes the controls window.
     */
    public void CloseControls()
    {
        pauseMenu.SetActive(true);
        controls.SetActive(false);
    }
    
    /**
     * Opens the controls window.
     */
    public void OpenControls()
    {
        pauseMenu.SetActive(false);
        controls.SetActive(true);
    }
    
    /**
     * Restarts the level from the pause menu.
     */
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        IsPaused = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        controls.SetActive(false);
    }

    /**
     * Loads the menu scene.
     */
    public void BackToMenu()
    {
        IsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MenuScene");

        
    }


    /**
     * Quits the game.
     */
    public void QuitGame()
    {
        
        Application.Quit();
    }
    
    
    
}
