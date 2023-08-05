using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCrafting : MonoBehaviour
{
    /* Will need to figure out either a switch statement to constantly check what the "primary weapon" is
    and depending on what that weapon is, then set the boolean of that value to true and the others false,
    only triggering the attack animation. Now WHERE this goes could be either here or player attack*/

    public GameObject macron;
    public GameObject sword;
    public GameObject axe;
    public WeaponCrafting instance;
    public InventoryManager inv;
    [SerializeField] private bool macronActive;
    [SerializeField] private bool swordActive;
    [SerializeField] private bool axeActive;

    public Image primaryImage;
    public Image secondaryImage;
    public Sprite swordSprite;
    public Sprite macronSprite;
    public Sprite axeSprite;
    public Sprite weaponReset;

    [SerializeField] private GameObject primaryWeapon;
    [SerializeField] private GameObject secondaryWeapon;
    private Animator anim;

    public WeaponClass swordObj;
    public WeaponClass axeObj;
    public WeaponClass macronObj;
    public Image primaryDurability;
    public Image secondaryDurability;

    private WeaponClass primary;
    private WeaponClass secondary;

    public Material swordMAT;
    public Material swordBurnedMAT;
    public Material axeMAT;
    public Material axeBurnedMAT;
    public Material starsMAT;
    public Material starsBurnedMAT;
    public Material mat;

    public Sprite burntSword;
    public Sprite burntStar;
    public Sprite burntAxe;

    // Start is called before the first frame update
    void Start()
    {
        macron.SetActive(false);
        sword.SetActive(false);
        axe.SetActive(false);
        axeActive = false;
        macronActive = false;
        swordActive = false;
        anim = GetComponentInChildren<Animator>();
        primaryDurability.enabled = false;
        secondaryDurability.enabled = false;
    }

    void PrimaryDurabilityZero()
    {
        if (primaryImage.sprite.name == "CakeSword" || primaryImage.sprite.name == "CakeSword_Burnt")
        {
            swordActive = false;
            anim.SetBool("SwordActive?", false);
        }
        else if (primaryImage.sprite.name == "Axe" || primaryImage.sprite.name == "Axe_Burnt")
        {
            axeActive = false;
            anim.SetBool("AxeActive?", false);
        }
        else if (primaryImage.sprite.name == "StrawMac" || primaryImage.sprite.name == "StrawMac_Burnt")
        {
            macronActive = false;
            anim.SetBool("StarActive?", false);
        }
        inv.Remove(primary);

        if (secondaryDurability.enabled)
        {
            TwoPressed();
            secondaryImage.sprite = weaponReset;
            secondary = null;
            secondaryWeapon = null;

            secondaryDurability.enabled = false;

            if (primaryImage.sprite.name == "CakeSword" || primaryImage.sprite.name == "CakeSword_Burnt")
            {
                swordActive = true;
                anim.SetBool("SwordActive?", true);
            }
            else if (primaryImage.sprite.name == "Axe" || primaryImage.sprite.name == "Axe_Burnt")
            {
                axeActive = true;
                anim.SetBool("AxeActive?", true);
            }
            else if (primaryImage.sprite.name == "StrawMac" || primaryImage.sprite.name == "StrawMac_Burnt")
            {
                axeActive = true;
                anim.SetBool("StarActive?", true);
            }
        }
        else
        {
            macron.SetActive(false);
            sword.SetActive(false);
            axe.SetActive(false);

            anim.SetBool("SwordActive?", false);
            anim.SetBool("AxeActive?", false);
            anim.SetBool("StarActive?", false);
            primaryImage.sprite = weaponReset;
            primary = null;
            primaryDurability.enabled = false;
            primaryWeapon.SetActive(false);
            primaryWeapon = null;


            // Add all other anim.SetBool for weapons false

        }
    }

    void SecondaryDurabilityZero()
    {
        if (secondaryImage.sprite.name == "CakeSword" || secondaryImage.sprite.name == "CakeSword_Burnt")
        {
            swordActive = false;
            anim.SetBool("SwordActive?", false);
        }
        else if (secondaryImage.sprite.name == "Axe" || secondaryImage.sprite.name == "Axe_Burnt")
        {
            axeActive = false;
            anim.SetBool("AxeActive?", false);
        }
        else if (secondaryImage.sprite.name == "StrawMac" || secondaryImage.sprite.name == "StrawMac_Burnt")
        {
            macronActive = false;
            anim.SetBool("StarActive?", false);
        }
        inv.Remove(secondary);

        if (primaryDurability.enabled)
        {
            OnePressed();
            primaryImage.sprite = weaponReset;
            primary = null;
            primaryWeapon = null;

            primaryDurability.enabled = false;

            if (secondaryImage.sprite.name == "CakeSword")
            {
                swordActive = true;
                anim.SetBool("SwordActive?", true);
            }
            else if (secondaryImage.sprite.name == "Axe")
            {
                axeActive = true;
                anim.SetBool("AxeActive?", true);
            }
            else if (secondaryImage.sprite.name == "StrawMac")
            {
                axeActive = true;
                anim.SetBool("StarActive?", true);
            }
        }
        else
        {
            macron.SetActive(false);
            sword.SetActive(false);
            axe.SetActive(false);

            anim.SetBool("SwordActive?", false);
            anim.SetBool("AxeActive?", false);
            anim.SetBool("StarActive?", false);
            secondaryImage.sprite = weaponReset;
            secondary = null;
            secondaryDurability.enabled = false;
            secondaryWeapon.SetActive(false);
            secondaryWeapon = null;


            // Add all other anim.SetBool for weapons false

        }
    }

    public void Update()
    {
        if (primary != null)
        {
            primaryDurability.fillAmount = primary.durability / primary.totalDurability;

            if (primaryDurability.enabled)
            {
                if (primaryDurability.fillAmount == 0)
                {
                    PrimaryDurabilityZero();
                }
            }
        }

        if (secondary != null)
        {
            secondaryDurability.fillAmount = secondary.durability / secondary.totalDurability;

            if (secondaryDurability.enabled)
            {
                if (secondaryDurability.fillAmount == 0)
                {
                    SecondaryDurabilityZero();
                }
            }
        }
        // Check if the fill amount is ever 0
    }

    void LateUpdate()
    {

    }

    public void EnableMacron(bool burnt)
    {
        if (!swordActive && !axeActive)
        {
            if (burnt)
            {
                macronObj.isBurnt = true;
                primaryImage.sprite = burntStar;
            }
            else
            {
                macronObj.isBurnt = false;
                primaryImage.sprite = macronSprite;
            }

            WeaponMaterialChange(macron);

            primaryWeapon = macron;
            primaryWeapon.SetActive(true);
            macronActive = true;
            primary = macronObj;
            primaryDurability.enabled = true;
            anim.SetBool("StarActive?", true);
        }

        else
        {
            if (burnt)
            {
                macronObj.isBurnt = true;
                secondaryImage.sprite = burntStar;
            }
            else
            {
                macronObj.isBurnt = false;
                secondaryImage.sprite = macronSprite;
            }

            WeaponMaterialChange(macron);

            secondaryWeapon = macron;
            macronActive = true;
            secondary = macronObj;
            secondaryDurability.enabled = true;
        }
    }

    public void EnableSword(bool burnt)
    {
        if (!macronActive && !axeActive)
        {
            if (burnt)
            {
                swordObj.isBurnt = true;
                primaryImage.sprite = burntSword;
            }
            else
            {
                swordObj.isBurnt = false;
                primaryImage.sprite = swordSprite;
            }

            WeaponMaterialChange(sword);

            primaryWeapon = sword;
            primaryWeapon.SetActive(true);
            swordActive = true;
            primary = swordObj;
            primaryDurability.enabled = true;
            anim.SetBool("SwordActive?", true);
            
        }

        else
        {
            if (burnt)
            {
                swordObj.isBurnt = true;
                secondaryImage.sprite = burntSword;
            }
            else
            {
                swordObj.isBurnt = false;
                secondaryImage.sprite = swordSprite;
            }

            WeaponMaterialChange(sword);

            secondaryWeapon = sword;
            swordActive = true;
            secondary = swordObj;
            secondaryDurability.enabled = true;
        }
    }

    public void EnableAxe(bool burnt)
    {
        if (!macronActive && !swordActive)
        {
            if (burnt)
            {
                axeObj.isBurnt = true;
                primaryImage.sprite = burntAxe;
            }
            else
            {
                axeObj.isBurnt = false;
                primaryImage.sprite = axeSprite;
            }

            WeaponMaterialChange(axe);

            primaryWeapon = axe;
            primaryWeapon.SetActive(true);
            axeActive = true;
            primaryDurability.enabled = true; ;
            primary = axeObj;
            anim.SetBool("AxeActive?", true);
        }

        else
        {
            if (burnt)
            {
                axeObj.isBurnt = true;
                secondaryImage.sprite = burntAxe;
            }
            else
            {
                axeObj.isBurnt = false;
                secondaryImage.sprite = axeSprite;
            }

            WeaponMaterialChange(axe);

            secondaryWeapon = axe;
            axeActive = true;
            secondary = axeObj;
            secondaryDurability.enabled = true;
        }
    }

    public void OnePressed()
    {
        // If active for primary is false and secondary is true
        if (!primaryWeapon.activeSelf && secondaryWeapon.activeSelf)
        {
            GameObject temp = secondaryWeapon;
            secondaryWeapon = primaryWeapon;
            primaryWeapon = temp;
            primaryWeapon.SetActive(true);
            secondaryWeapon.SetActive(false);
            SwapWeapons();
        }
    }

    public void TwoPressed()
    {
        if (primaryWeapon.activeSelf && !secondaryWeapon.activeSelf && secondaryWeapon != null)
        {
            GameObject temp = secondaryWeapon;
            secondaryWeapon = primaryWeapon;
            primaryWeapon = temp;
            primaryWeapon.SetActive(true);
            secondaryWeapon.SetActive(false);
            SwapWeapons();
        }
    }

    public void SwapWeapons()
    {
        Sprite temp = primaryImage.sprite;
        primaryImage.sprite = secondaryImage.sprite;
        secondaryImage.sprite = temp;
        WeaponClass temp2 = primary;
        primary = secondary;
        secondary = temp2;

        // Check Animations
        if (primaryImage.sprite.name == "CakeSword" || primaryImage.sprite.name == "CakeSword_Burnt")
        {
            anim.SetBool("AxeActive?", false);
            anim.SetBool("StarActive?", false);
            anim.SetBool("SwordActive?", true);
        }
        else if (primaryImage.sprite.name == "Axe" || primaryImage.sprite.name == "Axe_Burnt")
        {
            anim.SetBool("SwordActive?", false);
            anim.SetBool("StarActive?", false);
            anim.SetBool("AxeActive?", true);
        }
        else if (primaryImage.sprite.name == "StrawMac" || primaryImage.sprite.name == "StrawMac_Burnt")
        {
            anim.SetBool("SwordActive?", false);
            anim.SetBool("AxeActive?", false);
            anim.SetBool("StarActive?", true);
        }
    }

    public void WeaponMaterialChange(GameObject weapon)
    {
        bool burned;

        if (weapon.name == "CakeSword1")
        {
            burned = swordObj.isBurnt;
            if (burned)
            {
                mat = swordBurnedMAT;
            }
            else
            {
                mat = swordMAT;
            }
        }
        else if (weapon.name == "CarrotAxeModel")
        {
            burned = axeObj.isBurnt;
            if (burned)
            {
                mat = axeBurnedMAT;
            }
            else
            {
                mat = axeMAT;
            }
        }
        else if (weapon.name == "MacaroneStar")
        {
            burned = macronObj.isBurnt;
            if (burned)
            {
                mat = starsBurnedMAT;
            }
            else
            {
                mat = starsMAT;
            }
        }

        Renderer[] children = weapon.GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in children)
        {
            Material[] mats = new Material[rend.materials.Length];

            for (int i = 0; i < rend.materials.Length; i++)
            {
                mats[i] = mat;
            }

            rend.materials = mats;
        }
    }
}