using UnityEngine;


[System.Serializable]
public class EnemyBullet : MonoBehaviour
{
    private Transform characterTransform;
    [SerializeField]
    public BulletSO bullet;

    private void Awake()
    {
        characterTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        if (bullet.isHoming && characterTransform!=null)
        {
            transform.position = Vector3.MoveTowards(transform.position, characterTransform.position , bullet._bulletSpeed  * Time.deltaTime);
            Destroy(gameObject, 3);
        }
        else
        {
            transform.Translate((Vector3.up * bullet._bulletSpeed) * Time.deltaTime, Space.Self);
            Destroy(gameObject, 3);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<Character>().Damage(bullet._bulletDamage);
        }
        if (bullet.vfx != null)
        {
            var _vfx = Instantiate(bullet.vfx, transform.position, Quaternion.identity);
            Destroy(_vfx, 1.5f);
        }
        Destroy(gameObject);
    }
}
