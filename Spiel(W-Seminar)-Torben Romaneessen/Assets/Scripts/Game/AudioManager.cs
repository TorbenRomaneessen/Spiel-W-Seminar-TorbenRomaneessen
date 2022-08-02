using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound [] Sounds;
    public static AudioManager Instance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            // This prevents having multiple AudioManager in one scene.
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in Sounds)
        {           
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            s.Source.outputAudioMixerGroup = s.Group;
        }
    }


    private void Start()
    {
        Play("ThemeSong");
    }


    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.Source.Play();
    }
}
