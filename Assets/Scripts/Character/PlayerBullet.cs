using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationsHandling;
public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private BulletSO bullet;
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
            transform.position += (transform.up * bullet._bulletSpeed) * Time.deltaTime;
            Destroy(this.gameObject, 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.IsTouchingLayers(bullet.Damageables)) //enemy layer mask
            {
                collision.gameObject.GetComponent<EnemyStateMachine>().health -= bullet._bulletDamage;
            }
            else Debug.Log("NO LAYER IS TOUCHED");

            Destroy(this.gameObject);
    }
    public PlayerBullet(bool homing, int damage, float area, float speed, GameObject _vfx, int _destroyTime)
    {
        BulletSO bullet = new BulletSO();
        bullet.isHoming = homing;
        bullet._bulletDamage = damage;
        bullet.bulletAreaDamage = area;
        bullet._bulletSpeed = speed;
        bullet.vfx = _vfx;
        bullet.destroyTime = _destroyTime;
    }


}
