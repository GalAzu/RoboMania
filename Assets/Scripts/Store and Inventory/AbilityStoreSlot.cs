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
        inventory = FindObjectOfType<Inventory>();
        id = FindObjectOfType<ItemDatabase>();
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
        if (Character.instance.machineParts >= abillity.price)
            OnItemPurchase();
        else Debug.Log("You don't have enough machine parts!");
    }
    public void OnItemPurchase()
    {
        Character.instance.machineParts -= abillity.price;
        Debug.Log(abillity.itemName + " is Purchased");
        inventory.UpdateAbillitySlot(abillity.id, abillity.icon);
        id.AddAbillity(abillity.itemName);
        gameObject.SetActive(false);
    }

}
