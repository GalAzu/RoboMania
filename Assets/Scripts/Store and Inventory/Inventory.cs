using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public  List<Item> itemList = new List<Item>();
    private ItemDatabase id;

    private void Start()
    {
        id = FindObjectOfType<ItemDatabase>();
    } 
}
