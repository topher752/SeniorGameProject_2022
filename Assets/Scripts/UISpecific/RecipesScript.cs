using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipesScript : MonoBehaviour
{
    // These two variables will change over time on how many weapons are implemented
    public GameObject[] weapons = new GameObject[3];
    public GameObject[] buttons = new GameObject[3];

    void Start()
    {
        weapons[0].SetActive(true);
        weapons[1].SetActive(false);
        weapons[2].SetActive(false);
    }

    // Viewing Different weapons
    public void ViewWeapon()
    {
        string recipeName = EventSystem.current.currentSelectedGameObject.name;
        int index = System.Int32.Parse(recipeName);
        Debug.Log("The selected recipe index is: " + index);

        for (int i = 0; i < 3; i++)
        {
            if (i != index)
            {
                weapons[i].SetActive(false);
            }
            else
            {
                weapons[i].SetActive(true);
            }
        }
    }
}
