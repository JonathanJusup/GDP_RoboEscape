using System;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.audioClip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        PlaySound("BackgroundMusic");
    }

    // Update is called once per frame
    public void PlaySound(string name)
    {
      Sound sound = Array.Find(sounds, sound => sound.name == name);
      sound.source.Play();
    }
}
