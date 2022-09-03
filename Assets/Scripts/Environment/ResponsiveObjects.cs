using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveObjects : MonoBehaviour
{
    private LayerMask playerBullets = 10;
    private LayerMask enemyBullets = 12;
    private LayerMask enemyLayer = 8;
    [SerializeField]
    private GameObject[] ItemPool;
    private GameObject itemToDrop;
    private float health;
    [Header("Spatial properties")]
    [SerializeField] private float spatialDamage;
    [SerializeField] private float spatialRadius;
    [Space]
    [Header("Object Properties")]
    [SerializeField] private float throwDamage;
    [SerializeField] public bool isExplosive;
    [SerializeField] public bool isMoving;
    void Start()
    {
        health = 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerBullets || collision.gameObject.layer == enemyBullets)
        {
            DamageObject();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMoving && collision.gameObject.layer == enemyLayer)
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.health -= throwDamage;
            DamageObject();
        }
    }
    private void DamageObject()
    {
        health -= 20;
        if (health <= 0)
        {
            if(isExplosive)
            {
                //Circle raycast array with spatialRadius
                //damage each raycast array element with the spatial damage
                //Activate vfx within that raycast? or animation?
            }
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
        Debug.Log("DROP ITEM: " + RandomItem().name);
    }
    public void ItemIsMoving() => isMoving = true;
}
