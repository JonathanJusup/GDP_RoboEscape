using UnityEngine.Audio;
using UnityEngine;


/// <summary>
/// Class for a single sound that can be used in a game.
///
/// @author Florian Kern (cgt104661)
/// </summary>
[System.Serializable]
public class Sound
{
    /** Name of the sound */
    public string name;
    
    /** Used audio clip for the sound */
    public AudioClip audioClip;
    
    /** Used audio mixer */
    public AudioMixerGroup audioMixer;

    /** Volume of the sound */
    [Range(0.01f, 1f)]
    public float volume;
    
    /** Flag to decide whether a sound loops or not */
    public bool loop;
    
    /** Source for the sound */
    [HideInInspector]
    public AudioSource source;

}
