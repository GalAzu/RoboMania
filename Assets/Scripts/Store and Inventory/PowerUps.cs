using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

[CreateAssetMenu(fileName = "PowerUpItem", menuName
= "Items/Consumable")]
public class PowerUps : Item
{
    public int hpAdded;
    public int speedAdded;
    public int shotRate;
    public bool invisibility;
}
