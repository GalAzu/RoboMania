using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class Store : MonoBehaviour 
{
    [Title("Shop Data"), ShowInInspector]
    public static List<Consumable> consumableShopList = new List<Consumable>();
    [ShowInInspector]
    public static List<AbillitiesItem> abillitiesShopList = new List<AbillitiesItem>();
    [Space]

    [Space]
    [SerializeField]
    private ItemDatabase id;
    [SerializeField]
    private StoreUI storeUI;
    [SerializeField]
    private bool storeHasInit;

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach(var abillity in abillitiesShopList)
        {
            abillity.itemName = abillity.abillity.ToString();
        }
    }
#endif

    public void PopulateStore()
    {
        if(!storeHasInit) storeUI.StoreInit();
        storeUI.UpdateShopLists();
        PopulateShopList();
    }
    public void PopulateShopList()
    {
        PopulateAbillitiesShopList();
        PopulateConsumablesShopList();
    }
    public void PopulateConsumablesShopList()
    {
        foreach (var consumable in id.consumableList)
        {
            consumableShopList.Add(consumable);
        }
    }
    public void PopulateAbillitiesShopList()
    {
        foreach(var abillity in id.abillitiesList)
        {
                abillitiesShopList.Add(abillity);
        }
    }


    public static Consumable RandomConsumable() // Get Random Item to add to the store from the item database, with icon and everything

    {
        var itemName = consumableShopList[Random.Range(0, consumableShopList.Count)].itemName;
        return consumableShopList.Find(item => item.itemName == itemName);
    }

    public static AbillitiesItem RandomAbillity() // Get Random Item to add to the store from the item database, with icon and everything

    {
        var itemName = abillitiesShopList[Random.Range(0, abillitiesShopList.Count)].itemName;
        return abillitiesShopList.Find(item => item.itemName == itemName);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ToggleStoreUI();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ToggleStoreUI();
    }
    private void ToggleStoreUI()
    {
        if (storeUI.gameObject.activeSelf == true) storeUI.gameObject.SetActive(false);
        else storeUI.gameObject.SetActive(true);
        if (!storeHasInit) storeHasInit = true;
    }

}
