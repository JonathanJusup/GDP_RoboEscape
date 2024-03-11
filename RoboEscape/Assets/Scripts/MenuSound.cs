using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSound : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixerMusic;
    [SerializeField] private AudioMixer audioMixerSFX;
    [SerializeField] private Slider audioSliderMusic;
    [SerializeField] private TMP_Text audioTextMusic;
    [SerializeField] private Slider audioSliderSFX;
    [SerializeField] private TMP_Text audioTextSFX;
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSliderMusic.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterVolume", 0.3f));
        audioTextMusic.text = (audioSliderMusic.value * 100.0f).ToString("0") + "%";
        audioSliderSFX.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterSFX", 0.3f));
        audioTextSFX.text = (audioSliderSFX.value * 100.0f).ToString("0") + "%";
        audioSource.volume = audioSliderMusic.value;

    }

    public void SetMusicVolume(float volume)
    {
        audioTextMusic.text = (volume * 100.0f).ToString("0") + "%";
        // Audiomixer volume changes logarithmically, slider values change linearly
        audioMixerMusic.SetFloat("volume",Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
        audioSource.volume = audioSliderMusic.value;
        Debug.Log("CHANGING VOL MUSIC");
        PlayerPrefs.Save();
    }
    
    public void SetSFXVolume(float volume)
    {
        audioTextSFX.text = (volume * 100.0f).ToString("0") + "%";
        audioMixerSFX.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterSFX", volume);
        Debug.Log("CHANGING VOL SFX");
        PlayerPrefs.Save();
    }

}
