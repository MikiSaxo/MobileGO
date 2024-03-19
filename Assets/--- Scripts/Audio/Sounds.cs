using UnityEngine;
using UnityEngine.Audio;
using System;
[Serializable] public class Sounds
{
    public string Name;
    public AudioClip Clip;
    public float Volume;
    public float Pitch;
    public bool Loop;

    [HideInInspector] public AudioSource Source;
}