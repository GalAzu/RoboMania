using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class ItemDatabase : MonoBehaviour
{
    [GUIColor(1,0.5f,0.4f,1)]
    public List <Consumable> consumableList = new List<Consumable>();
    [GUIColor(1, 0.5f, 0.9f, 1)]
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
        return abillitiesList.Find(name => name.itemName == itemName);
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
