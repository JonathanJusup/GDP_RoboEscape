using System;
using TMPro;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    [SerializeField] private AudioMixer audioMixerMusic;
    [SerializeField] private AudioMixer audioMixerSfx;
    
    [SerializeField] private Slider audioSliderMusic;
    [SerializeField] private TMP_Text audioTextMusic;
    [SerializeField] private Slider audioSliderSfx;
    [SerializeField] private TMP_Text audioTextSfx;
    private Sound _backgroundMusic;
    
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.audioClip;
            sound.source.outputAudioMixerGroup = sound.audioMixer;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
        _backgroundMusic = Array.Find(sounds, sound => sound.name == "BackgroundMusic");
    }

    private void Start()
    {
        audioSliderMusic.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterVolume", 0.3f));
        audioTextMusic.text = (audioSliderMusic.value * 100.0f).ToString("0") + "%";
        audioSliderSfx.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterSFX", 0.3f));
        audioTextSfx.text = (audioSliderSfx.value * 100.0f).ToString("0") + "%";
        audioMixerMusic.SetFloat("volume",PlayerPrefs.GetFloat("masterVolume", 0.5f));
        audioMixerSfx.SetFloat("volume",PlayerPrefs.GetFloat("masterSFX", 0.5f));
        _backgroundMusic.source.volume = PlayerPrefs.GetFloat("masterVolume", 0.5f);
        Debug.Log("PLAY BGM");
        PlaySound("BackgroundMusic");

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    public void PlaySound(string name)
    {
      Sound sound = Array.Find(sounds, sound => sound.name == name);
      if (!sound.source.isPlaying)
      {
          sound.source.Play();
      }
      
    }

    public void PauseSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound.source.isPlaying)
        {
            sound.source.Pause();
        }
        
    }
    
    public void SetMusicVolume(float volume)
    {
        audioTextMusic.text = (volume * 100.0f).ToString("0") + "%";
        audioMixerMusic.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
        _backgroundMusic.source.volume = audioSliderMusic.value;
        Debug.Log("CHANGING VOL MUSIC");
        PlayerPrefs.Save();
    }
    
    public void SetSFXVolume(float volume)
    {
        audioTextSfx.text = (volume * 100.0f).ToString("0") + "%";
        audioMixerSfx.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterSFX", volume);
        Debug.Log("CHANGING VOL SFX");
        PlayerPrefs.Save();
    }
}
