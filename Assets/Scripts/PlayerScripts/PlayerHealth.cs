using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100f;
    public int healthIncrease = 25;
    public TextMeshProUGUI livesText;

    //Audio
    public AudioSource kitchenMusic;
    public AudioSource sFX;
    public AudioClip pickupSFX;
    public AudioClip playerTakeDamage;

    //Character Controller
    public CharacterController characterController;


    private void Start()
    {
        livesText.text = "3";
    }

    private void Update()
    {
        if(playerHealth <= 0)
        {
            playerHealth = 0;
            StartCoroutine(FadeAudioSource.StartFade(kitchenMusic, 3.0f, PlayerPrefs.GetFloat("musicVolume")));

            if (livesText.text != "1")
            {
                Respawn();
            }
            else
            {
                SceneManager.LoadScene("PreliminaryEndScreen");
            }
            
        }

        if(playerHealth >= 100)
        {
            playerHealth = 100;
        }
    }

    // Here is where damage health would go, affected the HealthBar script

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HealthPickup")
        {
            playerHealth += healthIncrease;
            Destroy(other.gameObject);
            sFX.PlayOneShot(pickupSFX);
        }

        if(other.gameObject.tag == "EnemyProjectile")
        {
            playerHealth -= 3;
            sFX.PlayOneShot(playerTakeDamage);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "DeathZone")
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        characterController.enabled = false;
        gameObject.transform.position = new Vector3(-13f, 0f, -37f);
        characterController.enabled = true;
        int temp = int.Parse(livesText.text);
        temp -= 1;
        livesText.text = temp.ToString();
        playerHealth = 100;
    }
}