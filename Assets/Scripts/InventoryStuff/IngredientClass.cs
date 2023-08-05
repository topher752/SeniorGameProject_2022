using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Item/Ingredient")]
public class IngredientClass : ItemClass
{
    // Add any variables here for ingredients specifically

    public override ItemClass GetItem() { return this; }
    public override IngredientClass GetIngredient() { return this; }
    public override WeaponClass GetWeapon() { return null; }
}
