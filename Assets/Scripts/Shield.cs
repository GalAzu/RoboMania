using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int initShieldHP = 700;
    private int enemyBulletLayer = 12;
    private Character character;

    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Character>();
    }
    private void Start()
    {
        hp = initShieldHP;
        character.GetComponent<Collider2D>().enabled = false;
    }
    public void DamageShield(float damage)
    {
        hp -=(int)damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            character.GetComponent<Collider2D>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyBulletLayer)
        {
            DamageShield(collision.GetComponent<EnemyBullet>()._bulletDamage);
            Destroy(collision.gameObject);
        }
    }
}
