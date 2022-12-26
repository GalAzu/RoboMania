using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int initHP = 700;
    private int enemyBulletLayer = 12;
    private Character character;

    private void Awake()
    {
        character = FindObjectOfType<Character>();
    }
    private void Start()
    {
        hp = initHP;
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
            DamageShield(collision.GetComponent<EnemyBullets>()._bulletDamage);
            Destroy(collision.gameObject);
        }
    }
}
