using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LoreScript : MonoBehaviour
{
    public TextMeshProUGUI textField;
    public string[] messages = new string[5];
    public Dictionary<string, int> dict = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        textField.text = "";
        dict.Add("Select1", 0);
        dict.Add("Select2", 1);
        dict.Add("Select3", 2);
        dict.Add("Select4", 3);
        dict.Add("Select5", 4);
    }

    public void SetText()
    {
        string loreName = EventSystem.current.currentSelectedGameObject.name;
        int index = dict[loreName];

        string lore = messages[index];
        textField.text = lore;
    }
}
