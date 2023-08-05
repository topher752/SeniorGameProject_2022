using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInMenuMusic : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        StartCoroutine(FadeAudioSource.StartFade(audioSource, 5.0f, PlayerPrefs.GetFloat("musicVolume")));
    }
}
