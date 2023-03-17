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
            Destroy(gameObject, bullet.destroyTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<Character>().Damage(bullet._bulletDamage);
        }
        //VFX
        Destroy(gameObject);
    }
}
