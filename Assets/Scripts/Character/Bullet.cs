using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        light,
        missile
    }
    public BulletType type;
    public float bulletSpeed;
    public Transform LeftShotPos, RightShotPos;
    private Rigidbody2D rb;
    private Shooting playerShooting;
    public LayerMask Enemy  ;
    void Start()
    {
        playerShooting = FindObjectOfType<Shooting>();
        LeftShotPos = playerShooting.shootPointL;
        RightShotPos = playerShooting.shootPointR;
        transform.position = transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        
            //Laser Attack Behaviour
          type = BulletType.light;
        transform.position += (transform.up * bulletSpeed) * Time.deltaTime;
        if(playerShooting.shotType == Shooting.ShotType.Abillity)
        {
            type = BulletType.missile;
            //Missile Attack stuff
        }
        Destroy(this.gameObject, 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(type == BulletType.light)
        {
            if (collision.gameObject.layer == 8)
            {
                collision.gameObject.GetComponent<Enemy>().health -= playerShooting.bulletDamage;
            }
            Destroy(this.gameObject);
        }
        else if(type == BulletType.missile)
        {
            //Missile Stuff
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
