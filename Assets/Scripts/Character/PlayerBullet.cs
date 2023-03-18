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
        if (collision.gameObject.layer == 8) //enemy layer mask
        {
            collision.gameObject.GetComponent<Entity>().Damage(bullet._bulletDamage);
        }
        else Debug.Log("NO LAYER IS TOUCHED");

        Destroy(this.gameObject);
    }
}
