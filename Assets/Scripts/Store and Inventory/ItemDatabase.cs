using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemDatabase : MonoBehaviour
{
    public List <Consumable> consumableList = new List<Consumable>();
    public List<Abillities> abillitiesList = new List<Abillities>();
    public Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }
    public Consumable GetConsumable(string itemName)
    {
        return consumableList.Find(name => name.name == itemName );
    }

    public void AddConsumable(string item)
    {
        Consumable itemToAdd = GetConsumable(item);
        inventory.itemList.Add(itemToAdd);
    }
    public void RemoveConsumable(string item)
    {
        Consumable itemToRemove = GetConsumable(item);
        inventory.itemList.Remove(itemToRemove);
    }
    public Abillities GetAbillity(string itemName)
    {
        return abillitiesList.Find(name => name.name == itemName);
    }

    public void RemoveAbillity(string item)
    {
        Abillities itemToRemove = GetAbillity(item);
        inventory.itemList.Remove(itemToRemove);
    }
    public void AddAbillity(string item)
    {
        Abillities itemToAdd = GetAbillity(item);
        inventory.itemList.Add(itemToAdd);
    }
}
