using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayerAnimation : MonoBehaviour
{
    [SerializeField] private Sound sound;

    private AudioSource source;

    private void Start()
    {
        TryGetComponent(out source);
        sound.source = source;
    }

    public void PlaySound()
    {
        sound.source.Play();
    }
}
