using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float movementSpeed;
    public float maxHealth;
    public float curHealth;
    private delegate void Death();
    private event Death entityDeathEvent;
    public void Damage(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            entityDeathEvent?.Invoke();
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        
    }
}