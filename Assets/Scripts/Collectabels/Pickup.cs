using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, ICollectable
{
    public static event HandlePickup onPickup;
    public delegate void HandlePickup(ItemClass item);
    public ItemClass pickupItem;
    public GameObject userInterface;

    //Audio
    private GameObject sFXGO;
    private AudioSource sFX;
    public AudioClip pickupSFX;

    private void Awake()
    {
        sFXGO = GameObject.FindGameObjectWithTag("SFX");
        sFX = sFXGO.GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        userInterface.transform.rotation = Camera.main.transform.rotation;
    }

    public void Collect()
    {
        Destroy(gameObject);
        onPickup?.Invoke(pickupItem);
        sFX.PlayOneShot(pickupSFX);
    }
}
