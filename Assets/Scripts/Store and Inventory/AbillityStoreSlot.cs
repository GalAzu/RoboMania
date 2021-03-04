using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbillityStoreSlot : MonoBehaviour, IPointerDownHandler
{
    public Abillities abillity;
    private ItemDatabase id;
    private void Awake()
    {
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
        if (Character.instance.cash >= abillity.price)
            OnItemPurchase();
        else Debug.Log("You don't have enough machine parts!");
    }
    public void OnItemPurchase()
    {
        Character.instance.cash -= abillity.price;
        Debug.Log(abillity.name + " is Purchased");
        id.AddAbillity(abillity.name);
        Destroy(this.gameObject);
    }
}
