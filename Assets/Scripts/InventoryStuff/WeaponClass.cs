using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Item/Weapon")]
public class WeaponClass : ItemClass 
{
    // Add any variables here for weapons specifically
    // Damage dealt and durability
    public float damange;
    public float durability;
    public float totalDurability;
    public bool isBurnt;

    public override ItemClass GetItem() { return this; }
    public override IngredientClass GetIngredient() { return null; }
    public override WeaponClass GetWeapon() { return this; }
}
