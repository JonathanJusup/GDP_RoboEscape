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
    [SerializeField] private AudioMixer audioMixerSFX;
    [SerializeField] private Slider audioSliderMusic;
    [SerializeField] private TMP_Text audioTextMusic;
    [SerializeField] private Slider audioSliderSFX;
    [SerializeField] private TMP_Text audioTextSFX;
    public AudioMixerGroup audioMixerG;

    private Sound BGM;
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
        BGM = Array.Find(sounds, sound => sound.name == "BackgroundMusic");
    }

    private void Start()
    {
        audioSliderMusic.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterVolume", 0.3f));
        audioTextMusic.text = (audioSliderMusic.value * 100.0f).ToString("0") + "%";
        audioSliderSFX.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterSFX", 0.3f));
        audioTextSFX.text = (audioSliderSFX.value * 100.0f).ToString("0") + "%";
        audioMixerMusic.SetFloat("volume",PlayerPrefs.GetFloat("masterVolume", 0.5f));
        audioMixerSFX.SetFloat("volume",PlayerPrefs.GetFloat("masterSFX", 0.5f));
        BGM.source.volume = PlayerPrefs.GetFloat("masterVolume", 0.5f);
        DontDestroyOnLoad(this);
        Debug.Log("PLAY BGM");
        PlaySound("BackgroundMusic");
    }

    // Update is called once per frame
    public void PlaySound(string name)
    {
      Sound sound = Array.Find(sounds, sound => sound.name == name);
      sound.source.Play();
    }
    
    public void SetMusicVolume(float volume)
    {
        audioTextMusic.text = (volume * 100.0f).ToString("0") + "%";
        audioMixerMusic.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
        BGM.source.volume = audioSliderMusic.value;
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
