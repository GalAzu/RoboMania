using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public enum BulletType
    {
        Light,
        Heavy,
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
        if(type == BulletType.Light)
        {
            if (collision.gameObject.layer == 8) //enemy layer mask
            {
                collision.gameObject.GetComponent<Enemy>().health -= bulletDamage;
            }
            Destroy(this.gameObject);
        }
        if(type == BulletType.Heavy)
        {
            if (collision.gameObject.layer == 8)
            {
                anim.SetTrigger("explosion");
                collision.gameObject.GetComponent<Enemy>().health -= bulletDamage;
            }
            Destroy(this.gameObject);
        }

    }
}
