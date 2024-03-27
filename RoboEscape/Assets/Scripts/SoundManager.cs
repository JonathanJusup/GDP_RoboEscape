using System;
using TMPro;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for managing all sounds that get used in the game.
/// Allows playing a specific sound or pausing it. Also gives the possibility to change the
/// volume of the sounds.
///
/// @authors Florian Kern (cgt104661)
/// </summary>
public class SoundManager : MonoBehaviour {
    /** Sound-array for all used sounds */
    public Sound[] sounds;

    /** Audiomixer for the music */
    [SerializeField] private AudioMixer audioMixerMusic;

    /** Audiomixer for the SFX */
    [SerializeField] private AudioMixer audioMixerSfx;

    /** Slider for the music */
    [SerializeField] private Slider audioSliderMusic;

    /** Text for the slider of the music */
    [SerializeField] private TMP_Text audioTextMusic;

    /** Slider for the SFX */
    [SerializeField] private Slider audioSliderSfx;

    /** Text for the slider of the SFX */
    [SerializeField] private TMP_Text audioTextSfx;

    private static SoundManager _instance;


    // Public property to access the singleton instance
    public static SoundManager Instance => _instance;

    
    /// <summary>
    /// Gets called when the script instance is being loaded.
    /// Initializes all sounds.
    /// </summary>
    void Awake() {
        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.audioClip;
            sound.source.volume = sound.volume;
            sound.source.outputAudioMixerGroup = sound.audioMixer;
            sound.source.loop = sound.loop;
        }
    }
    
    /// <summary>
    /// Method is called before the first frame update.
    /// Sets the slider values for the music and SFX and sets the volume of the music.
    /// Instantiates the SoundManager as a singleton.
    /// </summary>
    private void Start() {
        audioSliderMusic.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterVolume", 0.3f));
        audioTextMusic.text = (audioSliderMusic.value * 100.0f).ToString("0") + "%";
        audioSliderSfx.SetValueWithoutNotify(PlayerPrefs.GetFloat("masterSFX", 0.3f));
        audioTextSfx.text = (audioSliderSfx.value * 100.0f).ToString("0") + "%";
        audioMixerMusic.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("masterVolume", 0.5f)) * 20);
        audioMixerSfx.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("masterSFX", 0.3f)) * 20);
        PlaySound("BackgroundMusic");
        // If the instance doesn't already exist, set it to this object
        if (_instance == null) {
            _instance = this;
            // Make the object persistent across scenes
            DontDestroyOnLoad(gameObject);
        } else {
            // If an instance already exists, destroy this object
            Destroy(gameObject);
        }
    }

    
    /// <summary>
    /// Plays a sound.
    /// </summary>
    /// <param name="soundName"> The sound that gets played. </param>
    public void PlaySound(string soundName) {
        // Finding the sound
        Sound sound = Array.Find(sounds, sound => sound.name == soundName);

        // Avoiding that a sound gets played multiple times
        if (!sound.source.isPlaying) {
            sound.source.Play();
        }
    }

    
    /// <summary>
    /// Pauses a currently playing sound.
    /// </summary>
    /// <param name="soundName"> The sound that gets paused </param>
    public void PauseSound(string soundName) {
        // Find the wanted sound
        Sound sound = Array.Find(sounds, sound => sound.name == soundName);
        if (sound.source.isPlaying) {
            // Pausing the sound
            sound.source.Pause();
        }
    }

    
    /// <summary>
    /// Gets called when the slider value for the volume of the music gets changed.
    /// Sets the volume for the music.
    /// </summary>
    /// <param name="volume"> The volume for the music. </param>
    public void SetMusicVolume(float volume) {
        audioTextMusic.text = (volume * 100.0f).ToString("0") + "%";
        // Audiomixer volume changes logarithmically, slider values change linearly
        audioMixerMusic.SetFloat("volume", Mathf.Log10(volume) * 20);

        // Setting playerprefs for the music with the chosen volume
        PlayerPrefs.SetFloat("masterVolume", volume);

        PlayerPrefs.Save();
    }
    
    /// <summary>
    /// Gets called when the slider value for the volume of the special effects gets changed.
    /// Sets the volume for the special effects.
    /// </summary>
    /// <param name="volume"> The volume for the special effects. </param>
    public void SetSFXVolume(float volume) {
        audioTextSfx.text = (volume * 100.0f).ToString("0") + "%";
        // Audiomixer volume changes logarithmically, slider values change linearly
        audioMixerSfx.SetFloat("volume", Mathf.Log10(volume) * 20);

        // Setting playerprefs for the special effects with the chosen volume
        PlayerPrefs.SetFloat("masterSFX", volume);

        // Saving playerprefs
        PlayerPrefs.Save();
    }
}