using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttackManager : MonoBehaviour
{
    public enum AttackType 
    { RangedLight, 
      RangedHeavy, 
      Melee
    }
    public float bulletSpeed;
    public float meleeAttackRange;
    public AttackType attackState;
    [SerializeField] 
    private float timeToNextShot;
    [SerializeField] 
    private GameObject lightBullet;
    [SerializeField] 
    private GameObject HeaveyBullet;
    public Transform heavyBulletPos, lightBulletPos;
    [SerializeField] 
    private float shotRate;

    void Start()
    {
        attackState = (AttackType)Random.Range(0, 3);  //Get enemy attack type
        timeToNextShot = 0;
    }
    private void LightShot()
    {
        if (Time.time > timeToNextShot)
        {
            var bulletPos = lightBulletPos;
            timeToNextShot = Time.time + shotRate;
            var bullet = Instantiate(lightBullet, bulletPos.position, transform.rotation);
        }
    }
    private void HeavyShot()
    {
        if (Time.time > timeToNextShot)
        {
            var bulletPos = heavyBulletPos;
            timeToNextShot = Time.time + shotRate;
            var bullet = Instantiate(HeaveyBullet, bulletPos.position, transform.rotation);
        }
    }
    private void MeleeAttack()
    {
        Debug.Log("Melee Attack");
    }
    public void Attack()
    {
        if (Character.instance != null)
            switch (attackState)
            {
                case (AttackType.Melee):
                    MeleeAttack();
                    break;
                case (AttackType.RangedHeavy):
                    HeavyShot();
                    break;
                case (AttackType.RangedLight):
                    LightShot();
                    break;
            }
    }
}
