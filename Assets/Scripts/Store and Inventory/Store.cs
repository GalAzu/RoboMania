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
    public GameObject StoreUI;
    [SerializeField]
    private ItemDatabase id;
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

    private void Start()
    {
        ToggleStoreUI();
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

    public void PopulateStore()
    {
          storeUI.CreateNewItemSlots();
          storeUI.UpdateShopLists();            
          PopulateShopList();
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
            Debug.Log("open store");
            ToggleStoreUI();
        }
    }
    private void ToggleStoreUI()
    {
        if (StoreUI.activeSelf == true) StoreUI.SetActive(false);
        else StoreUI.SetActive(true);
    }

}
