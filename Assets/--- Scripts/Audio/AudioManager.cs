using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sounds[] Sounds;
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
        foreach (Sounds s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    private void Start()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        
        if (currentScene == 0)
        {
            PlaySound("snd_music_menu");
            StopSound("snd_music_main");
        }
        else
        {
            PlaySound("snd_music_main");
            StopSound("snd_music_menu");
        }
    }

    public void PlaySound(string name)
    {
        Sounds s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found");
            return;
        }
        s.Source.Play();
    }

    public void StopSound(string name)
    {
        Sounds s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found in StopSound");
            return;
        }
        s.Source.Stop();
    }
}