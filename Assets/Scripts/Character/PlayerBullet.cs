using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationsHandling;
public class PlayerBullet : Bullet
{
    public enum BulletType
    {
        Light,
        Heavy,
    }
    public BulletType type;
    public Transform LeftShotPos, RightShotPos;
    void Start()
    {
            transform.position = transform.localPosition;
    }
    void Update()
    {
            transform.position += (transform.up * _bulletSpeed) * Time.deltaTime;
            Destroy(this.gameObject, 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.IsTouchingLayers(Damageables)) //enemy layer mask
            {
                collision.gameObject.GetComponent<EnemyStateMachine>().health -= _bulletDamage;
            }
            else Debug.Log("NO LAYER IS TOUCHED");

            Destroy(this.gameObject);
    }
}
