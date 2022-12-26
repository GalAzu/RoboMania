using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//EnemyBullets managing all damage that is taken by enemy projectiles of any kind, according to what it collides with.

public class EnemyBullets : MonoBehaviour
{
    public int _bulletDamage;
    public float _bulletSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.up * _bulletSpeed) * Time.deltaTime, Space.Self);
        Destroy(this.gameObject, 3);
    }

}
