using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemDatabase : MonoBehaviour
{
    public List <Consumables> consumableList = new List<Consumables>();
    public List<AbillitiesItem> abillitiesList = new List<AbillitiesItem>();
    private Inventory inventory;
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        foreach(var obj in abillitiesList)
        {
            obj.id = (int)obj.abillity;
        }
    }
    public AbillitiesItem GetAbillity(string itemName)
    {
        return abillitiesList.Find(name => name.name == itemName);
    }

    public void RemoveAbillity(string item)
    {
        AbillitiesItem itemToRemove = GetAbillity(item);
        inventory.itemList.Remove(itemToRemove);
    }
    public void AddAbillity(string item)
    {
        AbillitiesItem itemToAdd = GetAbillity(item);
        inventory.itemList.Add(itemToAdd);
    }
}
