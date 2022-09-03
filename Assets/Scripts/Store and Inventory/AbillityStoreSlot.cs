using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbillityStoreSlot : MonoBehaviour, IPointerDownHandler
{
    public AbillitiesItem abillity;
    public ItemDatabase id;
    private Inventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        id = FindObjectOfType<ItemDatabase>();
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
        Debug.Log(abillity.name + " is Purchased");
        inventory.UpdateAbillitySlot(abillity.id, abillity.icon);
        id.AddAbillity(abillity.name);
        gameObject.SetActive(false);
    }

}
