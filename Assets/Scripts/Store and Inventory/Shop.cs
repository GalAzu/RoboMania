using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour 
{
    public List<Consumable> consumableShopList = new List<Consumable>();
    public List<ConsumableStoreSlot> consumableSlots = new List<ConsumableStoreSlot>();
    public List<AbillityStoreSlot> abillitySlots = new List<AbillityStoreSlot>();
    public List<Abillities> abillitiesShopList = new List<Abillities>();
    public GameObject abillitySlot, consumableSlot, StorePanel;
    private ItemDatabase id;
    public int numberOfAbillitySlots = 3;
    public int numberOfconsumableSlots = 3;

    private void Awake()
    {
        CreateNewItemSlots();
    }
    private void Start()
    {
        id = FindObjectOfType<ItemDatabase>();
        PopulateShopList();
        UpdateShopLists();
    }
    private void SyncAllSlotsImages()
    {
        for(int i = 0; i < consumableSlots.Count ; i++)
        {
            consumableSlots[i].GetComponent<ConsumableStoreSlot>().SyncSlotAndItem();
        }
        for(int i = 0; i < abillitySlots.Count; i++)
        {
            abillitySlots[i].GetComponent<AbillityStoreSlot>().SyncSlotAndItem();
        }
    }
    public void PopulateShopList()
    {
        PopulateAbillities();
        PopulateConsumables();
    }
    public void UpdateShopLists()
    {
        UpdateAbillitySlot();
        UpdateConsumableSlot();
        SyncAllSlotsImages();
    }
    public void CreateNewItemSlots()
    {
       for(int i = 0; numberOfAbillitySlots > i; i++)
        {
            var aSlot = Instantiate(abillitySlot, StorePanel.transform.position, Quaternion.identity);
            aSlot.transform.parent = StorePanel.transform;
            abillitySlots.Add(aSlot.GetComponent<AbillityStoreSlot>());
        }
       for(int i=0; numberOfconsumableSlots > i; i++)
        {
            var cSlot = Instantiate(consumableSlot, StorePanel.transform.position, Quaternion.identity);
            cSlot.transform.parent = StorePanel.transform;
            consumableSlots.Add(cSlot.GetComponent<ConsumableStoreSlot>());
        }

    }
    public void PopulateConsumables()
    {
        foreach(var consumable in id.consumableList)
        {
                consumableShopList.Add(consumable);
        }
    }
    public void UpdateConsumableSlot()
    {
        foreach (var slot in consumableSlots)
        {
            slot.consumable = RandomConsumable();
            print("Slots name are: " + slot.consumable.name);
        }
    }
    public void PopulateAbillities()
    {
        foreach(var abillity in id.abillitiesList)
        {
                abillitiesShopList.Add(abillity);
        }
    }

    public void UpdateAbillitySlot()
    {
        foreach (var slot in abillitySlots)
        {
            slot.abillity = RandomAbillity();
            print("Slots name are: " + slot.abillity.name);
        }
    }
    public Consumable RandomConsumable() // Get Random Item to add to the store from the item database, with icon and everything

    {
        var itemName = consumableShopList[Random.Range(0, consumableShopList.Count)].name;
        return consumableShopList.Find(item => item.name == itemName);
    }

    public Abillities RandomAbillity() // Get Random Item to add to the store from the item database, with icon and everything

    {
        var itemName = abillitiesShopList[Random.Range(0, abillitiesShopList.Count)].name;
        return abillitiesShopList.Find(item => item.name == itemName);
    }
}
