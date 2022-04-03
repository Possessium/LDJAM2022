using System;
using UnityEngine.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioController : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioController Instance
    {
        get; private set;
    }

    private void Awake()
    {
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.playOnAwake = false;

            s.source.loop = s.loop;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + "not found!");
            return;
        }

        s.source.pitch = Random.Range(.5f, 1.5f);
        s.source.Play();
    }
}