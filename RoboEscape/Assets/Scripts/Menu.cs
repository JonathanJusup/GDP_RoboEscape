using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TMP_Dropdown dropdownResolutions;
    [SerializeField] private TMP_Dropdown dropdownGraphics;
    [SerializeField] private TMP_Text audioText;
    [SerializeField] private Toggle toggleFullscreen;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private GameObject controlsMenu;
    void Start()
    {
        Destroy (GameObject.Find("SoundManager"));
        settingsMenu.SetActive(false);
        InitResolutionDropdown();
        toggleFullscreen.isOn = Screen.fullScreen;

        // TODO ALLES MIT AUDIO WOMÖGLICH LIEBER IN SOUND SCRIPT EINFÜGEN 
        audioSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterVolume", 0.3f));
        audioText.text = (audioSlider.value * 100.0f).ToString("0") + "%";
        dropdownGraphics.SetValueWithoutNotify(PlayerPrefs.GetInt("masterGraphics", 1));
        dropdownResolutions.SetValueWithoutNotify(PlayerPrefs.GetInt("masterResolution", dropdownResolutions.options.Count));

    }

    public void Play()
    {
        SceneManager.LoadScene("Intro");
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void CloseControls()
    {
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OpenControls()
    {
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(true);
        
    }
    
    public void LoadSandboxLevel()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void SetVolume(float volume)
    {
        audioText.text = (volume * 100.0f).ToString("0") + "%";
        PlayerPrefs.SetFloat("masterVolume", volume);
        Debug.Log("CHANGING VOL");
        PlayerPrefs.Save();
    }


    public void SetFullscreenMode(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    
    public void SetResolution(int index)
    {
        string[] resolution = dropdownResolutions.options[index].text.Split("x");
        int resolutionWidth = int.Parse(resolution[0]);
        int resolutionHeight = int.Parse(resolution[1]);
        Screen.SetResolution(resolutionWidth, resolutionHeight, Screen.fullScreen);
        PlayerPrefs.SetInt("masterResolution", index);
        PlayerPrefs.Save();
    }

    public void SetGraphics(int index)
    {
        PlayerPrefs.SetInt("masterGraphics", index);
        PlayerPrefs.Save();
        QualitySettings.SetQualityLevel(index);
    }
    
    /**
     * Füllt das Dropdown-Menü für die Auswahl der Bildschirmauflösung mit
     * den möglichen Auflösungen des Benutzer-Bildschirms.
     */
    private void InitResolutionDropdown()
    {
        List<string> resolutions = new List<string>();
        Resolution[] screenResolutions = Screen.resolutions;

        List<Resolution> filteredResolutions = new List<Resolution>();
        RefreshRate fps = Screen.currentResolution.refreshRateRatio;
        
        for (int i = 0; i < screenResolutions.Length; i++) {
            if (screenResolutions[i].refreshRateRatio.Equals(fps)) {
                filteredResolutions.Add(screenResolutions[i]);
            }
        }
        
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            resolutions.Add(filteredResolutions[i].width + "x" + filteredResolutions[i].height);
            if (Screen.currentResolution.width == filteredResolutions[i].width &&
                Screen.currentResolution.height == filteredResolutions[i].height)
            {
                Debug.Log("RES INDEX: " + i);
                dropdownResolutions.SetValueWithoutNotify(i); ;
            }
        }
        dropdownResolutions.AddOptions(resolutions);
    }
}
