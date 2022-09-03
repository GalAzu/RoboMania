using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item  : ScriptableObject
{
    public string name;
    public int id;
    public int price;
    public string description;
    public Sprite icon;
}
