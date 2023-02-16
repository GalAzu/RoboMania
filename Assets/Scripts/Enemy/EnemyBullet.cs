using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyBullet : MonoBehaviour
{
    public LayerMask damageables;
    public EnemyBulletData bulletData;
    [SerializeField]
    private Transform characterTransform;

    private void Awake()
    {
        characterTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        if (bulletData.isHoming)
        {
            transform.position = Vector3.MoveTowards(transform.position, characterTransform.position , bulletData._bulletSpeed  * Time.deltaTime);
            Destroy(gameObject, 3);
        }
        else
        {
            transform.Translate((Vector3.up * bulletData._bulletSpeed) * Time.deltaTime, Space.Self);
            Destroy(gameObject, 3);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if(collision.gameObject.layer == 9)
        {
            Debug.Log("TriggerWithPlayer");
            collision.gameObject.GetComponent<Character>().Damage(bulletData._bulletDamage);
            var _vfx = Instantiate(bulletData.vfx, transform.position, Quaternion.identity);
            Destroy(_vfx, 1.5f);
            Destroy(gameObject);
        }
    }
}
