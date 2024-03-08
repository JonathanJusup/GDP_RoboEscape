using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField] private TMP_Text volumePercentage;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private TMP_Text discardButtonText;
    [SerializeField] private TMP_Text applyButtonText;
    [SerializeField] private Button discardButton;
    [SerializeField] private Button applyButton;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private TMP_Dropdown dropDownResolutions;
    [SerializeField] private TMP_Dropdown dropDownGraphicsLevel;
    [SerializeField] private Toggle toggleWindowMode;
    [SerializeField] private GameObject controls;


    private float _currentVolume;
    private int _currentResolutionIndex;
    private bool _currentWindowModeToggle;
    private int _currentGraphicsLevel;
    private int _initialGraphicsDropdownIndex;
    
    private bool _isWindowed;
    private bool _isChangedVolume;
    private bool _isChangedResolution;
    private bool _isChangedGraphics;
    
    private int _resolutionHeight;
    private int _resolutionWidth;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy (GameObject.Find("SoundManager"));
        if (Screen.fullScreen == false)
        {
            toggleWindowMode.isOn = true;
        }
        else
        {
            toggleWindowMode.isOn = false;
        }
            optionsMenu.SetActive(false);
        controls.SetActive(false);
        _currentWindowModeToggle = toggleWindowMode.isOn;
        _currentVolume = PlayerPrefs.GetFloat("musicVolume", soundSlider.value);
        soundSlider.value = _currentVolume;
        InitResolutionDropdown();
        InitGraphicsDropdown(PlayerPrefs.GetInt("masterGraphics", 1));
        // The disabling of the buttons Needs to be last at all times, delete this comment when finishing the project
        DisableApplyDiscardButtons();
        Debug.Log("STARTING");
        
    }
    

    /**
     * Startet das Spiel mit dem Intro-Level.
     */
    public void StartGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void LoadSandboxLevel()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void OpenControls()
    {
        controls.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void CloseControls()
    {
        controls.SetActive(false);
        optionsMenu.SetActive(true);
    }
    
    
    /**
     * Beendet das Programm.
     */
    public void Exit()
    {
        Application.Quit();
    }

    /**
     * Setzt die Lautstärke der Musik.
     *
     * @param volume Die Lautstärke
     */
    public void SetVolume(float volume)
    {
        
        _isChangedVolume = true;
        EnableApplyDiscardButtons();

    }


    /**
     * Setzt die Werte für das Anpassen der Bildschirmauflösung.
     *
     * @param
     */
    public void SetResolution(int index)
    {
        Debug.Log("CHANGE");
        int dropDownIndex = dropDownResolutions.value;
        string[] resolution = dropDownResolutions.options[dropDownIndex].text.Split("x");
        _resolutionWidth = int.Parse(resolution[0]);
        _resolutionHeight = int.Parse(resolution[1]);

        _isChangedResolution = true;
        EnableApplyDiscardButtons();
    }

    private void InitGraphicsDropdown(int index)
    {
        // Set the dropdown value without triggering its event
        dropDownGraphicsLevel.SetValueWithoutNotify(index);
        _currentGraphicsLevel = index;
        _initialGraphicsDropdownIndex = index;
    }


    public void SetGraphicsLevel(int index)
    {
        _isChangedGraphics = true;
        Debug.Log(_isChangedGraphics);
        _currentGraphicsLevel = dropDownGraphicsLevel.value;
        EnableApplyDiscardButtons();
    }
    
    /**
     * Verwirft die ausgewählten Änderungen an den Einstellungen.
     */
    public void DiscardChanges()
    {
        AudioListener.volume = _currentVolume;
        soundSlider.value = _currentVolume;
        dropDownResolutions.value = _currentResolutionIndex;

        volumePercentage.text = (soundSlider.value * 100.0f).ToString("0") + "%";
        _isChangedVolume = false;
        _isChangedResolution = false;
        _isChangedGraphics = false;
        toggleWindowMode.isOn = _currentWindowModeToggle;
        dropDownGraphicsLevel.value = _initialGraphicsDropdownIndex;
        DisableApplyDiscardButtons();


    }
    
    /**
     * Wendet die ausgewählten Einstellungen an.
     */
    public void ApplySettings()
    {
        if (_isChangedVolume)
        {
            PlayerPrefs.SetFloat("musicVolume", soundSlider.value);
            _isChangedVolume = false;
        }
        
        if (_isChangedResolution)
        {
            Screen.SetResolution(_resolutionWidth, _resolutionHeight, !_isWindowed);
            
            _currentResolutionIndex = dropDownResolutions.value;
            PlayerPrefs.SetInt("masterResolution", _currentResolutionIndex);
            _isChangedResolution = false;
        }

        _currentVolume = soundSlider.value;

        if (_isChangedGraphics)
        {
            PlayerPrefs.SetInt("masterGraphics", _currentGraphicsLevel);
            PlayerPrefs.Save();
            Debug.Log(_currentGraphicsLevel);
            QualitySettings.SetQualityLevel(_currentGraphicsLevel);
            _isChangedGraphics = false;
            _initialGraphicsDropdownIndex = _currentGraphicsLevel;
        }

        if (_isWindowed)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
        DisableApplyDiscardButtons();

    }
    
    
    /**
     * Schaltet die Knöpfe zum Verwerfen und Anwenden der Optionen aus.
     */
    private void DisableApplyDiscardButtons()
    {
        applyButtonText.color = Color.grey;
        discardButtonText.color = Color.grey;
        applyButton.enabled = false;
        discardButton.enabled = false;
    }


    /**
     * Schaltet die Knöpfe zum Verwerfen und Anwenden der Optionen ein.
     */
    private void EnableApplyDiscardButtons()
    {
        applyButton.enabled = true;
        discardButton.enabled = true;
        applyButtonText.color = Color.white;
        discardButtonText.color = Color.white;
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
                dropDownResolutions.SetValueWithoutNotify(i); ;
                _currentResolutionIndex = i;
            }
        }
        dropDownResolutions.AddOptions(resolutions);
    }


    public void SetIsWindowed(bool isWindowed)
    {
        Debug.Log(toggleWindowMode.isOn);
        _isWindowed = toggleWindowMode.isOn;
        EnableApplyDiscardButtons();
    }
    
    
}
