using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Class for the functionality of the main menu.
///
/// @authors Florian Kern (cgt104661), Jonathan Jusup (cgt104707)
/// </summary>
public class Menu : MonoBehaviour
{
    /** Window for the settings */
    [SerializeField] private GameObject settingsMenu;
    
    /** Window for the main menu */
    [SerializeField] private GameObject mainMenu;
    
    /** Dropdown menu for all available resolutions */
    [SerializeField] private TMP_Dropdown dropdownResolutions;
    
    /** Dropdown menu for graphics settings */
    [SerializeField] private TMP_Dropdown dropdownGraphics;
    
    /** Toggle for fullscreen or window mode */
    [SerializeField] private Toggle toggleFullscreen;
    
    /** Window for the controls */
    [SerializeField] private GameObject controlsMenu;
    
    
    
    /// <summary>
    /// Method is called before the first frame update.
    /// Sets the slider values for the music and SFX and sets the volume of the music.
    /// </summary>
    void Start()
    {
        // Destroying instances that serve no purposes for the main menu
        Destroy (GameObject.Find("SoundManager"));
        Destroy (GameObject.Find("PauseMenu"));
        settingsMenu.SetActive(false);
        
        // Initializing the dropdown menu for the resolutions
        InitResolutionDropdown();
        toggleFullscreen.isOn = Screen.fullScreen;

        dropdownGraphics.SetValueWithoutNotify(PlayerPrefs.GetInt("masterGraphics", 1));
        dropdownResolutions.SetValueWithoutNotify(PlayerPrefs.GetInt("masterResolution", dropdownResolutions.options.Count));

    }
    
    /// <summary>
    /// Starts the game from the first level.
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
    /// <summary>
    /// Opens the settings menu.
    /// </summary>
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    
    /// <summary>
    /// Closes the settings menu.
    /// </summary>
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

   
    /// <summary>
    /// Closes the controls window.
    /// </summary>
    public void CloseControls()
    {
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    
    /// <summary>
    /// Opens the controls window. 
    /// </summary>
    public void OpenControls()
    {
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(true);
        
    }
    
    /// <summary>
    /// Loads the sandbox level. 
    /// </summary>
    public void LoadSandboxLevel()
    {
        SceneManager.LoadScene("Sandbox");
    }

    
    /// <summary>
    /// Exits the game. 
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    
    /// <summary>
    /// Sets the screen to fullscreen or window mode. 
    /// </summary>
    /// <param name="isFullscreen"> Decides if the window should change to window or fullscreen mode </param>
    public void SetFullscreenMode(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    
    /// <summary>
    /// Sets the selected resolution in the dropdown menu and applies it.
    /// </summary>
    /// <param name="index"> The index of the dropdown menu. </param>
    public void SetResolution(int index)
    {
        // Getting the dimensions and parsing them
        string[] resolution = dropdownResolutions.options[index].text.Split("x");
        int resolutionWidth = int.Parse(resolution[0]);
        int resolutionHeight = int.Parse(resolution[1]);
        
        // Applying selected resolution and saving it to the player prefs
        Screen.SetResolution(resolutionWidth, resolutionHeight, Screen.fullScreen);
        PlayerPrefs.SetInt("masterResolution", index);
        PlayerPrefs.Save();
    }
    
    
    
    /// <summary>
    /// Sets the selected graphics setting in the dropdown menu and applies it.
    /// </summary>
    /// <param name="index"> The index of the dropdown menu. </param>
    public void SetGraphics(int index)
    {
        // Saving index to the player prefs
        PlayerPrefs.SetInt("masterGraphics", index);
        PlayerPrefs.Save();
        
        // Applying graphics setting
        QualitySettings.SetQualityLevel(index);
    }
    
    /// <summary>
    /// Fills the resolution dropdown menu with all to the system of the player
    /// available screen resolutions.
    /// </summary>
    private void InitResolutionDropdown()
    {
        // Creating list for resolutions
        List<string> resolutions = new List<string>();
        // Getting all resolutions available to the system regardless of the refresh rate
        Resolution[] screenResolutions = Screen.resolutions;

        // Getting the resolutions that match the refresh rate of the screen of the user
        List<Resolution> filteredResolutions = new List<Resolution>();
        RefreshRate fps = Screen.currentResolution.refreshRateRatio;
        
        for (int i = 0; i < screenResolutions.Length; i++) {
            if (screenResolutions[i].refreshRateRatio.Equals(fps)) {
                filteredResolutions.Add(screenResolutions[i]);
            }
        }
        
        // Adding resolutions to the dropdown menu and setting current resolution in the dropdown menu
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            resolutions.Add(filteredResolutions[i].width + "x" + filteredResolutions[i].height);
            if (Screen.currentResolution.width == filteredResolutions[i].width &&
                Screen.currentResolution.height == filteredResolutions[i].height)
            {
                dropdownResolutions.SetValueWithoutNotify(i); ;
            }
        }
        dropdownResolutions.AddOptions(resolutions);
    }
}
