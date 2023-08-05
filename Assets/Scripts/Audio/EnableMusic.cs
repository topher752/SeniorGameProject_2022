using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMusic : MonoBehaviour
{
    public AudioSource musicAudioSource;

    public AudioClip kitchenMusicClip;
    public AudioClip woodsMusicClip;
    public AudioClip castleMusicClip;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "KitchenMusic")
        {
            musicAudioSource.clip = kitchenMusicClip;
            musicAudioSource.Play();
        }

        if (other.gameObject.tag == "WoodsMusic")
        {
            musicAudioSource.clip = woodsMusicClip;
            musicAudioSource.Play();
        }

        if (other.gameObject.tag == "CastleMusic")
        {
            musicAudioSource.clip = castleMusicClip;
            musicAudioSource.Play();
        }
    }
}
