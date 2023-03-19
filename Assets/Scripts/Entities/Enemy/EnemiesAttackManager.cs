using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

public class EnemiesAttackManager : MonoBehaviour
{
    //MAKE THIS VIRTUAL AND INHERIT AS MELEE/LIGHT/HEAVY

    [ShowInInspector]
    [Header("Prefabs")]
    [SerializeField]
    private EnemyBullet bullet;
    [SerializeField]
    private Transform shootTransform;
    [Header("State Properties")]
    [SerializeField]
    private float timeToNextShot;
    [Header("Attack Properties")]
    public float attackDistance;
    [SerializeField]
    private float shotRate;
    public int physicalDamage;

    public void Attack()
    {
        if (Time.time > timeToNextShot)
        {
            timeToNextShot = Time.time + shotRate;
            Instantiate(bullet, shootTransform.position, transform.rotation); ;
        }
    }
}
