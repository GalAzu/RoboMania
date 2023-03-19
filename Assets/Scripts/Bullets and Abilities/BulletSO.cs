using UnityEngine;

//EnemyBullets managing all damage that is taken by enemy projectiles of any kind, according to what it collides with.
[System.Serializable]
[CreateAssetMenu(fileName = "PowerUpItem", menuName
= "Items/BulletSO")]

public class BulletSO  : ScriptableObject
{
    public bool isHoming;
    public float _bulletDamage;
    public float bulletAreaDamage;
    public float _bulletSpeed;
    public int destroyTime;
    [SerializeField]
    public LayerMask Damageables;
}

