using UnityEngine;


[System.Serializable]
public class EnemyBullet : Bullet
{
    private Transform characterTransform;

    private void Awake()
    {
        characterTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        if (isHoming && characterTransform!=null)
        {
            transform.position = Vector3.MoveTowards(transform.position, characterTransform.position , _bulletSpeed  * Time.deltaTime);
            Destroy(gameObject, 3);
        }
        else
        {
            transform.Translate((Vector3.up * _bulletSpeed) * Time.deltaTime, Space.Self);
            Destroy(gameObject, 3);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<Character>().Damage(_bulletDamage);
        }
        if (vfx != null)
        {
            var _vfx = Instantiate(vfx, transform.position, Quaternion.identity);
            Destroy(_vfx, 1.5f);
        }
        Destroy(gameObject);
    }
}
