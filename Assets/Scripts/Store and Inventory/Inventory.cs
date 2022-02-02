using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public  List<Item> itemList = new List<Item>();
    private ItemDatabase id;
    public static Inventory instance;

    private void Start()
    {
        instance = this;
        id = FindObjectOfType<ItemDatabase>();
    } 

    private void UsingAbillity()
    {

    }
}
