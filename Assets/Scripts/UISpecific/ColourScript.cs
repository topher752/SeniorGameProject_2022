using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColourScript : MonoBehaviour
{
    // Changing the UI
    public GameObject[] colours = new GameObject[7];
    public Dictionary<string, int> dict = new Dictionary<string, int>();

    // Changing the Player
    public Material[] textureForChange = new Material[7];
    public GameObject character;

    // Add public objects to every weapon and their textures
    // and reset them in another method
    public GameObject sword;
    public Material swordMAT;
    public GameObject axe;
    public Material axeMAT;

    public GameObject throwingStars; 
    public Material starsMAT;
    public WeaponCrafting weapCraftScript;

    // Start is called before the first frame update
    void Start()
    {
        colours[0].SetActive(true);
        for (int i = 1; i < 7; i++)
        {
            colours[i].SetActive(false);
        }

        dict.Add("Select1", 0);
        dict.Add("Select2", 1);
        dict.Add("Select3", 2);
        dict.Add("Select4", 3);
        dict.Add("Select5", 4);
        dict.Add("Select6", 5);
        dict.Add("Select7", 6);
    }

    // Update is called once per frame
    public void ChangeColours()
    {
        string colourName = EventSystem.current.currentSelectedGameObject.name;
        int index = dict[colourName];
        
        // Showcase the actual model
        for (int i = 0; i < 7; i++)
        {
            if (index != i)
            {
               colours[i].SetActive(false); 
            }
            else
            {
                colours[i].SetActive(true);
                ModelChange(textureForChange[i]);
            }
        }
    }

    public void ModelChange(Material matChange)
    {
        Renderer[] children = character.GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in children)
        {
            Material[] mats = new Material[rend.materials.Length];

            for (int i = 0; i < rend.materials.Length; i++)
            {
                mats[i] = matChange;
            }

            rend.materials = mats;
        }

        weapCraftScript.WeaponMaterialChange(sword);
        weapCraftScript.WeaponMaterialChange(axe);
        weapCraftScript.WeaponMaterialChange(throwingStars);
    }
}