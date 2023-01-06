using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class StoreUI : MonoBehaviour
{
    [Title("Shop UI")]
    public List<ConsumableStoreSlot> consumableSlots = new List<ConsumableStoreSlot>();
    public List<AbilityStoreSlot> abillitySlots = new List<AbilityStoreSlot>();
    public int numberOfAbillitySlots = 3;
    public int numberOfconsumableSlots = 3;
    [Title("Dependencies")]
    public GameObject abillitySlot;
    public GameObject consumableSlot;
    [SerializeField]
    private bool storeHasInit = false;
    [SerializeField]
    private GameObject StorePanel;

    private void OnEnable()
    {
        if (!storeHasInit) CreateNewItemSlots();
    }
    private void OnDisable()
    {
       // RemoveAllItemSlots();
    }
    public void CreateNewItemSlots()
    {
        Debug.Log("CreateOnce");
        for (int i = 0; numberOfAbillitySlots > i; i++) AddAbilityStoreSlot();
        for (int i = 0; numberOfconsumableSlots > i; i++) AddConsumableStoreSlot();
        storeHasInit = true;
        UpdateShopLists();
    }
    private void RemoveAllItemSlots()
    {
        abillitySlots.Clear();
        consumableSlots.Clear();
    }

    public void SyncAllSlotsImages()
    {
        for (int i = 0; i < consumableSlots.Count; i++)
        {
            consumableSlots[i].GetComponent<ConsumableStoreSlot>().SyncSlotAndItem();
        }
        for (int i = 0; i < abillitySlots.Count; i++)
        {
            abillitySlots[i].GetComponent<AbilityStoreSlot>().SyncSlotAndItem();
        }
    }
    private void AddAbilityStoreSlot()
    {
        GameObject aSlot = Instantiate(abillitySlot,StorePanel.transform.position, Quaternion.identity);
        AbilityStoreSlot abilityStoreSlot = aSlot.GetComponent<AbilityStoreSlot>();
        aSlot.transform.SetParent(StorePanel.transform);
        abillitySlots.Add(abilityStoreSlot);
        Store.abillitiesShopList.Add(abilityStoreSlot.abillity);
    }
    private void AddConsumableStoreSlot()
    {
        GameObject cSlot = Instantiate(consumableSlot, StorePanel.transform.position, Quaternion.identity);
        ConsumableStoreSlot consumableStoreSlot = cSlot.GetComponent<ConsumableStoreSlot>();
        cSlot.transform.parent =StorePanel.transform;
        consumableSlots.Add(consumableStoreSlot);
        Store.consumableShopList.Add(consumableStoreSlot.consumable);
    }
    public void UpdateConsumableSlot()
    {
        foreach (var slot in consumableSlots)
        {
            slot.gameObject.SetActive(true);
            slot.consumable = Store.RandomConsumable();
            print("Slots name are: " + slot.consumable.itemName);
        }
    }
    public void UpdateAbillitySlot()
    {
        print("UPDATE ABILLITIES");
        foreach (var slot in abillitySlots)
        {
            slot.gameObject.SetActive(true);
            slot.abillity = Store.RandomAbillity();
            print("Slots name are: " + slot.abillity.itemName);
        }
    }
    public void UpdateShopLists()
    {
        UpdateAbillitySlot();
        UpdateConsumableSlot();
        SyncAllSlotsImages();
    }

}
