using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAttackManager : MonoBehaviour
{
    [System.Serializable]
    public class Bullet 
        {
        [SerializeField]
        public GameObject bulletPrefab;
        public Transform bulletPos;
        }
    public enum AttackType 
    { RangedLight, 
      RangedHeavy, 
      Melee
    }
    public AttackType attackType;
    [SerializeField] 
    private float timeToNextShot;
    public float bulletSpeed;
    [SerializeField]
    private float shotRate;
    [Header("Prefabs")]
    [SerializeField]
    private Bullet bullet;

    private Dictionary<AttackType, Bullet> BulletToAttackType;
    private void Awake()
    {
        bullet = GetBulletFromAttackType();
    }
    private Bullet GetBulletFromAttackType()
    {
        Bullet bullet;
        BulletToAttackType.TryGetValue(attackType, out bullet);
        return bullet;
    }
    void Start()
    {
        attackType = (AttackType)Random.Range(0, 3);  //Get enemy attack type
        timeToNextShot = 0;
    }
    private void LightShot()
    {
        if (Time.time > timeToNextShot)
        {
            timeToNextShot = Time.time + shotRate;
            Instantiate(bullet.bulletPrefab, bullet.bulletPos.position, transform.rotation);
        }
    }
    private void HeavyShot()
    {
        if (Time.time > timeToNextShot)
        {
            timeToNextShot = Time.time + shotRate;
            Instantiate(bullet.bulletPrefab, bullet.bulletPos.position, transform.rotation);
        }
    }
    private void MeleeAttack()
    {
        Debug.Log("Melee Attack");
    }
    public void Attack()
    {
        if (Character.instance != null)
            switch (attackType)
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
