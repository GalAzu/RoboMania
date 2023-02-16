using UnityEngine;

//EnemyBullets managing all damage that is taken by enemy projectiles of any kind, according to what it collides with.
[System.Serializable]
public class EnemyBulletData 
{
    public bool isHoming;
    public int _bulletDamage;
    public float bulletAreaDamage;
    public float _bulletSpeed;
    public GameObject vfx;
}

