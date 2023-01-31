using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class StoreUI : MonoBehaviour
{
    [Title("Shop UI")]
    public List<ConsumableSlotUI> consumableSlots = new List<ConsumableSlotUI>();
    public List<AbilitySlotUI> abillitySlots = new List<AbilitySlotUI>();
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
        if (!storeHasInit) StoreInit();
        else UpdateShopLists();
    }
    public void StoreInit()
    {
        for (int i = 0; numberOfAbillitySlots > i; i++) AddAbilityStoreSlot();
        for (int i = 0; numberOfconsumableSlots > i; i++) AddConsumableStoreSlot();
        storeHasInit = true;
        UpdateShopLists();
    }

    public void UpdateShopLists()
    {
        Debug.Log("Updating Lists");
        UpdateAbillitySlot();
        UpdateConsumableSlot();
        SyncAllSlotsImages();
    }

    public void SyncAllSlotsImages()
    {
        for (int i = 0; i < consumableSlots.Count; i++)
        {
            consumableSlots[i].GetComponent<ConsumableSlotUI>().SyncSlotAndItem();
        }
        for (int i = 0; i < abillitySlots.Count; i++)
        {
            abillitySlots[i].GetComponent<AbilitySlotUI>().SyncSlotAndItem();
        }
    }
    private void ResetStore()
    {
        abillitySlots.Clear();
        consumableSlots.Clear();
    }
    private void AddAbilityStoreSlot()
    {
        GameObject aSlot = Instantiate(abillitySlot,StorePanel.transform.position, Quaternion.identity);
        AbilitySlotUI abilityStoreSlot = aSlot.GetComponent<AbilitySlotUI>();
        aSlot.transform.SetParent(StorePanel.transform);
        abillitySlots.Add(abilityStoreSlot);
        Store.abillitiesShopList.Add(abilityStoreSlot.abillity);
    }
    private void AddConsumableStoreSlot()
    {
        GameObject cSlot = Instantiate(consumableSlot, StorePanel.transform.position, Quaternion.identity);
        ConsumableSlotUI consumableStoreSlot = cSlot.GetComponent<ConsumableSlotUI>();
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

}
