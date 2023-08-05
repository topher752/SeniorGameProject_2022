using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBlackScreen : MonoBehaviour
{
    public GameObject blackScreen;
    public GameObject blackScreen2;

    public GameObject dialogueBox;

    private void Update()
    {
        if (!dialogueBox.activeInHierarchy)
        {
            StartCoroutine(DisableBlackScreenObject());
            StartCoroutine(DisableBlackScreenObject2());
        }
    }

    IEnumerator DisableBlackScreenObject()
    {
        yield return new WaitForSeconds(2.0f);
        blackScreen.SetActive(false);
    }

    IEnumerator DisableBlackScreenObject2()
    {
        yield return new WaitForSeconds(2.0f);
        blackScreen2.SetActive(false);
    }
}
