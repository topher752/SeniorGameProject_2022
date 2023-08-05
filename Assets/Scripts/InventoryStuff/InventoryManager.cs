using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public List<SlotClass> items = new List<SlotClass>();
    [SerializeField]
    private GameObject slotHolder;
    [SerializeField]
    public GameObject[] slots;

    // Crafting Specific variables
    public ItemClass ing1;
    public ItemClass ing2;
    public ItemClass ing3;

    public WeaponClass swordObj;
    public WeaponClass axeObj;
    public WeaponClass macronObj;

    // Crafting Things
    public List<ItemClass> weapons = new List<ItemClass>();
    public WeaponCrafting craftWeapons;
    public CharacterController playerController;
    public PlayerMovement playerM;

    #region Start methods

    private void OnEnable()
    {
        Pickup.onPickup += Add;
    }

    private void OnDisable()
    {
        Pickup.onPickup -= Add;
    }

    public void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
         slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
        RefreshUI();
        playerController = gameObject.GetComponent<CharacterController>();
    }


    #endregion



    #region Main Inventory Methods

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;

                if (items[i].GetItem().isStackable)
                {
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = items[i].GetQuantity().ToString();
                }
                else
                {
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
                }
            }

            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
    }

    public void Add(ItemClass item)
    {
        // items.Add(item);
        SlotClass slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable)
        {
            slot.AddQuantity(1);
        }
        else
        {
            if (items.Count < slots.Length)
            {
                items.Add(new SlotClass(item, 1));
            }
        }

        RefreshUI();
    }

    // Change this method into moving back and forth with crafting UI
    public void Remove(ItemClass item)
    {
        SlotClass temp = Contains(item);
        if (temp != null)
        {
            if (temp.GetQuantity() > 1)
            {
                temp.SubQuantity(1);
            }
            else
            {
                SlotClass slotToRemove = new SlotClass();
                foreach (SlotClass slot in items)
                {
                    if (slot.GetItem() == item)
                    {
                        slotToRemove = slot;
                        break;
                    }
                }

                items.Remove (slotToRemove);
           }
        }
        RefreshUI();
    }

    public SlotClass Contains(ItemClass item)
    {
        foreach (SlotClass slot in items)
        {
            if (slot.GetItem() == item)
            {

                return slot;
            }
        }

        return null;
    }


    #endregion

    #region Crafting Methods

    public void CraftMacron()
    {
        SlotClass weapontemp = Contains(macronObj);
        if (weapontemp != null)
        {
            Debug.Log("You have this weapon already!");
            return;
        }

        // Items are ing1 AND ing2
        SlotClass temp = Contains(ing2);

        if (temp != null)
        {
            int num = temp.GetQuantity();
            if (num >= 7)
            {
                Debug.Log("Enough ing1");

                // Testing 2nd ingredient
                temp = Contains(ing3);
                if (temp != null)
                {
                    num = temp.GetQuantity();
                    if(num >= 3)
                    {
                        Remove(ing2);
                        Remove(ing2);
                        Remove(ing2);
                        Remove(ing2);
                        Remove(ing2);
                        Remove(ing2);
                        Remove(ing2);
                        Remove(ing3);
                        Remove(ing3);
                        Remove(ing3);

                        playerM.QuickTimeStart("throwingstar");
                    }
                    else
                    {
                        return;
                    }
                }
                
                else
                {
                    return;
                }
                
            }
            else
            {
                Debug.Log("Not Enough in1");
            }
        }
        else
        {
            Debug.Log("No of ing1");
            return;
        }
    }

    public void CraftSword()
    {
        SlotClass weapontemp = Contains(swordObj);
        if (weapontemp != null)
        {
            Debug.Log("You have this weapon already!");
            return;
        }

        SlotClass temp = Contains(ing1);

        if (temp != null)
        {
            int num = temp.GetQuantity();
            if (num >= 3)
            {
                Debug.Log("Enough ing1");

                // Testing 2nd ingredient
                temp = Contains(ing2);
                if (temp != null)
                {
                    num = temp.GetQuantity();
                    if(num >= 1)
                    {
                        Remove(ing1);
                        Remove(ing1);
                        Remove(ing1);
                        Remove(ing2);

                        Debug.Log("CraftingSword");
                        playerM.QuickTimeStart("sword");
                    }
                    else
                    {
                        return;
                    }
                }
                
                else
                {
                    return;
                }
                
            }
            else
            {
                Debug.Log("Not Enough in1");
            }
        }
        else
        {
            Debug.Log("No of ing1");
            return;
        }
    }

    // Eventually, add a CraftAxe()
    public void CraftAxe()
    {
        SlotClass weapontemp = Contains(axeObj);
        if (weapontemp != null)
        {
            Debug.Log("You have this weapon already!");
            return;
        }

        SlotClass temp = Contains(ing1);

        if (temp != null)
        {
            int num = temp.GetQuantity();
            if (num >= 5)
            {
                Debug.Log("Enough ing1");

                // Testing 2nd ingredient
                temp = Contains(ing3);
                if (temp != null)
                {
                    num = temp.GetQuantity();
                    if(num >= 8)
                    {
                        Remove(ing1);
                        Remove(ing1);
                        Remove(ing1);
                        Remove(ing1);
                        Remove(ing1);
                        Remove(ing3);
                        Remove(ing3);
                        Remove(ing3);
                        Remove(ing3);
                        Remove(ing3);
                        Remove(ing3);
                        Remove(ing3);
                        Remove(ing3);

                        playerM.QuickTimeStart("axe");

                    }
                    else
                    {
                        return;
                    }
                }
                
                else
                {
                    return;
                }
                
            }
            else
            {
                Debug.Log("Not Enough in1");
            }
        }
        else
        {
            Debug.Log("No of ing1");
            return;
        }
    }

    public void AddWeapons(string weapon, bool isntBurnt)
    {
        if (isntBurnt)
        {
            if (weapon == "throwingstar")
            {
                Add(weapons[1]);
                macronObj.durability = 150;
                craftWeapons.EnableMacron(false);
            }
            else if (weapon == "sword")
            {
                Add(weapons[0]);
                swordObj.durability = 100;
                craftWeapons.EnableSword(false);
            }
            else if (weapon == "axe")
            {
                Add(weapons[2]);
                axeObj.durability = 200;
                craftWeapons.EnableAxe(false);
            }
        }
        else
        {
            if (weapon == "throwingstar")
            {
                Add(weapons[1]);
                macronObj.durability = 75;
                craftWeapons.EnableMacron(true);
            }
            else if (weapon == "sword")
            {
                Add(weapons[0]);
                swordObj.durability = 50;
                craftWeapons.EnableSword(true);
            }
            else if (weapon == "axe")
            {
                Add(weapons[2]);
                axeObj.durability = 100;
                craftWeapons.EnableAxe(true);
            }
        }
    }


    #endregion

}
