using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public enum ShotType { DoubleLaser, Abillity };
    public ShotType shotType;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    public float bulletSpeed;
    public float missileSpeed;
    public Transform shootPointR, shootPointL, missilePoint;
    public float timeToNextShot;
    public float shotRate;
    public float bulletDamage;

    private void Update()
    {
        OnShoot();
    }
    private void OnShoot()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToNextShot)
        {
            Shoot();
            timeToNextShot = Time.time + shotRate;
        }
    }
    private void Shoot()
    {
        Character.instance.state = Character.PlayerState.Shooting;
        if (shotType == ShotType.DoubleLaser)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPointR.transform.position, Character.instance.transform.rotation);
            GameObject bullet2 = Instantiate(bulletPrefab, shootPointL.transform.position, Character.instance.transform.rotation);
        }
        else if (shotType == ShotType.Abillity)
        {

        }
    }
    private void SelectShopType()
    {
        //On UI Mouse Click - Select Shot Type
    }
}
