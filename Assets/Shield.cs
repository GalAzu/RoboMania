using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private int hp;
    private int initHP = 50;
    private void Start()
    {
        hp = initHP;
    }
    public void DamageShield(float damage)
    {
        hp -=(int)damage;
        if (hp <= 0) Destroy(gameObject);
    }
}
