using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioSource : MonoBehaviour
{
    private static GlobalAudioSource I;

    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        I = this;
    }

    public static void PlayOneShot(AudioClip clip)
    {
        I.audioSource.PlayOneShot(clip);
    }
}
