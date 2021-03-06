﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;

            s.source.pitch = s.pitch;

            s.source.name = s.name;

            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }
    }

    void Start()
    {
        PlaySound("Theme", false);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        PlaySound("Theme", false);
    }

    public void PlaySound(string name, bool isPitchVariable)
    {  
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        if (isPitchVariable)
        {
            float pitch = UnityEngine.Random.Range(.1f, 3f);
            s.source.pitch = pitch;
        }

        s.source.Play();
    }

    public void PauseSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;
        
        s.source.Pause();
    }

    public void UnPauseSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        s.source.UnPause();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        s.source.Stop();
    }
}
