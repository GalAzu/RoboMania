using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsumableStoreSlot : MonoBehaviour , IPointerDownHandler 
{
    private float initSpeed;
    private float initRate;
    public Consumable consumable;
    private Shooting shooting;
    private ItemDatabase id;
    private void Awake()
    {
        shooting = FindObjectOfType<Shooting>();
        id = FindObjectOfType<ItemDatabase>();
        initSpeed = FindObjectOfType<Character>().movementSpeed;
        initRate = shooting.shotRate;
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
            case 1:  // hp adding consumables
                HPadded();
                break;
            case 2:   // fireRate adding consumables
                IncreaseFireRate();
                break;
            case 3:   // dashEnable adding consumables
                StartCoroutine(Haste());
                break;
        }
        ReduceCash();
        if(consumable.id == 1) // id 1 for fire rate increase
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
        Character.instance.machineParts -= consumable.price;
    }
    public void IncreaseFireRate()
    {
        shooting.shotRate -= consumable.shotRate;
    }
    private IEnumerator ResetStats()
    {
        shooting.shotRate = initRate;
        Character.instance.movementSpeed = initSpeed;
        yield return new WaitForSeconds(5);
    }
    public IEnumerator Haste()
    {
        var _char = FindObjectOfType<Character>();
        var oldSpeed = _char.movementSpeed ;
        _char.movementSpeed += consumable.speedAdded;
        yield return new WaitForSeconds(5);
        _char.movementSpeed = oldSpeed;
    }
    public void DashEnable()
    {

    }
}
