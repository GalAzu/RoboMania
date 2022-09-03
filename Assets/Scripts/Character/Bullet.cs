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
    public int bulletDamage;
    public Transform LeftShotPos, RightShotPos;
    private Rigidbody2D rb;
    private ShootingManager playerShooting;
    public LayerMask Enemy  ;
    private Animator anim;
    void Start()
    {
        playerShooting = FindObjectOfType<ShootingManager>();
        transform.position = transform.localPosition;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.up * bulletSpeed) * Time.deltaTime;
        Destroy(this.gameObject, 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(type == BulletType.light)
        {
            if (collision.gameObject.layer == 8)
            {
                collision.gameObject.GetComponent<Enemy>().health -= bulletDamage;
            }
            Destroy(this.gameObject);
        }
        if(type == BulletType.missile)
        {
            if (collision.gameObject.layer == 8)
            {
                anim.SetTrigger("explosion");
                collision.gameObject.GetComponent<Enemy>().health -= bulletDamage;
            }
            Destroy(this.gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
