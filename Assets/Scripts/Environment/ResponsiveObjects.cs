using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveObjects : MonoBehaviour
{
    private LayerMask playerBullets = 10;
    public LayerMask enemyBullets = 12;
    [SerializeField]
    private GameObject[] ItemPool;
    private GameObject itemToDrop;
    private float health;
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("hit");

        if (collision.gameObject.layer == playerBullets || collision.gameObject.layer == enemyBullets)
        {
            ObjectDestroy();
        }

    }
    private void ObjectDestroy()
    {
        health -= 20;
        if (health <= 0)
        {
          Destroy(this.gameObject);
          if (itemToDrop != null) DropRandomItem();
        }
    }
    private GameObject RandomItem()
    {
        var index = Random.Range(0, ItemPool.Length);
        itemToDrop = ItemPool[index];
        return itemToDrop;
    }
    private void DropRandomItem()
    {
        Instantiate(RandomItem(), transform.position, Quaternion.identity);
    }
}
