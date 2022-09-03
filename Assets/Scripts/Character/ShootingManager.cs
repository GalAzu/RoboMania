using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public enum LightShotType { DoubleLaser, Fireball, Blizzard, Shockwave };
    public LightShotType lightShotType;

    private Character character;
    public LayerMask Damagable;
    [Header("Shot Properties")]
    [SerializeField] private float timeToNextShot;
    [SerializeField] private float timeToNextHeavyShot;
    public bool canShootHeavy = true;
    public float lightShotRate;
    public float heavyShotRate;
    [Space]
    [Header("Dependencies")]
    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    public Transform shootPointR, shootPointL, missilePoint;
    public GameObject ElectricitySphere;
    [Space]
    [Header("Abillities")]
    public float doubleShotRate = 100;
    public float shockwaveRadius;
    public float shockwaveShotRate;
    public float shockwaveDamage;


    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        OnShoot();
    }
    private void OnShoot()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToNextShot)
        {
            ShootLight();
            character.anim.SetBool("isShooting" , true);
            timeToNextShot = Time.time + lightShotRate * Time.deltaTime;
        }

        else if (Input.GetButton("Fire2") && canShootHeavy && Time.time >= timeToNextHeavyShot) ShootHeavy();
    }

    private void ShootHeavy()
    {
        Character.instance.state = Character.PlayerState.Shooting;
        timeToNextHeavyShot = Time.time + heavyShotRate * Time.deltaTime;
        GameObject missile = Instantiate(missilePrefab, missilePoint.transform.position, Character.instance.transform.rotation);
    }
    private void ShootLight()
    {
        Character.instance.state = Character.PlayerState.Shooting;
        switch(lightShotType)
        {
            case (LightShotType.DoubleLaser):
                GameObject bullet = Instantiate(bulletPrefab, shootPointR.transform.position, Character.instance.transform.rotation);   //TDL Bullets pool.
                GameObject bullet2 = Instantiate(bulletPrefab, shootPointL.transform.position, Character.instance.transform.rotation);

                break;
            case (LightShotType.Shockwave):
                Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, shockwaveRadius , Damagable );
                foreach(var collider2D in collider)
                {
                    if(collider2D != null)
                    {
                        var enemyMask = 8;
                        if(collider2D.gameObject.layer == enemyMask)
                        {
                            var enemy = collider2D.GetComponent<Enemy>();
                            enemy.Damage(shockwaveDamage);
                        }
                    }
                }
                var sphere = Instantiate(ElectricitySphere, transform.position , Quaternion.identity);
                break;
            case (LightShotType.Blizzard):
                break;
            case (LightShotType.Fireball):
                break;
        }
    }
    private void OnDrawGizmosSelected()
    {
       Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(transform.position, shockwaveRadius);
    }
}
