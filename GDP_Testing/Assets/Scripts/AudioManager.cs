using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioBackgroundMusic;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text volumePercentage;

    
    public AudioClip levelMusic;
    public AudioClip menuMusic;

    

    void Start()
    {

        if (PlayerPrefs.HasKey("musicVolume") && IsMenuScene())
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }
        
        
        audioBackgroundMusic.clip = !IsMenuScene() ? levelMusic : menuMusic;
        
        audioBackgroundMusic.Play();
    }

    public void SetVolume()
    {
        if (IsMenuScene())
        {
            float volume = volumeSlider.value;
            volumePercentage.text = (volume * 100.0f).ToString("0") + "%";
            musicMixer.SetFloat("music", MathF.Log10(volume) * 20);
            PlayerPrefs.SetFloat("musicVolume", volume);
        }
        else
        {
            musicMixer.SetFloat("music", MathF.Log10(0.4f) * 20);
            PlayerPrefs.SetFloat("musicVolume", 0.4f);
        }
        
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetVolume();
    }

    private bool IsMenuScene()
    {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }
}
