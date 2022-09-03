using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsumableStoreSlot : MonoBehaviour , IPointerDownHandler 
{

    public Consumables consumable;
    private ShootingManager shooting;
    private ItemDatabase id;
    private Shop store;
    private void Awake()
    {
        shooting = FindObjectOfType<ShootingManager>();
        id = FindObjectOfType<ItemDatabase>();
        store = FindObjectOfType<Shop>();
    }
    public void SyncSlotAndItem()
    {
        var iconChild = gameObject.transform.Find("ItemIcon");
        Image iconChildImage = iconChild.GetComponentInChildren<Image>();
        iconChildImage.sprite = consumable.icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Character.instance.machineParts >= consumable.price)
        {
            OnItemPurchase();
        }
        else Debug.Log("You Don't have enough machine parts!");
    }
    public void OnItemPurchase()
    {
        switch(consumable.id)
        {
            case 0:  // hp adding consumables
                HPadded();
                break;
            case 1:   // fireRate adding consumables
                IncreaseFireRate();
                Character.instance.ActivateResetStatTimer(15);
                break;
            case 2:   // Increase moving speed
                Haste();
                Character.instance.ActivateResetStatTimer(15);
                break;
        }
        ReduceCash();
        gameObject.SetActive(false);
    }
    public void HPadded()
    {
        print(consumable.hpAdded.ToString() + " HP ADDED");
        Character.instance.curHealth += consumable.hpAdded;
        if (Character.instance.curHealth > 100) Character.instance.curHealth = 100;
        UIManager.instance.UpdateHP();
    }
    public void ReduceCash()
    {
        UIManager.instance.UpdateMachineParts();
        Character.instance.machineParts -= consumable.price;
    }
    public void IncreaseFireRate() => shooting.lightShotRate -= consumable.shotRate;
    public void Haste() => Character.instance.movementSpeed += consumable.speedAdded;

   

}
