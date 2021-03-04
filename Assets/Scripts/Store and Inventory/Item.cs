using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public string name;
    public enum ItemType { Consumable, Abillity };
    public ItemType itemType;
    public int id;
    public int price;
    public string description;
    public Sprite icon;

}
