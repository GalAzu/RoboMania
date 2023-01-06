using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityStoreSlot : MonoBehaviour, IPointerDownHandler
{
    public AbillitiesItem abillity;
    public ItemDatabase id;
    private Inventory inventory;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        id = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        if (abillity == null) abillity = id.abillitiesList[Random.Range(0,id.abillitiesList.Count)];
    }
    public void SyncSlotAndItem()
    {
        var iconChild = gameObject.transform.Find("ItemIcon");
        Image iconChildImage = iconChild.GetComponentInChildren<Image>();
        iconChildImage.sprite = abillity.icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("UI CLICK");
        if (Character.instance.machineParts >= abillity.price)
            OnItemPurchase();
    }
    public void OnItemPurchase()
    {
        Character.instance.machineParts -= abillity.price;
        inventory.AddAbilityToInventory(abillity.id, abillity.icon);
        id.AddAbillity(abillity.itemName);
        gameObject.SetActive(false);
    }

}
