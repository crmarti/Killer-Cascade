using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Play("MainMenuTheme");
        }
        else
        {
            Play("LevelTheme");
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }
}
