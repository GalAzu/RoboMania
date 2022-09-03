using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour 
{
    public List<Consumables> consumableShopList = new List<Consumables>();
    public List<AbillitiesItem> abillitiesShopList = new List<AbillitiesItem>();
    [Space]
    public List<ConsumableStoreSlot> consumableSlots = new List<ConsumableStoreSlot>();
    public List<AbillityStoreSlot> abillitySlots = new List<AbillityStoreSlot>();
    [Space]
    public GameObject abillitySlot, consumableSlot, StorePanel;
    private ItemDatabase id;
    public int numberOfAbillitySlots = 3;
    public int numberOfconsumableSlots = 3;

    private void OnValidate()
    {
        foreach(var abillity in abillitiesShopList)
        {
            abillity.name = abillity.abillity.ToString();
        }
    }
    private void Awake()
    {
        id = FindObjectOfType<ItemDatabase>();
        CreateNewItemSlots();
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
        PopulateAbillitiesShopList();
        PopulateConsumablesShopList();
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

    public void UpdateConsumableSlot()
    {
        foreach (var slot in consumableSlots)
        {
            slot.gameObject.SetActive(true);
            slot.consumable = RandomConsumable();
            print("Slots name are: " + slot.consumable.name);
        }
    }
    public void UpdateAbillitySlot()
    {
        print("UPDATE ABILLITIES");
        foreach (var slot in abillitySlots)
        {
            slot.gameObject.SetActive(true);
            slot.abillity = RandomAbillity();
            print("Slots name are: " + slot.abillity.name);
        }
    }
    public void PopulateConsumablesShopList()
    {
        foreach (var consumable in id.consumableList)
        {
            consumableShopList.Add(consumable);
        }
    }
    public void PopulateAbillitiesShopList()
    {
        foreach(var abillity in id.abillitiesList)
        {
                abillitiesShopList.Add(abillity);
        }
    }


    public Consumables RandomConsumable() // Get Random Item to add to the store from the item database, with icon and everything

    {
        var itemName = consumableShopList[Random.Range(0, consumableShopList.Count)].name;
        return consumableShopList.Find(item => item.name == itemName);
    }

    public AbillitiesItem RandomAbillity() // Get Random Item to add to the store from the item database, with icon and everything

    {
        var itemName = abillitiesShopList[Random.Range(0, abillitiesShopList.Count)].name;
        return abillitiesShopList.Find(item => item.name == itemName);
    }
    public void PopulateStore()
    {
          CreateNewItemSlots();
          UpdateShopLists();              ///////////// PUT IT ALL IN A PLACE WHERE STORE IS ACTIVE!! FFS
          PopulateShopList();
    }

}
