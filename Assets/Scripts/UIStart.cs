using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : MonoBehaviour
{
    public GameObject inventory;
    public GameObject recipes;
    public GameObject lore;
    public GameObject colours;
    public GameObject settings;
    public GameObject bakingUI;
    public GameObject controlsScreen;

    // Start is called before the first frame update
    void Start()
    {
        inventory.SetActive(true);
        recipes.SetActive(false);
        lore.SetActive(false);
        colours.SetActive(false);
        settings.SetActive(false);
        bakingUI.SetActive(false);
        controlsScreen.SetActive(false);
    }

}
