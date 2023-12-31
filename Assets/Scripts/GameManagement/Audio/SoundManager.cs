using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Update()
    {
        int currentLevelThemeIndex = SceneManager.GetActiveScene().buildIndex - 1;
        int previousLevelThemeIndex = SceneManager.GetActiveScene().buildIndex - 2;

        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            if (SoundIsPlaying("MainMenuTheme"))
            {
                Stop("MainMenuTheme");
                Play("Level" + currentLevelThemeIndex + "Theme");
            }
            else if (previousLevelThemeIndex != 0 && SoundIsPlaying("Level" + previousLevelThemeIndex + "Theme"))
            {
                Stop("Level" + previousLevelThemeIndex + "Theme");
                Play("Level" + currentLevelThemeIndex + "Theme");
            }
        } 
        else
        {
            if (!SoundIsPlaying("MainMenuTheme"))
            {
                Play("MainMenuTheme");
                for (int i = 1; i <= 3; i++)
                {
                    Stop("Level" + i + "Theme");
                }
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.Log("Sound: " + name + " could not be played!");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound: " + name + " could not be stopped!");
            return;
        }

        s.source.Stop();
    }

    private bool SoundIsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return false;
        }

        return s.source.isPlaying;
    }
}
