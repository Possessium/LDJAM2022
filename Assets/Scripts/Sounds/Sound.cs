using UnityEngine.Audio;
using UnityEngine;



[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float Volume = 1;

    [Range(0f, 3f)]
    public float Pitch = 1;

    public bool loop;


    [HideInInspector]
    public AudioSource source;
}