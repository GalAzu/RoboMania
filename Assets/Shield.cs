using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private int hp;
    private float timeToShutOff;

    public void DamageShield(float damage)
    {
        hp -=(int)damage;
    }
}
