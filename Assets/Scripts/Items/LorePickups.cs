using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LorePickups : MonoBehaviour
{
    public Button lore1Button;
    public Button lore2Button;
    public Button lore3Button;  
    public Button lore4Button;
    public Button lore5Button;

    public GameObject lore1Locked;
    public GameObject lore2Locked;
    public GameObject lore3Locked;
    public GameObject lore4Locked;
    public GameObject lore5Locked;

    // Dialogue Variables
    public Dialogue dialogueCode;
    public GameObject wantsToTalk;

    //Audio
    public AudioSource sFX;
    public AudioClip pickupSFX;

    private void Awake()
    {
        dialogueCode = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();
        wantsToTalk = GameObject.FindGameObjectWithTag("Notify");
    }

    public void Unlock1()
    {
        lore1Button.interactable = true;
        lore1Locked.SetActive(false);
    }

    public void Unlock3()
    {
        lore3Button.interactable = true;
        lore3Locked.SetActive(false);
    }

    public void Unlock5()
    {
        lore5Button.interactable = true;
        lore5Locked.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LorePage2")
        {
            lore2Button.interactable = true;
            lore2Locked.SetActive(false);
            Destroy(other.gameObject);
            dialogueCode.setFruitBoss(true);
            wantsToTalk.SetActive(true);
            sFX.PlayOneShot(pickupSFX);
        }

        if (other.gameObject.tag == "LorePage4")
        {
            lore4Button.interactable = true;
            lore4Locked.SetActive(false);
            Destroy(other.gameObject);
            dialogueCode.setDairyBoss(true);
            wantsToTalk.SetActive(true);
            sFX.PlayOneShot(pickupSFX);
        }
    }
}
