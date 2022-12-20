using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//EnemyBullets managing all damage that is taken by enemy projectiles of any kind, according to what it collides with.

public class EnemyBullets : MonoBehaviour
{
    public int _bulletDamage;
    public float _bulletSpeed;
    private Shield shield;
    private PowerUpsManager powerUpManager;
    private void Awake()
    {
        shield = FindObjectOfType<Shield>();
        powerUpManager = FindObjectOfType<PowerUpsManager>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.up * _bulletSpeed) * Time.deltaTime, Space.Self);
        Destroy(this.gameObject, 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9) //Player Layer
        {
            if (powerUpManager._onShield) 
            {
                shield.DamageShield(_bulletDamage);
                Destroy(this.gameObject);
            }
            else
            {
                Character.instance.Damage(_bulletDamage);
                Destroy(this.gameObject);
            }
        }
        else if(collision.gameObject.layer == 11) //Environment Layer
        {
            Destroy(this.gameObject);
        }
        //Destroy(this.gameObject);
    }
}
