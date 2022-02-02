using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsumableStoreSlot : MonoBehaviour , IPointerDownHandler 
{

    public Consumable consumable;
    private ItemDatabase id;
    private void Awake()
    {
        id = FindObjectOfType<ItemDatabase>();
    }
    public void SyncSlotAndItem()
    {
        var iconChild = gameObject.transform.Find("ItemIcon");
        Image iconChildImage = iconChild.GetComponentInChildren<Image>();
        iconChildImage.sprite = consumable.icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Character.instance.cash >= consumable.price)
        {
            OnItemPurchase();
        }
        else Debug.Log("You Don't have enough machine parts!");
    }
    public void OnItemPurchase()
    {
        Debug.Log(consumable.name + " is Purchased");
        HPadded();
        ReduceCash();
        id.AddConsumable(consumable.name);
        Destroy(this.gameObject);
    }
    public void HPadded()
    {
        Character.instance.curHealth += consumable.hpAdded;
        UIManager.instance.UpdateHP();
    }
    public void ReduceCash()
    {
        UIManager.instance.UpdateMachineParts();
        Character.instance.cash -= consumable.price;
    }
    
}
