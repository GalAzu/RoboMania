using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public  List<Item> itemList = new List<Item>();
    //public List<inventory_AbillitySlot> inventoryAbillities = new List<inventory_AbillitySlot>();
    public int abillitiesSlotsCount;
    private ItemDatabase id;
    public GameObject AbillitySlot , InventoryPanel;
    private void Awake()
    {
        id = FindObjectOfType<ItemDatabase>();
    }
    public void UpdateAbillitySlots()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            var obj = Instantiate(AbillitySlot, InventoryPanel.transform, true);
            obj.GetComponent<AbillitySlot>().UpdateAbillity(itemList[i].id, itemList[i].icon);
            obj.GetComponent<UnityEngine.UI.Image>().sprite = itemList[i].icon;
        }
    }
    public void UpdateAbillitySlot(int id , Sprite icon)
    {
         var obj = Instantiate(AbillitySlot, InventoryPanel.transform, true);
         obj.GetComponent<AbillitySlot>().UpdateAbillity(id,icon);
    }
    public void DeleteAllAbillitySlots()
    {
        for (int i = 0; i < itemList.Count; i++ )
        {
            var obj = GetComponent<AbillitySlot>();
            Destroy(obj.gameObject);
        }
    }
   /* private void SyncSlots()
    {
        for(int i = 0; i < itemList.Count; i++)
        {
        }
    }*/
}
