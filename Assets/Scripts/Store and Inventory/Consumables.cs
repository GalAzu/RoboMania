using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

[CreateAssetMenu(fileName = "ConsumableItem", menuName
= "Items/Consumable")]
public class Consumables : Item
{
    public int hpAdded;
    public int speedAdded;
    public int shotRate;
}
