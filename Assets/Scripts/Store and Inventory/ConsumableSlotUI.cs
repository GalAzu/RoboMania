using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsumableSlotUI : MonoBehaviour , IPointerDownHandler 
{

    public Consumable consumable;
    private ShootingManager shooting;
    private ItemDatabase id;
    private Store store;
    private PowerUpsManager powerUpManager;
    private void Awake()
    {
        powerUpManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpsManager>();
        shooting = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingManager>();
        id =  GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        store = GameObject.FindGameObjectWithTag("Store").GetComponent<Store>();
        if (consumable == null) consumable = id.consumableList[Random.Range(0, id.consumableList.Count)];
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
                powerUpManager.IncreaseFireRate(consumable);
               powerUpManager.ActivateResetStatTimer(15);
                break;
            case 2:   // Increase moving speed
                Haste();
                powerUpManager.ActivateResetStatTimer(15);
                break;
        }
        ReduceCash();
        //gameObject.SetActive(false);
        Destroy(gameObject);
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
    public void Haste() => Character.instance.movementSpeed += consumable.speedAdded;

   

}
